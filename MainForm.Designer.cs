﻿namespace StringLocalizer
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
            toolStripSeparator1 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            findToolStripMenuItem = new ToolStripMenuItem();
            replaceToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            createMissingFilesToolStripMenuItem = new ToolStripMenuItem();
            projectToolStripMenuItem = new ToolStripMenuItem();
            addLanguageToolStripMenuItem = new ToolStripMenuItem();
            folderBrowserDialog = new FolderBrowserDialog();
            treeView = new TreeView();
            imageList = new ImageList(components);
            splitContainer = new SplitContainer();
            panelFilter = new Panel();
            lbFilter = new Label();
            buttonCancelFiter = new Button();
            resourceEditor = new ResourceEditor();
            statusStrip = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lbResourcesFolder = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panelFilter.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, projectToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1174, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { projectFoderToolStripMenuItem, selectResourcesFolderToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem });
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
            projectFoderToolStripMenuItem.Text = "Select &project foder";
            projectFoderToolStripMenuItem.Click += ProjectFoderToolStripMenuItem_Click;
            // 
            // selectResourcesFolderToolStripMenuItem
            // 
            selectResourcesFolderToolStripMenuItem.Name = "selectResourcesFolderToolStripMenuItem";
            selectResourcesFolderToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            selectResourcesFolderToolStripMenuItem.Size = new Size(233, 22);
            selectResourcesFolderToolStripMenuItem.Text = "Select &resources folder";
            selectResourcesFolderToolStripMenuItem.Click += selectResourcesFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(230, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(233, 22);
            closeToolStripMenuItem.Text = "&Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { findToolStripMenuItem, replaceToolStripMenuItem, toolStripSeparator2, createMissingFilesToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // findToolStripMenuItem
            // 
            findToolStripMenuItem.Enabled = false;
            findToolStripMenuItem.Name = "findToolStripMenuItem";
            findToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            findToolStripMenuItem.Size = new Size(221, 22);
            findToolStripMenuItem.Text = "&Find";
            findToolStripMenuItem.Click += findToolStripMenuItem_Click;
            // 
            // replaceToolStripMenuItem
            // 
            replaceToolStripMenuItem.Enabled = false;
            replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            replaceToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            replaceToolStripMenuItem.Size = new Size(221, 22);
            replaceToolStripMenuItem.Text = "&Replace";
            replaceToolStripMenuItem.Click += replaceToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(218, 6);
            // 
            // createMissingFilesToolStripMenuItem
            // 
            createMissingFilesToolStripMenuItem.Enabled = false;
            createMissingFilesToolStripMenuItem.Name = "createMissingFilesToolStripMenuItem";
            createMissingFilesToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.M;
            createMissingFilesToolStripMenuItem.Size = new Size(221, 22);
            createMissingFilesToolStripMenuItem.Text = "&Create missing files";
            createMissingFilesToolStripMenuItem.Click += createMissingFilesToolStripMenuItem_Click;
            // 
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addLanguageToolStripMenuItem });
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new Size(56, 20);
            projectToolStripMenuItem.Text = "&Project";
            // 
            // addLanguageToolStripMenuItem
            // 
            addLanguageToolStripMenuItem.Enabled = false;
            addLanguageToolStripMenuItem.Name = "addLanguageToolStripMenuItem";
            addLanguageToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            addLanguageToolStripMenuItem.Size = new Size(190, 22);
            addLanguageToolStripMenuItem.Text = "Add &language";
            addLanguageToolStripMenuItem.Click += addLanguageToolStripMenuItem_Click;
            // 
            // treeView
            // 
            treeView.BackColor = SystemColors.Window;
            treeView.Dock = DockStyle.Fill;
            treeView.ImageIndex = 0;
            treeView.ImageList = imageList;
            treeView.Location = new Point(0, 21);
            treeView.Name = "treeView";
            treeView.SelectedImageIndex = 0;
            treeView.Size = new Size(284, 667);
            treeView.TabIndex = 1;
            treeView.AfterSelect += treeView_AfterSelect;
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
            splitContainer.Panel1.Controls.Add(panelFilter);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(resourceEditor);
            splitContainer.Size = new Size(1174, 688);
            splitContainer.SplitterDistance = 284;
            splitContainer.TabIndex = 2;
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.Red;
            panelFilter.Controls.Add(lbFilter);
            panelFilter.Controls.Add(buttonCancelFiter);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(0, 0);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(284, 21);
            panelFilter.TabIndex = 2;
            panelFilter.Visible = false;
            // 
            // lbFilter
            // 
            lbFilter.Dock = DockStyle.Fill;
            lbFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbFilter.ForeColor = Color.White;
            lbFilter.Location = new Point(0, 0);
            lbFilter.Name = "lbFilter";
            lbFilter.Size = new Size(209, 21);
            lbFilter.TabIndex = 1;
            lbFilter.Text = "Filter Active";
            lbFilter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonCancelFiter
            // 
            buttonCancelFiter.Dock = DockStyle.Right;
            buttonCancelFiter.Location = new Point(209, 0);
            buttonCancelFiter.Name = "buttonCancelFiter";
            buttonCancelFiter.Size = new Size(75, 21);
            buttonCancelFiter.TabIndex = 0;
            buttonCancelFiter.Text = "Clear";
            buttonCancelFiter.UseVisualStyleBackColor = true;
            buttonCancelFiter.Click += buttonCancelFiter_Click;
            // 
            // resourceEditor
            // 
            resourceEditor.Dock = DockStyle.Fill;
            resourceEditor.Location = new Point(0, 0);
            resourceEditor.Name = "resourceEditor";
            resourceEditor.Size = new Size(886, 688);
            resourceEditor.TabIndex = 0;
            resourceEditor.CommentChanged += resourceEditor_CommentChanged;
            resourceEditor.NeutralChanged += resourceEditor_NeutralChanged;
            resourceEditor.LanguageChanged += resourceEditor_LanguageChanged;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, lbResourcesFolder, toolStripProgressBar });
            statusStrip.Location = new Point(0, 712);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1174, 22);
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
            ClientSize = new Size(1174, 734);
            Controls.Add(splitContainer);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "String Localizer";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
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
        private ResourceEditor resourceEditor;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripMenuItem replaceToolStripMenuItem;
        private ToolStripMenuItem createMissingFilesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private Panel panelFilter;
        private Label lbFilter;
        private Button buttonCancelFiter;
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem addLanguageToolStripMenuItem;
    }
}
