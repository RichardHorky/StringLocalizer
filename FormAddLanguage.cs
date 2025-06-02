using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StringLocalizer
{
    public partial class FormAddLanguage : DialogBase
    {
        public FormAddLanguage()
        {
            InitializeComponent();
            LoadCulturesIntoTreeView();
        }

        public string CultureName
        {
            get
            {
                return (treeView?.SelectedNode?.Tag as CultureInfo)?.Name;
            }
        }

        private void LoadCulturesIntoTreeView()
        {
            treeView.Nodes.Clear();

            // Get all specific cultures (i.e., with regional info like en-US, fr-FR)
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(c => !string.IsNullOrEmpty(c.Name)) // Skip invariant
                .ToList();

            // Group variant cultures by their parent (neutral) language code
            var grouped = allCultures
                .GroupBy(c => c.TwoLetterISOLanguageName)
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                // Try to get the neutral culture for the group (e.g. "en")
                var neutralCulture = group.FirstOrDefault(c => c.Name == c.TwoLetterISOLanguageName);

                string parentLabel;
                if (neutralCulture != null)
                {
                    parentLabel = neutralCulture.Name;
                }
                else
                {
                    // Fallback if no neutral found (very rare)
                    parentLabel = group.Key;
                }

                TreeNode parentNode = new TreeNode(parentLabel) { Tag = neutralCulture };

                // Add only the variants (exclude the neutral itself)
                foreach (var culture in group
                    .Where(c => c.Name != group.Key)
                    .OrderBy(c => c.Name))
                {
                    TreeNode childNode = new TreeNode(culture.Name) { Tag = culture };
                    parentNode.Nodes.Add(childNode);
                }

                // Only add group if there are children
                if (parentNode.Nodes.Count > 0)
                    treeView.Nodes.Add(parentNode);
            }

            treeView.ExpandAll();
        }

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
            if (e.Node.Tag == null)
                return;

            var span = (int)Math.Ceiling(e.Graphics.MeasureString("AAAAAAAAAA", treeView.Font).Width);
            using (var font = new Font(treeView.Font, FontStyle.Italic))
            {
                e.Graphics.DrawString((e.Node.Tag as CultureInfo).EnglishName, font, Brushes.Black, new Point(e.Bounds.Location.X + span, e.Bounds.Y));
            }
        }
    }
}
