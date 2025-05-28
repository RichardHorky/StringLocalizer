namespace StringLocalizer
{
    partial class ResourceEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView = new DataGridView();
            colKey = new DataGridViewTextBoxColumn();
            colComment = new DataGridViewTextBoxColumn();
            colNeutral = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { colKey, colComment, colNeutral });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(0, 0);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(684, 390);
            dataGridView.TabIndex = 0;
            dataGridView.CellValidated += dataGridView_CellValidated;
            // 
            // colKey
            // 
            colKey.DataPropertyName = "Key";
            colKey.HeaderText = "Key";
            colKey.Name = "colKey";
            colKey.ReadOnly = true;
            colKey.Width = 200;
            // 
            // colComment
            // 
            colComment.DataPropertyName = "Comment";
            colComment.HeaderText = "Comment";
            colComment.Name = "colComment";
            colComment.Width = 300;
            // 
            // colNeutral
            // 
            colNeutral.DataPropertyName = "Neutral";
            colNeutral.HeaderText = "Neutral";
            colNeutral.Name = "colNeutral";
            colNeutral.Width = 200;
            // 
            // ResourceEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridView);
            Name = "ResourceEditor";
            Size = new Size(684, 390);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn colKey;
        private DataGridViewTextBoxColumn colComment;
        private DataGridViewTextBoxColumn colNeutral;
    }
}
