using System.Text;
using System.Xml;
using System.Xml.Linq;

using StringLocalizer.Models;
using StringLocalizer.Properties;
using static System.Net.Mime.MediaTypeNames;

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
                try
                {
                    _rootFolderItem = new FolderItem(_projectFolder, null);
                    ScanFolder(_projectFolder, _rootFolderItem);
                    this.Invoke((MethodInvoker)delegate
                    {
                        CreateTree(false);
                        if (!string.IsNullOrEmpty(_resourcesFolder))
                            AddLanguageColumns(_rootFolderItem);
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate { MessageBox.Show(ex.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                }
                finally
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetComponentsOnAnalyzing(false);
                        SetMenuItemsEnabled();
                    });
                }
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
            SetResourceEditorContent();
        }

        void SetResourceEditorContent()
        {
            resourceEditor.Clear();

            if (treeView.SelectedNode == null)
                return;
            if (!(treeView.SelectedNode.Tag is ClassItem))
                return;
            if (string.IsNullOrEmpty(_resourcesFolder))
                return;

            var item = treeView.SelectedNode.Tag as ClassItem;
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
            if (!Directory.Exists(path))
                return [];
            var allResourceFiles = Directory.GetFiles(path, $"{classItem.Name}*.resx");
            var result = new List<string>();
            foreach (var file in allResourceFiles)
            {
                var classNameParts = classItem.Name.Split('.');
                var fileNameParts = Path.GetFileNameWithoutExtension(file).Split('.');
                var lengthDiff = fileNameParts.Length - classNameParts.Length;
                if (lengthDiff > 1)
                    continue;
                bool skip = false;
                for (int i = 0; i < classNameParts.Length; i++)
                {
                    if (!classNameParts[i].Equals(fileNameParts[i]))
                    {
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                    result.Add(file);
            }
            return result;
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
            var path = GetResourceFilePath(treeView.SelectedNode?.Tag as ClassItem, language);
            var xDoc = GetXDocument(path);
            var element = GetDataElement(xDoc, key, out bool isNew);
            if (isNew && string.IsNullOrEmpty(value))
                return;
            element.SetElementValue(nameAttr, value);
            SaveXDocSafe(xDoc, path);
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

        private string GetResourceFilePath(ClassItem classItem, string language)
        {
            var languagePart = string.IsNullOrEmpty(language) ? string.Empty : $".{language}";
            var fileName = $"{classItem.Name}{languagePart}.resx";
            return Path.Combine(GetResourceFolderPath(classItem), fileName);
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
                _rootFolderItem.ClearMatchFilter();
                FilterTree(formFind.FinText, startItem, formFind.MatchCase, formFind.MatchWholeWord);
            }
        }

        private void FilterTree(string text, FolderItem folderItem, bool matchCase, bool matchWholeWord)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    FilterFolder(text, folderItem, matchCase, matchWholeWord);
                    this.Invoke((MethodInvoker)delegate
                    {
                        treeView.Nodes.Clear();
                        CreateTree(true);
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate { MessageBox.Show(ex.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                }
                finally
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        panelFilter.Visible = true;
                        SetComponentsOnAnalyzing(false);
                        SetResourceEditorContent();
                    });
                }
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

                var startItem = formReplace.InSelectedPathOnly ? treeView.SelectedNode?.Tag as FolderItem : _rootFolderItem;
                if (startItem == null)
                {
                    MessageBox.Show("Please select a folder or file to start the search.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SetComponentsOnAnalyzing(true);
                _replacedItems = 0;
                ReplaceInTreeTree(formReplace.FinText, formReplace.ReplaceWith, startItem, formReplace.MatchCase, formReplace.MatchWholeWord);
            }
        }

        private void ReplaceInTreeTree(string text, string replaceWith, FolderItem folderItem, bool matchCase, bool matchWholeWord)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    ReplaceInFolder(text, replaceWith, folderItem, matchCase, matchWholeWord);
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"Replaced {_replacedItems} occurrences of '{text}' with '{replaceWith}'.", "Replace Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate { MessageBox.Show(ex.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                }
                finally
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetComponentsOnAnalyzing(false);
                        SetResourceEditorContent();
                    });
                }
            });
        }

        int _replacedItems;
        private void ReplaceInFolder(string text, string replaceWith, FolderItem folderItem, bool matchCase, bool matchWholeWord)
        {
            foreach (var subFolder in folderItem.SubFolders)
            {
                ReplaceInFolder(text, replaceWith, subFolder, matchCase, matchWholeWord);
            }
            foreach (var classItem in folderItem.ClassItems)
            {
                var resxFiles = GetResourceFiles(classItem);
                foreach (var resxFile in resxFiles)
                {
                    bool hasBeenChanged = false;
                    var xdoc = XDocument.Load(resxFile);
                    var dataElements = xdoc.Descendants("data");
                    var valueElements = dataElements.Select(i => i.Element("value"));
                    var commentElements = dataElements.Select(i => i.Element("comment"));
                    var allElements = valueElements.Concat(commentElements).Where(i => i != null).ToArray();
                    var stringComparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
                    foreach (var element in allElements)
                    {
                        if (string.IsNullOrEmpty(element.Value))
                            continue;
                        if (matchWholeWord)
                        {
                            if (element.Value.Equals(text, stringComparison))
                            {
                                element.SetValue(replaceWith);
                                _replacedItems++;
                                hasBeenChanged = true;
                            }
                        }
                        else
                        {
                            int pos = 0;
                            int index = 0;
                            while (pos < element.Value.Length && (index = element.Value.IndexOf(text, pos, stringComparison)) >= 0)
                            {
                                element.SetValue(element.Value.Remove(index, text.Length).Insert(index, replaceWith));
                                _replacedItems++;
                                hasBeenChanged = true;
                                pos = index + replaceWith.Length; // Move past the replaced text
                            }
                        }
                    }
                    if (hasBeenChanged)
                        SaveXDocSafe(xdoc, resxFile);
                }
            }
        }

        private void createMissingFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formCreateMissing = new FormCreateMissingResources())
            {
                if (formCreateMissing.ShowDialog() != DialogResult.OK)
                    return;

                var startItem = formCreateMissing.InSelectedPathOnly ? treeView.SelectedNode?.Tag as FolderItem : _rootFolderItem;
                if (startItem == null)
                {
                    MessageBox.Show("Please select a folder or file to start the search.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SetComponentsOnAnalyzing(true);
                _addedKeys = 0;
                _addedResources = 0;
                CreateMissing(startItem, formCreateMissing.CreareNeutral, formCreateMissing.SetNeutralByKey);
            }
        }

        private void CreateMissing(FolderItem folderItem, bool createNeutral, bool setNeutralByKey)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    CreateMissingFilesInFolder(folderItem, createNeutral, setNeutralByKey);
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"added {_addedResources} resources files and {_addedKeys} keys.", "Creating missing Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate { MessageBox.Show(ex.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                }
                finally
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        SetComponentsOnAnalyzing(false);
                        SetResourceEditorContent();
                    });
                }
            });
        }

        int _addedResources;
        int _addedKeys;
        private void CreateMissingFilesInFolder(FolderItem folderItem, bool createNeutral, bool setNeutralByKey)
        {
            foreach (var subFolder in folderItem.SubFolders)
            {
                CreateMissingFilesInFolder(subFolder, createNeutral, setNeutralByKey);
            }
            foreach (var classItem in folderItem.ClassItems)
            {
                if (createNeutral)
                {
                    CreateMissingNeutralResx(classItem, setNeutralByKey);
                }
                foreach (var language in resourceEditor.GetLanguages())
                {
                    bool needToBeSaved = false;
                    var resxFilePath = GetResourceFilePath(classItem, language);
                    if (!File.Exists(resxFilePath))
                    {
                        _addedResources++;
                        needToBeSaved = true;
                    }
                    var xDoc = GetXDocument(resxFilePath);
                    foreach (var key in classItem.Keys)
                    {
                        var element = GetDataElement(xDoc, key, out bool isNew);
                        if (isNew)
                        {
                            _addedKeys++;
                            needToBeSaved = true;
                        }
                    }
                    if (needToBeSaved)
                        SaveXDocSafe(xDoc, resxFilePath);
                }
            }
        }

        private void CreateMissingNeutralResx(ClassItem classItem, bool setNeutralByKey)
        {
            var resxFilePath = GetResourceFilePath(classItem, null);
            bool needToBeSaved = false;
            if (!File.Exists(resxFilePath))
            {
                _addedResources++;
                needToBeSaved = true;
            }
            var xDoc = GetXDocument(resxFilePath);
            if (setNeutralByKey)
            {
                foreach (var key in classItem.Keys)
                {
                    var element = GetDataElement(xDoc, key, out bool isNew);
                    if (isNew)
                    {
                        _addedKeys++;
                        needToBeSaved = true;
                    }

                    if (setNeutralByKey && !key.Equals(element.Value))
                    {
                        element.SetElementValue("value", key);
                        needToBeSaved = true;
                    }
                }
            }
            if (needToBeSaved)
            {
                SaveXDocSafe(xDoc, resxFilePath);
            }
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
            addLanguageToolStripMenuItem.Enabled = treeView.Nodes.Count > 0 && !string.IsNullOrEmpty(_resourcesFolder);
        }

        private void buttonCancelFiter_Click(object sender, EventArgs e)
        {
            panelFilter.Visible = false;
            treeView.Nodes.Clear();
            _rootFolderItem.ClearMatchFilter();
            CreateTree(false);
            SetResourceEditorContent();
        }

        private void addLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formAddLanguage = new FormAddLanguage())
            {
                if (formAddLanguage.ShowDialog() != DialogResult.OK)
                    return;

                if (formAddLanguage.CultureName == null)
                {
                    MessageBox.Show("Please select a culture to add.", "No Culture Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                resourceEditor.AddLanguage(formAddLanguage.CultureName);
            }
        }

        private void SaveXDocSafe(XDocument xDoc, string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            xDoc.Save(path);
        }
    }
}
