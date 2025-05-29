using System.Text;
using System.Xml;
using System.Xml.Linq;

using StringLocalizer.Models;
using StringLocalizer.Properties;

namespace StringLocalizer
{
    public partial class MainForm : Form
    {
        private string _projectFolder;
        private string _resourcesFolder;
        private FolderItem _rootFolderItem;
        private readonly string[] _allowedExtensions = [".cs", ".cshtml", ".razor"];
        private const string _justCodeExtension = ".cs";

        public MainForm()
        {
            InitializeComponent();
        }

        private void ProjectFoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            treeView.Nodes.Clear();
            resourceEditor.ClearLanguageColums();
            SetResourcesFolder(null);
            SetComponentsOnAnalyzing(true);
            _projectFolder = folderBrowserDialog.SelectedPath;
            ScanProjectFolder();
        }

        private void selectResourcesFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = Path.Combine(_rootFolderItem?.Name, "Resources");
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            SetResourcesFolder(folderBrowserDialog.SelectedPath);
            AddLanguageColumns(_rootFolderItem);
        }

        private void AddLanguageColumns(FolderItem folderItem)
        {
            foreach (var folder in folderItem.SubFolders)
            {
                AddLanguageColumns(folder);
            }
            foreach (var classItem in folderItem.ClassItems)
            {
                AddMissingLanguageColumns(classItem);
            }
        }

        private void SetResourcesFolder(string folder)
        {
            _resourcesFolder = folder;
            lbResourcesFolder.Text = _resourcesFolder;
            SetMenuItemsEnabled();
        }

        private void ScanProjectFolder()
        {
            Task.Factory.StartNew(() =>
            {
                _rootFolderItem = new FolderItem(_projectFolder, null);
                ScanFolder(_projectFolder, _rootFolderItem);
                this.Invoke((MethodInvoker)delegate
                {
                    CreateTree(false);
                    SetComponentsOnAnalyzing(false);
                    if (!string.IsNullOrEmpty(_resourcesFolder))
                        AddLanguageColumns(_rootFolderItem);
                    SetMenuItemsEnabled();
                });
            });
        }

        private void ScanFolder(string folder, FolderItem folderItem)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            foreach (var directory in di.GetDirectories())
            {
                var subFolderItem = folderItem.AddSubFolder(directory.Name);
                ScanFolder(directory.FullName, subFolderItem);
            }
            foreach (var file in di.GetFiles().Where(i => _allowedExtensions.Contains(i.Extension.ToLower())))
            {
                if (file.Extension.ToLower() != _justCodeExtension)
                {
                    //keys from just from page
                    var keys = ParsingHelper.ExtractKeysFromRazorFile(file.FullName);
                    if (keys.Keys.Count() > 0)
                        folderItem.AddClassItem(keys.ClassName, file.Extension, keys.Keys);
                }
                var classes = ParsingHelper.ExtractKeysFromCsFile(file.FullName);
                foreach (var cl in classes.Where(i => i.Value.Count() > 0))
                {
                    folderItem.AddClassCSItem(cl.Key, cl.Value);
                }
            }
        }

        private void AddMissingLanguageColumns(ClassItem classItem)
        {
            foreach (var language in GetResourceLanguages(classItem))
            {
                resourceEditor.AddLanguage(language);
            }
        }

        private void CreateTree(bool filtered)
        {
            AddTreeFolder(_rootFolderItem, treeView.Nodes, filtered);
        }

        private void AddTreeFiles(FolderItem folderItem, TreeNodeCollection treeNode, bool filtered)
        {
            foreach (var item in folderItem.ClassItems)
            {
                if (filtered && !item.MatchFilter)
                    continue;

                int imageIndex = -1;
                switch (item.ItemType)
                {
                    case ClassItem.ItemTypeEnum.CSharp:
                        imageIndex = 1; // C# file
                        break;
                    case ClassItem.ItemTypeEnum.CSHTML:
                    case ClassItem.ItemTypeEnum.Razor:
                        imageIndex = 2; // Razor file
                        break;
                }
                var node = new TreeNode(item.Name, imageIndex, imageIndex)
                {
                    Tag = item
                };
                treeNode.Add(node);
            }
        }

        private void AddTreeFolder(FolderItem folderItem, TreeNodeCollection treeNode, bool filtered)
        {
            if (!folderItem.HasLocalizers || (filtered && !folderItem.MatchFilter))
                return;

            var node = new TreeNode(folderItem.Name, 0, 0) { Tag = folderItem };
            treeNode.Add(node);
            foreach (var subFolder in folderItem.SubFolders)
            {
                AddTreeFolder(subFolder, node.Nodes, filtered);
            }
            AddTreeFiles(folderItem, node.Nodes, filtered);
        }

        private void SetComponentsOnAnalyzing(bool isAnalyzing)
        {
            splitContainer.Enabled = !isAnalyzing;
            projectFoderToolStripMenuItem.Enabled = !isAnalyzing;
            toolStripProgressBar.Visible = isAnalyzing;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            resourceEditor.Clear();

            if (!(e.Node.Tag is ClassItem))
                return;
            if (string.IsNullOrEmpty(_resourcesFolder))
                return;

            var item = e.Node.Tag as ClassItem;
            foreach (var key in item.Keys)
                resourceEditor.AddKey(key);

            var resxFiles = GetResourceFiles(item);
            foreach (var resxFile in resxFiles)
            {
                var language = Path.GetFileNameWithoutExtension(resxFile).Substring(item.Name.Length).Split('.').Last();
                var xdoc = XDocument.Load(resxFile);
                foreach (var data in xdoc.Descendants("data"))
                {
                    var key = data.Attribute("name")?.Value;
                    if (key == null)
                        continue;
                    var value = data.Element("value")?.Value;
                    if (string.IsNullOrEmpty(language))
                    {
                        var comment = data.Element("comment")?.Value;
                        resourceEditor.AddNeutralValues(key, comment, value);
                    }
                    else
                        resourceEditor.AddLanguageValue(key, language, value);
                }
            }
        }

        private IEnumerable<string> GetResourceFiles(ClassItem classItem)
        {
            var path = GetResourceFolderPath(classItem);
            return Directory.Exists(path) ? Directory.GetFiles(path, $"{classItem.Name}*.resx") : [];
        }

        private void resourceEditor_CommentChanged(object sender, (string Key, string Value) e)
        {
            SetXValue(e.Key, null, e.Value, "comment");
        }

        private void resourceEditor_NeutralChanged(object sender, (string Key, string Value) e)
        {
            SetXValue(e.Key, null, e.Value, "value");
        }

        private void resourceEditor_LanguageChanged(object sender, (string Key, string Language, string Value) e)
        {
            SetXValue(e.Key, e.Language, e.Value, "value");
        }

        private void SetXValue(string key, string language, string value, string nameAttr)
        {
            var path = GetResourceFilePath(language);
            var xDoc = GetXDocument(path);
            var element = GetDataElement(xDoc, key, out bool isNew);
            if (isNew && string.IsNullOrEmpty(value))
                return;
            element.SetElementValue(nameAttr, value);
            xDoc.Save(path);
        }

        private XDocument GetXDocument(string path)
        {
            if (File.Exists(path))
            {
                return XDocument.Load(path);
            }
            using (var ms = new MemoryStream(Resources.BaseResx))
            {
                return XDocument.Load(ms);
            }
        }

        private string GetResourceFilePath(string language)
        {
            var item = treeView.SelectedNode?.Tag as ClassItem;
            var languagePart = string.IsNullOrEmpty(language) ? string.Empty : $".{language}";
            var fileName = $"{item.Name}{languagePart}.resx";
            return Path.Combine(GetResourceFolderPath(item), fileName);
        }

        private XElement GetDataElement(XDocument xDoc, string key, out bool isNew)
        {
            isNew = false;
            var element = xDoc.Descendants("data").FirstOrDefault(d => d.Attribute("name")?.Value == key);
            if (element == null)
            {
                element = new XElement("data", new XAttribute("name", key), new XAttribute(XNamespace.Xml + "space", "preserve"));
                xDoc.Root.Add(element);
                isNew = true;
            }
            return element;
        }

        private string GetResourceFolderPath(ClassItem item)
        {
            var path = item.GetFullPath();
            var resPath = path == _rootFolderItem.Name ? string.Empty : path.Substring(_rootFolderItem.Name.Length + 1);
            return Path.Combine(_resourcesFolder, resPath);
        }

        private IEnumerable<string> GetResourceLanguages(ClassItem classItem)
        {
            var resxFiles = GetResourceFiles(classItem);
            var languages = resxFiles.Select(i => Path.GetFileNameWithoutExtension(i).Substring(classItem.Name.Length).Split('.').Last());
            return languages.Where(i => !string.IsNullOrEmpty(i));
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formFind = new FormFind())
            {
                if (formFind.ShowDialog() != DialogResult.OK)
                    return;

                var startItem = formFind.InSelectedPathOnly ? treeView.SelectedNode?.Tag as FolderItem : _rootFolderItem;
                if (startItem == null)
                {
                    MessageBox.Show("Please select a folder or file to start the search.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SetComponentsOnAnalyzing(true);
                resourceEditor.Clear();
                _rootFolderItem.ClearMatchFilter();
                FilterTree(formFind.FinText, startItem, formFind.MatchCase, formFind.MatchWholeWord);
            }
        }

        private void FilterTree(string text, FolderItem folderItem, bool matchCase, bool matchWholeWord)
        {
            Task.Factory.StartNew(() =>
            {
                FilterFolder(text, folderItem, matchCase, matchWholeWord);
                this.Invoke((MethodInvoker)delegate
                {
                    treeView.Nodes.Clear();
                    CreateTree(true);
                    panelFilter.Visible = true;
                    SetComponentsOnAnalyzing(false);
                });
            });
        }

        private void FilterFolder(string text, FolderItem folderItem, bool matchCase, bool matchWholeWord)
        {
            foreach (var subFolder in folderItem.SubFolders)
            {
                FilterFolder(text, subFolder, matchCase, matchWholeWord);
            }
            foreach (var classItem in folderItem.ClassItems)
            {
                var resxFiles = GetResourceFiles(classItem);
                foreach (var resxFile in resxFiles)
                {
                    var xdoc = XDocument.Load(resxFile);
                    var dataElements = xdoc.Descendants("data");
                    var keys = dataElements
                        .Select(i => i.Attribute("name")?.Value)
                        .Where(i => !string.IsNullOrEmpty(i));
                    var values = dataElements
                        .Select(i => i.Element("value")?.Value)
                        .Where(i => !string.IsNullOrEmpty(i));
                    var comments = dataElements
                        .Select(i => i.Element("comment")?.Value)
                        .Where(i => !string.IsNullOrEmpty(i));
                    var allTexts = keys.Concat(values).Concat(comments);
                    var stringComparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                    if (matchWholeWord)
                    {
                        if (allTexts.Any(i => i.Equals(text, stringComparison)))
                            classItem.SetMatchFilter();
                    }
                    else
                    {
                        if (allTexts.Any(i => i.Contains(text, stringComparison)))
                            classItem.SetMatchFilter();
                    }
                }
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formReplace = new FormReplace())
            {
                if (formReplace.ShowDialog() != DialogResult.OK)
                    return;
            }
        }

        private void createMissingFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetMenuItemsEnabled()
        {
            findToolStripMenuItem.Enabled = treeView.Nodes.Count > 0 && !string.IsNullOrEmpty(_resourcesFolder);
            replaceToolStripMenuItem.Enabled = treeView.Nodes.Count > 0 && !string.IsNullOrEmpty(_resourcesFolder);
            createMissingFilesToolStripMenuItem.Enabled = treeView.Nodes.Count > 0 && !string.IsNullOrEmpty(_resourcesFolder);
        }

        private void buttonCancelFiter_Click(object sender, EventArgs e)
        {
            panelFilter.Visible = false;
            resourceEditor.Clear();
            treeView.Nodes.Clear();
            _rootFolderItem.ClearMatchFilter();
            CreateTree(false);
        }
    }
}
