using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMI
{
    internal class metodusok{
        public void hibaUzenet(string uzenet, Exception ex)
        {
            string hibaszoveg = "";
            if (ex != null)
                hibaszoveg = ex.Message;
            else
                hibaszoveg = "Ismeretlen hiba.";

            MessageBox.Show(
                uzenet + "\n\nHiba részletei:\n" + hibaszoveg,
                "Hiba történt",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            try
            {
                string logSor =
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                    " - " + uzenet +
                    " - " + hibaszoveg + "\n";

                System.IO.File.AppendAllText("errors.log", logSor);
            }
            catch
            {
            }
        }
    }
}
