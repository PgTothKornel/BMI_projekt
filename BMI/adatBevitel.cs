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
using Google.Protobuf.WellKnownTypes;
using K4os.Compression.LZ4.Internal;
using Mysqlx.Crud;

namespace BMI{
    public partial class adatBevitel : Form{

        public adatBevitel()
        {
            InitializeComponent();
            input_generalas();

            comboBox.TextChanged += new System.EventHandler(text_nullazas);

            /*
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
            */
        }

        private void text_nullazas(object sender, EventArgs e)
        {
            comboBox.Text = "";
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
            btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            btn_exit.BackColor = System.Drawing.Color.Crimson;
            btn_exit.Location = new System.Drawing.Point(700, 400);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new System.Drawing.Size(91, 38);
            btn_exit.TabIndex = 0;
            btn_exit.Text = "Kilépés";
            btn_exit.UseVisualStyleBackColor = false;
            btn_exit.Click += new System.EventHandler(kilepes);
            btn_exit.FlatStyle = FlatStyle.Flat;
            btn_exit.Cursor = Cursors.No;
            // 
            // btn_vissza
            // 
            btn_vissza.Location = new System.Drawing.Point(1000, 642);
            btn_vissza.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            btn_vissza.Name = "btn_vissza";
            btn_vissza.Size = new System.Drawing.Size(91, 38);
            btn_vissza.TabIndex = 1;
            btn_vissza.Text = "Vissza";
            btn_vissza.UseVisualStyleBackColor = true;
            btn_vissza.Click += new System.EventHandler(vissza);
            btn_vissza.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_vissza.FlatStyle = FlatStyle.Flat;
            btn_vissza.Cursor = Cursors.Cross;

            tableLayoutPanel1.Controls.Add(btn_exit, 0 , 8);
            tableLayoutPanel1.Controls.Add(btn_vissza, 1, 8);

            alapgombokAktivak = 1;

            input_generalas();
        }

        public System.Windows.Forms.Button btn_hozzaad = new System.Windows.Forms.Button();
        public System.Windows.Forms.Button btn_kereses = new System.Windows.Forms.Button();
        public System.Windows.Forms.Button btn_meres = new System.Windows.Forms.Button();
        private void input_generalas()
        {
            
            //System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox textBox1 = new System.Windows.Forms.TextBox();
            //System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
            // 
            // btn_hozzaad
            // 
            btn_hozzaad.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_hozzaad.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            btn_hozzaad.Name = "btn_hozzaad";
            btn_hozzaad.TabIndex = 0;
            btn_hozzaad.Text = "Új ember hozzáadása";
            btn_hozzaad.UseVisualStyleBackColor = true;
            btn_hozzaad.Click += new System.EventHandler(hozzaad_menu);
            btn_hozzaad.BackColor = Color.FromArgb(52, 58, 64);
            btn_hozzaad.FlatStyle = FlatStyle.Flat;
            btn_hozzaad.ForeColor = Color.White;
            btn_hozzaad.FlatAppearance.BorderSize = 0;
            btn_hozzaad.Cursor = Cursors.Hand;

            // 
            // btn_kereses
            // 
            btn_kereses.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            btn_kereses.Name = "btn_kereses";
            btn_kereses.TabIndex = 1;
            btn_kereses.Text = "Adat módosítás egy embernél";
            btn_kereses.UseVisualStyleBackColor = true;
            btn_kereses.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_kereses.Click += new System.EventHandler(kereses_menu);
            btn_kereses.BackColor = Color.FromArgb(52, 58, 64);
            btn_kereses.FlatStyle = FlatStyle.Flat;
            btn_kereses.ForeColor = Color.White;
            btn_kereses.FlatAppearance.BorderSize = 0;
            btn_kereses.Cursor = Cursors.Hand;

            //
            // btn_meres
            //
            btn_meres.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            btn_meres.FlatStyle = FlatStyle.Flat;
            btn_meres.Name = "btn_kereses";
            btn_meres.TabIndex = 1;
            btn_meres.Text = "Új mérés rögzítése";
            btn_meres.UseVisualStyleBackColor = true;
            btn_meres.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_meres.Click += new System.EventHandler(meres_menu);
            btn_meres.BackColor = Color.FromArgb(52, 58, 64);
            btn_meres.ForeColor = Color.White;
            btn_meres.FlatAppearance.BorderSize = 0;
            btn_meres.Cursor = Cursors.Hand;

            tableLayoutPanel3.Controls.Add(btn_kereses, 0, 1);
            tableLayoutPanel3.Controls.Add(btn_meres, 0, 2);
            tableLayoutPanel3.Controls.Add(btn_hozzaad, 0, 0);
        }
        
