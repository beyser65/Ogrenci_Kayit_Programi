using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data.OleDb;
using System.Collections;
using System.Drawing.Printing;

namespace Öğrenci_Takip_Programı_1._0
{
    public partial class Anasayfa : Form
    {

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        private VideoCaptureDevice cam2;

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbCommand komut2 = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        OleDbCommandBuilder cmd;
        DataTable yeni;
     //   OleDbDataReader dr;
        public Anasayfa()
        {
            InitializeComponent();

            btnOgrenciler.Tag = panelOğrenciler;
            btnOgrenciEkle.Tag = panelOgrenciEkle;
           // btnPcdenAL.Tag = panelKamera;
            

            btnOgrenciler.Click += new EventHandler(PanelSec);
            btnOgrenciEkle.Click += new EventHandler(PanelSec);
            //btnPcdenAL.Click += new EventHandler(PanelSec);
           
        }
        public void OvalKenar()// Oval kenarlı PİCTURBOX İÇİN GÖRÜNTÜ AYARLARI
        {
            Rectangle r = new Rectangle(0, 0, pictureBoxOgrBılgı.Width, pictureBoxOgrBılgı.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBoxOgrBılgı.Region = new Region(gp);
        }

        public void Kamera() {// Bilgisayadaki Kameraları Bulmak ve göstermek. 

            webcam = new
           FilterInfoCollection(FilterCategory.VideoInputDevice); //webcam dizisine mevcut kameraları dolduruyoruz.

            foreach (FilterInfo item in webcam)
            {
               comboBoxKameralar.Items.Add(item.Name); //kameraları combobox a dolduruyoruz.
                comboBoxGünKameralar.Items.Add(item.Name); // gÜNCELLEME kameraları combobox a dolduruyoruz.
            }
            comboBoxGünKameralar.SelectedIndex = 0;
            comboBoxKameralar.SelectedIndex = 0;
            comboBoxGünKameralar.SelectedIndex = 0;

        }// Kamera() end

        private void PanelSec(object sender, EventArgs e)// Tıklanacak Buton için Panel seçmek.
        {
            // Tıklanan butonu al
            Button buton = (Button)sender;

            // Tüm panelleri gizle
            Panel[] tumPaneller = { panelOğrenciler, panelOgrenciEkle };
            foreach (Panel p in tumPaneller) p.Visible = false;

            // Butonun Tag özelliğinde panel belirtilmişse görünür yap
            if (buton.Tag is Panel)
                ((Panel)buton.Tag).Visible = true;

        }//PanelSec END

        //int degerım =0;
        public void KullanıcıTut()
        {
            
            try
            {
                string komut = "select MAX(kul_ID) from KullBilgi";
                adtr = new OleDbDataAdapter(komut, baglanti);
                cmd = new OleDbCommandBuilder(adtr);
                yeni = new DataTable();
                adtr.Fill(yeni);
                label2.Text = yeni.Rows[0][0].ToString();
                KullanıcıBıgıCek();
            }
            catch
            {
                MessageBox.Show("Kullanıcı Tutulurken Veritabanı Hatası Oluştu !", "Hata Mesajı");
            }
        }
        public void KullanıcıBıgıCek()
        {
            string kullanıcıAdı = "", kullanıcıSoyadı = ""; // Alacağımız verileri tanımladık
           
            baglanti.Open(); // Bağlantıyı açtık

            komut = new OleDbCommand("Select * from [KullBilgi] where kul_ID=@deger ", baglanti);
            komut.Parameters.AddWithValue("@deger", label2.Text); //Girdiğimiz şifreye uygun kişiyi istedik
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                // Eğer veritabanında girdiğimiz şifre var ise bilgilerini değişkenlere atadık..
                kullanıcıAdı = dr["kul_adı"].ToString();
                kullanıcıSoyadı = dr["kul_soyad"].ToString();
                labelKullanıcıAdi.Text = kullanıcıAdı + " " + kullanıcıSoyadı;
            }
            dr.Close();
            komut.Dispose();
            baglanti.Close();
        }

        //int KullanıcıId = 0;
        private void Anasayfa_Load(object sender, EventArgs e)///////////////////////////////////////////////////ANASAYFA
        {
            //DataGridViewButtonColumn buton = new DataGridViewButtonColumn();
            //dataGridViewOgrencıler.Columns.Add(buton);
            //buton.HeaderText = "Tıkla";
            //buton.Text = "Tıkla";
            //buton.Name = "buton";
            //buton.UseColumnTextForButtonValue = true; ////////////// datagrid içinde buton oluşturmak içiin kullanılır.


            KullanıcıTut();

            Kamera();// Fotoğraf Çekmek için Kamera Metodu etkin. 


            panelMenu.Hide();//Menü Paneli gizle,
            lblTarih.Text = DateTime.Today.ToShortDateString();//Labele tarihi atar,

            panelOğrenciler.Hide();//Öğrencile paneli gizle,
            panelOgrenciEkle.Hide();//Öğrenci Ekle paneli gizle,
            panelGuncelleme.Hide();

            radioButtonElıfBa.Checked = true;

            textBoxResımYolu.Hide();
            textBoxGrup.Hide();
            textBoxGuncelleGrup.Hide();
            labelFAd.Hide();
            labelResimName.Hide();
            label2.Hide();
            labelGunİçinID.Hide();
            textBoxGünFotoAd.Hide();
            panelGünKamera.Hide();

            TumOgrncıSayısı(); //Tüm öğrenci sayısını göstermek.
            OvalKenar();// Oval kenarlı picturbox3

            textBoxTcNo.MaxLength = 11; // kaç karakter girilecek tc için
            textBoxSınıf.MaxLength = 2;
            textBoxGuncelleTcNo.MaxLength = 11;
            textBoxGuncelleSınıf.MaxLength = 2;

        }/////////////////////////////////////////////////////////////////////////////////////////////////////// Anasayfa END
        private void btnClose_Click(object sender, EventArgs e)//Form Kapatma.
        {
            DialogResult snc;
            snc = MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (snc == DialogResult.No)
            {
                //MessageBox.Show("");// hiçbir işlem yaptırmıyorum
            }
            if (snc == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }          
        }

        private void btnMınımz_Click(object sender, EventArgs e)//Form Minimized yapma.
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxMenu_Click(object sender, EventArgs e)// Menü açma kapama butonu arkaplan rengi ayarlama.
        {

            if (pictureBoxMenu.BackColor == Color.PaleTurquoise)
            {
                pictureBoxMenu.BackColor = Color.Red;
                panelMenu.Show();
            }
            else
            {
                pictureBoxMenu.BackColor = Color.PaleTurquoise;
                panelMenu.Hide();
            }
        }//pictureBoxMenu_Click END

        private void btnOgrenciler_MouseLeave(object sender, EventArgs e)
        {
            btnOgrenciler.BackColor = Color.DarkSlateGray;
        }

