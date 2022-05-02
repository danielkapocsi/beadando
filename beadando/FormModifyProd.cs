using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using beadando.AutoDir;

namespace beadando
{
    public partial class FormModifyProd : Form
    {
        public DanceDb database;
        Production toModify;
        public FormModifyProd(Production referenceComp)
        {
            InitializeComponent();
            database = new DanceDb();
            toModify = database.Productions.Where(x => x.Id == referenceComp.Id).First();
            textBoxId.Text = toModify.Id.ToString();
            textBoxName.Text = toModify.Name;
            textBoxAssoc.Text = toModify.Association;
            numericUpDownNoOfComp.Value = toModify.NoOfCompetitors;
            numericUpDownAgeGroup.Value = toModify.AgeGroup;
            comboBoxCategory.Text = toModify.Category;
        }

        private void buttonCancelProdModify_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Nem adott meg nevet!", "Hiányzó adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxAssoc.Text == "")
            {
                MessageBox.Show("Nem adott meg egyesületet!", "Hiányzó adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numericUpDownNoOfComp.Value < 1 || numericUpDownNoOfComp.Text == "")
            {
                MessageBox.Show("Érvénytelen versenyző mennyiség!", "Hiányzó adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numericUpDownAgeGroup.Value < 3 || numericUpDownAgeGroup.Text == "")
            {
                MessageBox.Show("Érvénytelen korosztály! (Alsó korhatár: 3 év)", "Hibás adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (comboBoxCategory.Text == "")
            {
                MessageBox.Show("Válasszon kategóriát!", "Hiányzó adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                toModify.Name = textBoxName.Text;
                toModify.Association = textBoxAssoc.Text;
                toModify.NoOfCompetitors = (int)numericUpDownNoOfComp.Value;
                toModify.AgeGroup = (int)numericUpDownAgeGroup.Value;
                toModify.Category = comboBoxCategory.Text;
                database.SaveChanges();
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
