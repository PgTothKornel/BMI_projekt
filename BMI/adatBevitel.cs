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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace BMI{
    public partial class adatBevitel : Form{

        public adatBevitel()
        {
            InitializeComponent();
            alapGombok();

            try
            {
                string databaseSql = File.ReadAllText("database.sql");
                string dataConnectionString = "Server=localhost;Database=BMI_Projekt;User ID=root;Password=mysql;";
                
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
            System.Windows.Forms.Button btn_exit = new System.Windows.Forms.Button();
            System.Windows.Forms.Button btn_vissza = new System.Windows.Forms.Button();

            btn_exit = new System.Windows.Forms.Button();
            btn_vissza = new System.Windows.Forms.Button();
            // 
            // btn_exit
            // 
            btn_exit.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_exit.BackColor = System.Drawing.Color.Red;
            btn_exit.Location = new System.Drawing.Point(700, 400);
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
            btn_vissza.Dock = System.Windows.Forms.DockStyle.Fill;

            tableLayoutPanel1.Controls.Add(btn_exit, 4 , 8);
            tableLayoutPanel1.Controls.Add(btn_vissza, 3, 8);
            input_generalas();
        }

        public System.Windows.Forms.Button btn_hozzaad = new System.Windows.Forms.Button();

        private void input_generalas()
        {
            System.Windows.Forms.Button btn_kereses = new System.Windows.Forms.Button();
            //System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox textBox1 = new System.Windows.Forms.TextBox();
            //System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
            // 
            // btn_hozzaad
            // 
            btn_hozzaad.Dock = System.Windows.Forms.DockStyle.Fill;
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
            btn_kereses.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_kereses.Click += new System.EventHandler(kereses_menu);
            // 
            // label1
            // 
            label_hozzaad("Egy létező személyhez új adat:", 0, 3);
            // 
            // textBox1
            // 
            textbox_hozzaad("keresesMezo",1,3, false);
            // 
            // label2
            // 
            /*
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label2.Location = new System.Drawing.Point(12, 589);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(298, 37);
            label2.TabIndex = 0;
            label2.Text = "Létezik ilyen ember!";
            label2.Visible = false;*/
            /*
            tableLayoutPanel1.Controls.Add(textBox1, 1, 3);
            tableLayoutPanel1.Controls.Add(label1, 0 , 3);*/
            tableLayoutPanel1.Controls.Add(btn_kereses, 1, 4);
            tableLayoutPanel1.Controls.Add(btn_hozzaad, 0, 0);
        }
        
        public System.Windows.Forms.Button button = new System.Windows.Forms.Button();
        System.Windows.Forms.ComboBox comboBox = new System.Windows.Forms.ComboBox();

        private void hozzaad_menu(object sender, EventArgs e)
        {
            btn_hozzaad.Enabled = false;
            //MessageBox.Show("teszt");
            
            label_hozzaad("OM azonosító:", 3, 0);
            textbox_hozzaad("0om", 4, 0, true);

            label_hozzaad("Név:", 3, 1);
            textbox_hozzaad("1nev", 4, 1, true);

            label_hozzaad("Lakcím:", 3, 2);
            textbox_hozzaad("2lakcim", 4, 2, true);

            label_hozzaad("Taj szám:", 3, 3);
            textbox_hozzaad("3taj", 4, 3, true);

            label_hozzaad("Nem:", 3, 4);
            comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(new object[] {
            "Férfi",
            "Nő",
            "Nem kívánom megadni"});
            comboBox.Name = "comboBox";
            tableLayoutPanel1.Controls.Add(comboBox,4,4);
            
            label_hozzaad("Születési dátum:", 3, 5);
            textbox_hozzaad("5szuletes", 4, 5, true);

            label_hozzaad("Osztály", 3, 6);
            textbox_hozzaad("6osztaly", 4, 6, true);

            label_hozzaad("Kártya Típus", 3, 7);
            textbox_hozzaad("7kartya", 4, 7, true);

            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            button.Location = new System.Drawing.Point(18, 158);
            button.Name = "btn_add";
            button.Size = new System.Drawing.Size(322, 140);
            button.TabIndex = 0;
            button.Text = "Hozzáadás";
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(hozzaad);
            button.Enabled = false;

            tableLayoutPanel1.Controls.Add(button, 2, 8);
        }
        private void hozzaad(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex != -1) 
            {
                argumentumok[4] = (string)comboBox.SelectedItem;
            }
            else {
                MessageBox.Show("Kérem válasszon ki egy nemet!");
                return; }
            try
            {
                Random random = new Random();
                int tmp = random.Next(1, 5);
                string output = string.Empty;
                output += random.Next(0, 10);
                output += random.Next(0, 10);
                output += random.Next(0, 10);

                foreach (var item in argumentumok[1])
                {
                    output += cipher(item, tmp);
                }
                //MessageBox.Show(output);

                string dataConnectionString = "Server=localhost;Database=BMI_Projekt;User ID=root;Password=mysql;";
                using (MySqlConnection connection = new MySqlConnection(dataConnectionString))
                {
                    connection.Open();

                    string prompt = $"INSERT INTO `kartya` (`UID`, `kartyaTipus`) VALUES ('{output}', '{argumentumok[7]}');";

                    using (var cmd = new MySqlCommand(prompt, connection))
                    { 
                        cmd.ExecuteNonQuery();
                    }

                    string query = @"
                        INSERT INTO szemelyek
                        (OM, nev, lakcim, TAJ, nem, szuletesiDatum, osztaly, kartya)
                        VALUES (@c1, @c2, @c3, @c4, @c5, @c6, @c7, @c8)
                        ";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@c1", argumentumok[0]);
                        cmd.Parameters.AddWithValue("@c2", argumentumok[1]);
                        cmd.Parameters.AddWithValue("@c3", argumentumok[2]);
                        cmd.Parameters.AddWithValue("@c4", argumentumok[3]);
                        cmd.Parameters.AddWithValue("@c5", argumentumok[4]);
                        cmd.Parameters.AddWithValue("@c6", argumentumok[5]);
                        cmd.Parameters.AddWithValue("@c7", argumentumok[6]);
                        cmd.Parameters.AddWithValue("@c8", output);

                        //MessageBox.Show(cmd.CommandText);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az adatbevitel alatt!\n" + ex.ToString().Split('\n')[0]);
            }
        }

        private void label_hozzaad(string nev, int x, int y)
        {
            System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
            label3.AutoSize = false;
            label3.Dock = DockStyle.Fill;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Visible = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            label3.Name = nev.Replace(" ", "_");
            label3.TabIndex = 0;
            label3.Text = nev;
            

            tableLayoutPanel1.Controls.Add(label3, x, y);
        }
        private void textbox_hozzaad(string nev, int x, int y, bool fontos)
        {
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            textBox.AutoSize = false;
            textBox.Dock = DockStyle.Fill;
            textBox.Visible = true;
            textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            textBox.Name = nev.Replace(" ", "_");
            textBox.TabIndex = 0;
            if (fontos)
            {
                textBox.TextChanged += new System.EventHandler(szamlalo);
            }

            tableLayoutPanel1.Controls.Add(textBox, x, y);
        }


        public int count = 0;

        public HashSet<string> textBoxes = new HashSet<string>();
        public string[] argumentumok = new string[8];

        private void szamlalo(object sender, EventArgs e)
        {
            var textBox = sender as System.Windows.Forms.TextBox;
            
            if (textBox.Text.Length > 1)
            {
                //MessageBox.Show(textBox.Text.Length + " " + count);
                if (!textBoxes.Contains(textBox.Name)) { 
                textBoxes.Add(textBox.Name);
                count++;
                }
                argumentumok[Convert.ToInt16(textBox.Name[0].ToString())] = textBox.Text;
            }
            else if (textBox.Text.Length <= 0 && textBoxes.Contains(textBox.Name))
            {
                textBoxes.Remove(textBox.Name);
                count--;
            }
            if (count == 7)
            {
                button.Enabled = true;
            }
            else { button.Enabled = false; }
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
            tableLayoutPanel1.Controls.Clear();
            btn_hozzaad.Enabled = true;
            alapGombok();
            //InitializeComponent();
        }

        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);

            //
            // Caesar kódolás a kártya UID-k-hoz, lusta vagyok megcsinálni a hexadecimálisat
            //
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (comboBox1.SelectedIndex == 0)
            {
                alapGombok();
                hozzaad_menu();
            }
            */
        }
    }
}