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
    public partial class adatKiiras : Form
    {
        public adatKiiras()
        {
            InitializeComponent();
        }

        private void adatKiiras_Load(object sender, EventArgs e)
        {
            Label lbl = new Label();
            lbl.Text = "Adatkiírási oldal";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lbl);
        }
    }
}
