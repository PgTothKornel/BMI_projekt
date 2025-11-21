using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace BMI{
    public partial class adatBevitel : Form{

        public adatBevitel()
        {
            alapGombok();
            InitializeComponent();

            try
            {
                string masterConnectionString = "Server=localhost;Database=;User ID=root;Password=mysql;";
                string databaseSql = File.ReadAllText("database.sql");
                string dataConnectionString = "Server=localhost;Database=BMI_Projekt;User ID=root;Password=mysql;";
                using (MySqlConnection masterConnection = new MySqlConnection(masterConnectionString))
                {
                    masterConnection.Open();
                    //MySqlCommand setupScript = new MySqlCommand();
                    //setupScript.CommandText = "DROP DATABASE IF EXISTS BMI_Projekt; CREATE DATABASE IF NOT EXISTS BMI_Projekt;";
                    //setupScript.ExecuteNonQuery();

                    using (MySqlCommand command = new MySqlCommand("DROP DATABASE IF EXISTS BMI_Projekt; CREATE DATABASE IF NOT EXISTS BMI_Projekt;", masterConnection))
                    {
                        command.ExecuteNonQuery();
                    }

                    masterConnection.Close();

                }
                using (MySqlConnection connection = new MySqlConnection(dataConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command1 = new MySqlCommand(databaseSql, connection))
                    {
                        command1.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az adatbázis inicializálásakor! Kérjük, ellenőrizze a MySQL szerver futását, vagy az internetkapcsolatot." + ex);
            }
        }

        private void alapGombok()
        {
            System.Windows.Forms.Button btn_input = new System.Windows.Forms.Button();
            System.Windows.Forms.Button btn_output = new System.Windows.Forms.Button();
            System.Windows.Forms.Button btn_exit = new System.Windows.Forms.Button();
            System.Windows.Forms.Button btn_vissza = new System.Windows.Forms.Button();

            // 
            // btn_input
            // 
            btn_input.Location = new System.Drawing.Point(13, 13);
            btn_input.Name = "btn_input";
            btn_input.Size = new System.Drawing.Size(128, 40);
            btn_input.TabIndex = 0;
            btn_input.Text = "Bevitel";
            btn_input.UseVisualStyleBackColor = true;
            btn_input.Click += new System.EventHandler(btn_input_Click);
            // 
            // btn_output
            // 
            btn_output.Location = new System.Drawing.Point(157, 13);
            btn_output.Name = "btn_output";
            btn_output.Size = new System.Drawing.Size(128, 40);
            btn_output.TabIndex = 1;
            btn_output.Text = "Kiolvasás";
            btn_output.UseVisualStyleBackColor = true;

            btn_exit = new System.Windows.Forms.Button();
            btn_vissza = new System.Windows.Forms.Button();
            // 
            // btn_exit
            // 
            btn_exit.BackColor = System.Drawing.Color.Red;
            btn_exit.Location = new System.Drawing.Point(1097, 642);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new System.Drawing.Size(91, 38);
            btn_exit.TabIndex = 0;
            btn_exit.Text = "Kilépés";
            btn_exit.UseVisualStyleBackColor = false;
            btn_exit.Click += new System.EventHandler(kilepes);
            // 
            // btn_vissza
            // 
            btn_vissza.Location = new System.Drawing.Point(1000, 642);
            btn_vissza.Name = "btn_vissza";
            btn_vissza.Size = new System.Drawing.Size(91, 38);
            btn_vissza.TabIndex = 1;
            btn_vissza.Text = "Vissza";
            btn_vissza.UseVisualStyleBackColor = true;
            btn_vissza.Click += new System.EventHandler(vissza);

            Controls.Add(btn_input);
            Controls.Add(btn_output);
            Controls.Add(btn_exit);
            Controls.Add(btn_vissza);
        }


        private void btn_input_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn_hozzaad = new System.Windows.Forms.Button();
            System.Windows.Forms.Button btn_kereses = new System.Windows.Forms.Button();
            System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox textBox1 = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
            // 
            // btn_hozzaad
            // 
            btn_hozzaad.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            btn_hozzaad.Location = new System.Drawing.Point(18, 158);
            btn_hozzaad.Name = "btn_hozzaad";
            btn_hozzaad.Size = new System.Drawing.Size(322, 140);
            btn_hozzaad.TabIndex = 0;
            btn_hozzaad.Text = "Új ember hozzáadása";
            btn_hozzaad.UseVisualStyleBackColor = true;
            btn_hozzaad.Click += new System.EventHandler(hozzaad_menu);
            // 
            // btn_kereses
            // 
            btn_kereses.Location = new System.Drawing.Point(290, 463);
            btn_kereses.Name = "btn_kereses";
            btn_kereses.Size = new System.Drawing.Size(157, 44);
            btn_kereses.TabIndex = 1;
            btn_kereses.Text = "Keresés";
            btn_kereses.UseVisualStyleBackColor = true;
            btn_kereses.Click += new System.EventHandler(kereses_menu);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label1.Location = new System.Drawing.Point(12, 404);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(581, 37);
            label1.TabIndex = 2;
            label1.Text = "Egy létező személyhez új adatot bevinni";
            // 
            // textBox1
            // 
            textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            textBox1.Location = new System.Drawing.Point(18, 463);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(252, 44);
            textBox1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label2.Location = new System.Drawing.Point(12, 589);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(298, 37);
            label2.TabIndex = 0;
            label2.Text = "Létezik ilyen ember!";
            label2.Visible = false;

            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(btn_kereses);
            Controls.Add(btn_hozzaad);
            InitializeComponent();
        }

        private void hozzaad_menu(object sender, EventArgs e)
        {



            //System.Windows.Forms.Button btn_raKeres = new System.Windows.Forms.Button();
            System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox tb_om = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox tb_nev = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label4 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox tb_lakcim = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label5 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox tb_taj = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label6 = new System.Windows.Forms.Label();
            System.Windows.Forms.Label label7 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox tb_szuletes = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label8 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox tb_osztaly = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label9 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox tb_kartya = new System.Windows.Forms.TextBox();
            System.Windows.Forms.Label label10 = new System.Windows.Forms.Label();
            System.Windows.Forms.RadioButton rb_fiu = new System.Windows.Forms.RadioButton();
            System.Windows.Forms.RadioButton rb_lany = new System.Windows.Forms.RadioButton();


            // 
            // btn_raKeres
            //
            /*
            btn_raKeres.Location = new System.Drawing.Point(502, 204);
            btn_raKeres.Name = "btn_raKeres";
            btn_raKeres.Size = new System.Drawing.Size(91, 38);
            btn_raKeres.TabIndex = 1;
            btn_raKeres.Text = "Rákeres";
            btn_raKeres.UseVisualStyleBackColor = true;*/
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label3.Location = new System.Drawing.Point(656, 50);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(224, 37);
            label3.TabIndex = 0;
            label3.Text = "OM azonosító:";
            // 
            // tb_om
            // 
            tb_om.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_om.Location = new System.Drawing.Point(906, 47);
            tb_om.Name = "tb_om";
            tb_om.Size = new System.Drawing.Size(227, 44);
            tb_om.TabIndex = 1;
            // 
            // tb_nev
            // 
            tb_nev.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_nev.Location = new System.Drawing.Point(906, 109);
            tb_nev.Name = "tb_nev";
            tb_nev.Size = new System.Drawing.Size(227, 44);
            tb_nev.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label4.Location = new System.Drawing.Point(656, 112);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(82, 37);
            label4.TabIndex = 2;
            label4.Text = "Név:";
            // 
            // tb_lakcim
            // 
            tb_lakcim.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_lakcim.Location = new System.Drawing.Point(906, 174);
            tb_lakcim.Name = "tb_lakcim";
            tb_lakcim.Size = new System.Drawing.Size(227, 44);
            tb_lakcim.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label5.Location = new System.Drawing.Point(656, 177);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(128, 37);
            label5.TabIndex = 4;
            label5.Text = "Lakcím:";
            // 
            // tb_taj
            // 
            tb_taj.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_taj.Location = new System.Drawing.Point(906, 242);
            tb_taj.Name = "tb_taj";
            tb_taj.Size = new System.Drawing.Size(227, 44);
            tb_taj.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label6.Location = new System.Drawing.Point(656, 245);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(170, 37);
            label6.TabIndex = 6;
            label6.Text = "TAJ szám:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label7.Location = new System.Drawing.Point(656, 314);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(94, 37);
            label7.TabIndex = 8;
            label7.Text = "Nem:";
            // 
            // tb_szuletes
            // 
            tb_szuletes.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_szuletes.Location = new System.Drawing.Point(906, 373);
            tb_szuletes.Name = "tb_szuletes";
            tb_szuletes.Size = new System.Drawing.Size(227, 44);
            tb_szuletes.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label8.Location = new System.Drawing.Point(656, 376);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(253, 37);
            label8.TabIndex = 10;
            label8.Text = "Születési dátum:";
            // 
            // tb_osztaly
            // 
            tb_osztaly.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_osztaly.Location = new System.Drawing.Point(906, 438);
            tb_osztaly.Name = "tb_osztaly";
            tb_osztaly.Size = new System.Drawing.Size(227, 44);
            tb_osztaly.TabIndex = 13;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label9.Location = new System.Drawing.Point(656, 441);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(132, 37);
            label9.TabIndex = 12;
            label9.Text = "Osztály:";
            // 
            // tb_kartya
            // 
            tb_kartya.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            tb_kartya.Location = new System.Drawing.Point(906, 504);
            tb_kartya.Name = "tb_kartya";
            tb_kartya.Size = new System.Drawing.Size(227, 44);
            tb_kartya.TabIndex = 15;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label10.Location = new System.Drawing.Point(656, 507);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(195, 37);
            label10.TabIndex = 14;
            label10.Text = "Kártya típus:";
            // 
            // rb_fiu
            // 
            rb_fiu.AutoSize = true;
            rb_fiu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            rb_fiu.Location = new System.Drawing.Point(906, 314);
            rb_fiu.Name = "rb_fiu";
            rb_fiu.Size = new System.Drawing.Size(72, 33);
            rb_fiu.TabIndex = 16;
            rb_fiu.TabStop = true;
            rb_fiu.Text = "Fiú";
            rb_fiu.UseVisualStyleBackColor = true;
            // 
            // rb_lany
            // 
            rb_lany.AutoSize = true;
            rb_lany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            rb_lany.Location = new System.Drawing.Point(984, 314);
            rb_lany.Name = "rb_lany";
            rb_lany.Size = new System.Drawing.Size(88, 33);
            rb_lany.TabIndex = 17;
            rb_lany.TabStop = true;
            rb_lany.Text = "Lány";
            rb_lany.UseVisualStyleBackColor = true;


            //Controls.Add(btn_raKeres);
            Controls.Add(rb_lany);
            Controls.Add(rb_fiu);
            Controls.Add(tb_kartya);
            Controls.Add(label10);
            Controls.Add(tb_osztaly);
            Controls.Add(label9);
            Controls.Add(tb_szuletes);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(tb_taj);
            Controls.Add(label6);
            Controls.Add(tb_lakcim);
            Controls.Add(label5);
            Controls.Add(tb_nev);
            Controls.Add(label4);
            Controls.Add(tb_om);
            Controls.Add(label3);
            InitializeComponent();
        }

        private void kereses_menu(object sender, EventArgs e)
        {

        }

        private void kilepes(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void vissza(object sender, EventArgs e)
        {
            Controls.Clear();

            alapGombok();
            InitializeComponent();
        }
    }
}