using beadando.AutoDir;

namespace beadando
{
    public partial class Form1 : Form
    {
        const int MAX_PRODUCTION = 150;
        DanceDb database;

        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy - MM - dd";
            
            database = new DanceDb();
            foreach (Competition c in database.Competitions)
            {
                listBoxComps.Items.Add(c);
            }
            
        }
       
        /*
            prodot hozz�adni versenyhez: kell egy textarea ahonnan olvassuk h melyik versenyhez akarjuk hozz�adni (azonos�t� alapj�n)
            meg kell vizsg�lni h l�tezik-e a verseny ha nem akkor error �zenet,
            ha l�tezik, akkor sim�n az compid-khez hozz�adjuk a versenyid-t
        */

        // Logic for Competitions

        // Add competition
        
        private void buttonAddComp_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Nem adott meg nevet!", "Hi�nyz� adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxLocation.Text == "")
            {
                MessageBox.Show("Nem adott meg helysz�nt!", "Hi�nyz� adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value < DateTime.Now.Date)
            {
                MessageBox.Show("�rv�nytelen d�tum!", "Hib�s adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool validDay = true;
                DateTime date = dateTimePicker1.Value.Date;
                
                foreach (Competition comp in database.Competitions)
                {
                    if (comp.Date == date)
                    {
                        validDay = false;
                    }
                }

                if (!validDay)
                {
                    MessageBox.Show("Az adott d�tum m�r foglalt!", "D�tum foglal�si hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string nameTmp = textBoxName.Text;
                    DateTime dateTmp = dateTimePicker1.Value.Date;
                    string locationTmp = textBoxLocation.Text;


                    Competition c = new Competition((from n in database.Competitions orderby n.Id descending select n.Id).FirstOrDefault() + 1 , textBoxName.Text, dateTimePicker1.Value.Date, textBoxLocation.Text);
                    database.Competitions.Add(c);
                    database.SaveChanges();
                    listBoxComps.Items.Clear();
                    foreach (Competition cOut in database.Competitions)
                    {
                        listBoxComps.Items.Add(cOut);
                    }
                    MessageBox.Show($"A verseny bejegyezve.", "Sikeres bejegyz�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Modify competition
        private void buttonModifyComp_Click(object sender, EventArgs e)
        {
            if (listBoxComps.SelectedItem != null)
            {
                FormModifyComp modifyComp = new FormModifyComp((Competition)listBoxComps.SelectedItem);
                modifyComp.ShowDialog();

                listBoxComps.Items.Clear();
                foreach (Competition cOut in modifyComp.database.Competitions)
                {
                    listBoxComps.Items.Add(cOut);
                }
                if (modifyComp.DialogResult == DialogResult.OK)
                {
                    MessageBox.Show($"A verseny sikeresen m�dos�tva.", "Sikeres m�dos�t�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Nincs kijel�lve verseny!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete competition
        private void buttonDeleteComp_Click(object sender, EventArgs e)
        {
            if (listBoxComps.SelectedItem != null)
            {
                Competition referencComp = (Competition)listBoxComps.SelectedItem;
                Competition toDelete = database.Competitions.Where(x => x.Id == referencComp.Id ).First();

                database.Competitions.Remove(toDelete);
                database.SaveChanges();

                listBoxComps.Items.Clear();
                foreach (Competition cOut in database.Competitions)
                {
                    listBoxComps.Items.Add(cOut);
                }
                MessageBox.Show($"A(z) ({referencComp.Id}) azonos�t�j� verseny sikeresen t�r�lve.", "Sikeres verseny t�rl�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Nincs kijel�lve verseny!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logic for Productions

        // Add production
        private void buttonAddProduction_Click(object sender, EventArgs e)
        {
            if (listBoxComps.SelectedItem != null)
            {
                Competition selectedComp = (Competition)listBoxComps.SelectedItem;

                if (textBoxProdName.Text == "")
                {
                    MessageBox.Show("Nem adott meg nevet!", "Hi�nyz� adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxProdAssoc.Text == "")
                {
                    MessageBox.Show("Nem adott meg egyes�letet!", "Hi�nyz� adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (numericUpDownCompetitorAmount.Value < 1)
                {
                    MessageBox.Show("�rv�nytelen versenyz� mennyis�g!", "Hib�s adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (numericUpDownAgeGroup.Value < 3)
                {
                    MessageBox.Show("�rv�nytelen koroszt�ly! (Als� korhat�r: 3 �v)", "Hib�s adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (comboBoxCategory.Text == "")
                {
                    MessageBox.Show("V�lasszon kateg�ri�t!", "Hi�nyz� adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int prodCount = 0;
                    foreach (CompProd idSet in database.CompProds)
                    {
                        if (idSet.CompIds == selectedComp.Id)
                        {
                            prodCount += 1;
                        }
                    }

                    if (prodCount < MAX_PRODUCTION)
                    {
                        Production p = new Production((from n in database.Productions orderby n.Id descending select n.Id).FirstOrDefault() + 1, textBoxProdName.Text, textBoxProdAssoc.Text, (int)numericUpDownCompetitorAmount.Value, (int)numericUpDownAgeGroup.Value, comboBoxCategory.Text);
                        labelProd.Text = "Produkci�k ((" + selectedComp.Id.ToString() + ") azonos�t�j� verseny)";
                        CompProd cp = new CompProd((from n in database.CompProds orderby n.Id descending select n.Id).FirstOrDefault() + 1, p.Id, selectedComp.Id);
                        database.CompProds.Add(cp);
                        database.Productions.Add(p);
                        //p.CompIds.Add(selectedComp.Id);
                        database.SaveChanges();
                        listBoxProd.Items.Clear();

                        var joinedTable = database.Productions.Join(database.CompProds, prod => prod.Id, comp => comp.ProdId, (prod, comp) => new { prod, comp });
                        listBoxProd.Items.Add((from n in joinedTable where n.comp.Id == selectedComp.Id select n.prod));
                        /*
                        foreach (var jt in joinedTable)
                        {
                            
                            
                            if (selectedComp.Id == jt.comp.Id)
                            {
                                listBoxProd.Items.Add(jt.prod);
                            }
                            
                            /*
                            foreach (Production pOut in database.Productions)
                            {
                                if (pOut.Id == cpOut.ProdId && cpOut.CompIds == selectedComp.Id)
                                {
                                    listBoxProd.Items.Add(pOut);
                                }
                            }
                            */
                            /*
                            if (idOut.CompIDs.Contains(selectedComp.Id.ToString()))
                            {
                                listBoxProd.Items.Add(idOut);
                            }
                           
                        }*/
                        MessageBox.Show($"A produkci� bejegyezve a(z) ({selectedComp.Id}) azonos�t�j� {selectedComp.Date.ToString("yyyy-MM-dd")} d�tum� versenyre. Produkci�k sz�ma: {prodCount+1}", "Sikeres jelentkez�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"A(z) ({selectedComp.Id}) azonos�t�j� {selectedComp.Date.ToString("yyyy-MM-dd")} d�tum� verseny betelt.", "Sikertelen jelentkez�s!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Nincs kijel�lve verseny!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBoxProd.SelectedItem != null)
            {
                string[] pieces = labelProd.Text.Split('(', ')');
                
                Production selectedProd = (Production)listBoxProd.SelectedItem;
                database.Productions.Remove(selectedProd);
                database.SaveChanges();

                listBoxProd.Items.Clear();
                foreach (Production pOut in database.Productions)
                {
                    /*
                    if (pOut.CompIDs.Contains(pieces[2]))
                    {
                        listBoxProd.Items.Add(pOut);
                    }
                    */
                }
                MessageBox.Show($"A produkci� t�rl�se sikeres volt.", "Sikeres t�rl�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Nincs kijel�lve produkci�!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxComps_DoubleClick(object sender, EventArgs e)
        {
            Competition selectedComp = (Competition)listBoxComps.SelectedItem;
            labelProd.Text = "Produkci�k ((" + selectedComp.Id.ToString() + ") azonos�t�j� verseny)";
            listBoxProd.Items.Clear();

            //var joinedTable = database.Productions.Join(database.CompProds, prod => prod.Id, comp => comp.ProdId, (prod, comp) => new { prod, comp });
            var prodByCompId = database.CompProds.Where(x => x.CompIds == selectedComp.Id);
            var prods = database.Productions.Join(prodByCompId, prodByCompId => prodByCompId., )
            listBoxProd.Items.Add((from n in database.Productions join g in database.CompProds on n.Id equals g.ProdId where g.CompIds == selectedComp.Id select n).ToList()[0]);
                //if (selectedComp.Id == jt.comp.Id)
                //{
                //    listBoxProd.Items.Add(jt.prod);
                //}

        }
    }
}