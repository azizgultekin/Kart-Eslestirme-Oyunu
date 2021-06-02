using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Yaz_Lab_Poje_2
{
    public partial class Siralama : Form
    {
        public Siralama()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        MySqlConnection con;
        MySqlDataAdapter da;
        DataSet ds;
        public int i = 0;
        private void Siralama_Load(object sender, EventArgs e)
        {
            griddoldur();
            
        }
        void kaydet()
        {
            DataGridView veriTablosu = dataGridView1;
            string file = "C:\\Users\\Azizg\\Desktop\\Yaz_Lab_Poje_2\\İlk5.txt";
            FileStream stream = File.Create(file);
            StreamWriter sw = new StreamWriter(stream);
            foreach (DataGridViewColumn sutun in veriTablosu.Columns)
            {
                sw.Write(sutun.HeaderText + "     ");
            }
            sw.WriteLine("\n");
            foreach (DataGridViewRow satir in veriTablosu.Rows)
            {
                i++;
                sw.Write(i + ". ");
                foreach (DataGridViewCell hucre in satir.Cells)
                {
                    sw.Write(hucre.Value.ToString() + "     ");
                }
                sw.Write("\n");
                if (i == 5)
                {
                    break;
                }
            }
            MessageBox.Show("Başarıyla kaydedilmilştir.");
            sw.Close();
        }
        void baglantiKontrol()
        {
            con = new MySqlConnection("Server=localhost;Database=yaz_lab_proje2;user=root;Pwd=123456789;SslMode=none");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        void griddoldur()
        {
            baglantiKontrol();
            da = new MySqlDataAdapter("Select ad_soyad as Adı_Soyadı,puan as Puanı,sure as Süresi,deneme_sayisi as Deneme_Sayısı,oynanilan_zaman as Oynanılan_Zaman From Siralama ORDER BY puan/deneme_sayisi DESC, sure", con);
            ds = new DataSet();
            da.Fill(ds, "Siralama");
            dataGridView1.DataSource = ds.Tables["Siralama"];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kaydet();
        }

        private void Siralama_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris g = new Giris();
            this.Hide();
            g.Show();
        }
    }
}
