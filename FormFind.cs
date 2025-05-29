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
    public partial class FormFind : FormEditBase
    {
        public FormFind()
        {
            InitializeComponent();
        }

        public string FinText
        {
            get => edFindText.Text;
        }

        public bool MatchCase
        {
            get => chMatchCase.Checked;
        }

        public bool MatchWholeWord
        {
            get => chMatchWholeWords.Checked;
        }

        private void FormFind_Shown(object sender, EventArgs e)
        {
            edFindText.Focus();
        }
    }
}
