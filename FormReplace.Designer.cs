namespace StringLocalizer
{
    partial class FormReplace
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
            tableLayoutPanelReplace = new TableLayoutPanel();
            lbRelaceWith = new Label();
            edReplaceWith = new TextBox();
            tableLayoutPanelReplace.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelReplace
            // 
            tableLayoutPanelReplace.AutoSize = true;
            tableLayoutPanelReplace.ColumnCount = 2;
            tableLayoutPanelReplace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.45592F));
            tableLayoutPanelReplace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.54408F));
            tableLayoutPanelReplace.Controls.Add(lbRelaceWith, 0, 0);
            tableLayoutPanelReplace.Controls.Add(edReplaceWith, 1, 0);
            tableLayoutPanelReplace.Dock = DockStyle.Top;
            tableLayoutPanelReplace.Location = new Point(3, 74);
            tableLayoutPanelReplace.Name = "tableLayoutPanelReplace";
            tableLayoutPanelReplace.RowCount = 1;
            tableLayoutPanelReplace.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanelReplace.Size = new Size(578, 26);
            tableLayoutPanelReplace.TabIndex = 3;
            // 
            // lbRelaceWith
            // 
            lbRelaceWith.AutoSize = true;
            lbRelaceWith.Dock = DockStyle.Fill;
            lbRelaceWith.Location = new Point(3, 0);
            lbRelaceWith.Name = "lbRelaceWith";
            lbRelaceWith.Size = new Size(152, 26);
            lbRelaceWith.TabIndex = 0;
            lbRelaceWith.Text = "Relace with";
            lbRelaceWith.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // edReplaceWith
            // 
            edReplaceWith.Dock = DockStyle.Fill;
            edReplaceWith.Location = new Point(161, 3);
            edReplaceWith.Name = "edReplaceWith";
            edReplaceWith.Size = new Size(414, 23);
            edReplaceWith.TabIndex = 1;
            // 
            // FormReplace
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 165);
            Controls.Add(tableLayoutPanelReplace);
            Name = "FormReplace";
            Text = "FormReplace";
            Controls.SetChildIndex(tableLayoutPanelReplace, 0);
            tableLayoutPanelReplace.ResumeLayout(false);
            tableLayoutPanelReplace.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelReplace;
        private Label lbRelaceWith;
        private TextBox edReplaceWith;
    }
}