        public System.Windows.Forms.Button button = new System.Windows.Forms.Button();
        System.Windows.Forms.ComboBox comboBox = new System.Windows.Forms.ComboBox();
         
        public int current = 0;
        public int alapgombokAktivak = 0;
        private void hozzaad_menu(object sender, EventArgs e)
        {
            comboBox.SelectedIndex = -1;
            if (current == 1)
            {
                return;
            }
            else
            {
                count = 0;
                textBoxes.Clear();
            }
            
            tableLayoutPanel1.Controls.Clear();
            
            alapGombok();


            current = 1;
            //btn_hozzaad.Enabled = false;
            //MessageBox.Show("teszt");
            
            label_hozzaad("OM azonosító:", 1, 0);
            textbox_hozzaad("0om", 2, 0, true);

            label_hozzaad("Név:", 1, 1);
            textbox_hozzaad("1nev", 2, 1, true);

            label_hozzaad("Lakcím:", 1, 2);
            textbox_hozzaad("2lakcim", 2, 2, true);

            label_hozzaad("Taj szám:", 1, 3);
            textbox_hozzaad("3taj", 2, 3, true);

            label_hozzaad("Nem:", 1, 4);
            comboBox.Items.Clear();
            comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 23.25F);
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(new object[] {
            "Férfi",
            "Nő"});
            comboBox.Name = "comboBox";
            tableLayoutPanel1.Controls.Add(comboBox,2,4);
            
            label_hozzaad("Születési dátum:", 1, 5);
            textbox_hozzaad("5szuletes", 2, 5, true);

            label_hozzaad("Osztály", 1, 6);
            textbox_hozzaad("6osztaly", 2, 6, true);

            label_hozzaad("Kártya Típus", 1, 7);
            textbox_hozzaad("7kartya", 2, 7, true);

            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            button.Name = "btn_add";
            button.Text = "Hozzáadás";
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.Black;
            button.Cursor = Cursors.Hand;
            button.Click += new System.EventHandler(hozzaad);
            button.Click -= new System.EventHandler(mer);
            button.Click -= new System.EventHandler(modosit);
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

                    string check = $"SELECT * FROM szemelyek WHERE OM = {argumentumok[0]};";

                    using (var cmd = new MySqlCommand(check, connection))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            MessageBox.Show("Már létezik ilyen OM azonosítójú diák!");

