namespace StringLocalizer
{
    partial class FormFind
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
            tableLayoutPanelFind = new TableLayoutPanel();
            lbFindText = new Label();
            edFindText = new TextBox();
            chMatchCase = new CheckBox();
            chMatchWholeWords = new CheckBox();
            tableLayoutPanelFind.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelFind
            // 
            tableLayoutPanelFind.AutoSize = true;
            tableLayoutPanelFind.ColumnCount = 2;
            tableLayoutPanelFind.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.58186F));
            tableLayoutPanelFind.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.41814F));
            tableLayoutPanelFind.Controls.Add(lbFindText, 0, 0);
            tableLayoutPanelFind.Controls.Add(edFindText, 1, 0);
            tableLayoutPanelFind.Controls.Add(chMatchCase, 0, 1);
            tableLayoutPanelFind.Controls.Add(chMatchWholeWords, 1, 1);
            tableLayoutPanelFind.Dock = DockStyle.Top;
            tableLayoutPanelFind.Location = new Point(3, 22);
            tableLayoutPanelFind.Name = "tableLayoutPanelFind";
            tableLayoutPanelFind.RowCount = 2;
            tableLayoutPanelFind.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanelFind.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanelFind.Size = new Size(578, 52);
            tableLayoutPanelFind.TabIndex = 2;
            // 
            // lbFindText
            // 
            lbFindText.AutoSize = true;
            lbFindText.Dock = DockStyle.Fill;
            lbFindText.Location = new Point(3, 0);
            lbFindText.Name = "lbFindText";
            lbFindText.Size = new Size(153, 26);
            lbFindText.TabIndex = 0;
            lbFindText.Text = "Find text";
            lbFindText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // edFindText
            // 
            edFindText.Dock = DockStyle.Fill;
            edFindText.Location = new Point(162, 3);
            edFindText.Name = "edFindText";
            edFindText.Size = new Size(413, 23);
            edFindText.TabIndex = 1;
            // 
            // chMatchCase
            // 
            chMatchCase.AutoSize = true;
            chMatchCase.Location = new Point(3, 29);
            chMatchCase.Name = "chMatchCase";
            chMatchCase.Size = new Size(86, 19);
            chMatchCase.TabIndex = 2;
            chMatchCase.Text = "Match &case";
            chMatchCase.UseVisualStyleBackColor = true;
            // 
            // chMatchWholeWords
            // 
            chMatchWholeWords.AutoSize = true;
            chMatchWholeWords.Location = new Point(162, 29);
            chMatchWholeWords.Name = "chMatchWholeWords";
            chMatchWholeWords.Size = new Size(130, 19);
            chMatchWholeWords.TabIndex = 3;
            chMatchWholeWords.Text = "Match &whole words";
            chMatchWholeWords.UseVisualStyleBackColor = true;
            // 
            // FormFind
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 136);
            Controls.Add(tableLayoutPanelFind);
            Name = "FormFind";
            Text = "Find";
            Controls.SetChildIndex(tableLayoutPanelFind, 0);
            tableLayoutPanelFind.ResumeLayout(false);
            tableLayoutPanelFind.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelFind;
        private Label lbFindText;
        private TextBox edFindText;
        private CheckBox chMatchCase;
        private CheckBox chMatchWholeWords;
    }
}