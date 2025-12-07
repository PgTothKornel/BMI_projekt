using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace BMI
{
    public partial class Form1 : Form{
        private Panel panelTop;
        private Button btnAdatBevitel;
        private Button btnAdatKiiras;
        private Button menu;
        public Panel panelContent;
        public string dataConnectionString = "Server=localhost;Database=BMI_PROJEKT;User ID=root;Password=mysql;";
        private Label menuSzoveg1;
        private Label menuSzoveg2;
        public Form1(){
            InitializeComponent();
            Text = "Egészségügyi Méréskezelő Rendszer";
            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 58;
            panelTop.BackColor = Color.FromArgb(22, 196 ,127);

            menu = new Button();
            menu.Location = new Point(10, 10);
            menu.Text = "Menü";
            menu.AutoSize = true;
            menu.Padding = new Padding(10);
            menu.FlatStyle = FlatStyle.Flat;
            panelTop.Controls.Add(menu);

            btnAdatBevitel = new Button();
            btnAdatBevitel.Text = "Adatbevitel";
            btnAdatBevitel.AutoSize = true;
            btnAdatBevitel.Padding = new Padding(10);
            btnAdatBevitel.Location = new Point(menu.Right +10, menu.Top);
            btnAdatBevitel.FlatStyle = FlatStyle.Flat;
            panelTop.Controls.Add(btnAdatBevitel);

            btnAdatKiiras = new Button();
            btnAdatKiiras.Location = new Point(btnAdatBevitel.Right + 10, btnAdatBevitel.Top);
            btnAdatKiiras.Text = "Adatkiírás";
            btnAdatKiiras.AutoSize = true;
            btnAdatKiiras.Padding = new Padding(10);
            btnAdatKiiras.FlatStyle = FlatStyle.Flat;
            panelTop.Controls.Add(btnAdatKiiras);

            panelContent = new Panel();
            panelContent.Dock = DockStyle.Fill;

            Controls.Add(panelContent);
            Controls.Add(panelTop);

            menuSzoveg1 = new Label();
            menuSzoveg1.Text = "Üdvözlöm az Egészségügyi Méréskezelő Rendszerben!";
            menuSzoveg1.AutoSize = true;
            menuSzoveg1.Font = new Font("Arial", 24, FontStyle.Bold);
            menuSzoveg1.Location = new Point(panelTop.Location.X, 30);
            panelContent.Controls.Add(menuSzoveg1);
            
            menuSzoveg2 = new Label();
            menuSzoveg2.Text = "Kérem válasszon fent a menüpontok közül.";
            menuSzoveg2.AutoSize = true;
            menuSzoveg2.Font = new Font("Arial", 19);
            menuSzoveg2.Location = new Point(menuSzoveg1.Location.X, menuSzoveg1.Location.Y + 50);
            panelContent.Controls.Add(menuSzoveg2);


            btnAdatBevitel.Click += new EventHandler(Betolt);
            btnAdatKiiras.Click += new EventHandler(Betolt);
            menu.Click += new EventHandler(Betolt);
            this.MinimumSize = new Size(1100, 630);
            InitializeDatabase();
        }
        private void Betolt(object sender, EventArgs e){
            Button button = sender as Button;

            if (button == btnAdatBevitel)
            {
                BMI.adatBevitel adatBevitelForm = new adatBevitel();
                adatBevitelForm.TopLevel = false;
                panelContent.Controls.Clear();
                panelContent.Controls.Add(adatBevitelForm);
                adatBevitelForm.FormBorderStyle = FormBorderStyle.None;
                adatBevitelForm.Dock = DockStyle.Fill;
                adatBevitelForm.Show();
            }
            else if (button == btnAdatKiiras)
            {
                adatKiiras adatKiirasForm = new adatKiiras();
                adatKiirasForm.TopLevel = false;
                panelContent.Controls.Clear();
                panelContent.Controls.Add(adatKiirasForm);
                adatKiirasForm.FormBorderStyle = FormBorderStyle.None;
                adatKiirasForm.Dock = DockStyle.Fill;
                adatKiirasForm.Show();
            }
            else if (button == menu)
            {
                panelContent.Controls.Clear();
                panelContent.Controls.Add(menuSzoveg1);
                panelContent.Controls.Add(menuSzoveg2);
            }
            else
            {
                MessageBox.Show("Ismeretlen gomb!");
            }
        }
        private void Form1_Load(object sender, EventArgs e){
        }
        private void InitializeDatabase()
        {
            try
            {
                string masterConnectionString = "Server=localhost;User ID=root;Password=mysql;";
                string kezdoFoglalasokSqlContent = File.ReadAllText("adatok.sql");
                string databaseSql = File.ReadAllText("database.sql");
                using (MySqlConnection masterConnection = new MySqlConnection(masterConnectionString))
                {
                    masterConnection.Open();
                    string setupScript = "DROP DATABASE IF EXISTS BMI_PROJEKT; CREATE DATABASE IF NOT EXISTS BMI_PROJEKT;";
                    ExecuteSqlScript(setupScript, masterConnection);
                }
                using (MySqlConnection connection = new MySqlConnection(dataConnectionString))
                {
                    connection.Open();
                    ExecuteSqlScript(databaseSql, connection);
                    ExecuteSqlScript(kezdoFoglalasokSqlContent, connection);
                }
            }
            catch (Exception e)
            {
               adatKiiras.hibaUzenet("Adatbázis inicializálási hiba!", e);
                Environment.Exit(1);
            }
        }
        private void ExecuteSqlScript(string sqlScript, MySqlConnection connection)
        {
            string[] commands = sqlScript.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var commandText in commands)
            {
                string trimmedCommand = commandText.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedCommand))
                {
                    using (MySqlCommand command = new MySqlCommand(trimmedCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
