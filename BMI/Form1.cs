using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMI;
namespace BMI{
    public partial class Menu : Form{
        private Panel panelTop;
        private Button btnAdatBevitel;
        private Button btnAdatKiiras;
        public Panel panelContent;
        public Menu(){
            InitializeComponent();
            panelTop = new Panel();
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 58;
            panelTop.BackColor = Color.FromArgb(22, 196 ,127);

            btnAdatBevitel = new Button();
            btnAdatBevitel.Text = "Adatbevitel";
            btnAdatBevitel.AutoSize = true;
            btnAdatBevitel.Padding = new Padding(10);
            btnAdatBevitel.Location = new Point(10, 10);
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

            btnAdatBevitel.Click += new EventHandler(Betolt);
            btnAdatKiiras.Click += new EventHandler(Betolt);

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
            else{
                MessageBox.Show("Ismeretlen gomb!");
            }
        }
        private void Menu_Load(object sender, EventArgs e){
        }
    }
}
