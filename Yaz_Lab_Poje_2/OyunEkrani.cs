using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BunifuAnimatorNS;
using MediaPlayer;
using WMPLib;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Yaz_Lab_Poje_2
{
    public partial class OyunEkrani : Form
    {
        public OyunEkrani()
        {
            InitializeComponent();
        }
        public string kullanici_adi,tema_adi, kart_satir, kart_sutun;
        public int ilkindeks, bulunan, deneme, puan = 0, saniye = 60, dakika, btn_sayisi = 0, kart_seviyesi,btn=0,sayi = 0;
        int[] indeksler;
        PictureBox ilkkutu;
        Image[] resimler =new Image[10];
        MySqlConnection con;
        
        private void button2_Click(object sender, EventArgs e)
        {
            Giris g = new Giris();
            this.Hide();
            g.Show();
        }
       
        private void timer2_Tick(object sender, EventArgs e)
        {
            panel1.BackColor = Color.LightGreen;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(250,92,89);
        }

        public void sure()
        {
            if (kart_seviyesi < 7)
            {
                dakika = 1;
            }
            else if (kart_seviyesi < 13)
            {
                dakika = 2;
            }
            else if (kart_seviyesi < 21)
            {
                dakika = 3;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sure();
            btn =1;
            timer1.Start();
            button1.Visible = false;
        }

        void resimleriKaristir()
        {
            Random rnd = new Random();
            for (int i = 0; i < indeksler.Length; i++)
            {
                int sayi = rnd.Next(indeksler.Length);

                int gecici = indeksler[i];
                indeksler[i] = indeksler[sayi];
                indeksler[sayi] = gecici;
            }
            for (int i = 0; i < resimler.Length; i++)
            {
                int sayi = rnd.Next(resimler.Length);

                Image gecici = resimler[i];
                resimler[i] = resimler[sayi];
                resimler[sayi] = gecici;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000; // 1 saniye
            saniye = saniye - 1;
            label2.Text = saniye.ToString();
            if (dakika==0)
            {
                label1.Text = "00";
            }
            else
            {
                label1.Text = "0" + (dakika - 1).ToString();
            }
            
            if (saniye==9)
            {
                label2.Text = "09";
            }
            else if (saniye == 8)
            {
                label2.Text = "08";
            }
            else if (saniye == 7)
            {
                label2.Text = "07";
            }
            else if (saniye == 6)
            {
                label2.Text = "06";
            }
            else if (saniye == 5)
            {
                label2.Text = "05";
            }
            else if (saniye == 4)
            {
                label2.Text = "04";
            }
            else if (saniye == 3)
            {
                label2.Text = "03";
            }
            else if (saniye == 2)
            {
                label2.Text = "02";
            }
            else if (saniye == 1)
            {
                label2.Text = "01";
            }
            else if (saniye == 0)
            {
                label2.Text = "00";
            }
            if (label1.Text == "00" && label2.Text == "1")
            {
                MessageBox.Show("Süreniz Bitti.");
            }
            if (saniye == 00)
            {
                dakika = dakika - 1;
                label1.Text = dakika.ToString();
                saniye = 60;
            }
            if (label1.Text == "-1")
            {
                timer1.Stop();
                label1.Text = "00";
                label2.Text = "00";
                timer2.Enabled = false;
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show( "Süreniz bitti. Tekrar oynamak ister misiniz?", "TEKRAR OYNAMA", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    sure();
                    saniye = 60;
                    puan = 0;
                    deneme = 0;
                    btn = 0;
                    btn_sayisi = 0;
                    button1.Visible = true;
                    temaBul();
                    kartseviyeBul();
                    resimleriKaristir();
                    label7.Text = kullanici_adi.ToString();
                }
                else
                {
                    Giris g = new Giris();
                    this.Hide();
                    g.Show();
                }
            }
        }

        public void temaBul()
        {
            Image[] resimlerbul=new Image[10];
            for (int i = 0; i < 10; i++)
            {
               resimlerbul[i]=Image.FromFile("C:\\Users\\Azizg\\Desktop\\" + tema_adi + "\\" + i + ".png");
            }
            resimler=resimlerbul;
        }
        public void kartseviyeBul()
        {
            kart_seviyesi = int.Parse(kart_satir) * int.Parse(kart_sutun);
            int a = 0;
            indeksler = new int[kart_seviyesi];
            for (int i = 0; i < kart_seviyesi/2; i++)
            {
                indeksler[i] = i;
            }
            for (int j = kart_seviyesi / 2; j < kart_seviyesi ; j++)
            {
                indeksler[j] = a;
                a++;
            }

            for (int i = 0; i < int.Parse(kart_satir); i++)
            {
                for (int j = 0; j < int.Parse(kart_sutun); j++)
                {
                    btn_sayisi++;
                    PictureBox pb = new PictureBox();
                    pb.Name = "pictureBox" + btn_sayisi;
                    pb.Text = Convert.ToString(btn_sayisi);
                    pb.Size = new Size(125, 125);
                    pb.BackgroundImage = Image.FromFile("C:\\Users\\Azizg\\Desktop\\soru_isareti.png");
                    pb.BackgroundImageLayout = ImageLayout.Stretch;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Location = new Point(9 + 130 * j, 11 + 130 * i);
                    panel1.Controls.Add(pb);
                    pb.Click += Btn_Click;
                }
            }
            this.Width = 5 + 130 * int.Parse(kart_sutun) +235;
            this.Height = 75 + 130 * int.Parse(kart_satir);
            panel1.Width = 5 + 130 * int.Parse(kart_sutun)+10;
            panel1.Height= 43 + 130 * int.Parse(kart_satir)-30;
            button1.Location= new Point(panel1.Width+30,25);
            label1.Location= new Point(panel1.Width + 30, 69);
            label9.Location = new Point(panel1.Width + 30+28, 69);
            label2.Location= new Point(panel1.Width + 30+28+21, 69);
            label7.Location= new Point(panel1.Width + 30, 97);
            label3.Location = new Point(panel1.Width + 30, 134);
            label4.Location = new Point(panel1.Width + 30+86, 134);
            label6.Location = new Point(panel1.Width + 30, 172);
            label5.Location = new Point(panel1.Width + 30 + 143, 172);
            button2.Location=new Point(panel1.Width +30, 210);

        }
        private void OyunEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        
        private void Btn_Click(object sender, EventArgs e)
        {
           
            if (btn==1)
            {
                panel1.BackColor = Color.White;
                timer2.Enabled = false;
                timer3.Enabled = false;
                BunifuTransition transition = new BunifuTransition();
                PictureBox kutu = (PictureBox)sender;
                
                int kutuNo = int.Parse(kutu.Name.Substring(10));
                int indeksNo = indeksler[kutuNo - 1];
                
                if (ilkkutu == null)
                {
                    bunifuTransition1.HideSync(kutu, false, BunifuAnimatorNS.Animation.Leaf);
                    kutu.Image = resimler[indeksNo];
                    kutu.Refresh();
                    bunifuTransition1.ShowSync(kutu, false, BunifuAnimatorNS.Animation.Leaf);
                    ilkkutu = kutu;
                    ilkindeks = indeksNo;
                    deneme++;
                    label5.Text = deneme.ToString();
                }
                else
                {
                    if (ilkkutu.Name!=kutu.Name)
                    {
                        bunifuTransition1.HideSync(kutu, false, BunifuAnimatorNS.Animation.Leaf);
                        kutu.Image = resimler[indeksNo];
                        kutu.Refresh();
                        bunifuTransition1.ShowSync(kutu, false, BunifuAnimatorNS.Animation.Leaf);
                        System.Threading.Thread.Sleep(1000);
                        ilkkutu.Image = Image.FromFile("C:\\Users\\Azizg\\Desktop\\soru_isareti.png"); 
                        kutu.Image = Image.FromFile("C:\\Users\\Azizg\\Desktop\\soru_isareti.png"); 
                        if (ilkindeks == indeksNo)
                        {
                            System.Media.SoundPlayer ses1 = new System.Media.SoundPlayer();
                            ses1.SoundLocation = "C:\\Users\\Azizg\\Desktop\\dogru_ses.wav";
                            ses1.Play();
                            bulunan++;
                            puan = puan + 100;
                            label4.Text = puan.ToString();
                            bunifuTransition1.HideSync(kutu);
                            bunifuTransition1.HideSync(ilkkutu);
                            timer2.Enabled = true;
                            if (bulunan == (indeksler.Length / 2))
                            {
                                baglantiKontrol();
                                DateTime bugun = DateTime.Now;
                                puan = int.Parse(label4.Text);
                                string zaman = label1.Text.ToString() + label9.Text.ToString() + label2.Text.ToString(); ;
                                MySqlCommand ekle = new MySqlCommand("insert into Siralama (ad_soyad,puan,sure,deneme_sayisi,oynanilan_zaman) values ('" + label7.Text + "','" + puan + "','" + zaman + "','" + deneme + "','" + bugun + "')", con);
                                ekle.ExecuteNonQuery();
                                con.Close();
                                timer1.Stop();
                                timer2.Enabled = false;
                                string a = label1.Text.ToString() + label9.Text.ToString() + label2.Text.ToString();
                                DialogResult dialog = new DialogResult();
                                dialog = MessageBox.Show("Tebrikler. " + kullanici_adi.ToString() + "," + deneme + ". denemede , " + puan + " puan toplayarak ve " + a + " sürede oyunu bitirdiniz." + " Tekrar oynamak ister misiniz?", "TEKRAR OYNAMA", MessageBoxButtons.YesNo);
                                if (dialog == DialogResult.Yes)
                                {
                                    sure();
                                    saniye=60;
                                    puan = 0;
                                    label4.Text = "0";
                                    label5.Text = "0";
                                    label1.Text = "00";
                                    label2.Text = "00";
                                    deneme = 0;
                                    btn = 0;
                                    btn_sayisi = 0;
                                    button1.Visible = true;
                                    temaBul();
                                    kartseviyeBul();
                                    resimleriKaristir();
                                    label7.Text = kullanici_adi.ToString();
                                }
                                else
                                {
                                    Siralama s=new Siralama();
                                    this.Hide();
                                    s.Show();
                                }

                                bulunan = 0;
                                deneme = 0;
                                foreach (Control kontrol in Controls)
                                {
                                    kontrol.Visible = true;
                                }
                                resimleriKaristir();
                            }

                        }
                        else
                        {
                            timer3.Enabled = true;
                            System.Media.SoundPlayer ses2 = new System.Media.SoundPlayer();
                            ses2.SoundLocation = "C:\\Users\\Azizg\\Desktop\\yanlis_ses.wav";
                            ses2.Play();
                            System.Threading.Thread.Sleep(1000);
                        }
                        ilkkutu = null; 
                    }
                } 
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            temaBul();
            kartseviyeBul();
            resimleriKaristir();
            label7.Text = kullanici_adi.ToString();
        }
        void baglantiKontrol()
        {
            
            con = new MySqlConnection("Server=localhost;Database=yaz_lab_proje2;user=root;Pwd=123456789;SslMode=none");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
