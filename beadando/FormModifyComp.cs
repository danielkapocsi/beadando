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
    public partial class FormModifyComp : Form
    {
        
        public DanceDb database;
        Competition toModify;
        public FormModifyComp(Competition referenceComp)
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy - MM - dd";
            database = new DanceDb();
            toModify = database.Competitions.Where(x => x.Id == referenceComp.Id).First();
            textBoxId.Text = toModify.Id.ToString();
            textBoxName.Text = toModify.Name;
            dateTimePicker1.Value = toModify.Date;
            textBoxLocation.Text = toModify.Location;
            // ide még kell a produkciók száma
        }

        private void buttonCancelCompModify_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCompModify_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text == "")
            {
                MessageBox.Show("Nem adott meg nevet!", "Hiányzó adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxLocation.Text == "")
            {
                MessageBox.Show("Nem adott meg helyszínt!", "Hiányzó adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value < DateTime.Now.Date)
            {
                MessageBox.Show("Érvénytelen dátum!", "Hibás adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool validDay = true;
                DateTime date = dateTimePicker1.Value.Date;

                foreach (Competition comp in database.Competitions)
                {
                    if (comp.Date == date && comp.Id != toModify.Id)
                    {
                        validDay = false;
                    }
                }

                if (!validDay)
                {
                    MessageBox.Show("Az adott dátum már foglalt!", "Dátum foglalási hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    toModify.Name = textBoxName.Text;
                    toModify.Date = dateTimePicker1.Value.Date;
                    toModify.Location = textBoxLocation.Text;
                    database.SaveChanges();
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
            }
            
        }
    }
}
