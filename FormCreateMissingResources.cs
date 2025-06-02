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
    public partial class FormCreateMissingResources : FormEditBase
    {
        public FormCreateMissingResources()
        {
            InitializeComponent();
        }

        public bool CreareNeutral
        {
            get => chCreateNeutral.Checked;
        }

        public bool SetNeutralByKey
        {
            get => chSetNeutralByKey.Checked;
        }
    }
}
