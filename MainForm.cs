namespace StringLocalizer
{
    public partial class MainForm : Form
    {
        private string _projectFolder;
        private string _resourcesFolder;
        private FolderItem _rootFolderItem;
        private readonly string[] _allowedExtensions = [ ".cs", ".cshtml", ".razor" ];
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
            toolStripProgressBar.Visible = true;
            _projectFolder = folderBrowserDialog.SelectedPath;
            ScanProjectFolder();
        }

        private void selectResourcesFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            _resourcesFolder = folderBrowserDialog.SelectedPath;
            lbResourcesFolder.Text = _resourcesFolder;
        }

        private void ScanProjectFolder()
        {
            Task.Factory.StartNew(() =>
            {
                _rootFolderItem = new FolderItem(_projectFolder);
                ScanFolder(_projectFolder, _rootFolderItem);
                this.Invoke((MethodInvoker)delegate
                {
                    CreateTree();
                    toolStripProgressBar.Visible = false;
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

        private void CreateTree()
        {
            AddTreeFolder(_rootFolderItem, treeView.Nodes);
        }

        private void AddTreeFiles(FolderItem folderItem, TreeNodeCollection treeNode)
        {
            foreach (var item in folderItem.ClassItems)
            {
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
                var node = new TreeNode(item.Name, imageIndex, imageIndex);
                treeNode.Add(node);
            }
        }

        private void AddTreeFolder(FolderItem folderItem, TreeNodeCollection treeNode)
        {
            if (!folderItem.HasLocalizers)
                return;

            var node = new TreeNode(folderItem.Name, 0, 0);
            treeNode.Add(node);
            foreach (var subFolder in folderItem.SubFolders)
            {
                AddTreeFolder(subFolder, node.Nodes);
            }
            AddTreeFiles(folderItem, node.Nodes);
        }
    }
}