                            return;
                        }
                        reader.Close();
                    }

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

                    MessageBox.Show("A személy sikeresen hozzá lett adva!");
                }

                mentes();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Duplicate entry"))
                {
                    MessageBox.Show("Már létezik ilyen OM azonosítójú diák!");
                }
                else { 
                MessageBox.Show("Hiba az adatbevitel alatt!\n" + ex.ToString().Split('\n')[0]);
                }
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
            textBox.BorderStyle = BorderStyle.FixedSingle;

            if (fontos)
            {
                textBox.TextChanged += new System.EventHandler(szamlalo);
            }
            if (x > 10)
            {
                x -= 10;
                textBox.TextChanged += new System.EventHandler(max);
            }

            tableLayoutPanel1.Controls.Add(textBox, x, y);
        }

        private void max(object sender, EventArgs e)
        {
            var textBox = sender as System.Windows.Forms.TextBox;
            try
            {
                if (Convert.ToInt16(textBox.Text) <= 0)
                {
                    textBox.Text = "";
                }
                /*else if (Convert.ToInt16(textBox.Text) > 99 && textBox.Name == "1testzsir")
                {
                    textBox.Text = "";
                    MessageBox.Show("A test-zsír százalék nem lehet 100, vagy annál több!");
                }*/
                else if (Convert.ToInt16(textBox.Text) >= 400 && textBox.Name == "2magassag")
                {
                    textBox.Text = "";
                    MessageBox.Show("A magasság nem lehet 400cm, vagy annál több!");
                }
                else if (Convert.ToInt16(textBox.Text) >= 260 && textBox.Name == "3suly")
                {
                    textBox.Text = "";
                    MessageBox.Show("A súly nem lehet 260kg, vagy annál több!");
                }
            }
            catch { 
                textBox.Text = "";
            }
        }
        public int count = 0;

        public HashSet<string> textBoxes = new HashSet<string>();
        public string[] argumentumok = new string[8];

        private void szamlalo(object sender, EventArgs e)
        {
            var textBox = sender as System.Windows.Forms.TextBox;
            
            if (textBox.Text.Length > 0)
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
            switch (current)
            {
                case 1:
                    if (count == 7) button.Enabled = true;
                    else button.Enabled = false;
                    break;
                case 2:
                    if (count == 6) button.Enabled = true;
                    else button.Enabled = false;
                    break;
                case 3:
                    if (count == 3) button.Enabled = true;
                    else button.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void kereses_menu(object sender, EventArgs e)
        {
            if (current == 2)
            {
                return;
            }
            else
            {
                count = 0;
                textBoxes.Clear();
            }
            tableLayoutPanel1.Controls.Clear();
            comboBox.SelectedIndex = -1;

            alapGombok();

            current = 2;


            label_hozzaad("Megváltoztatandó személy OM azonosítója:", 1, 0);
            textbox_hozzaad("0om", 2, 0, true);

            label_hozzaad("Név:", 1, 1);
            textbox_hozzaad("1nev", 2, 1, true);

            label_hozzaad("Lakcím:", 1, 2);
            textbox_hozzaad("2lakcim", 2, 2, true);

            label_hozzaad("Taj szám:", 1, 3);
            textbox_hozzaad("3taj", 2, 3, true);

            label_hozzaad("Nem:", 1, 4);
            comboBox.Items.Clear();
            comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 23.25F);
            comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(new object[] {
            "Férfi",
            "Nő"});
            comboBox.Name = "comboBox";
            tableLayoutPanel1.Controls.Add(comboBox, 2, 4);

            label_hozzaad("Születési dátum:", 1, 5);
            textbox_hozzaad("5szuletes", 2, 5, true);

            label_hozzaad("Osztály", 1, 6);
            textbox_hozzaad("6osztaly", 2, 6, true);

            /*
            label_hozzaad("Kártya Típus", 1, 7);
            textbox_hozzaad("7kartya", 2, 7, true);
            */

            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            button.Name = "btn_modify";
            button.Text = "Módosítás";
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.Black;
            button.Cursor = Cursors.Hand;
            button.Click += new System.EventHandler(modosit);
            button.Click -= new System.EventHandler(hozzaad);
            button.Click -= new System.EventHandler(mer);
            button.Enabled = false;

            tableLayoutPanel1.Controls.Add(button, 2, 8);
        }
        private void modosit(object sender, EventArgs e)
        {
            
            if (comboBox.SelectedIndex != -1)
            {
                argumentumok[4] = (string)comboBox.SelectedItem;
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki egy nemet!");
                return;
            }
            try
            {
                /*
                Random random = new Random();
                int tmp = random.Next(1, 5);
                string output = string.Empty;
                output += random.Next(0, 10);
                output += random.Next(0, 10);
                output += random.Next(0, 10);

                foreach (var item in argumentumok[1])
                {
                    output += cipher(item, tmp);
                }*/
                //MessageBox.Show(output);

                string dataConnectionString = "Server=localhost;Database=BMI_Projekt;User ID=root;Password=mysql;";
                using (MySqlConnection connection = new MySqlConnection(dataConnectionString))
                {
                    connection.Open();

                    string check = $"SELECT szemelyek.OM FROM szemelyek WHERE szemelyek.OM = {argumentumok[0]};";

                    using (var cmd = new MySqlCommand(check, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Nem létezik ilyen OM azonosítóval diák!");
                            return;
                        }

                        //string van = reader.GetString(0);
                        /*
                        MessageBox.Show(argumentumok[0]);
                        MessageBox.Show(argumentumok[1]);
                        MessageBox.Show(argumentumok[2]);
                        MessageBox.Show(argumentumok[3]);
                        MessageBox.Show(argumentumok[4]);
                        */
                    }

                    MessageBox.Show("Személy adatai sikeresen módosítva!");

                    string query = @"
                        UPDATE szemelyek
                        SET
                            nev = @c2,
                            lakcim = @c3,
                            TAJ = @c4,
                            nem = @c5,
                            szuletesiDatum = @c6,
                            osztaly = @c7
                        WHERE OM = @c1;
                        ";
                    /*
                    string prompt = $"INSERT INTO `kartya` (`UID`, `kartyaTipus`) VALUES ('{output}', '{argumentumok[7]}');";

                    using (var cmd = new MySqlCommand(prompt, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }*/

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@c1", argumentumok[0]);
                        cmd.Parameters.AddWithValue("@c2", argumentumok[1]);
                        cmd.Parameters.AddWithValue("@c3", argumentumok[2]);
                        cmd.Parameters.AddWithValue("@c4", argumentumok[3]);
                        cmd.Parameters.AddWithValue("@c5", argumentumok[4]);
                        cmd.Parameters.AddWithValue("@c6", argumentumok[5]);
                        cmd.Parameters.AddWithValue("@c7", argumentumok[6]);
                        //cmd.Parameters.AddWithValue("@c8", output);

                        //MessageBox.Show(cmd.CommandText);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az adatmódosítás alatt!\n" + ex.ToString().Split('\n')[0]);
            }

            mentes();
        }

        private void meres_menu(object sender, EventArgs e)
        {
            if (current == 3)
            {
                return;
            }
            else
            {
                count = 0;
                textBoxes.Clear();
            }

            tableLayoutPanel1.Controls.Clear();

            alapGombok();


            current = 3;
            //MessageBox.Show("teszt");

            label_hozzaad("A személy OM azonosítója:", 1, 0);
            textbox_hozzaad("0om", 2, 0, true);


            label_hozzaad("A személy magassága (cm):", 1, 1);
            textbox_hozzaad("2magassag", 12, 1, true);

            label_hozzaad("A személy súlya (kg):", 1, 2);
            textbox_hozzaad("3suly", 12, 2, true);

            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            button.Name = "btn_mer";
            button.Text = "Rögzítés";
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Color.Black;
            button.Cursor = Cursors.Hand;
            button.Click += new System.EventHandler(mer);
            button.Click -= new System.EventHandler(hozzaad);
            button.Click -= new System.EventHandler(modosit);
            button.Enabled = false;

            tableLayoutPanel1.Controls.Add(button, 2, 8);
        }

        private void mer(object sender, EventArgs e)
        {
            try
            {

                string dataConnectionString = "Server=localhost;Database=BMI_Projekt;User ID=root;Password=mysql;";
                using (MySqlConnection connection = new MySqlConnection(dataConnectionString))
                {
                    connection.Open();

                    string check = $"SELECT szemelyek.OM FROM szemelyek WHERE szemelyek.OM = {argumentumok[0]};";

                    using (var cmd = new MySqlCommand(check, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Nem létezik ilyen OM azonosítóval diák!");
                            return;
                        }
                    }
                    if (int.Parse(argumentumok[2]) < 60) { 
                        MessageBox.Show("A magasság nem lehet kevesebb, mint 60cm!","Magasság hiba");
                        return;
                    }
                    string query = @"
                         INSERT INTO meresek
                         (szemely, magassag, suly, datum)
                         VALUES (@c2, @c4, @c5, @c6)
                        ";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@c2", argumentumok[0]);
                        cmd.Parameters.AddWithValue("@c3", argumentumok[1]);
                        cmd.Parameters.AddWithValue("@c4", argumentumok[2]);
                        cmd.Parameters.AddWithValue("@c5", argumentumok[3]);
                        cmd.Parameters.AddWithValue("@c6", DateTime.Now);
                    

                        //MessageBox.Show(cmd.CommandText);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                    MessageBox.Show("Mérés sikeresen rögzítve!","Siker");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a mérés rögzítése alatt!\n" + ex.ToString().Split('\n')[0]);
            }
            mentes();
        }

        private void kilepes(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void vissza(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            current = 0;
            alapgombokAktivak = 0;
            
            //alapGombok();
            //InitializeComponent();
        }

        private void mentes()
        {
            string dataConnectionString = "Server=localhost;Database=BMI_Projekt;User ID=root;Password=mysql;";

            StreamWriter sw = new StreamWriter("adatok.sql", false);
            
            sw.WriteLine("DELETE FROM meresek;");
            sw.WriteLine("DELETE FROM szemelyek;");
            sw.WriteLine("DELETE FROM kartya;");
            sw.WriteLine();
            sw.WriteLine("INSERT INTO kartya (`UID`, `kartyaTipus`) VALUES");
            using (MySqlConnection connection = new MySqlConnection(dataConnectionString))
            {
                connection.Open();

                string check = $"SELECT * FROM kartya;";

                using (var cmd = new MySqlCommand(check, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    int comma = 0;
                    while (reader.Read())
                    {
                        string line = $"{(comma == 1 ? "," : "")}(\'{reader.GetValue(0).ToString()}\', \'{reader.GetValue(1).ToString()}\')";
                        comma = 1;    
                        sw.WriteLine(line);
                    }
                    sw.WriteLine(";\n");
                    reader.Close();
                }
                
                
                sw.WriteLine("INSERT INTO szemelyek(`OM`, `nev`, `lakcim`, `TAJ`, `nem`, `szuletesiDatum`, `osztaly`, `kartya`) VALUES");

                check = $"SELECT * FROM szemelyek;";

                using (var cmd = new MySqlCommand(check, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    int comma = 0;
                    while (reader.Read())
                    {
                        string date = reader.GetValue(5).ToString().Replace(". ", "-");
                        //MessageBox.Show(date);
                        string line = $"{(comma == 1 ? "," : "")}(\'{reader.GetValue(0).ToString()}\', \'{reader.GetValue(1).ToString()}\', \'{reader.GetValue(2).ToString()}\', \'{reader.GetValue(3).ToString()}\', \'{reader.GetValue(4).ToString()}\', \'{date}\', \'{reader.GetValue(6).ToString()}\', \'{reader.GetValue(7).ToString()}\')";
                        comma = 1;
                        sw.WriteLine(line);
                    }
                    sw.WriteLine(";\n");
                    reader.Close();
                }


                sw.WriteLine("INSERT INTO meresek (`szemely`, `magassag`, `suly`, `datum`) VALUES");

                check = $"SELECT * FROM meresek;";

                using (var cmd = new MySqlCommand(check, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    int comma = 0;
                    while (reader.Read())
                    {
                        string date = reader.GetValue(4).ToString().Replace(". ", "-");
                        //MessageBox.Show(date);
                        string line = $"{(comma == 1 ? "," : "")}(\'{reader.GetValue(1).ToString()}\', \'{reader.GetValue(2).ToString()}\', \'{reader.GetValue(3).ToString()}\', \'{date}\')";
                        comma = 1;
                        sw.WriteLine(line);
                    }
                    sw.WriteLine(";\n");
                    reader.Close();
                }

                connection.Close();
                sw.Close();
            }
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
    }
}