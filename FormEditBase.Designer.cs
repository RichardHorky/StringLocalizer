namespace StringLocalizer
{
    partial class FormEditBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditBase));
            chInSelectedPathOnly = new CheckBox();
            panelBottom = new Panel();
            buttonOK = new Button();
            buttonCancel = new Button();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // chInSelectedPathOnly
            // 
            chInSelectedPathOnly.AutoSize = true;
            chInSelectedPathOnly.Dock = DockStyle.Top;
            chInSelectedPathOnly.Location = new Point(3, 3);
            chInSelectedPathOnly.Name = "chInSelectedPathOnly";
            chInSelectedPathOnly.Padding = new Padding(3, 0, 0, 0);
            chInSelectedPathOnly.Size = new Size(578, 19);
            chInSelectedPathOnly.TabIndex = 0;
            chInSelectedPathOnly.Text = "In &selected path only";
            chInSelectedPathOnly.UseVisualStyleBackColor = true;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonOK);
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(3, 163);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(578, 29);
            panelBottom.TabIndex = 1;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(500, 3);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 2;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(3, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 0;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormEditBase
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(584, 195);
            Controls.Add(panelBottom);
            Controls.Add(chInSelectedPathOnly);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormEditBase";
            Padding = new Padding(3);
            Text = "FormEditBase";
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chInSelectedPathOnly;
        private Panel panelBottom;
        private Button buttonOK;
        private Button buttonCancel;
    }
}