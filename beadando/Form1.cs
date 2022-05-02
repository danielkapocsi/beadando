using beadando.AutoDir;
using OxyPlot;
using OxyPlot.Series;

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
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy - MM - dd";

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
                    Competition c = new Competition((from n in database.Competitions orderby n.Id descending select n.Id).FirstOrDefault() + 1, textBoxName.Text, dateTimePicker1.Value.Date, textBoxLocation.Text);
                    database.Competitions.Add(c);
                    database.SaveChanges();

                    textBoxName.Text = "";
                    dateTimePicker1.ResetText();
                    textBoxLocation.Text = "";

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
                labelProd.Text = "Produkci�k";
                Competition referencComp = (Competition)listBoxComps.SelectedItem;
                Competition toDelete = database.Competitions.Where(x => x.Id == referencComp.Id).First();

                var toDeleteProdComps = database.CompProds.Where(x => x.CompIds == referencComp.Id).ToList();

                foreach (CompProd cprod in database.CompProds)
                {
                    foreach (var toDelPC in toDeleteProdComps)
                    {
                        if (toDelPC.Id == cprod.Id)
                        {
                            database.CompProds.Remove(cprod);
                        }
                    }
                }

                database.Competitions.Remove(toDelete);
                database.SaveChanges();

                listBoxProd.Items.Clear();
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
                else if (numericUpDownCompetitorAmount.Value < 1 || numericUpDownCompetitorAmount.Text == "")
                {
                    MessageBox.Show("�rv�nytelen versenyz� mennyis�g!", "Hib�s adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (numericUpDownAgeGroup.Value < 3 || numericUpDownAgeGroup.Text == "")
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
                        database.SaveChanges();

                        textBoxProdName.Text = "";
                        textBoxProdAssoc.Text = "";
                        numericUpDownCompetitorAmount.ResetText();
                        numericUpDownAgeGroup.ResetText();
                        comboBoxCategory.SelectedItem = null;

                        var productions = database.Productions.Where(x => database.CompProds.Where(x => x.CompIds == selectedComp.Id).Select(x => x.ProdId).ToList().Contains(x.Id));

                        listBoxProd.Items.Clear();
                        foreach (Production prod in productions)
                        {
                            listBoxProd.Items.Add(prod);
                        }
                        MessageBox.Show($"A produkci� bejegyezve a(z) ({selectedComp.Id}) azonos�t�j� {selectedComp.Date.ToString("yyyy-MM-dd")} d�tum� versenyre. Produkci�k sz�ma: {prodCount + 1}", "Sikeres jelentkez�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Modify production
        private void buttonModifyProduction_Click(object sender, EventArgs e)
        {
            if (listBoxProd.SelectedItem != null)
            {
                FormModifyProd modifyProd = new FormModifyProd((Production)listBoxProd.SelectedItem);
                modifyProd.ShowDialog();

                listBoxProd.Items.Clear();
                foreach (Production pOut in modifyProd.database.Productions)
                {
                    listBoxProd.Items.Add(pOut);
                }
                if (modifyProd.DialogResult == DialogResult.OK)
                {
                    MessageBox.Show($"A produkci� sikeresen m�dos�tva.", "Sikeres m�dos�t�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Nincs kijel�lve produkci�!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete production
        private void buttonDeleteProduction_Click(object sender, EventArgs e)
        {
            if (listBoxProd.SelectedItem != null)
            {
                Production selectedProd = (Production)listBoxProd.SelectedItem;
                string[] pieces = labelProd.Text.Split('(', ')');
                database.Productions.Remove(selectedProd);
                /*
                var toDeleteProdComps = database.CompProds.Where(x => x.ProdId == selectedProd.Id).ToList();
                
                foreach (CompProd cprod in database.CompProds)
                {
                    foreach (var toDelPC in toDeleteProdComps)
                    {
                        if (toDelPC.Id == cprod.Id)
                        {
                            database.CompProds.Remove(cprod);
                        }
                    }
                }
                */
                foreach (CompProd cprod in database.CompProds)
                {
                    if (cprod.ProdId == selectedProd.Id)
                    {
                        database.CompProds.Remove(cprod);
                    }
                }
                database.SaveChanges();

                listBoxProd.Items.Clear();

                if (labelProd.Text != "Produkci�k")
                {
                    var productions = database.Productions.Where(x => database.CompProds.Where(x => x.CompIds == int.Parse(pieces[2])).Select(x => x.ProdId).ToList().Contains(x.Id));

                    foreach (Production prod in productions)
                    {
                        listBoxProd.Items.Add(prod);
                    }
                }
                else
                {
                    var productions = database.Productions.Where(x => database.CompProds.Count(y => y.ProdId == x.Id) == 0);

                    foreach (Production prod in productions)
                    {
                        listBoxProd.Items.Add(prod);
                    }
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

            var productions = database.Productions.Where(x => database.CompProds.Where(x => x.CompIds == selectedComp.Id).Select(x => x.ProdId).ToList().Contains(x.Id));

            foreach (Production prod in productions)
            {
                listBoxProd.Items.Add(prod);
            }
        }

        // Add existing production to another competition
        private void buttonAddProd_Click(object sender, EventArgs e)
        {
            if (listBoxProd.SelectedItem != null)
            {
                if (textBoxAddID.Text != "")
                {
                    bool validID = false;
                    foreach (Competition c in database.Competitions)
                    {
                        if (c.Id == Int32.Parse(textBoxAddID.Text))
                        {
                            validID = true;

                        }
                    }

                    if (validID)
                    {
                        Production selectedProd = (Production)listBoxProd.SelectedItem;
                        var selectedComp = database.Competitions.Where(x => x.Id == Int32.Parse(textBoxAddID.Text)).First();

                        bool alreadyConnected = false;

                        foreach (CompProd cp in database.CompProds)
                        {
                            if (cp.CompIds == selectedComp.Id && cp.ProdId == selectedProd.Id)
                            {
                                alreadyConnected = true;
                            }
                        }

                        if (!alreadyConnected)
                        {
                            int prodCount = 0;
                            foreach (CompProd idSet in database.CompProds)
                            {
                                if (idSet.CompIds == Int32.Parse(textBoxAddID.Text))
                                {
                                    prodCount += 1;
                                }
                            }

                            if (prodCount < MAX_PRODUCTION)
                            {
                                labelProd.Text = "Produkci�k ((" + textBoxAddID.Text + ") azonos�t�j� verseny)";
                                CompProd cp = new CompProd((from n in database.CompProds orderby n.Id descending select n.Id).FirstOrDefault() + 1, selectedProd.Id, Int32.Parse(textBoxAddID.Text));

                                database.CompProds.Add(cp);
                                database.SaveChanges();

                                var productions = database.Productions.Where(x => database.CompProds.Where(x => x.CompIds == selectedComp.Id).Select(x => x.ProdId).ToList().Contains(x.Id));


                                listBoxProd.Items.Clear();
                                foreach (Production prod in productions)
                                {
                                    listBoxProd.Items.Add(prod);
                                }
                                textBoxAddID.Text = "";
                                MessageBox.Show($"A produkci� bejegyezve a(z) ({selectedComp.Id}) azonos�t�j� {selectedComp.Date.ToString("yyyy-MM-dd")} d�tum� versenyre. Produkci�k sz�ma: {prodCount + 1}", "Sikeres jelentkez�s.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"A(z) ({selectedComp.Id}) azonos�t�j� {selectedComp.Date.ToString("yyyy-MM-dd")} d�tum� verseny betelt.", "Sikertelen jelentkez�s!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"A(z) ({Int32.Parse(textBoxAddID.Text)}) azonos�t�j� versenyre m�r jelentkezett.", "Sikertelen jelentkez�s!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"A(z) ({Int32.Parse(textBoxAddID.Text)}) azonos�t�j� verseny nem l�tezik.", "Sikertelen jelentkez�s!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Adja meg a verseny azonos�t�t!", "Hib�s adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                textBoxAddID.Text = "";
            }
            else
            {
                MessageBox.Show($"Nincs kijel�lve produkci�!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonListFreeProductions_Click(object sender, EventArgs e)
        {
            textBoxAddID.Text = "";
            var productions = database.Productions.Where(x => database.CompProds.Count(y => y.ProdId == x.Id) == 0);

            listBoxProd.Items.Clear();
            foreach (Production prod in productions)
            {
                listBoxProd.Items.Add(prod);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonListProdsByCategory_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                labelProd.Text = "Produkci�k";
                listBoxProd.Items.Clear();
                foreach (Production prod in database.Productions)
                {
                    if (prod.Category == comboBox1.Text)
                    {
                        listBoxProd.Items.Add(prod);
                    }
                }
            }
            else
            {
                MessageBox.Show("V�lasszon kateg�ri�t!", "Hi�nyz� adat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStatistic_Click(object sender, EventArgs e)
        {
            int toddler = 0;
            int kid = 0;
            int junior = 0;
            int adult = 0;

            var competitions = database.Competitions.Where(x => x.Date == dateTimePicker2.Value).Select(x => x.Id).ToList();

            var compProd = database.CompProds.Where(x => competitions.Contains(x.CompIds)).Select(x => x.ProdId).ToList();

            var productions = database.Productions.Where(x => compProd.Contains(x.Id));

            foreach (var prod in productions)
            {
                if (prod.AgeGroup <= 5)
                {
                    toddler += 1;
                }
                else if (prod.AgeGroup >= 6 && prod.AgeGroup <= 10)
                {
                    kid += 1;
                }
                else if (prod.AgeGroup >= 11 && prod.AgeGroup <= 17)
                {
                    junior += 1;
                }
                else
                {
                    adult += 1;
                }
            }

            if (toddler == 0 && kid == 0 && junior == 0 && adult == 0)
            {
                var model = new PlotModel { };
                plotViewCompetitors.Model = model;
                MessageBox.Show("A kijel�lt napon nincs verseny.", "�res versenynap.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var model = new PlotModel { Title = $"{dateTimePicker1.Value.ToString("yyyy.MM.dd")} napi versenyz�k koroszt�ly szerinti eloszl�sa" };
                var cakeDiagramm = new PieSeries { StrokeThickness = 3.0, InsideLabelPosition = 0.7, AngleSpan = 360, StartAngle = 0, Diameter = 0.9 };
                cakeDiagramm.Slices.Add(new PieSlice("Totyog�", toddler) { IsExploded = true, Fill = OxyColor.FromRgb(95, 173, 86) });
                cakeDiagramm.Slices.Add(new PieSlice("Gyerek", kid) { IsExploded = true, Fill = OxyColor.FromRgb(71, 72, 72) });
                cakeDiagramm.Slices.Add(new PieSlice("Junior", junior) { IsExploded = true, Fill = OxyColor.FromRgb(61, 165, 217) });
                cakeDiagramm.Slices.Add(new PieSlice("Feln�tt", adult) { IsExploded = true, Fill = OxyColor.FromRgb(254, 97, 72) });

                model.Series.Add(cakeDiagramm);
                plotViewCompetitors.Model = model;
            }       
        }
    }
}