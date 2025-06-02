namespace StringLocalizer
{
    partial class FormAddLanguage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddLanguage));
            treeView = new TreeView();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Dock = DockStyle.Fill;
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeView.Location = new Point(0, 0);
            treeView.Name = "treeView";
            treeView.Size = new Size(511, 421);
            treeView.TabIndex = 3;
            treeView.DrawNode += treeView_DrawNode;
            // 
            // FormAddLanguage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 450);
            Controls.Add(treeView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormAddLanguage";
            Text = "Add Language";
            Controls.SetChildIndex(treeView, 0);
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView;
    }
}