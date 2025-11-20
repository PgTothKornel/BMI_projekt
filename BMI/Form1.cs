using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMI
{
    public partial class Menu : Form
    {
        private Panel panelTop;
        private Button btnAdatBevitel;
        private Button btnAdatKiiras;
        private Panel panelContent;
        private FlowLayoutPanel flp;

        public Menu()
        {
            InitializeComponent();

            this.panelTop = new Panel();
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.BackColor = Color.LightGray;

            this.btnAdatBevitel = new Button();
            this.btnAdatBevitel.Text = "Adatbevitel";
            this.btnAdatBevitel.AutoSize = true;
            this.btnAdatBevitel.Padding = new Padding(10);

            this.btnAdatKiiras = new Button();
            this.btnAdatKiiras.Text = "Adatkiírás";
            this.btnAdatKiiras.AutoSize = true;
            this.btnAdatKiiras.Padding = new Padding(10);

            this.flp = new FlowLayoutPanel();
            this.flp.Dock = DockStyle.Fill;
            this.flp.FlowDirection = FlowDirection.LeftToRight;
            this.flp.WrapContents = true;   
            this.flp.Padding = new Padding(10);
            this.flp.BackColor = Color.LightGray;

            this.flp.Controls.Add(this.btnAdatBevitel);
            this.flp.Controls.Add(this.btnAdatKiiras);

            this.panelTop.Controls.Add(flp);

            this.panelContent = new Panel();
            this.panelContent.Dock = DockStyle.Fill;

            Controls.Add(this.panelContent);
            Controls.Add(this.panelTop);

            this.btnAdatBevitel.Click += Betolt;
            this.btnAdatKiiras.Click += Betolt;

        }

        private void Betolt(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button == btnAdatBevitel)
            {
                adatBevitel adatBevitelForm = new adatBevitel();
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
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
