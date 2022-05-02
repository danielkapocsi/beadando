namespace beadando
{
    partial class FormModifyProd
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxAssoc = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDownNoOfComp = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAgeGroup = new System.Windows.Forms.NumericUpDown();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoOfComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAgeGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Produkció adatai";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Azonosító:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Név:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Egyesület:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Versenyzők száma:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Korosztály:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Kategória:";
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(122, 38);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.ReadOnly = true;
            this.textBoxId.Size = new System.Drawing.Size(128, 23);
            this.textBoxId.TabIndex = 7;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(122, 76);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(128, 23);
            this.textBoxName.TabIndex = 8;
            // 
            // textBoxAssoc
            // 
            this.textBoxAssoc.Location = new System.Drawing.Point(122, 115);
            this.textBoxAssoc.Name = "textBoxAssoc";
            this.textBoxAssoc.Size = new System.Drawing.Size(128, 23);
            this.textBoxAssoc.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 13;
            this.button1.Text = "Mégsem";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonCancelProdModify_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(153, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 14;
            this.button2.Text = "Módosítás";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDownNoOfComp
            // 
            this.numericUpDownNoOfComp.Location = new System.Drawing.Point(122, 155);
            this.numericUpDownNoOfComp.Name = "numericUpDownNoOfComp";
            this.numericUpDownNoOfComp.Size = new System.Drawing.Size(128, 23);
            this.numericUpDownNoOfComp.TabIndex = 15;
            // 
            // numericUpDownAgeGroup
            // 
            this.numericUpDownAgeGroup.Location = new System.Drawing.Point(122, 195);
            this.numericUpDownAgeGroup.Name = "numericUpDownAgeGroup";
            this.numericUpDownAgeGroup.Size = new System.Drawing.Size(128, 23);
            this.numericUpDownAgeGroup.TabIndex = 16;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] {
            "Hiphop",
            "Latin táncok",
            "Akrobatika",
            "Balett"});
            this.comboBoxCategory.Location = new System.Drawing.Point(122, 240);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(128, 23);
            this.comboBoxCategory.TabIndex = 17;
            // 
            // FormModifyProd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 355);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.numericUpDownAgeGroup);
            this.Controls.Add(this.numericUpDownNoOfComp);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxAssoc);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormModifyProd";
            this.Text = "FormModifyProd";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoOfComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAgeGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBoxId;
        private TextBox textBoxName;
        private TextBox textBoxAssoc;
        private Button button1;
        private Button button2;
        private NumericUpDown numericUpDownNoOfComp;
        private NumericUpDown numericUpDownAgeGroup;
        private ComboBox comboBoxCategory;
    }
}