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
    public partial class adatBevitel : Form
    {
        public adatBevitel()
        {
            InitializeComponent();
        }

        private void adatBevitel_Load(object sender, EventArgs e)
        {
            Label lbl = new Label();
            lbl.Text = "Adatbevitel oldal";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lbl);

        }
    }
}
