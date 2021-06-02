using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yaz_Lab_Poje_2
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        public string a;
        public string b;
        public int c;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "" && comboBox3.Text != "" && comboBox2.Text != "" && comboBox4.Text != "")
            {
                OyunEkrani o = new OyunEkrani();
                o.kullanici_adi = textBox1.Text;
                o.tema_adi = comboBox2.Text;
                a = comboBox3.Text.ToString();
                b = comboBox4.Text.ToString();
                o.kart_satir = a;
                o.kart_sutun = b;
                this.Hide();
                o.Show();
            }
            else
            {
                if (textBox1.Text == "")
                {
                    errorProvider1.SetError(textBox1, "Ad Soyad Giriniz!!!");

                }
                if (comboBox3.Text == "")
                {
                    errorProvider1.SetError(comboBox3, "Kart Seviyesi Giriniz!!!");

                }
                if (comboBox4.Text == "")
                {
                    errorProvider1.SetError(comboBox4, "Kart Seviyesi Giriniz!!!");

                }
                if (comboBox2.Text == "")
                {
                    errorProvider1.SetError(comboBox2, "Tema Giriniz!!!");
                }
            }
          
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Giris_Load(object sender, EventArgs e)
        {
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            if (comboBox3.SelectedItem.ToString() == "2")
            {
                comboBox4.Items.Add(2);
            }
            else if (comboBox3.SelectedItem.ToString() == "3")
            {
                comboBox4.Items.Add(2);   
            }
            else if (comboBox3.SelectedItem.ToString() == "4")
            {
                comboBox4.Items.Add(2);
                comboBox4.Items.Add(3);
                comboBox4.Items.Add(4);
            }
            else if (comboBox3.SelectedItem.ToString() == "5")
            {
                comboBox4.Items.Add(2);
                comboBox4.Items.Add(4);
            }
            else if (comboBox3.SelectedItem.ToString() == "6")
            {
                comboBox4.Items.Add(2);
                comboBox4.Items.Add(3);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Siralama s = new Siralama();
            this.Hide();
            s.Show();
        }
    }
}
