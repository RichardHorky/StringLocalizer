using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringLocalizer
{
    public partial class ResourceEditor : UserControl
    {
        private DataTable _table;

        public ResourceEditor()
        {
            InitializeComponent();
            CreateDataSource();
        }

        public event EventHandler<(string Key, string Value)> CommentChanged;
        public event EventHandler<(string Key, string Value)> NeutralChanged;
        public event EventHandler<(string Key, string Language, string Value)> LanguageChanged;

        public void Clear()
        {
            _table.Clear();
        }

        public DataRow AddKey(string key)
        {
            var keyRow = _table.Rows.OfType<DataRow>().FirstOrDefault(i => i["Key"].ToString() == key);
            if (keyRow != null)
                return keyRow;
            return _table.Rows.Add(key);
        }

        public void AddNeutralValues(string key, string comment, string neutral)
        {
            var row = AddKey(key);
            row["Comment"] = comment;
            row["Neutral"] = neutral;
        }

        public void AddLanguageValue(string key, string language, string value)
        {
            var row = AddKey(key);
            if (row.Table.Columns.Contains(language))
            {
                row[language] = value;
            }
            else
            {
                row.Table.Columns.Add(language, typeof(string));
                row[language] = value;
            }
        }

        public void AddLanguage(string language)
        {
            if (!_table.Columns.Contains(language))
            {
                _table.Columns.Add(language, typeof(string));
            }
        }

        public void ClearLanguageColums()
        {
            while (_table.Columns.Count > 3)
                _table.Columns.RemoveAt(_table.Columns.Count - 1);
        }

        private void CreateDataSource()
        {
            _table = new DataTable();
            _table.Columns.Add("Key", typeof(string));
            _table.Columns.Add("Comment", typeof(string));
            _table.Columns.Add("Neutral", typeof(string));
            dataGridView.DataSource = _table;
        }

        private void dataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_table.Columns[e.ColumnIndex].ColumnName == colComment.DataPropertyName)
            {
                CommentChanged?.Invoke(this, (_table.Rows[e.RowIndex]["Key"].ToString(), _table.Rows[e.RowIndex]["Comment"].ToString()));
                return;
            }
            if (_table.Columns[e.ColumnIndex].ColumnName == colNeutral.DataPropertyName)
            {
                NeutralChanged?.Invoke(this, (_table.Rows[e.RowIndex]["Key"].ToString(), _table.Rows[e.RowIndex]["Neutral"].ToString()));
                return;
            }
            if (e.ColumnIndex > 2)
            {
                var language = _table.Columns[e.ColumnIndex].ColumnName;
                LanguageChanged?.Invoke(this, (_table.Rows[e.RowIndex]["Key"].ToString(), language, _table.Rows[e.RowIndex][e.ColumnIndex].ToString()));
            }
        }
    }
}
