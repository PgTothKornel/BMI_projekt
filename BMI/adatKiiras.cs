using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace BMI{
    public class Szemely{
        public long OM;
        public string Nev;
        public string Lakcim;
        public string TAJ;
        public string Nem;
        public DateTime SzuletesiDatum;
        public long Osztaly;
        public string KartyaUID;
    }
    public class Meres{
        public long Id;
        public long SzemelyOM;
        public double TestzsirSzazalek;
        public double MagassagCm;
        public double SulyKg;
        public DateTime Datum;
    }
    public class KartyaRecord
    {
        public string UID;
        public string Tipus;
        public long OM;
        public string Nev;
        public long Osztaly;
        public string Nem;
    }

    public partial class adatKiiras : Form{
        #region változók
        private static string connectionString = "Server=localhost;Database=BMI_PROJEKT;User ID=root;Password=mysql;";

        private DataGridView dataGridView1;
        private TextBox txtUIDInput;
        private Panel content;
        private Panel sidebar;
        private Label title;
        private Panel card;
        private Button btnSzemelyek;
        private Button btnMeresek;
        private Button btnKartyak;
        private Button btnPdf;
        private ComboBox cmbFilterBy;

        #endregion
        public adatKiiras()
        {
            InitializeComponent();

            content = new Panel();
            content.Dock = DockStyle.Fill;
            content.BackColor = Color.FromArgb(248, 249, 250);
            content.Padding = new Padding(20);
            this.Controls.Add(content);

            sidebar = new Panel();
            sidebar.Width = 220;
            sidebar.Dock = DockStyle.Left;
            sidebar.BackColor = Color.FromArgb(33, 37, 41);
            sidebar.Padding = new Padding(10);
            this.Controls.Add(sidebar);

            btnSzemelyek = new Button();
            btnSzemelyek.Text = "Személyek";
            btnSzemelyek.Height = 50;
            btnSzemelyek.Dock = DockStyle.Top;
            btnSzemelyek.FlatStyle = FlatStyle.Flat;
            btnSzemelyek.FlatAppearance.BorderSize = 0;
            btnSzemelyek.ForeColor = Color.White;
            btnSzemelyek.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnSzemelyek.BackColor = Color.FromArgb(52, 58, 64);
            btnSzemelyek.Click += btnSzemelyek_Click;
            sidebar.Controls.Add(btnSzemelyek);

            btnMeresek = new Button();
            btnMeresek.Text = "Mérések";
            btnMeresek.Height = 50;
            btnMeresek.Dock = DockStyle.Top;
            btnMeresek.FlatStyle = FlatStyle.Flat;
            btnMeresek.FlatAppearance.BorderSize = 0;
            btnMeresek.ForeColor = Color.White;
            btnMeresek.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnMeresek.BackColor = Color.FromArgb(52, 58, 64);
            btnMeresek.Click += btnMeresek_Click;
            sidebar.Controls.Add(btnMeresek);

            btnKartyak = new Button();
            btnKartyak.Text = "Kártyák";
            btnKartyak.Height = 50;
            btnKartyak.Dock = DockStyle.Top;
            btnKartyak.FlatStyle = FlatStyle.Flat;
            btnKartyak.FlatAppearance.BorderSize = 0;
            btnKartyak.ForeColor = Color.White;
            btnKartyak.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnKartyak.BackColor = Color.FromArgb(52, 58, 64);
            btnKartyak.Click += btnKartyak_Click;
            sidebar.Controls.Add(btnKartyak);

            title = new Label();
            title.Text = "Egészségügyi Méréskezelő Rendszer";
            title.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            title.AutoSize = true;
            content.Controls.Add(title);

            card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(500, 90);
            card.Top = 50;
            card.Left = 0;
            card.Padding = new Padding(10);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            content.Controls.Add(card);

            Label lblUID = new Label();
            lblUID.Text = "Keresés:";
            lblUID.Font = new System.Drawing.Font("Segoe UI", 11);
            lblUID.AutoSize = true;
            lblUID.Location = new Point(10, 10);
            card.Controls.Add(lblUID);

            txtUIDInput = new TextBox();
            txtUIDInput.Width = 230;
            txtUIDInput.Font = new System.Drawing.Font("Segoe UI", 11);
            txtUIDInput.Location = new Point(10, 35);
            txtUIDInput.TextChanged += txtUIDInput_TextChanged;
            card.Controls.Add(txtUIDInput);

            cmbFilterBy = new ComboBox();
            cmbFilterBy.Items.Add("Név");
            cmbFilterBy.Items.Add("OM");
            cmbFilterBy.Items.Add("Osztály");
            cmbFilterBy.Items.Add("Kártya UID");
            cmbFilterBy.SelectedIndex = 3;
            cmbFilterBy.Font = new System.Drawing.Font("Segoe UI", 11);
            cmbFilterBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterBy.Width = 150;
            cmbFilterBy.Location = new Point(250, 35);
            card.Controls.Add(cmbFilterBy);

            btnPdf = new Button();
            btnPdf.Text = "Mentés PDF-ben";
            btnPdf.Height = 36;
            btnPdf.Width = 160;
            btnPdf.FlatStyle = FlatStyle.Flat;
            btnPdf.FlatAppearance.BorderSize = 0;
            btnPdf.BackColor = Color.FromArgb(13, 110, 253);
            btnPdf.ForeColor = Color.White;
            btnPdf.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnPdf.Top = 60;
            btnPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            content.Controls.Add(btnPdf);
            btnPdf.Left = content.Width - btnPdf.Width - 10;
            content.Resize += content_Resize;
            btnPdf.Click += btnPdf_Click;

            dataGridView1 = new DataGridView();
            dataGridView1.Top = 150;
            dataGridView1.Left = 0;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.Width = content.Width - 50;
            dataGridView1.Height = content.Height - 200;
            content.Controls.Add(dataGridView1);

            StilusBeallitasok();
            Load += adatKiiras_Load;
        }
        private void content_Resize(object sender, EventArgs e){
            btnPdf.Left = content.Width - btnPdf.Width - 10;
            dataGridView1.Width = content.Width - 50;
            dataGridView1.Height = content.Height - 200;
        }
        private void adatKiiras_Load(object sender, EventArgs e){
            BeallitSzemelyekTablaFejlec();
            LoadSzemelyek();
        }
        public static MySqlConnection GetConnection(){
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public static List<Szemely> GetSzemelyek(){
            List<Szemely> lista = new List<Szemely>();

            using (MySqlConnection conn = GetConnection()){
                string sql = "SELECT * FROM szemelyek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()){
                    Szemely sz = new Szemely();
                    sz.OM = reader.GetInt64("OM");
                    sz.Nev = reader.GetString("nev");
                    sz.Lakcim = reader.GetString("lakcim");
                    sz.TAJ = reader.GetString("TAJ");
                    sz.Nem = reader.GetString("nem");
                    sz.SzuletesiDatum = reader.GetDateTime("szuletesiDatum");
                    sz.Osztaly = reader.GetInt64("osztaly");
                    sz.KartyaUID = reader.GetString("kartya");
                    lista.Add(sz);
                }
            }
            return lista;
        }
        public static List<Meres> GetMeresek(){
            List<Meres> lista = new List<Meres>();

            using (MySqlConnection conn = GetConnection()){
                string sql = "SELECT * FROM meresek";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()){
                    Meres m = new Meres();
                    m.Id = reader.GetInt64("id");
                    m.SzemelyOM = reader.GetInt64("szemely");
                    m.TestzsirSzazalek = reader.GetDouble("testzsir%");
                    m.MagassagCm = reader.GetDouble("magassag");
                    m.SulyKg = reader.GetDouble("suly");
                    m.Datum = reader.GetDateTime("datum");
                    lista.Add(m);
                }
            }
            return lista;
        }
        public static List<KartyaRecord> GetKartyak()
        {
            List<KartyaRecord> l = new List<KartyaRecord>();

            string sql =
                "SELECT k.UID, k.kartyaTipus, s.OM, s.nev, s.osztaly, s.nem " +
                "FROM kartya k LEFT JOIN szemelyek s ON s.kartya = k.UID";

            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            using (MySqlDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    KartyaRecord kr = new KartyaRecord();
                    kr.UID = r.IsDBNull(0) ? "" : r.GetString(0);
                    kr.Tipus = r.IsDBNull(1) ? "" : r.GetString(1);
                    kr.OM = r.IsDBNull(2) ? 0 : r.GetInt64(2);
                    kr.Nev = r.IsDBNull(3) ? "-" : r.GetString(3);
                    kr.Osztaly = r.IsDBNull(4) ? 0 : r.GetInt64(4);
                    kr.Nem = r.IsDBNull(5) ? "-" : r.GetString(5);
                    l.Add(kr);
                }
            }
            return l;
        }
        private void LoadSzemelyek(){
            List<Szemely> lista = GetSzemelyek();
            List<Meres> meresek = GetMeresek();
            dataGridView1.Rows.Clear();

            for (int i = 0; i < lista.Count; i++){
                Szemely sz = lista[i];
                int meresDb = 0;
                DateTime utolsoMeres = DateTime.MinValue;

                for (int j = 0; j < meresek.Count; j++){
                    if (meresek[j].SzemelyOM == sz.OM){
                        meresDb++;
                        if (meresek[j].Datum > utolsoMeres)
                            utolsoMeres = meresek[j].Datum;
                    }
                }

                string utolso = meresDb == 0 ? "-" : utolsoMeres.ToString("yyyy-MM-dd");

                dataGridView1.Rows.Add(
                    sz.OM,
                    sz.Nev,
                    sz.Osztaly,
                    sz.Nem,
                    sz.KartyaUID,
                    meresDb,
                    utolso
                );
            }
        }
        private void LoadMeresek(){
            List<Szemely> szemelyek = GetSzemelyek();
            List<Meres> meresek = GetMeresek();
            dataGridView1.Rows.Clear();

            for (int i = 0; i < meresek.Count; i++){
                Meres m = meresek[i];

                Szemely sz = null;
                for (int j = 0; j < szemelyek.Count; j++){
                    if (szemelyek[j].OM == m.SzemelyOM){
                        sz = szemelyek[j];
                        break;
                    }
                }

                string nev = sz != null ? sz.Nev : "Ismeretlen";
                string nem = sz != null ? sz.Nem : "Ismeretlen";

                double bmi = SzamolBMI(m.SulyKg, m.MagassagCm);
                string bmiKat = BMICategoria(bmi);
                string tzsKat = TestzsirKategoria(nem, m.TestzsirSzazalek);

                dataGridView1.Rows.Add(
                    nev,
                    m.SulyKg,
                    m.MagassagCm,
                    bmi.ToString("0.0"),
                    bmiKat,
                    m.TestzsirSzazalek,
                    tzsKat,
                    m.Datum.ToString("yyyy-MM-dd")
                );
            }
        }
        private void LoadKartyakTulajjal(){
            BeallitKartyakTablaFejlec();
            dataGridView1.Rows.Clear();

            string sql =
                "SELECT k.UID, k.kartyaTipus, s.OM, s.nev, s.osztaly, s.nem " +
                "FROM kartya k " +
                "LEFT JOIN szemelyek s ON s.kartya = k.UID " +
                "ORDER BY k.UID;";

            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            using (MySqlDataReader r = cmd.ExecuteReader()) {
                while (r.Read()){
                    string uid = r.IsDBNull(0) ? "" : r.GetString(0);
                    string tipus = r.IsDBNull(1) ? "" : r.GetString(1);
                    string om = r.IsDBNull(2) ? "-" : r.GetInt64(2).ToString();
                    string nev = r.IsDBNull(3) ? "-" : r.GetString(3);
                    string oszt = r.IsDBNull(4) ? "-" : r.GetInt64(4).ToString();
                    string nem = r.IsDBNull(5) ? "-" : r.GetString(5);

                    dataGridView1.Rows.Add(uid, tipus, om, nev, oszt, nem);
                }
            }
        }
        private double SzamolBMI(double sulyKg, double magassagCm){
            double m = magassagCm / 100.0;
            if (m <= 0) return 0;
            return sulyKg / (m * m);
        }
        private string BMICategoria(double bmi){
            if (bmi < 18.1) return "Sovány";
            if (bmi < 25.1) return "Normál";
            if (bmi < 30.1) return "Túlsúly";
            if (bmi < 35.1) return "Elhízás";
            return "Súlyos elhízás";
        }
        private string TestzsirKategoria(string nem, double tzs){
            string n = nem.ToLower();
            bool ferfi = n.Contains("férfi") || n.Contains("fiú");
            bool no = n.Contains("nő") || n.Contains("lány");

            if (ferfi){
                if (tzs > 40) return "Extrém túlsúly";
                if (tzs > 20) return "Közepes túlsúly";
                if (tzs > 15) return "Normál";
                if (tzs > 12) return "Fitt";
                if (tzs > 6) return "Atletikus";
                return "Esszenciális";
            }
            if (no){
                if (tzs > 45) return "Extrém túlsúly";
                if (tzs > 26) return "Közepes túlsúly";
                if (tzs > 20) return "Normál";
                if (tzs > 17) return "Fitt";
                if (tzs > 14) return "Atletikus";
                return "Esszenciális";
            }

            return "Ismeretlen";
        }
        private void StilusBeallitasok(){
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowTemplate.Height = 28;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }
        private void BeallitSzemelyekTablaFejlec(){
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("OM", "OM");
            dataGridView1.Columns.Add("Nev", "Név");
            dataGridView1.Columns.Add("Osztaly", "Osztály");
            dataGridView1.Columns.Add("Nem", "Nem");
            dataGridView1.Columns.Add("Kartya", "Kártya UID");
            dataGridView1.Columns.Add("Db", "Mérések száma");
            dataGridView1.Columns.Add("Utolso", "Utolsó mérés");
        }
        private void BeallitMeresekTablaFejlec(){
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Nev", "Név");
            dataGridView1.Columns.Add("Suly", "Súly (kg)");
            dataGridView1.Columns.Add("Mag", "Magasság (cm)");
            dataGridView1.Columns.Add("BMI", "BMI");
            dataGridView1.Columns.Add("BMK", "BMI kategória");
            dataGridView1.Columns.Add("TZS", "Testzsír (%)");
            dataGridView1.Columns.Add("TZK", "Testzsír kategória");
            dataGridView1.Columns.Add("Dat", "Dátum");
        }
        private void BeallitKartyakTablaFejlec(){
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("UID", "Kártya UID");
            dataGridView1.Columns.Add("Tipus", "Típus");
            dataGridView1.Columns.Add("OM", "Tulaj OM");
            dataGridView1.Columns.Add("Nev", "Tulaj név");
            dataGridView1.Columns.Add("Osztaly", "Osztály");
            dataGridView1.Columns.Add("Nem", "Nem");
        }
        private void btnSzemelyek_Click(object sender, EventArgs e) {
            BeallitSzemelyekTablaFejlec();
            LoadSzemelyek();
        }
        private void btnMeresek_Click(object sender, EventArgs e){
            BeallitMeresekTablaFejlec();
            LoadMeresek();
        }
        private void btnKartyak_Click(object sender, EventArgs e){
            LoadKartyakTulajjal();
        }
        private void txtUIDInput_TextChanged(object sender, EventArgs e){
            string uid = txtUIDInput.Text.Trim();
            if (uid.Length == 0){
                if (dataGridView1.Columns.Count > 0){
                    if (dataGridView1.Columns[0].HeaderText == "OM"){
                        LoadSzemelyek();
                        return;
                    }
                    else if (dataGridView1.Columns[0].HeaderText == "Név"){
                        LoadMeresek();
                        return;
                    }
                    else if (dataGridView1.Columns[0].HeaderText == "Kártya UID"){
                        LoadKartyakTulajjal();
                        return;
                    }
                }
                return;
            }
            SzurUIDAlapjan(uid);
        }
        private void SzurUIDAlapjan(string value)
        {
            dataGridView1.Rows.Clear();

            string mode = "Kártya UID";
            if (cmbFilterBy != null && cmbFilterBy.SelectedItem != null)
            {
                mode = cmbFilterBy.SelectedItem.ToString();
            }

            if (dataGridView1.Columns.Count > 0 && dataGridView1.Columns[0].HeaderText == "OM")
            {
                List<Szemely> lista = GetSzemelyek();
                List<Meres> meresek = GetMeresek();

                for (int i = 0; i < lista.Count; i++)
                {
                    Szemely sz = lista[i];
                    bool ok = false;

                    if (mode == "Név")
                    {
                        if (sz.Nev != null && sz.Nev.ToLower().Contains(value.ToLower())) ok = true;
                    }
                    else if (mode == "OM")
                    {
                        if (sz.OM.ToString().Contains(value)) ok = true;
                    }
                    else if (mode == "Osztály")
                    {
                        if (sz.Osztaly.ToString().Contains(value)) ok = true;
                    }
                    else if (mode == "Kártya UID")
                    {
                        if (sz.KartyaUID != null && sz.KartyaUID.Contains(value)) ok = true;
                    }

                    if (ok)
                    {
                        int meresDb = 0;
                        DateTime utolsoMeres = DateTime.MinValue;

                        for (int j = 0; j < meresek.Count; j++)
                        {
                            if (meresek[j].SzemelyOM == sz.OM)
                            {
                                meresDb++;
                                if (meresek[j].Datum > utolsoMeres)
                                    utolsoMeres = meresek[j].Datum;
                            }
                        }

                        string utolso = meresDb == 0 ? "-" : utolsoMeres.ToString("yyyy-MM-dd");

                        dataGridView1.Rows.Add(
                            sz.OM,
                            sz.Nev,
                            sz.Osztaly,
                            sz.Nem,
                            sz.KartyaUID,
                            meresDb,
                            utolso
                        );
                    }
                }
                return;
            }

            if (dataGridView1.Columns.Count > 0 && dataGridView1.Columns[0].HeaderText == "Név")
            {
                List<Szemely> szemelyek = GetSzemelyek();
                List<Meres> meresek = GetMeresek();

                for (int i = 0; i < meresek.Count; i++)
                {
                    Meres m = meresek[i];

                    Szemely sz = null;
                    for (int j = 0; j < szemelyek.Count; j++)
                    {
                        if (szemelyek[j].OM == m.SzemelyOM)
                        {
                            sz = szemelyek[j];
                            break;
                        }
                    }
                    if (sz == null) continue;

                    bool ok = false;

                    if (mode == "Név")
                    {
                        if (sz.Nev != null && sz.Nev.ToLower().Contains(value.ToLower())) ok = true;
                    }
                    else if (mode == "OM")
                    {
                        if (sz.OM.ToString().Contains(value)) ok = true;
                    }
                    else if (mode == "Osztály")
                    {
                        if (sz.Osztaly.ToString().Contains(value)) ok = true;
                    }
                    else if (mode == "Kártya UID")
                    {
                        if (sz.KartyaUID != null && sz.KartyaUID.Contains(value)) ok = true;
                    }

                    if (ok)
                    {
                        double bmi = SzamolBMI(m.SulyKg, m.MagassagCm);
                        string bmiKat = BMICategoria(bmi);
                        string tzsKat = TestzsirKategoria(sz.Nem, m.TestzsirSzazalek);

                        dataGridView1.Rows.Add(
                            sz.Nev,
                            m.SulyKg,
                            m.MagassagCm,
                            bmi.ToString("0.0"),
                            bmiKat,
                            m.TestzsirSzazalek,
                            tzsKat,
                            m.Datum.ToString("yyyy-MM-dd")
                        );
                    }
                }
                return;
            }

            if (dataGridView1.Columns.Count > 0 && dataGridView1.Columns[0].HeaderText == "Kártya UID")
            {
                List<KartyaRecord> kartyak = GetKartyak();

                for (int i = 0; i < kartyak.Count; i++)
                {
                    KartyaRecord k = kartyak[i];
                    bool ok = false;

                    if (mode == "Kártya UID")
                    {
                        if (k.UID != null && k.UID.Contains(value)) ok = true;
                    }
                    else if (mode == "Név")
                    {
                        if (k.Nev != null && k.Nev.ToLower().Contains(value.ToLower())) ok = true;
                    }
                    else if (mode == "OM")
                    {
                        if (k.OM.ToString().Contains(value)) ok = true;
                    }
                    else if (mode == "Osztály")
                    {
                        if (k.Osztaly.ToString().Contains(value)) ok = true;
                    }

                    if (ok)
                    {
                        dataGridView1.Rows.Add(
                            k.UID,
                            k.Tipus,
                            k.OM == 0 ? "-" : k.OM.ToString(),
                            k.Nev,
                            k.Osztaly == 0 ? "-" : k.Osztaly.ToString(),
                            k.Nem
                        );
                    }
                }
                return;
            }
        }
        private void btnPdf_Click(object sender, EventArgs e){
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF fájl (*.pdf)|*.pdf";
            sfd.FileName = "lista.pdf";

            if (sfd.ShowDialog() == DialogResult.OK){
                ExportDataGridViewToPdf(sfd.FileName);
                MessageBox.Show("Sikeres mentés!");
            }
        }
        private void ExportDataGridViewToPdf(string path) {
            Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            doc.Add(new Paragraph(title.Text, titleFont));
            doc.Add(new Paragraph("Generálva: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            doc.Add(new Paragraph(" "));

            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
            table.WidthPercentage = 100;

            for (int i = 0; i < dataGridView1.Columns.Count; i++){
                PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }
            for (int r = 0; r < dataGridView1.Rows.Count; r++){
                if (dataGridView1.Rows[r].IsNewRow) continue;

                for (int c = 0; c < dataGridView1.Columns.Count; c++){
                    object val = dataGridView1.Rows[r].Cells[c].Value;
                    table.AddCell(val == null ? "" : val.ToString());
                }
            }
            doc.Add(table);
            doc.Close();
        }
    }
}