        private void btnOgrenciler_MouseDown(object sender, MouseEventArgs e)
        {
            btnOgrenciler.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void btnOgrenciEkle_MouseDown(object sender, MouseEventArgs e)
        {
            btnOgrenciEkle.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void btnOgrenciEkle_MouseLeave(object sender, EventArgs e)
        {
            btnOgrenciEkle.BackColor = Color.DarkSlateGray;
        }
    
        private void btnYoklamaAl_MouseDown(object sender, MouseEventArgs e)
        {
            btnYoklamaAl.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void btnYoklamaAl_MouseLeave(object sender, EventArgs e)
        {
            btnYoklamaAl.BackColor = Color.DarkSlateGray;
        }

        private void btnHakkında_MouseDown(object sender, MouseEventArgs e)
        {
            btnHakkında.ForeColor = Color.White;
            btnHakkında.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void btnHakkında_MouseLeave(object sender, EventArgs e)
        {
            btnHakkında.ForeColor = Color.Black;
            btnHakkında.BackColor = Color.Gainsboro;
        }

        private void btnKaydet_MouseDown(object sender, MouseEventArgs e)
        {
            btnKaydet.BackColor = Color.White;
            btnKaydet.ForeColor = Color.Black;
        }

        private void btnKaydet_MouseLeave(object sender, EventArgs e)
        {
            btnKaydet.ForeColor = Color.White;
            btnKaydet.BackColor = Color.FromArgb(192, 0, 0);
        }

        

        private void btnYenıFotografCEK_MouseDown(object sender, MouseEventArgs e)
        {
            btnYenıFotografCEK.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void btnYenıFotografCEK_MouseLeave(object sender, EventArgs e)
        {
            btnYenıFotografCEK.BackColor = Color.White;
        }

        public void PCdenFotoAl()  //bİLGİSAYARDAN fOTOĞRAF ALMAK
        {
            OpenFileDialog ofd = new OpenFileDialog();
            try
            {
                ofd.Title = "Fotoğraf Seç";
                ofd.Filter = "Resim Dosyası |*.jpg;*.png| Video|*.avi| Tüm Dosyalar |*.*";
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    this.pictureBoxYenıKısıFoto.Image = new Bitmap(ofd.OpenFile());
                    
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Hata oluştu..", "HATA", MessageBoxButtons.OK);
            }
        }//PCdenFotoAl() END

        private void btnYenıFotografCEK_Click(object sender, EventArgs e)// yENİ FOTOĞRAF ÇEKMEK
        {
            panelKamera.Show();

            btnKameraAc.BackColor = Color.Red;
            btnFotoCek.BackColor = Color.PaleTurquoise;
            btnTamamYukle.BackColor = Color.PaleTurquoise;
            btnFotoCek.Enabled = false;
            btnTamamYukle.Enabled = false;

        }//btnYenıFotografCEK_Click END


        private void btnKameraAc_Click(object sender, EventArgs e)//Kamera Açma butonu
        {
            btnKameraAc.BackColor = Color.PaleTurquoise;
            btnFotoCek.BackColor = Color.Red;
            btnTamamYukle.BackColor = Color.PaleTurquoise;
            btnFotoCek.Enabled = true;
            btnKameraAc.Enabled = false;
            try
            {
                cam = new

               VideoCaptureDevice(webcam[comboBoxKameralar.SelectedIndex].MonikerString); //yandaki tanımladığımız cam değişkenine comboboxtta seçilmiş olan kamerayı atıyoruz.

                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

                cam.Start(); // Kamerayı başlatıyoruz.
            }
            catch (Exception)
            {

                MessageBox.Show("Kamera Açılırken Hata Oluştu !"," HATA", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                
            
        }//btnKameraAc_Click END

        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)//KameraGörüntüsü işlemi
        {
            try
            {
                Bitmap bmp = (Bitmap)eventArgs.Frame.Clone(); // Kameradan alınan görüntüyü picturebox a atıyoruz.

                pictureBox2.Image = bmp;

            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Kamera Seçin veya Bir Kamera Bağlayın !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//cam_NewFrame END

        public static int Tiklandi = 0;
        private void btnFotoCek_Click(object sender, EventArgs e)//fOTOĞRAF ÇEKMEK
        {
            btnFotoCek.BackColor = Color.PaleTurquoise;
            btnTamamYukle.BackColor = Color.Red;
            btnTamamYukle.Enabled = true;
            btnKameraAc.Enabled = true;
            btnFotoCek.Enabled = false;
            try
            {
                if (cam.IsRunning)
                {
                    cam.Stop(); // Kamerayı durduruyoruz.  
                    Tiklandi = 1;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Kamera Seçin veya Kamera Açın !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);             
            }
        }//btnFotoCek_Click END

        private void btnTamamYukle_Click(object sender, EventArgs e)// Fotoğraf Yükleme.
        {
            if (Tiklandi == 1)
            {
             pictureBoxYenıKısıFoto.Image = pictureBox2.Image;
                pictureBox2.Image = null;               
                panelKamera.Hide();
                Tiklandi = 0;
                
            }
            else
            {
                MessageBox.Show("Lütfen Fotoğraf Çekin !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            pictureBox2.Image = null;

        }//btnTamamYukle_Click END


        private void btnPcdenAL_Click(object sender, EventArgs e)// Bilgisayardan Fotoğraf Alma.
        {
            
        }//btnPcdenAL_Click END

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.Adım: Bilgisayarınıza bağlı kameralardan birini seçiniz, \n" +
                "2.Adım: Kamerayı açınız, \n" +
                "3.Adım: Bir fotoğraf çekiniz, \n" +
                "Son Adım: Yükleme İşlemini Gerçekleştirin.","Kamera Kullanımı",MessageBoxButtons.OK);
        }//button1_Click END

        private void button2_Click(object sender, EventArgs e)
        {
            btnKameraAc.Enabled = true;
          //  cam.Stop();
            pictureBox2.Image = null;
            panelKamera.Hide();
        }//button2_Click END
        

        private void btnKaydet_Click(object sender, EventArgs e) // yENİ kİŞİ kAYDETME
        {
            try
            {
                TxtKontrol();
                
                TumOgrncıSayısı();
                labelFAd.Show();
                labelResimName.Show();
            }
            catch (Exception)
            {

                MessageBox.Show("Kayıt Başarısız !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }// btnKaydet_Click eND
        public void OgrTkpoVeritabanıKayıt()
        {
            try
            {
                string sorgu = "Insert into OgrTkp (OgrTc_No) values (@OgrTc_No)";
                komut = new OleDbCommand(sorgu, baglanti);
                       
                komut.Parameters.AddWithValue("@OgrTc_No", textBoxTcNo.Text);
               
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
              
            }
            catch (Exception HATA)
            {
                string hata = HATA.ToString();
                MessageBox.Show("Kişi Eklenirken OgrTkp Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
            }
        }

        public enum harfler
        {
            a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, r, s, t, u, v, y, z
        }
        public void FotoEkleme()//Çekilen Fotoğraf pcbox da iken Proje dosyasına atmak ve atılan her fotoğrafa  farklı isim vermek. 
        {
            try
            {
                Random rnd = new Random();
                int harf = rnd.Next(0, 24);
                int sayi = rnd.Next(0, 100);
                int sayi2 = rnd.Next(0, 100);
             
                string RasHarf = ((harfler)(harf)).ToString().ToUpper(); //ratgele harf 1
                string RasSayi = sayi.ToString(); //rastgele gelen 1. sayı 
                string RasSayi2 = sayi2.ToString(); //rastgele gelen 2. sayı 

                pictureBoxYenıKısıFoto.Image.Save(Application.StartupPath + "/Images" + "\\Fotoğraf" +RasHarf +RasSayi +RasSayi2 + ".jpg");
                string DosyaYolu = Application.StartupPath + "/Images" + "\\Fotoğraf" + RasHarf + RasSayi + RasSayi2 + ".jpg";//Kaydedilen dosya yolu.
                string SonAdHalı = "Fotoğraf" + RasHarf + RasSayi + RasSayi2; //ADIN SON HALİNİ alıyoruz.

                labelResimName.Text =SonAdHalı;

                textBoxResımYolu.Text = DosyaYolu;//Dosya yolunu txte attık.      

                KayıtEkleme();

            }
            catch (Exception)
            {

                MessageBox.Show("Fotoğraf Yüklenmedi !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//FotoEkleme() End
        
        public void TxtKontrol()
        {
            string OgrGrubu = "";

            if (radioButtonElıfBa.Checked == true)
            {
                OgrGrubu = "ElifBa";
                textBoxGrup.Text = OgrGrubu;
            }
            else if (radioButtonKuran.Checked == true)
            {
                OgrGrubu = "Kuran";
                textBoxGrup.Text = OgrGrubu;
            }
            else if (radioButtonTecvıd.Checked == true)
            {
                OgrGrubu = "Tecvid";
                textBoxGrup.Text = OgrGrubu;
            }

            if (textBoxADI.Text=="")
            {
                MessageBox.Show("Lütfen 'Adı' giriniz.");
            }
            else if (textBoxSOYADI.Text=="")
            {
                MessageBox.Show("Lütfen ' Soyad ' giriniz.");
            }
            else if (textBoxBabaAdı.Text == "")
            {
                MessageBox.Show("Lütfen ' Baba Adı ' giriniz.");
            }
            else if (textBoxTcNo.Text == "")
            {
                MessageBox.Show("Lütfen ' Tc Numarasını ' giriniz.");
            }
            else if (textBoxOkul.Text == "")
            {
                MessageBox.Show("Lütfen ' Okul ' giriniz.");
            }
            else if (textBoxSınıf.Text == "")
            {
                MessageBox.Show("Lütfen ' Sınıf ' giriniz.");
            }
            
            else if (maskedTextBoxDıgerTlf.Text == "(   )    -")
            {
                MessageBox.Show("Lütfen 'Diğer Cep Telefonu ' giriniz.");
            }
            else if (textBoxEvAdresı.Text == "")
            {
                MessageBox.Show("Lütfen 'Ev Adresi' giriniz.");
            }
            else if (textBoxGrup.Text == "")
            {
                MessageBox.Show("Lütfen 'Grup' seçiniz.");
            }
            else
            {           
                FotoEkleme(); //metod              
            }

        }//TxtKontrol() END


        public void KayıtEkleme()
        {        
            try
            {
                string sorgu = "Insert into Ogrkaydet (ad,soyad,babaad,tcno,okul,sınıf,babatlf,annetlf,dıgertlf,evadres,grup,foto,kayıtTarihi) values (@ad,@soyad,@babaad,@tcno,@okul,@sınıf,@babatlf,@annetlf,@dıgertlf,@evadres,@grup,@foto,@kayıtTarihi)";
                komut = new OleDbCommand(sorgu, baglanti);
                // komut = new OleDbCommand("Alter table Ogrkaydet alter column OgrId counter (1,1)", baglanti); //Sıfırlama  *********************
                komut.Parameters.AddWithValue("@ad", textBoxADI.Text);
                komut.Parameters.AddWithValue("@soyad", textBoxSOYADI.Text);
                komut.Parameters.AddWithValue("@babaad", textBoxBabaAdı.Text);
                komut.Parameters.AddWithValue("@tcno", textBoxTcNo.Text);
                komut.Parameters.AddWithValue("@okul", textBoxOkul.Text);
                komut.Parameters.AddWithValue("@sınıf", textBoxSınıf.Text);
                komut.Parameters.AddWithValue("@babatlf", maskedTextBoxBabaTlf.Text);
                komut.Parameters.AddWithValue("@annetlf", maskedTextBoxAnneTlf.Text);
                komut.Parameters.AddWithValue("@dıgertlf", maskedTextBoxDıgerTlf.Text);
                komut.Parameters.AddWithValue("@evadres", textBoxEvAdresı.Text);
                komut.Parameters.AddWithValue("@grup", textBoxGrup.Text);
                komut.Parameters.AddWithValue("@foto", labelResimName.Text);
                komut.Parameters.AddWithValue("@kayıtTarihi", lblTarih.Text);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                OgrTkpoVeritabanıKayıt();// 2. veri tabanına tc ekleme
                MessageBox.Show("Kayıt Eklendi.","İşlem Başarılı");
                Temizleme();
            }
            catch (Exception HATA)
            {
                string hata = HATA.ToString();
                MessageBox.Show("Kişi Eklenirken Veritabanı hatası oluştu ! "+HATA.Message, hata.Substring(0, hata.IndexOf(':')));              
            }
        }//KayıtEkleme() END

        public void Temizleme()
        {
            pictureBoxYenıKısıFoto.Image = null;//pictureBoxYenıKısıFoto temizleme.
            textBoxADI.Clear();
            textBoxSOYADI.Clear();
            textBoxBabaAdı.Clear();
            textBoxTcNo.Clear();
            textBoxOkul.Clear();
            textBoxSınıf.Clear();
            maskedTextBoxBabaTlf.Clear();
            maskedTextBoxAnneTlf.Clear();
            maskedTextBoxDıgerTlf.Clear();
            textBoxEvAdresı.Clear();
            textBoxResımYolu.Clear();
            labelResimName.Text = "";

        }// Temizleme() END

        private void TumOgrncıSayısı()
        {
            griddoldur();
            int kayitsayisi;
            kayitsayisi = dataGridViewOgrencıler.RowCount;
            labelSayıRakamı.Text = kayitsayisi.ToString();
        }

        private void buttonTumGoster_Click(object sender, EventArgs e)// tüm öğrencileri göster
        {
            griddoldur();

            labelSAYI.Text = "Tüm Öğrenci Sayısı : ";
            int kayitsayisi;
            kayitsayisi = dataGridViewOgrencıler.RowCount;
            labelSayıRakamı.Text = kayitsayisi.ToString();

            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            dataGridViewOgrencıler.Show();

        }//buttonTumGoster_Click() end

        private void btnKuranKerim_Click(object sender, EventArgs e)//Kurandaki Öğrencileri göster
        {
            String word = "Kuran"; 
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                adtr = new OleDbDataAdapter("SElect *from Ogrkaydet where grup like '" + word + "%'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                adtr.Fill(ds, "Ogrkaydet");
                dataGridViewOgrencıler.DataSource = ds.Tables["Ogrkaydet"];
                baglanti.Close();

                labelSAYI.Text = "Kuran-ı Kerimdeki Öğrenci Sayısı : ";
                int kayitsayisi;
                kayitsayisi = dataGridViewOgrencıler.RowCount;
                labelSayıRakamı.Text = kayitsayisi.ToString();

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Kuran Bölümü Eklenirken Veri Tabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            dataGridViewOgrencıler.Show();

        }//btnKuranKerim_Click() end 

        private void btnElifBa_Click(object sender, EventArgs e)//ElifBa daki öğrencileri göster
        {
            String word = "ElifBa";
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                adtr = new OleDbDataAdapter("SElect *from Ogrkaydet where grup like '" + word + "%'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                adtr.Fill(ds, "Ogrkaydet");
                dataGridViewOgrencıler.DataSource = ds.Tables["Ogrkaydet"];
                baglanti.Close();

                labelSAYI.Text = "Elif-Ba da Olan Öğrenci Sayısı : ";
                int kayitsayisi;
                kayitsayisi = dataGridViewOgrencıler.RowCount;
                labelSayıRakamı.Text = kayitsayisi.ToString();

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Elif-Ba Bölümü Eklenirken Veri Tabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            dataGridViewOgrencıler.Show();

        }//btnElifBa_Click end

        private void buttonTecvıd_Click(object sender, EventArgs e)//Tecvid de OLan Öğrencileri görmek
        {
            String word = "Tecvid";
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                adtr = new OleDbDataAdapter("SElect *from Ogrkaydet where grup like '" + word + "%'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                adtr.Fill(ds, "Ogrkaydet");
                dataGridViewOgrencıler.DataSource = ds.Tables["Ogrkaydet"];
                baglanti.Close();

                labelSAYI.Text = "Tecvid de Olan Öğrenci Sayısı : ";
                int kayitsayisi;
                kayitsayisi = dataGridViewOgrencıler.RowCount;
                labelSayıRakamı.Text = kayitsayisi.ToString();

            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Tecvid Bölümü Eklenirken Veri Tabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            dataGridViewOgrencıler.Show();

        }//buttonTecvıd_Click() end

        void griddoldur()// veri çekmek dataGridViewOgrencıler Doldurma işlemi
        {
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                adtr = new OleDbDataAdapter("SElect *from Ogrkaydet", baglanti);
                ds = new DataSet();
                baglanti.Open();
                adtr.Fill(ds, "Ogrkaydet");
                dataGridViewOgrencıler.DataSource = ds.Tables["Ogrkaydet"];
                baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message,"Veri Tabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);             
            }

        }//griddoldur()  end


        /////
        ////
        ////
        ////
        //// MENU BUTTONLARI RENK AYARLARI   /////////////////////////////////////////////////////////////////////////
        private void buttonTumGoster_MouseDown(object sender, MouseEventArgs e)
        {
            buttonTumGoster.BackColor = Color.White;
            buttonTumGoster.ForeColor = Color.Black;
        }

        private void buttonTumGoster_MouseLeave(object sender, EventArgs e)
        {
            buttonTumGoster.BackColor = Color.Black;
            buttonTumGoster.ForeColor = Color.White;
        }

        private void btnKuranKerim_MouseDown(object sender, MouseEventArgs e)
        {
            btnKuranKerim.BackColor = Color.White;
            btnKuranKerim.ForeColor = Color.Black;
        }

        private void btnKuranKerim_MouseLeave(object sender, EventArgs e)
        {
            btnKuranKerim.BackColor = Color.Black;
            btnKuranKerim.ForeColor = Color.White;
        }

        private void btnElifBa_MouseDown(object sender, MouseEventArgs e)
        {
            btnElifBa.BackColor = Color.White;
            btnElifBa.ForeColor = Color.Black;
        }

        private void btnElifBa_MouseLeave(object sender, EventArgs e)
        {
            btnElifBa.BackColor = Color.Black;
            btnElifBa.ForeColor = Color.White;
        }

        private void buttonTecvıd_MouseDown(object sender, MouseEventArgs e)
        {
            buttonTecvıd.BackColor = Color.White;
            buttonTecvıd.ForeColor = Color.Black;
        }

        private void buttonTecvıd_MouseLeave(object sender, EventArgs e)
        {
            buttonTecvıd.BackColor = Color.Black;
            buttonTecvıd.ForeColor = Color.White;
        }/// 
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        

        private void buttonAramaYap_Click(object sender, EventArgs e)// Ada göre ARama yapma bölümü
        {
            if (textBoxAramaYap.Text == "")
            {
                MessageBox.Show("Aranacak Kişi İçin Lütfen Ad giriniz.", "UYARI", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                    adtr = new OleDbDataAdapter("SElect *from Ogrkaydet where ad like '" + textBoxAramaYap.Text + "%'", baglanti);
                    ds = new DataSet();
                    baglanti.Open();
                    adtr.Fill(ds, "Ogrkaydet");
                    dataGridViewOgrencıler.DataSource = ds.Tables["Ogrkaydet"];
                    baglanti.Close();
                    string aranan = textBoxAramaYap.Text;

                    labelSAYI.Text = "(" + aranan + ")" + " Adıyla Başlayan  Öğrenci Sayısı : ";
                    int kayitsayisi;
                    kayitsayisi = dataGridViewOgrencıler.RowCount;
                    labelSayıRakamı.Text = kayitsayisi.ToString();
                    if (kayitsayisi==0)
                    {
                        MessageBox.Show("Aradığınız Kişi Sisteme Kayıtlı Değil !", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        griddoldur();
                    }

                }
                catch (Exception Hata)
                {
                    MessageBox.Show(Hata.Message, "Arama Yapılırken Veritabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                panelOgrencıBılgısı.Hide();
                panelGuncelleme.Hide();
                dataGridViewOgrencıler.Show();
                textBoxAramaYap.Clear();
            }//buttonAramaYap_Click()  end
        }

        private void buttonAramaYap_MouseDown(object sender, MouseEventArgs e)
        {
            buttonAramaYap.BackColor = Color.Red;           
        }

        private void buttonAramaYap_MouseLeave(object sender, EventArgs e)
        {
            buttonAramaYap.BackColor = Color.White;          
        }

        ///        
        /// 
        /// 
        /// 
        /// MENU BUTTONLARI CLİCK ///////////////////////////////////////////////////////////////////
        private void btnOgrenciEkle_Click(object sender, EventArgs e)
        {
            panelMenu.Hide();
            panelGuncelleme.Hide();
            panelOgrencıBılgısı.Hide();
            dataGridViewOgrencıler.Show();
            griddoldur();
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnOgrenciler_Click(object sender, EventArgs e)
        {
            panelMenu.Hide();
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnOgrenciBilgiGüncelle_Click(object sender, EventArgs e)
        {
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnOgrenciCıkar_Click(object sender, EventArgs e)
        {
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnDuaVeSureler_Click(object sender, EventArgs e)
        {
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnVecizeler_Click(object sender, EventArgs e)
        {
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnDersProgramı_Click(object sender, EventArgs e)
        {
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void btnYoklamaAl_Click(object sender, EventArgs e)
        {
            panelMenu.Hide();
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
            YoklamaAlma ykm = new YoklamaAlma();
           
            MessageBox.Show("Öğrenci Yoklaması Alınırken Lütfen Dikkat Ediniz. \n"+
                "Girilecek kayıt bir daha değiştirilmez !","DİKKAT ÖNEMLİ UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ykm.ShowDialog();
        }

        public void DosyadanResimCekme() {
            
        }

        private void dataGridViewOgrencıler_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                //////////////////// KİŞİ BİLGİLERİNİ GÖSTERMEK İÇİN  //////////////////
                labelAdı.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[1].Value.ToString();
                labelSoyadı.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[2].Value.ToString();
                labelBabaAdı.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[3].Value.ToString();
                labelTcNo.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[4].Value.ToString();
                labelOkul.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[5].Value.ToString();
                labelSınıf.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[6].Value.ToString();
                labelBabaTLF.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[7].Value.ToString();
                labelAnneTLF.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[8].Value.ToString();
                labelDıgerTLF.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[9].Value.ToString();
                labelEvAdres.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[10].Value.ToString();
                labelGrup.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[11].Value.ToString();
                labelFoto.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[12].Value.ToString();
                labelKayıtTarıh.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[13].Value.ToString();

                //////////////////////////// KİŞİ GÜNCELLEME İÇİN ///////////////////////
                labelGunİçinID.Text= dataGridViewOgrencıler.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxGuncelleAd.Text= dataGridViewOgrencıler.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxGuncelleSoyad.Text=dataGridViewOgrencıler.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxGuncelleBabaAd.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxGuncelleTcNo.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxGuncelleOkul.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxGuncelleSınıf.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[6].Value.ToString();
                maskedTextBoxGuncelleBabaTlf.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[7].Value.ToString();
                maskedTextBoxGuncelleAnneTlf.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[8].Value.ToString();
                maskedTextBoxGuncelleDıgerTlf.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[9].Value.ToString();
                textBoxGuncelleAdres.Text = dataGridViewOgrencıler.Rows[e.RowIndex].Cells[10].Value.ToString();
                textBoxGuncelleGrup.Text=dataGridViewOgrencıler.Rows[e.RowIndex].Cells[11].Value.ToString();
                labelGunFotoAdı.Text=dataGridViewOgrencıler.Rows[e.RowIndex].Cells[12].Value.ToString();




                string dosyayoluResim = Application.StartupPath + "/Images" + "\\" + labelFoto.Text + ".jpg";// FOTOĞRAF YOLU ALMA.
                pictureBoxOgrBılgı.ImageLocation = dosyayoluResim; // Fotoğrafı Picturboxa almak.
                pictureBoxGuncelFoto.ImageLocation = dosyayoluResim;//Günceleme için foto almak

                dataGridViewOgrencıler.Hide();
                panelOgrencıBılgısı.Show();
            }
            catch (Exception)
            {

                
            }
        }//dataGridViewOgrencıler_CellClick END

        private void buttonCloseOgrBılgı_Click(object sender, EventArgs e)
        {
            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            dataGridViewOgrencıler.Show();

        }//buttonCloseOgrBılgı_Click END

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }///pANELmENU THE END

        int currentMouseOverRow;
        int numara;
        private void dataGridViewOgrencıler_MouseClick(object sender, MouseEventArgs e)
        {

            currentMouseOverRow = dataGridViewOgrencıler.HitTest(e.X, e.Y).RowIndex; //Uzerine gelinen satırın numarasını alıyoruz
            
            if (e.Button == MouseButtons.Right) //Click Eventi sağ tıklama ise
            {
                // Bir contextmenu oluşturuyoruz
                ContextMenu m = new ContextMenu();
                //eğer sağ tıklama boşluğa değilse
                if (currentMouseOverRow >= 0)
                {
                    dataGridViewOgrencıler.Rows[currentMouseOverRow].Selected = true;//bu tıkladığımız alanı seçtiriyoruz
                    numara = Convert.ToInt32(dataGridViewOgrencıler.Rows[currentMouseOverRow].Cells[0].Value);
                    //menuleri ekliyoruz

                 //   m.MenuItems.Add(new MenuItem("Sure Ve Dualar", dataGridView1_SureVeDualar));
                    m.MenuItems.Add(new MenuItem("Sil" , dataGridView1_Sil));
                                    
                } //boşluğada tıklansa hepsini sil menüsü gösterilsin

                m.MenuItems.Add(new MenuItem("Hepsini Sil", dataGridView1_hepsiniSil));
                m.Show(dataGridViewOgrencıler, new Point(e.X, e.Y));//menuyu goster
            }
        }//dataGridViewOgrencıler_MouseClick END

        void TümünüSil()
        {
            DialogResult cevap2;
            cevap2 = MessageBox.Show("Tüm Kayıtları Silmek İstediğinizden Eminmisiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap2 == DialogResult.Yes)
            {
                try
                {
                    string sorgu2 = "Delete From Ogrkaydet ";
                    komut2 = new OleDbCommand(sorgu2, baglanti);
                    baglanti.Open();
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    griddoldur();
                    MessageBox.Show("Tüm Kayıtlar Silindi.","İşlem Başarılı");

                }
                catch (Exception HATA)
                {
                    string hata = HATA.ToString();
                    MessageBox.Show("Tüm Kişiler Silinirken Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
                }
            }
        }
        void TümSureBilgiSil()
        {
            try
            {
                string sorgu2 = "Delete From OgrTkp ";
                komut2 = new OleDbCommand(sorgu2, baglanti);
                baglanti.Open();
                komut2.ExecuteNonQuery();
                baglanti.Close();
                griddoldur();
                //MessageBox.Show("Tüm Kayıtlar Silindi.", "İşlem Başarılı");

            }
            catch (Exception HATA)
            {
                string hata = HATA.ToString();
                MessageBox.Show("TümSureBilgiSil(); Silinirken Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
            }
        }
        void Sil(int numara)
        {
            baglanti.Open();
            string sql = "delete from Ogrkaydet where OgrId=@no";
            OleDbCommand com = new OleDbCommand(sql, baglanti);
            com.Parameters.AddWithValue("@no", numara);
            com.ExecuteNonQuery();
            baglanti.Close();
        }//Sil(int numara) END
        private void dataGridView1_hepsiniSil(object sender, EventArgs e)
        {         
            TümünüSil();
            TümSureBilgiSil();

        }//dataGridView1_hepsiniSil END

        

        private void dataGridView1_Sil(object sender, EventArgs e)
        {

            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {

                    Sil(numara);
                    MessageBox.Show("Kişi Silindi !", "İşlem Başarılı");
                    griddoldur();
                   
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }

        }//dataGridView1_Sil END

        private void dataGridView1_SureVeDualar(object sender, EventArgs e)
        {
            //panelGuncelleme.Show();

        }//dataGridView1_Duzenle END


       // StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        //int iCellHeight = 0;
        //int iTotalWidth = 0;
        //int iRow = 0;
       // bool bFirstPage = false;
        //bool bNewPage = false;
       // int iHeaderHeight = 0;

       
        private void buttonKayıtlarıYazdır_Click(object sender, EventArgs e)
        {
            
            try
            {
                //DialogResult cevap2;
                //cevap2 = MessageBox.Show("Yazdırma İşlemi Program Açıldıktan İtibaren Sadece Bir Defa Yazdırma İşlemi Gerçekleştirir. Eğer Tekrar Yazdırılmasını İstiyorsanız Lütfen Programı Tamamen Kapatıp Tekrar Açın !  ", "Dikkat Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //if (cevap2==DialogResult.OK)
                //{
                panelGuncelleme.Hide();
                panelOgrencıBılgısı.Hide();
                dataGridViewOgrencıler.Show();

                this.printDocument1.DefaultPageSettings.Landscape = true;
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                    griddoldur();
                
                //PrintPreviewDialog onizleme = new PrintPreviewDialog();
                //onizleme.Document = printDocument1;
                //onizleme.ShowDialog(); //DÜZ SAYFA İÇİN
                //}
            }
            catch
            {
                MessageBox.Show("Yazdırma İşleminde Hata Oluştu !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }//buttonKayıtlarıYazdır_Click EDN

        int i = 0, j = 0;
    
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {         
            try
            {
                Pen myPen = new Pen(Color.Black);// Kalem Oluşturma

                System.Drawing.Font baslik = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
                System.Drawing.Font baslik2 = new System.Drawing.Font("Arial", 20, FontStyle.Bold);
                System.Drawing.Font altbaslik2 = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
                System.Drawing.Font altbaslik = new System.Drawing.Font("Arial", 7, FontStyle.Regular);
                System.Drawing.Font dipnot = new System.Drawing.Font("Arial", 7, FontStyle.Regular);

                StringFormat ortahizala = new StringFormat(); //ortadan hizalama
                ortahizala.Alignment = StringAlignment.Center;//hizalama

                StringFormat myStringFormat = new StringFormat(); //sağdan hizalama
                myStringFormat.Alignment = StringAlignment.Far;//sağdan hizalama

                StringFormat sol = new StringFormat(); //soldan hizalama
                sol.Alignment = StringAlignment.Near;//soldan hizalama


                e.Graphics.DrawString("KUR-AN KURSU KAYITLI ÖĞRENCİLER", baslik2, Brushes.Black, 585, 40, ortahizala);// yazı başlığı

                //e.Graphics.DrawString("Talep Türü :  " + lojmantalepturu.Text, baslik, Brushes.Black, 75, 130, sol);// 
                //e.Graphics.DrawString("Dönemi                    :  " + ay.Text + " " + yil.Text, baslik, Brushes.Black, 75, 145, sol);// 

                e.Graphics.DrawString("Çıkarılma Tarihi: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), altbaslik2, Brushes.Black, 793, 80);
                e.Graphics.DrawLine(myPen, 50, 70, 1100, 70); // Çizgi çizdik...


                System.Drawing.Printing.PageSettings p = printDocument1.DefaultPageSettings;
                int x = 180, y = 150, say = dataGridViewOgrencıler.Rows.Count;

                e.Graphics.DrawLine(new Pen(Color.Black, 1), 70, 170, 1100, 170);// üst çizgi
                e.Graphics.DrawString("Sıra No", baslik, Brushes.Black, 93, 180, ortahizala);
                e.Graphics.DrawString("Adı Soyadı", baslik, Brushes.Black, 210, 180, ortahizala);
                e.Graphics.DrawString("Baba Adı", baslik, Brushes.Black, 350, 180, ortahizala);
                e.Graphics.DrawString("Tc No", baslik, Brushes.Black, 425, 180, ortahizala);
                e.Graphics.DrawString("Okul", baslik, Brushes.Black, 530, 180, ortahizala);
                e.Graphics.DrawString("Sınıf", baslik, Brushes.Black, 695, 180, ortahizala);
                e.Graphics.DrawString("Baba Tlf", baslik, Brushes.Black, 760, 180, ortahizala);
                e.Graphics.DrawString("Anne Tlf", baslik, Brushes.Black, 860, 180, ortahizala);
                e.Graphics.DrawString("Diğer Tlf", baslik, Brushes.Black, 950, 180, ortahizala);
                e.Graphics.DrawString("Grup", baslik, Brushes.Black, 1040, 180, ortahizala);
                e.Graphics.DrawLine(new Pen(Color.Black, 1), 70, 200, 1100, 200);//alt çizgi


                while (i < say && j < say)//iki adet döngü oluşturduk. i döngüsü ile tadagrivleri yazdırıyoruz,, j döngüsü ile sıra numarası verdiriyoruz...
                {

                    x += 25;

                    e.Graphics.DrawString(j.ToString() + "          " + dataGridViewOgrencıler.Rows[i].Cells[1].Value.ToString(), altbaslik, Brushes.Black, 90, x, sol);
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[2].Value.ToString(), altbaslik, Brushes.Black, 225, x, sol);//Soyadı
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[3].Value.ToString(), altbaslik, Brushes.Black, 312, x, sol);//Baba Adı
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[4].Value.ToString(), altbaslik, Brushes.Black, 397, x, sol);//Tc No
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[5].Value.ToString(), altbaslik, Brushes.Black, 462, x, sol);//Okulu
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[6].Value.ToString(), altbaslik, Brushes.Black, 690, x, sol);//Sınıf
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[7].Value.ToString(), altbaslik, Brushes.Black, 720, x, sol);//Baba Tlf
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[8].Value.ToString(), altbaslik, Brushes.Black, 810, x, sol);//Anne Tlf
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[9].Value.ToString(), altbaslik, Brushes.Black, 910, x, sol);//Diğer Tlf
                    e.Graphics.DrawString(dataGridViewOgrencıler.Rows[i].Cells[11].Value.ToString(), altbaslik, Brushes.Black, 1020, x, sol);//Grup

                    e.Graphics.DrawLine(new Pen(Color.Black, 1), 70, x + 20, 1100, x + 20);

                    i++;
                    j++;

                    if ((x + y + 10) > (p.PaperSize.Height - 100 - p.Margins.Bottom - 100))
                    {
                        e.HasMorePages = true;
                        break;
                    } // İkinci Sayfaya Geçmek için kulanılır.

                }
                e.Graphics.DrawLine(new Pen(Color.Black), 70, 170, 70, x + 20);
                e.Graphics.DrawLine(new Pen(Color.Black), 55 + 60, 170, 55 + 60, x + 20);//1 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 265, 170, 45 + 265, x + 20);//2 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 350, 170, 45 + 350, x + 20);//3 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 415, 170, 45 + 415, x + 20);//4 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 630, 170, 45 + 630, x + 20);//5 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 670, 170, 45 + 670, x + 20);//6 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 760, 170, 45 + 760, x + 20);//7 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 860, 170, 45 + 860, x + 20);//8 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 950, 170, 45 + 950, x + 20);//9 cizgi
                e.Graphics.DrawLine(new Pen(Color.Black), 45 + 1055, 170, 45 + 1055, x + 20);//10 cizgi

                if (i >= say && j >= say)
                {
                    e.HasMorePages = false;

                    i = 0;
                    j = 0;
                }//Eğer veriler bitiyse  diğer sayfaya geçme.

                e.Graphics.DrawString("ÇIKARANIN", baslik, Brushes.Black, 800, 110);
                e.Graphics.DrawString("Adı Soyadı : ", baslik, Brushes.Black, 800, 130);
               
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata Oluştu !","HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message.ToString());
            }
            
           
        }//printDocument1_PrintPage END         
        
        private void buttonKışıGuncelle_Click(object sender, EventArgs e)
        {
            panelGuncelleme.Show();

            if (textBoxGuncelleGrup.Text == "Kuran")
            {
                radioButtonGuncelleKuran.Checked = true;
            }
            else if (textBoxGuncelleGrup.Text=="ElifBa")
            {
                radioButtonGuncelleElifBa.Checked = true;
            }
            else if (textBoxGuncelleGrup.Text == "Tecvid")
            {
                radioButtonGuncelleTecvid.Checked = true;
            }
            
        }////buttonKışıGuncelle_Click END

        public void GuncellemeRadyoButonKontrol()
        {
            string OgrGrubu2 = "";
                 if (radioButtonGuncelleElifBa.Checked == true)
            {
                OgrGrubu2 = "ElifBa";
                textBoxGuncelleGrup.Text = OgrGrubu2;
            }
            else if (radioButtonGuncelleKuran.Checked == true)
            {
                OgrGrubu2 = "Kuran";
                textBoxGuncelleGrup.Text = OgrGrubu2;
            }
            else if (radioButtonGuncelleTecvid.Checked == true)
            {
                OgrGrubu2 = "Tecvid";
                textBoxGuncelleGrup.Text = OgrGrubu2;
            }
        }////GuncellemeRadyoButonKontrol() END

        private void buttonGUNCELLE_Click(object sender, EventArgs e) /// Button Güncelleme
        {
            GuncellemeRadyoButonKontrol();
            TxtKontrol2();
            


        }//// buttonGUNCELLE_Click end

        public void GuncellemePCFotoAL() /// gÜNCELLEME İÇİN pCDEN FOTO ALMA
        {
            
                OpenFileDialog ofd = new OpenFileDialog();
                try
                {
                    ofd.Title = "Fotoğraf Seç";
                    ofd.Filter = "Resim Dosyası |*.jpg;*.png| Video|*.avi| Tüm Dosyalar |*.*";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {

                        this.pictureBoxGuncelFoto.Image = new Bitmap(ofd.OpenFile());

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Hata oluştu..", "HATA", MessageBoxButtons.OK);
                }
        }//GuncellemePCFotoAL() END
       private string basıldı = "basılmadı";
        private void buttonFotoğrafEkle_Click(object sender, EventArgs e)
        {
            basıldı = "basıldı";
            GuncellemePCFotoAL();
        }

        private void buttonGuncelleFotoÇek_Click(object sender, EventArgs e)
        {
            basıldı = "basıldı";
            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            panelGünKamera.Show();

            buttonGünKameraAÇ.BackColor = Color.Red;
            buttonGünFotoğrafÇek.BackColor = Color.PaleTurquoise;
            buttonGünTamamYükle.BackColor = Color.PaleTurquoise;
            buttonGünFotoğrafÇek.Enabled = false;
            buttonGünTamamYükle.Enabled = false;

        }

        private void butonGünKamera_Click(object sender, EventArgs e)
        {
            buttonGünKameraAÇ.Enabled = true;
            cam2.Stop();
            pictureBoxGünFOTO.Image = null;
            panelGünKamera.Hide();
            panelOgrencıBılgısı.Show();
            panelGuncelleme.Show();
        }

        private void buttonGünKameraAÇ_Click(object sender, EventArgs e)
        {
            buttonGünKameraAÇ.BackColor = Color.PaleTurquoise;
            buttonGünFotoğrafÇek.BackColor = Color.Red;
            buttonGünFotoğrafÇek.Enabled = true;
            buttonGünKameraAÇ.Enabled = false;
            try
            {
                cam2 = new

               VideoCaptureDevice(webcam[comboBoxGünKameralar.SelectedIndex].MonikerString); //yandaki tanımladığımız cam değişkenine comboboxtta seçilmiş olan kamerayı atıyoruz.

                cam2.NewFrame += new NewFrameEventHandler(cam_NewFrame2);

                cam2.Start(); // Kamerayı başlatıyoruz.
            }
            catch (Exception)
            {

                MessageBox.Show("Kamera Açılırken Hata Oluştu !", " HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        void cam_NewFrame2(object sender, NewFrameEventArgs eventArgs)//KameraGörüntüsü işlemi
        {
            try
            {
                Bitmap bmp = (Bitmap)eventArgs.Frame.Clone(); // Kameradan alınan görüntüyü picturebox a atıyoruz.

                pictureBoxGünFOTO.Image = bmp;

            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Kamera Seçin veya Bir Kamera Bağlayın !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//cam_NewFrame END

        public static int Tiklandi2 = 0;
        private void buttonGünFotoğrafÇek_Click(object sender, EventArgs e)
        {
            buttonGünFotoğrafÇek.BackColor = Color.PaleTurquoise;
            buttonGünTamamYükle.BackColor = Color.Red;
            buttonGünTamamYükle.Enabled = true;
            buttonGünKameraAÇ.Enabled = true;
            buttonGünFotoğrafÇek.Enabled = false;

            try
            {
                if (cam2.IsRunning)
                {
                    cam2.Stop(); // Kamerayı durduruyoruz.  
                    Tiklandi2 = 1;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Kamera Seçin veya Kamera Açın !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonGünTamamYükle_Click(object sender, EventArgs e)
        {
            if (Tiklandi2 == 1)
            {
                pictureBoxGuncelFoto.Image = pictureBoxGünFOTO.Image;
                pictureBoxGünFOTO.Image = null;
                panelGünKamera.Hide();
                Tiklandi2 = 0;
                panelOgrencıBılgısı.Show();
                panelGuncelleme.Show();

            }
            else
            {
                MessageBox.Show("Lütfen Fotoğraf Çekin !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            pictureBoxGünFOTO.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.Adım: Bilgisayarınıza bağlı kameralardan birini seçiniz, \n" +
                "2.Adım: Kamerayı açınız, \n" +
                "3.Adım: Bir fotoğraf çekiniz, \n" +
                "Son Adım: Yükleme İşlemini Gerçekleştirin.", "Kamera Kullanımı", MessageBoxButtons.OK);
        }

        private void buttonIptal_Click(object sender, EventArgs e)
        {
            dataGridViewOgrencıler.Show();
            panelOgrencıBılgısı.Hide();
            panelGuncelleme.Hide();
            pictureBoxGuncelFoto.Image = null;
            textBoxGuncelleAd.Clear();
            textBoxGuncelleSoyad.Clear();
            textBoxGuncelleBabaAd.Clear();
            textBoxGuncelleTcNo.Clear();
            textBoxGuncelleOkul.Clear();
            textBoxGuncelleSınıf.Clear();
            textBoxGuncelleGrup.Clear();
            maskedTextBoxGuncelleBabaTlf.Clear();
            maskedTextBoxGuncelleAnneTlf.Clear();
            maskedTextBoxGuncelleDıgerTlf.Clear();
            

        }//buttonIptal_Click GÜNCELLEME IPTAL

        
        public void FotoEkleme2()//Çekilen Fotoğraf pcbox da iken Proje dosyasına atmak ve atılan her fotoğrafa  farklı isim vermek. 
        {
            try
            {
                Random rnd2 = new Random();
                int harf2 = rnd2.Next(0, 24);
                int sayi2 = rnd2.Next(0, 100);
                int sayi3 = rnd2.Next(0, 100);

                string RasHarf2 = ((harfler)(harf2)).ToString().ToUpper(); //ratgele harf 1
                string RasSayi2 = sayi2.ToString(); //rastgele gelen 1. sayı 
                string RasSayi3 = sayi3.ToString(); //rastgele gelen 2. sayı 

                pictureBoxGuncelFoto.Image.Save(Application.StartupPath + "/Images" + "\\Fotoğraf" + RasHarf2 + RasSayi2 + RasSayi3 + ".jpg");
                string DosyaYolu2 = Application.StartupPath + "/Images" + "\\Fotoğraf" + RasHarf2 + RasSayi2 + RasSayi3 + ".jpg";//Kaydedilen dosya yolu.
                string SonAdHalı2 = "Fotoğraf" + RasHarf2 + RasSayi2 + RasSayi3; //ADIN SON HALİNİ alıyoruz.

                labelGunFotoAdı.Text = SonAdHalı2;

                textBoxGünFotoAd.Text = DosyaYolu2;//Dosya yolunu txte attık.      
                
            }
            catch (Exception)
            {

                MessageBox.Show("Fotoğraf Yüklenmedi !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//FotoEkleme2() End

        public void TxtKontrol2()
        {
           
            if (textBoxGuncelleAd.Text == "")
            {
                MessageBox.Show("Lütfen 'Adı' giriniz.");
            }
            else if (textBoxGuncelleSoyad.Text == "")
            {
                MessageBox.Show("Lütfen ' Soyad ' giriniz.");
            }
            else if (textBoxGuncelleBabaAd.Text == "")
            {
                MessageBox.Show("Lütfen ' Baba Adı ' giriniz.");
            }
            else if (textBoxGuncelleTcNo.Text == "")
            {
                MessageBox.Show("Lütfen ' Tc Numarasını ' giriniz.");
            }
            else if (textBoxGuncelleOkul.Text == "")
            {
                MessageBox.Show("Lütfen ' Okul ' giriniz.");
            }
            else if (textBoxGuncelleSınıf.Text == "")
            {
                MessageBox.Show("Lütfen ' Sınıf ' giriniz.");
            }

            else if (maskedTextBoxGuncelleDıgerTlf.Text == "(   )    -")
            {
                MessageBox.Show("Lütfen 'Diğer Cep Telefonu ' giriniz.");
            }
            else if (textBoxGuncelleAdres.Text == "")
            {
                MessageBox.Show("Lütfen 'Ev Adresi' giriniz.");
            }
            else if (textBoxGuncelleGrup.Text == "")
            {
                MessageBox.Show("Lütfen 'Grup' seçiniz.");
            }
            else if (basıldı == "basıldı")
            {               
                FotoEkleme2();             
                basıldı = "basılmadı";
                Guncelleme();
                panelGuncelleme.Hide();
                panelOgrencıBılgısı.Hide();
                griddoldur();
                dataGridViewOgrencıler.Show();

            }
            else if (basıldı == "basılmadı")
            {             
                Guncelleme();
                panelGuncelleme.Hide();
                panelOgrencıBılgısı.Hide();
                griddoldur();
                dataGridViewOgrencıler.Show();
            }
            else
            {
             //  FotoEkleme2(); //metod              
            }

        }////TxtKontrol2() end Güncelleme için

        private void buttonAnasayfa_MouseDown(object sender, MouseEventArgs e)
        {

            buttonAnasayfa.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void buttonAnasayfa_MouseLeave(object sender, EventArgs e)
        {
            buttonAnasayfa.BackColor = Color.DarkSlateGray;
        }

        private void buttonAnasayfa_Click(object sender, EventArgs e)
        {
            panelOgrenciEkle.Hide();
            panelOğrenciler.Hide();
            panelMenu.Hide();
            panelGuncelleme.Hide();
            panelOgrencıBılgısı.Hide();
            dataGridViewOgrencıler.Show();
            griddoldur();
            pictureBoxMenu.BackColor = Color.PaleTurquoise;
        }

        private void buttonOgrBılgıBak_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1-) 'Tümünü Göster' Buttonundan Kayıtlı Bütün Öğrencileri Görebilirsiniz, \n"+
                "2-) 'Kuran-ı Kerim' Buttonundan Sadece Kuran de Olan Öğrencileri Görebilirsiniz, \n" +
                "3-) 'Elif-Ba' Buttonundan Sadece Elif-Ba da Olan Öğrencileri Görebilirsiniz, \n" +
                "4-) 'Tecvid' Buttonundan Sadece Tecvid de Olan Öğrencileri Görebilirsiniz, \n" +
                "5-) Öğrencilerin Bilgilerini Detaylı Görebilmek İçin Görmek istediğiniz Kişinin Üzerine Gelip Sol Tıklayıp Görebilirsiniz ve Açılan Bilgi Sayıfasından Aynı Kişi Bilgilerini Değiştirebilir Günceleme yapabilirsiniz, \n" +
                "6-) Öğrenci Silmek için Silmek İstediğiniz Kişinin Üzerine Gelip Sağ Tıklayıp Silebilirsiniz Ve Aynı Şekilde Tüm Kişileride Silebilirsiniz, \n"+
                "7-) Arama Bölümünden Kişi Adına Göre Arama Yapabilirsiniz, \n" +
                "8-) Çıktı için Alt Köşedeki Yazdırma Buttonundan Çıktı Alabilirsiniz, \n"+
                "9-) Gruba Göre Çıktı Almak İçin Çıkarmak İstediğiniz Grup Buttonuna Tıkladıktan Sonra Yazdırma Butonuna Tıklayıp Yazdırabilirsiniz, \n"+
                "10-) Öğrenci Gelişimi Bölümünden Öğrencinin Gelişimini Takip Edebilirsiniz..."
                , "Bilgilendirme", MessageBoxButtons.OK);
        }

        private void textBoxADI_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxSOYADI_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxBabaAdı_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
     
        private void textBoxSınıf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxOkul_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxGuncelleAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxGuncelleSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxGuncelleBabaAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxGuncelleTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxGuncelleOkul_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBoxGuncelleSınıf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

      
        private void buttonKisiGelisimi_Click(object sender, EventArgs e)
        {
            panelGuncelleme.Hide();
            DuaVeSureler dvs = new DuaVeSureler();

            dvs.OgrAd = labelAdı.Text;
            dvs.OgrSoyad = labelSoyadı.Text;
            dvs.OgrFoto = pictureBoxOgrBılgı.Image;
            dvs.OgrTc = labelTcNo.Text;

            dvs.ShowDialog();// Form u aç

        }// buttonKisiGelisimi_Click end

        private void panelOgrencıBılgısı_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void buttonPcdenFotoAl_Click(object sender, EventArgs e)
        {
            PCdenFotoAl();
        }

        private void buttonPcdenFotoAl_MouseDown(object sender, MouseEventArgs e)
        {
            buttonPcdenFotoAl.BackColor = Color.FromArgb(192, 0, 0);
        }

        private void buttonPcdenFotoAl_MouseLeave(object sender, EventArgs e)
        {
            buttonPcdenFotoAl.BackColor = Color.White;
        }

        private void btnHakkında_Click(object sender, EventArgs e)
        {
            ProgramHakkında ph = new ProgramHakkında();
            ph.ShowDialog();
        }//btnHakkında_Click END

        public void Guncelleme()
        {
            try
            {
                string sorgu2 = "Update Ogrkaydet Set ad=@ad, soyad=@soyad, babaad=@babaad, tcno=@tcno, okul=@okul, sınıf=@sınıf, babatlf=@babatlf, annetlf=@annetlf, dıgertlf=@dıgertlf, evadres=@evadres, grup=@grup, foto=@foto Where OgrId=@OgrId";
                komut2 = new OleDbCommand(sorgu2, baglanti);

               
                komut2.Parameters.AddWithValue("@ad", textBoxGuncelleAd.Text);
                komut2.Parameters.AddWithValue("@soyad", textBoxGuncelleSoyad.Text);
                komut2.Parameters.AddWithValue("@babaad", textBoxGuncelleBabaAd.Text);
                komut2.Parameters.AddWithValue("@tcno", textBoxGuncelleTcNo.Text);
                komut2.Parameters.AddWithValue("@okul", textBoxGuncelleOkul.Text);
                komut2.Parameters.AddWithValue("@sınıf", textBoxGuncelleSınıf.Text);
                komut2.Parameters.AddWithValue("@babatlf", maskedTextBoxGuncelleBabaTlf.Text);
                komut2.Parameters.AddWithValue("@annetlf", maskedTextBoxGuncelleAnneTlf.Text);
                komut2.Parameters.AddWithValue("@dıgertlf", maskedTextBoxGuncelleDıgerTlf.Text);
                komut2.Parameters.AddWithValue("@evadres", textBoxGuncelleAdres.Text);
                komut2.Parameters.AddWithValue("@grup", textBoxGuncelleGrup.Text);
                komut2.Parameters.AddWithValue("@foto", labelGunFotoAdı.Text);
                komut2.Parameters.AddWithValue("@OgrId", labelGunİçinID.Text);

                baglanti.Open();
                komut2.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kayıt Güncellendi.", "İşlem Başarılı");
                
            }
            catch (Exception HATA)
            {
                string hata = HATA.ToString();
                MessageBox.Show("Kişi Güncellenirken Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
            }

        }

    }
}
