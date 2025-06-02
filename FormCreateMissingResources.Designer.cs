namespace StringLocalizer
{
    partial class FormCreateMissingResources
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
            tableLayoutPanelCeateMissing = new TableLayoutPanel();
            chCreateNeutral = new CheckBox();
            chSetNeutralByKey = new CheckBox();
            tableLayoutPanelCeateMissing.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelCeateMissing
            // 
            tableLayoutPanelCeateMissing.AutoSize = true;
            tableLayoutPanelCeateMissing.ColumnCount = 2;
            tableLayoutPanelCeateMissing.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCeateMissing.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCeateMissing.Controls.Add(chCreateNeutral, 0, 0);
            tableLayoutPanelCeateMissing.Controls.Add(chSetNeutralByKey, 1, 0);
            tableLayoutPanelCeateMissing.Dock = DockStyle.Top;
            tableLayoutPanelCeateMissing.Location = new Point(3, 22);
            tableLayoutPanelCeateMissing.Name = "tableLayoutPanelCeateMissing";
            tableLayoutPanelCeateMissing.RowCount = 1;
            tableLayoutPanelCeateMissing.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanelCeateMissing.Size = new Size(494, 26);
            tableLayoutPanelCeateMissing.TabIndex = 3;
            // 
            // chCreateNeutral
            // 
            chCreateNeutral.AutoSize = true;
            chCreateNeutral.Dock = DockStyle.Fill;
            chCreateNeutral.Location = new Point(3, 3);
            chCreateNeutral.Name = "chCreateNeutral";
            chCreateNeutral.Size = new Size(241, 20);
            chCreateNeutral.TabIndex = 0;
            chCreateNeutral.Text = "Create files for neutral language";
            chCreateNeutral.UseVisualStyleBackColor = true;
            // 
            // chSetNeutralByKey
            // 
            chSetNeutralByKey.AutoSize = true;
            chSetNeutralByKey.Dock = DockStyle.Fill;
            chSetNeutralByKey.Location = new Point(250, 3);
            chSetNeutralByKey.Name = "chSetNeutralByKey";
            chSetNeutralByKey.Size = new Size(241, 20);
            chSetNeutralByKey.TabIndex = 1;
            chSetNeutralByKey.Text = "Set neutral value by key";
            chSetNeutralByKey.UseVisualStyleBackColor = true;
            // 
            // FormCreateMissingResources
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 141);
            Controls.Add(tableLayoutPanelCeateMissing);
            Name = "FormCreateMissingResources";
            Text = "Create Missing Resources";
            Controls.SetChildIndex(tableLayoutPanelCeateMissing, 0);
            tableLayoutPanelCeateMissing.ResumeLayout(false);
            tableLayoutPanelCeateMissing.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelCeateMissing;
        private CheckBox chCreateNeutral;
        private CheckBox chSetNeutralByKey;
    }
}