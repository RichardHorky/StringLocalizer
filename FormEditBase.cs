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
    public partial class FormEditBase : Form
    {
        public FormEditBase()
        {
            InitializeComponent();
        }

        public bool InSelectedPathOnly
        {
            get => chInSelectedPathOnly.Checked;
        }
    }
}
