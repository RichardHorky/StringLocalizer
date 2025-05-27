namespace StringLocalizer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            projectFoderToolStripMenuItem = new ToolStripMenuItem();
            selectResourcesFolderToolStripMenuItem = new ToolStripMenuItem();
            folderBrowserDialog = new FolderBrowserDialog();
            treeView = new TreeView();
            imageList = new ImageList(components);
            splitContainer = new SplitContainer();
            statusStrip = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lbResourcesFolder = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { projectFoderToolStripMenuItem, selectResourcesFolderToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // projectFoderToolStripMenuItem
            // 
            projectFoderToolStripMenuItem.Name = "projectFoderToolStripMenuItem";
            projectFoderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            projectFoderToolStripMenuItem.Size = new Size(233, 22);
            projectFoderToolStripMenuItem.Text = "Select project foder";
            projectFoderToolStripMenuItem.Click += ProjectFoderToolStripMenuItem_Click;
            // 
            // selectResourcesFolderToolStripMenuItem
            // 
            selectResourcesFolderToolStripMenuItem.Name = "selectResourcesFolderToolStripMenuItem";
            selectResourcesFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            selectResourcesFolderToolStripMenuItem.Size = new Size(233, 22);
            selectResourcesFolderToolStripMenuItem.Text = "Select resources folder";
            selectResourcesFolderToolStripMenuItem.Click += selectResourcesFolderToolStripMenuItem_Click;
            // 
            // treeView
            // 
            treeView.BackColor = SystemColors.Window;
            treeView.Dock = DockStyle.Fill;
            treeView.ImageIndex = 0;
            treeView.ImageList = imageList;
            treeView.Location = new Point(0, 0);
            treeView.Name = "treeView";
            treeView.SelectedImageIndex = 0;
            treeView.Size = new Size(266, 404);
            treeView.TabIndex = 1;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "folder-solid.png");
            imageList.Images.SetKeyName(1, "cs.png");
            imageList.Images.SetKeyName(2, "at-solid.png");
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 24);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(treeView);
            splitContainer.Size = new Size(800, 404);
            splitContainer.SplitterDistance = 266;
            splitContainer.TabIndex = 2;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, lbResourcesFolder, toolStripProgressBar });
            statusStrip.Location = new Point(0, 428);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 22);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(104, 17);
            toolStripStatusLabel1.Text = "Resources folder:";
            // 
            // lbResourcesFolder
            // 
            lbResourcesFolder.Name = "lbResourcesFolder";
            lbResourcesFolder.Size = new Size(0, 17);
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(100, 16);
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "String Localizer";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem projectFoderToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog;
        private TreeView treeView;
        private SplitContainer splitContainer;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lbResourcesFolder;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ImageList imageList;
        private ToolStripMenuItem selectResourcesFolderToolStripMenuItem;
        private ToolStripProgressBar toolStripProgressBar;
    }
}
