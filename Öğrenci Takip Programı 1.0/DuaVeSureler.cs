using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace Öğrenci_Takip_Programı_1._0
{
    public partial class DuaVeSureler : Form
    {

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbCommand komut2 = new OleDbCommand();
        OleDbCommand komut3 = new OleDbCommand();
        OleDbCommand komut4 = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        public DuaVeSureler()
        {
            InitializeComponent();

            buttonKisiVecizeler.Tag = panelOgrVecizeler;
            buttonKisiDuaVeSureler.Tag = panelBtnDuaVeSureler;
            buttonOkuduğuKitaplar.Tag = panelOkuduğuKitaplar;

            buttonKisiDuaVeSureler.Click += new EventHandler(PanelSec);
            buttonKisiVecizeler.Click += new EventHandler(PanelSec);
            buttonOkuduğuKitaplar.Click += new EventHandler(PanelSec);
            //buttonOkuduğuKitaplar.Click += new EventHandler(PanelSec);
        }
        private void PanelSec(object sender, EventArgs e)// Tıklanacak Buton için Panel seçmek.
        {
            // Tıklanan butonu al
            Button buton = (Button)sender;

            // Tüm panelleri gizle
            Panel[] tumPaneller = { panelOgrVecizeler , panelBtnDuaVeSureler , panelOkuduğuKitaplar};
            foreach (Panel p in tumPaneller) p.Visible = false;

            // Butonun Tag özelliğinde panel belirtilmişse görünür yap
            if (buton.Tag is Panel)
                ((Panel)buton.Tag).Visible = true;

        }//PanelSec END
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();// çıkış
        }
        public void OvalKenar()// Oval kenarlı PİCTURBOX İÇİN GÖRÜNTÜ AYARLARI
        {
            Rectangle r = new Rectangle(0, 0, pictureBoxKisiFotosu.Width, pictureBoxKisiFotosu.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 172;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBoxKisiFotosu.Region = new Region(gp);
        }//// OvalKenar() end

        public string OgrAd { get; set; } // Veri aktarımı
        public string OgrSoyad { get; set; }// Veri aktarımı
        public Image OgrFoto { get; set; }  // Veri aktarımı

        public string OgrTc { get; set; }//TC Aktarma



        private void DuaVeSureler_Load(object sender, EventArgs e)//////////////////////
        {
            OvalKenar();//Oval kenar için kullanılır

            panelKtpEkle.Hide();
            labelGızlıTc.Hide();
         //   panelOgrVecizeler.Hide();
            
            labelAdı.Text = OgrAd;// Form Anasayfadan Veri aktarımı
            labelSoyad.Text = OgrSoyad;// Form Anasayfadan Veri aktarımı
            pictureBoxKisiFotosu.Image = OgrFoto;//// Form Anasayfadan Veri aktarımı
            labelGızlıTc.Text = OgrTc;//tC AKTARMA

            CbKontrol();
            
            TıkRenKontrol();

        }///////////////////////////////////// DuaVeSureler_Load Form  END

        private void buttonOgrGelişimKaydet_Click(object sender, EventArgs e)
        {
            Kayıt();
            panelBtnDuaVeSureler.Refresh();

        } /// Form DuaVeSurele_Load END       

        private void buttonKisiVecizeler_Click(object sender, EventArgs e)
        {

        }//buttonKisiVecizeler_Click END

        private void buttonVecizelerBılgılendırme_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Adım : Öğrencinin Bildiği veya Ezberlediği Vecizelere 'Tik' Koyun, \n" +
               "2. Adım : Kaydet Buttonuna Basarak Kayıt Yapın. \n"

               , "Bilgilendirme", MessageBoxButtons.OK);

        }// buttonVecizelerBılgılendırme_Click END

        private void buttonKisiDuaVeSureler_Click(object sender, EventArgs e)
        {
           

        }//buttonKisiDuaVeSureler_Click END

        private void buttonOgrBılgıBak_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Adım : Öğrencinin Bildiği veya Ezberlediği Dua ve Surelere 'Tik' Koyun, \n" +
               "2. Adım : Kaydet Buttonuna Basarak Kayıt Yapın. \n" 
           
               , "Bilgilendirme", MessageBoxButtons.OK);
        }// Buton  buttonOgrBılgıBak_Click  end

        String tık1 = "Bilmiyor", tık2 = "Bilmiyor", tık3 = "Bilmiyor", tık4 = "Bilmiyor", tık5 = "Bilmiyor", tık6 = "Bilmiyor", tık7 = "Bilmiyor", tık8 = "Bilmiyor", tık9 = "Bilmiyor", tık10 = "Bilmiyor";
        String tık11 = "Bilmiyor", tık12 = "Bilmiyor", tık13 = "Bilmiyor", tık14 = "Bilmiyor", tık15 = "Bilmiyor", tık16 = "Bilmiyor", tık17 = "Bilmiyor", tık18 = "Bilmiyor", tık19 = "Bilmiyor", tık20 = "Bilmiyor", tık21 = "Bilmiyor";

        private void buttonSİL4_Click(object sender, EventArgs e)
        {
            labelK4.Text = "";
        }

        private void buttonSİL5_Click(object sender, EventArgs e)
        {
            labelK5.Text = "";
        }

        private void buttonSİL6_Click(object sender, EventArgs e)
        {
            labelK6.Text = "";
        }

        private void buttonSİL7_Click(object sender, EventArgs e)
        {
            labelK7.Text = "";
        }

        private void buttonSİL8_Click(object sender, EventArgs e)
        {
            labelK8.Text = "";
        }

        private void buttonSİL9_Click(object sender, EventArgs e)
        {
            labelK9.Text = "";
        }

        private void buttonSİL10_Click(object sender, EventArgs e)
        {
            labelK10.Text = "";
        }

        private void buttonSİL11_Click(object sender, EventArgs e)
        {
            labelK11.Text = "";
        }

        private void buttonSİL12_Click(object sender, EventArgs e)
        {
            labelK12.Text = "";
        }

        private void buttonSİL13_Click(object sender, EventArgs e)
        {
            labelK13.Text = "";
        }

        private void buttonSİL14_Click(object sender, EventArgs e)
        {
            labelK14.Text = "";
        }

        private void buttonSİL15_Click(object sender, EventArgs e)
        {
            labelK15.Text = "";
        }

        private void buttonSİL16_Click(object sender, EventArgs e)
        {
            labelK16.Text = "";
        }

        private void buttonSİL17_Click(object sender, EventArgs e)
        {
            labelK17.Text = "";
        }

        private void buttonSİL18_Click(object sender, EventArgs e)
        {
            labelK18.Text = "";
        }

        private void buttonSİL19_Click(object sender, EventArgs e)
        {
            labelK19.Text = "";
        }

        private void buttonSİL20_Click(object sender, EventArgs e)
        {
            labelK20.Text = "";
        }

        private void buttonOgrGelişimKaydet_MouseDown(object sender, MouseEventArgs e)
        {
            buttonOgrGelişimKaydet.BackColor = Color.White;
            buttonOgrGelişimKaydet.ForeColor = Color.Black;
        }

        private void buttonOgrGelişimKaydet_MouseLeave(object sender, EventArgs e)
        {
            buttonOgrGelişimKaydet.BackColor = Color.Red;
        }

        private void buttonsSİL3_Click(object sender, EventArgs e)
        {
            labelK3.Text = "";
        }

        private void buttonSİL2_Click(object sender, EventArgs e)
        {
            labelK2.Text = "";
        }

        private void buttonSİL1_Click(object sender, EventArgs e)
        {
            labelK1.Text ="";
        }

        private void buttonOgrKitapBılgı_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Adım : Öğrencinin Okuduğu Kitapları Kitap Ekle Buttonundan Ekleyebilirsiniz. \n" +
               "2. Adım : Kaydet Buttonuna Basarak Kayıt Yapın. \n"

               , "Bilgilendirme", MessageBoxButtons.OK);
        }

            
        private void buttonEkle_Click(object sender, EventArgs e)
        {

            KtpEKLE();
            panelKtpEkle.Hide();
            textBoxKitapAdı.Clear();    
         
        }

        public void KtpEKLE()
        {
            if (labelK1.Text == "")
            {
                labelK1.Text = textBoxKitapAdı.Text;
            }
            else if (labelK2.Text == "")
            {
                labelK2.Text = textBoxKitapAdı.Text;
            }
            else if (labelK3.Text == "")
            {
                labelK3.Text = textBoxKitapAdı.Text;
            }
            else if (labelK4.Text == "")
            {
                labelK4.Text = textBoxKitapAdı.Text;
            }
            else if (labelK5.Text == "")
            {
                labelK5.Text = textBoxKitapAdı.Text;
            }
            else if (labelK6.Text == "")
            {
                labelK6.Text = textBoxKitapAdı.Text;
            }
            else if (labelK7.Text == "")
            {
                labelK7.Text = textBoxKitapAdı.Text;
            }
            else if (labelK8.Text == "")
            {
                labelK8.Text = textBoxKitapAdı.Text;
            }
            else if (labelK9.Text == "")
            {
                labelK9.Text = textBoxKitapAdı.Text;
            }
            else if (labelK10.Text == "")
            {
                labelK10.Text = textBoxKitapAdı.Text;
            }
            else if (labelK11.Text == "")
            {
                labelK11.Text = textBoxKitapAdı.Text;
            }
            else if (labelK12.Text == "")
            {
                labelK12.Text = textBoxKitapAdı.Text;
            }
            else if (labelK13.Text == "")
            {
                labelK13.Text = textBoxKitapAdı.Text;
            }
            else if (labelK14.Text == "")
            {
                labelK14.Text = textBoxKitapAdı.Text;
            }
            else if (labelK15.Text == "")
            {
                labelK15.Text = textBoxKitapAdı.Text;
            }
            else if (labelK16.Text == "")
            {
                labelK16.Text = textBoxKitapAdı.Text;
            }
            else if (labelK17.Text == "")
            {
                labelK17.Text = textBoxKitapAdı.Text;
            }
            else if (labelK18.Text == "")
            {
                labelK18.Text = textBoxKitapAdı.Text;
            }
            else if (labelK19.Text == "")
            {
                labelK19.Text = textBoxKitapAdı.Text;
            }
            else if (labelK20.Text == "")
            {
                labelK20.Text = textBoxKitapAdı.Text;
            }
            else
            {
                MessageBox.Show("20 Adet Kitap Eklendi Daha Fazla Kitap Eklenemez.","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } /////  KtpEKLE() end
        private void buttonOgrnOkuKtpEkle_Click(object sender, EventArgs e)
        {
            panelKtpEkle.Show();

        }//buttonOgrnOkuKtpEkle_Click end

        private void buttonKtpEkleClose_Click(object sender, EventArgs e)
        {
            panelKtpEkle.Hide();
        }//buttonKtpEkleClose_Click end

        String tık22 = "Bilmiyor", tık23 = "Bilmiyor", tık24 = "Bilmiyor", tık25 = "Bilmiyor", tık26 = "Bilmiyor", tık27 = "Bilmiyor", tık28 = "Bilmiyor", tık29 = "Bilmiyor", tık30 = "Bilmiyor", tık31 = "Bilmiyor", tık32 = "Bilmiyor", tık33 = "Bilmiyor";

        String tıkvec1 = "Bilmiyor", tıkvec2 = "Bilmiyor", tıkvec3 = "Bilmiyor", tıkvec4 = "Bilmiyor", tıkvec5 = "Bilmiyor", tıkvec6 = "Bilmiyor", tıkvec7 = "Bilmiyor", tıkvec8 = "Bilmiyor", tıkvec9 = "Bilmiyor", tıkvec10 = "Bilmiyor";
        String tıkvec11 = "Bilmiyor", tıkvec12 = "Bilmiyor", tıkvec13 = "Bilmiyor", tıkvec14 = "Bilmiyor", tıkvec15 = "Bilmiyor", tıkvec16 = "Bilmiyor", tıkvec17 = "Bilmiyor", tıkvec18 = "Bilmiyor", tıkvec19 = "Bilmiyor", tıkvec20 = "Bilmiyor", tıkvec21 = "Bilmiyor";
        String tıkvec22 = "Bilmiyor", tıkvec23 = "Bilmiyor", tıkvec24 = "Bilmiyor", tıkvec25 = "Bilmiyor", tıkvec26 = "Bilmiyor", tıkvec27 = "Bilmiyor", tıkvec28 = "Bilmiyor", tıkvec29 = "Bilmiyor", tıkvec30 = "Bilmiyor";
        public void Islem()
        {
            
            if (cbSübhaneke.Checked==true ) { tık1 = "Biliyor"; }         
            if (cbTahiyat.Checked == true) { tık2 = "Biliyor"; }
            if (cbSalli.Checked == true) { tık3 = "Biliyor"; }
            if (cbBarik.Checked == true) { tık4 = "Biliyor"; }
            if (cbRebbena.Checked == true) { tık5 = "Biliyor"; }
            if (cbFatiha.Checked == true) { tık6 = "Biliyor"; }
            if (cbNas.Checked == true) { tık7 = "Biliyor"; }
            if (cbFelak.Checked == true) { tık8 = "Biliyor"; }
            if (cbİhlas.Checked == true) { tık9 = "Biliyor"; }
            if (cbLeheb.Checked == true) { tık10 = "Biliyor"; }
            if (cbNasr.Checked == true) { tık11 = "Biliyor"; }
            if (cbKafirun.Checked == true) { tık12 = "Biliyor"; }
            if (cbKevser.Checked == true) { tık13 = "Biliyor"; }
            if (cbMaun.Checked == true) { tık14 = "Biliyor"; }
            if (cbKureyş.Checked == true) { tık15 = "Biliyor"; }
            if (cbFil.Checked == true) { tık16 = "Biliyor"; }
            if (cbAsr.Checked == true) { tık17 = "Biliyor"; }
            if (cbKadir.Checked == true) { tık18 = "Biliyor"; }
            if (cbTin.Checked == true) { tık19 = "Biliyor"; }
            if (cbİnşirah.Checked == true) { tık20 = "Biliyor"; }
            if (cbHaşr.Checked == true) { tık21 = "Biliyor"; }
            if (cbFetih.Checked == true) { tık22 = "Biliyor"; }
            if (cbNebe.Checked == true) { tık23 = "Biliyor"; }
            if (cbAmeneresulu.Checked == true) { tık24 = "Biliyor"; }
            if (cbAyetelKürsi.Checked == true) { tık25 = "Biliyor"; }
            if (cbSalatentüncina.Checked == true) { tık26 = "Biliyor"; }
            if (cbYemekduası.Checked == true) { tık27 = "Biliyor"; }
            if (cbEzanduası.Checked == true) { tık28 = "Biliyor"; }
            if (cbMüezinlik.Checked == true) { tık29 = "Biliyor"; }
            if (cbTesbihat1.Checked == true) { tık30 = "Biliyor"; }
            if (cbTesbihat2.Checked == true) { tık31 = "Biliyor"; }
            if (cbVeccehtü.Checked == true) { tık32 = "Biliyor"; }
            if (cbKonut.Checked == true) { tık33 = "Biliyor"; }

        }////Islem() END

        public void İşlemVecize()
        {
            if (cbv1.Checked == true) { tıkvec1 = "Biliyor"; }
            if (cbv2.Checked == true) { tıkvec2 = "Biliyor"; }
            if (cbv3.Checked == true) { tıkvec3 = "Biliyor"; }
            if (cbv4.Checked == true) { tıkvec4 = "Biliyor"; }
            if (cbv5.Checked == true) { tıkvec5 = "Biliyor"; }
            if (cbv6.Checked == true) { tıkvec6 = "Biliyor"; }
            if (cbv7.Checked == true) { tıkvec7 = "Biliyor"; }
            if (cbv8.Checked == true) { tıkvec8 = "Biliyor"; }
            if (cbv9.Checked == true) { tıkvec9 = "Biliyor"; }
            if (cbv10.Checked == true) { tıkvec10 = "Biliyor"; }
            if (cbv11.Checked == true) { tıkvec11 = "Biliyor"; }
            if (cbv12.Checked == true) { tıkvec12 = "Biliyor"; }
            if (cbv13.Checked == true) { tıkvec13 = "Biliyor"; }
            if (cbv14.Checked == true) { tıkvec14 = "Biliyor"; }
            if (cbv15.Checked == true) { tıkvec15 = "Biliyor"; }
            if (cbv16.Checked == true) { tıkvec16 = "Biliyor"; }
            if (cbv17.Checked == true) { tıkvec17 = "Biliyor"; }
            if (cbv18.Checked == true) { tıkvec18 = "Biliyor"; }
            if (cbv19.Checked == true) { tıkvec19 = "Biliyor"; }
            if (cbv20.Checked == true) { tıkvec20 = "Biliyor"; }
            if (cbv21.Checked == true) { tıkvec21 = "Biliyor"; }
            if (cbv22.Checked == true) { tıkvec22 = "Biliyor"; }
            if (cbv23.Checked == true) { tıkvec23 = "Biliyor"; }
            if (cbv24.Checked == true) { tıkvec24 = "Biliyor"; }
            if (cbv25.Checked == true) { tıkvec25 = "Biliyor"; }
            if (cbv26.Checked == true) { tıkvec26 = "Biliyor"; }
            if (cbv27.Checked == true) { tıkvec27 = "Biliyor"; }
            if (cbv28.Checked == true) { tıkvec28 = "Biliyor"; }
            if (cbv29.Checked == true) { tıkvec29 = "Biliyor"; }
            if (cbv30.Checked == true) { tıkvec30 = "Biliyor"; }

        } //İşlemVecize() END


        public void Kayıt()
        {
            Islem();
            İşlemVecize();
            try
            {
                string sorgu2 = "Update OgrTkp Set OgrSure_1=@OgrSure_1, OgrSure_2=@OgrSure_2, OgrSure_3=@OgrSure_3, OgrSure_4=@OgrSure_4, OgrSure_5=@OgrSure_5, OgrSure_6=@OgrSure_6, OgrSure_7=@OgrSure_7, OgrSure_8=@OgrSure_8, OgrSure_9=@OgrSure_9, OgrSure_10=@OgrSure_10, OgrSure_11=@OgrSure_11, OgrSure_12=@OgrSure_12, OgrSure_13=@OgrSure_13, OgrSure_14=@OgrSure_14, OgrSure_15=@OgrSure_15, OgrSure_16=@OgrSure_16, OgrSure_17=@OgrSure_17, OgrSure_18=@OgrSure_18, OgrSure_19=@OgrSure_19, OgrSure_20=@OgrSure_20, OgrSure_21=@OgrSure_21, OgrSure_22=@OgrSure_22, OgrSure_23=@OgrSure_23, OgrSure_24=@OgrSure_24, OgrSure_25=@OgrSure_25, OgrSure_26=@OgrSure_26, OgrSure_27=@OgrSure_27, OgrSure_28=@OgrSure_28, OgrSure_29=@OgrSure_29, OgrSure_30=@OgrSure_30, OgrSure_31=@OgrSure_31, OgrSure_32=@OgrSure_32, OgrSure_33=@OgrSure_33 Where OgrTc_No =@OgrTc_No";
                komut2 = new OleDbCommand(sorgu2, baglanti);

                string sorgu = "Update OgrTkp Set OgrVecz_1=@OgrVecz_1 ,OgrVecz_2=@OgrVecz_2, OgrVecz_3=@OgrVecz_3, OgrVecz_4=@OgrVecz_4, OgrVecz_5=@OgrVecz_5, OgrVecz_6=@OgrVecz_6, OgrVecz_7=@OgrVecz_7, OgrVecz_8=@OgrVecz_8, OgrVecz_9=@OgrVecz_9, OgrVecz_10=@OgrVecz_10, OgrVecz_11=@OgrVecz_11, OgrVecz_12=@OgrVecz_12, OgrVecz_13=@OgrVecz_13, OgrVecz_14=@OgrVecz_14,OgrVecz_15=@OgrVecz_15, OgrVecz_16=@OgrVecz_16, OgrVecz_17=@OgrVecz_17, OgrVecz_18=@OgrVecz_18, OgrVecz_19=@OgrVecz_19, OgrVecz_20=@OgrVecz_20, OgrVecz_21=@OgrVecz_21, OgrVecz_22=@OgrVecz_22, OgrVecz_23=@OgrVecz_23, OgrVecz_24=@OgrVecz_24, OgrVecz_25=@OgrVecz_25, OgrVecz_26=@OgrVecz_26, OgrVecz_27=@OgrVecz_27, OgrVecz_28=@OgrVecz_28, OgrVecz_29=@OgrVecz_29, OgrVecz_30=@OgrVecz_30 Where OgrTc_No =@OgrTc_No";
                komut3 = new OleDbCommand(sorgu, baglanti);

                string sorgu3 = "Update OgrTkp Set OgrKtp_1=@OgrKtp_1 ,OgrKtp_2=@OgrKtp_2, OgrKtp_3=@OgrKtp_3, OgrKtp_4=@OgrKtp_4, OgrKtp_5=@OgrKtp_5, OgrKtp_6=@OgrKtp_6, OgrKtp_7=@OgrKtp_7, OgrKtp_8=@OgrKtp_8, OgrKtp_9=@OgrKtp_9, OgrKtp_10=@OgrKtp_10, OgrKtp_11=@OgrKtp_11, OgrKtp_12=@OgrKtp_12, OgrKtp_13=@OgrKtp_13, OgrKtp_14=@OgrKtp_14,OgrKtp_15=@OgrKtp_15, OgrKtp_16=@OgrKtp_16, OgrKtp_17=@OgrKtp_17, OgrKtp_18=@OgrKtp_18, OgrKtp_19=@OgrKtp_19, OgrKtp_20=@OgrKtp_20 Where OgrTc_No =@OgrTc_No";
                komut4 = new OleDbCommand(sorgu3, baglanti);

                komut2.Parameters.AddWithValue("@OgrSure_1", tık1);
                komut2.Parameters.AddWithValue("@OgrSure_2", tık2);
                komut2.Parameters.AddWithValue("@OgrSure_3", tık3);
                komut2.Parameters.AddWithValue("@OgrSure_4", tık4);
                komut2.Parameters.AddWithValue("@OgrSure_5", tık5);
                komut2.Parameters.AddWithValue("@OgrSure_6", tık6);
                komut2.Parameters.AddWithValue("@OgrSure_7", tık7);
                komut2.Parameters.AddWithValue("@OgrSure_8", tık8);
                komut2.Parameters.AddWithValue("@OgrSure_9", tık9);
                komut2.Parameters.AddWithValue("@OgrSure_10",tık10);
                komut2.Parameters.AddWithValue("@OgrSure_11", tık11);
                komut2.Parameters.AddWithValue("@OgrSure_12", tık12);
                komut2.Parameters.AddWithValue("@OgrSure_13", tık13);
                komut2.Parameters.AddWithValue("@OgrSure_14", tık14);
                komut2.Parameters.AddWithValue("@OgrSure_15", tık15);
                komut2.Parameters.AddWithValue("@OgrSure_16", tık16);
                komut2.Parameters.AddWithValue("@OgrSure_17", tık17);
                komut2.Parameters.AddWithValue("@OgrSure_18", tık18);
                komut2.Parameters.AddWithValue("@OgrSure_19", tık19);
                komut2.Parameters.AddWithValue("@OgrSure_20", tık20);
                komut2.Parameters.AddWithValue("@OgrSure_21", tık21);
                komut2.Parameters.AddWithValue("@OgrSure_22", tık22);
                komut2.Parameters.AddWithValue("@OgrSure_23", tık23);
                komut2.Parameters.AddWithValue("@OgrSure_24", tık24);
                komut2.Parameters.AddWithValue("@OgrSure_25", tık25);
                komut2.Parameters.AddWithValue("@OgrSure_26", tık26);
                komut2.Parameters.AddWithValue("@OgrSure_27", tık27);
                komut2.Parameters.AddWithValue("@OgrSure_28", tık28);
                komut2.Parameters.AddWithValue("@OgrSure_29", tık29);
                komut2.Parameters.AddWithValue("@OgrSure_30", tık30);
                komut2.Parameters.AddWithValue("@OgrSure_31", tık31);
                komut2.Parameters.AddWithValue("@OgrSure_32", tık32);
                komut2.Parameters.AddWithValue("@OgrSure_33", tık33);

                komut3.Parameters.AddWithValue("@OgrVecz_1", tıkvec1);
                komut3.Parameters.AddWithValue("@OgrVecz_2", tıkvec2);
                komut3.Parameters.AddWithValue("@OgrVecz_3", tıkvec3);
                komut3.Parameters.AddWithValue("@OgrVecz_4", tıkvec4);
                komut3.Parameters.AddWithValue("@OgrVecz_5", tıkvec5);
                komut3.Parameters.AddWithValue("@OgrVecz_6", tıkvec6);
                komut3.Parameters.AddWithValue("@OgrVecz_7", tıkvec7);
                komut3.Parameters.AddWithValue("@OgrVecz_8", tıkvec8);
                komut3.Parameters.AddWithValue("@OgrVecz_9", tıkvec9);
                komut3.Parameters.AddWithValue("@OgrVecz_10", tıkvec10);
                komut3.Parameters.AddWithValue("@OgrVecz_11", tıkvec11);
                komut3.Parameters.AddWithValue("@OgrVecz_12", tıkvec12);
                komut3.Parameters.AddWithValue("@OgrVecz_13", tıkvec13);
                komut3.Parameters.AddWithValue("@OgrVecz_14", tıkvec14);
                komut3.Parameters.AddWithValue("@OgrVecz_15", tıkvec15);
                komut3.Parameters.AddWithValue("@OgrVecz_16", tıkvec16);
                komut3.Parameters.AddWithValue("@OgrVecz_17", tıkvec17);
                komut3.Parameters.AddWithValue("@OgrVecz_18", tıkvec18);
                komut3.Parameters.AddWithValue("@OgrVecz_19", tıkvec19);
                komut3.Parameters.AddWithValue("@OgrVecz_20", tıkvec20);
                komut3.Parameters.AddWithValue("@OgrVecz_21", tıkvec21);
                komut3.Parameters.AddWithValue("@OgrVecz_22", tıkvec22);
                komut3.Parameters.AddWithValue("@OgrVecz_23", tıkvec23);
                komut3.Parameters.AddWithValue("@OgrVecz_24", tıkvec24);
                komut3.Parameters.AddWithValue("@OgrVecz_25", tıkvec25);
                komut3.Parameters.AddWithValue("@OgrVecz_26", tıkvec26);
                komut3.Parameters.AddWithValue("@OgrVecz_27", tıkvec27);
                komut3.Parameters.AddWithValue("@OgrVecz_28", tıkvec28);
                komut3.Parameters.AddWithValue("@OgrVecz_29", tıkvec29);
                komut3.Parameters.AddWithValue("@OgrVecz_30", tıkvec30);

                komut4.Parameters.AddWithValue("@OgrKtp_1", labelK1.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_2", labelK2.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_3", labelK3.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_4", labelK4.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_5", labelK5.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_6", labelK6.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_7", labelK7.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_8", labelK8.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_9", labelK9.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_10", labelK10.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_11", labelK11.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_12", labelK12.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_13", labelK13.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_14", labelK14.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_15", labelK15.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_16", labelK16.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_17", labelK17.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_18", labelK18.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_19", labelK19.Text);
                komut4.Parameters.AddWithValue("@OgrKtp_20", labelK20.Text);


                komut2.Parameters.AddWithValue("@OgrTc_No", labelGızlıTc.Text);
                komut3.Parameters.AddWithValue("@OgrTc_No", labelGızlıTc.Text);
                komut4.Parameters.AddWithValue("@OgrTc_No", labelGızlıTc.Text);

                baglanti.Open();
                komut4.ExecuteNonQuery();
                komut3.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("İşlem Başarılı.", "Başarılı");

            }
            catch (Exception HATA)
            {
                string hata = HATA.ToString();
                MessageBox.Show(" Kayıt(); için Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
            }

        }//Kayıt() END

        String tıkgel1, tıkgel2, tıkgel3, tıkgel4, tıkgel5, tıkgel6, tıkgel7, tıkgel8, tıkgel9, tıkgel10, tıkgel11, tıkgel12, tıkgel13, tıkgel14, tıkgel15, tıkgel16, tıkgel17, tıkgel18, tıkgel19, tıkgel20, tıkgel21, tıkgel22, tıkgel23, tıkgel24, tıkgel25, tıkgel26, tıkgel27, tıkgel28, tıkgel29, tıkgel30, tıkgel31, tıkgel32, tıkgel33;
        String tıkgelVez1, tıkgelVez2, tıkgelVez3, tıkgelVez4, tıkgelVez5, tıkgelVez6, tıkgelVez7, tıkgelVez8, tıkgelVez9, tıkgelVez10, tıkgelVez11, tıkgelVez12, tıkgelVez13, tıkgelVez14, tıkgelVez15, tıkgelVez16, tıkgelVez17, tıkgelVez18, tıkgelVez19, tıkgelVez20, tıkgelVez21, tıkgelVez22, tıkgelVez23, tıkgelVez24, tıkgelVez25, tıkgelVez26, tıkgelVez27, tıkgelVez28, tıkgelVez29, tıkgelVez30;
        public void CbKontrol()
        {
            try
            {
                baglanti.Open();
                komut = new OleDbCommand("SELECT * FROM OgrTkp where OgrTc_No=@OgrTc_No", baglanti);
                // komut.Parameters.AddWithValue("@OgrTc_No", labelGızlıTc.Text);
                komut.Connection = baglanti;
                komut.Parameters.AddWithValue("@OgrTc_No", labelGızlıTc.Text);
                OleDbDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    tıkgel1 = dr["OgrSure_1"].ToString();
                    tıkgel2 = dr["OgrSure_2"].ToString();
                    tıkgel3 = dr["OgrSure_3"].ToString();
                    tıkgel4 = dr["OgrSure_4"].ToString();
                    tıkgel5 = dr["OgrSure_5"].ToString();
                    tıkgel6 = dr["OgrSure_6"].ToString();
                    tıkgel7 = dr["OgrSure_7"].ToString();
                    tıkgel8 = dr["OgrSure_8"].ToString();
                    tıkgel9 = dr["OgrSure_9"].ToString();
                    tıkgel10 = dr["OgrSure_10"].ToString();
                    tıkgel11 = dr["OgrSure_11"].ToString();
                    tıkgel12 = dr["OgrSure_12"].ToString();
                    tıkgel13 = dr["OgrSure_13"].ToString();
                    tıkgel14 = dr["OgrSure_14"].ToString();
                    tıkgel15 = dr["OgrSure_15"].ToString();
                    tıkgel16 = dr["OgrSure_16"].ToString();
                    tıkgel17 = dr["OgrSure_17"].ToString();
                    tıkgel18 = dr["OgrSure_18"].ToString();
                    tıkgel19 = dr["OgrSure_19"].ToString();
                    tıkgel20 = dr["OgrSure_20"].ToString();
                    tıkgel21 = dr["OgrSure_21"].ToString();
                    tıkgel22 = dr["OgrSure_22"].ToString();
                    tıkgel23 = dr["OgrSure_23"].ToString();
                    tıkgel24 = dr["OgrSure_24"].ToString();
                    tıkgel25 = dr["OgrSure_25"].ToString();
                    tıkgel26 = dr["OgrSure_26"].ToString();
                    tıkgel27 = dr["OgrSure_27"].ToString();
                    tıkgel28 = dr["OgrSure_28"].ToString();
                    tıkgel29 = dr["OgrSure_29"].ToString();
                    tıkgel30 = dr["OgrSure_30"].ToString();
                    tıkgel31 = dr["OgrSure_31"].ToString();
                    tıkgel32 = dr["OgrSure_32"].ToString();
                    tıkgel33 = dr["OgrSure_33"].ToString();

                    tıkgelVez1 = dr["OgrVecz_1"].ToString();
                    tıkgelVez2 = dr["OgrVecz_2"].ToString();
                    tıkgelVez3 = dr["OgrVecz_3"].ToString();
                    tıkgelVez4 = dr["OgrVecz_4"].ToString();
                    tıkgelVez5 = dr["OgrVecz_5"].ToString();
                    tıkgelVez6 = dr["OgrVecz_6"].ToString();
                    tıkgelVez7 = dr["OgrVecz_7"].ToString();
                    tıkgelVez8 = dr["OgrVecz_8"].ToString();
                    tıkgelVez9 = dr["OgrVecz_9"].ToString();
                    tıkgelVez10 = dr["OgrVecz_10"].ToString();
                    tıkgelVez11 = dr["OgrVecz_11"].ToString();
                    tıkgelVez12 = dr["OgrVecz_12"].ToString();
                    tıkgelVez13 = dr["OgrVecz_13"].ToString();
                    tıkgelVez14 = dr["OgrVecz_14"].ToString();
                    tıkgelVez15 = dr["OgrVecz_15"].ToString();
                    tıkgelVez16 = dr["OgrVecz_16"].ToString();
                    tıkgelVez17 = dr["OgrVecz_17"].ToString();
                    tıkgelVez18 = dr["OgrVecz_18"].ToString();
                    tıkgelVez19 = dr["OgrVecz_19"].ToString();
                    tıkgelVez20 = dr["OgrVecz_20"].ToString();
                    tıkgelVez21 = dr["OgrVecz_21"].ToString();
                    tıkgelVez22 = dr["OgrVecz_22"].ToString();
                    tıkgelVez23 = dr["OgrVecz_23"].ToString();
                    tıkgelVez24 = dr["OgrVecz_24"].ToString();
                    tıkgelVez25 = dr["OgrVecz_25"].ToString();
                    tıkgelVez26 = dr["OgrVecz_26"].ToString();
                    tıkgelVez27 = dr["OgrVecz_27"].ToString();
                    tıkgelVez28 = dr["OgrVecz_28"].ToString();
                    tıkgelVez29 = dr["OgrVecz_29"].ToString();
                    tıkgelVez30 = dr["OgrVecz_30"].ToString();

                    labelK1.Text = dr["OgrKtp_1"].ToString();
                    labelK2.Text = dr["OgrKtp_2"].ToString();
                    labelK3.Text = dr["OgrKtp_3"].ToString();
                    labelK4.Text = dr["OgrKtp_4"].ToString();
                    labelK5.Text = dr["OgrKtp_5"].ToString();
                    labelK6.Text = dr["OgrKtp_6"].ToString();
                    labelK7.Text = dr["OgrKtp_7"].ToString();
                    labelK8.Text = dr["OgrKtp_8"].ToString();
                    labelK9.Text = dr["OgrKtp_9"].ToString();
                    labelK10.Text = dr["OgrKtp_10"].ToString();
                    labelK11.Text = dr["OgrKtp_11"].ToString();
                    labelK12.Text = dr["OgrKtp_12"].ToString();
                    labelK13.Text = dr["OgrKtp_13"].ToString();
                    labelK14.Text = dr["OgrKtp_14"].ToString();
                    labelK15.Text = dr["OgrKtp_15"].ToString();
                    labelK16.Text = dr["OgrKtp_16"].ToString();
                    labelK17.Text = dr["OgrKtp_17"].ToString();
                    labelK18.Text = dr["OgrKtp_18"].ToString();
                    labelK19.Text = dr["OgrKtp_19"].ToString();
                    labelK20.Text = dr["OgrKtp_20"].ToString();
                }
                baglanti.Close();

            }
            catch (Exception HATA)
            {
                string hata = HATA.ToString();
                MessageBox.Show(" cbKontrol(); için Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
            }          
        }   //CbKontrol()  END
        public void TıkRenKontrol()
        {
            if (tıkgel1== "Biliyor") { cbSübhaneke.Checked = true; cbSübhaneke.BackColor = Color.Lime; }
            if (tıkgel2 == "Biliyor") { cbTahiyat.Checked = true; cbTahiyat.BackColor = Color.Lime; }
            if (tıkgel3 == "Biliyor") { cbSalli.Checked = true; cbSalli.BackColor = Color.Lime; }
            if (tıkgel4 == "Biliyor") { cbBarik.Checked = true; cbBarik.BackColor = Color.Lime; }
            if (tıkgel5 == "Biliyor") { cbRebbena.Checked = true; cbRebbena.BackColor = Color.Lime; }
            if (tıkgel6 == "Biliyor") { cbFatiha.Checked = true; cbFatiha.BackColor = Color.Lime; }
            if (tıkgel7 == "Biliyor") { cbNas.Checked = true; cbNas.BackColor = Color.Lime; }
            if (tıkgel8 == "Biliyor") { cbFelak.Checked = true; cbFelak.BackColor = Color.Lime; }
            if (tıkgel9 == "Biliyor") { cbİhlas.Checked = true; cbİhlas.BackColor = Color.Lime; }
            if (tıkgel10 == "Biliyor") { cbLeheb.Checked = true; cbLeheb.BackColor = Color.Lime; }
            if (tıkgel11 == "Biliyor") { cbNasr.Checked = true; cbNasr.BackColor = Color.Lime; }
            if (tıkgel12 == "Biliyor") { cbKafirun.Checked = true; cbKafirun.BackColor = Color.Lime; }
            if (tıkgel13 == "Biliyor") { cbKevser.Checked = true; cbKevser.BackColor = Color.Lime; }
            if (tıkgel14 == "Biliyor") { cbMaun.Checked = true; cbMaun.BackColor = Color.Lime; }
            if (tıkgel15 == "Biliyor") { cbKureyş.Checked = true; cbKureyş.BackColor = Color.Lime; }
            if (tıkgel16 == "Biliyor") { cbFil.Checked = true; cbFil.BackColor = Color.Lime; }
            if (tıkgel17 == "Biliyor") { cbAsr.Checked = true; cbAsr.BackColor = Color.Lime; }
            if (tıkgel18 == "Biliyor") { cbKadir.Checked = true; cbKadir.BackColor = Color.Lime; }
            if (tıkgel19 == "Biliyor") { cbTin.Checked = true; cbTin.BackColor = Color.Lime; }
            if (tıkgel20 == "Biliyor") { cbİnşirah.Checked = true; cbİnşirah.BackColor = Color.Lime; }
            if (tıkgel21 == "Biliyor") { cbHaşr.Checked = true; cbHaşr.BackColor = Color.Lime; }
            if (tıkgel22 == "Biliyor") { cbFetih.Checked = true; cbFetih.BackColor = Color.Lime; }
            if (tıkgel23 == "Biliyor") { cbNebe.Checked = true; cbNebe.BackColor = Color.Lime; }
            if (tıkgel24 == "Biliyor") { cbAmeneresulu.Checked = true; cbAmeneresulu.BackColor = Color.Lime; }
            if (tıkgel25 == "Biliyor") { cbAyetelKürsi.Checked = true; cbAyetelKürsi.BackColor = Color.Lime; }
            if (tıkgel26 == "Biliyor") { cbSalatentüncina.Checked = true; cbSalatentüncina.BackColor = Color.Lime; }
            if (tıkgel27 == "Biliyor") { cbYemekduası.Checked = true; cbYemekduası.BackColor = Color.Lime; }
            if (tıkgel28 == "Biliyor") { cbEzanduası.Checked = true; cbEzanduası.BackColor = Color.Lime; }
            if (tıkgel29 == "Biliyor") { cbMüezinlik.Checked = true; cbMüezinlik.BackColor = Color.Lime; }
            if (tıkgel30 == "Biliyor") { cbTesbihat1.Checked = true; cbTesbihat1.BackColor = Color.Lime; }
            if (tıkgel31 == "Biliyor") { cbTesbihat2.Checked = true; cbTesbihat2.BackColor = Color.Lime; }
            if (tıkgel32 == "Biliyor") { cbVeccehtü.Checked = true; cbVeccehtü.BackColor = Color.Lime; }
            if (tıkgel33 == "Biliyor") { cbKonut.Checked = true; cbKonut.BackColor = Color.Lime; }

            if (tıkgelVez1 == "Biliyor") { cbv1.Checked = true; cbv1.BackColor = Color.DarkOrange; }
            if (tıkgelVez2 == "Biliyor") { cbv2.Checked = true; cbv2.BackColor = Color.DarkOrange; }
            if (tıkgelVez3 == "Biliyor") { cbv3.Checked = true; cbv3.BackColor = Color.DarkOrange; }
            if (tıkgelVez4 == "Biliyor") { cbv4.Checked = true; cbv4.BackColor = Color.DarkOrange; }
            if (tıkgelVez5 == "Biliyor") { cbv5.Checked = true; cbv5.BackColor = Color.DarkOrange; }
            if (tıkgelVez6 == "Biliyor") { cbv6.Checked = true; cbv6.BackColor = Color.DarkOrange; }
            if (tıkgelVez7 == "Biliyor") { cbv7.Checked = true; cbv7.BackColor = Color.DarkOrange; }
            if (tıkgelVez8 == "Biliyor") { cbv8.Checked = true; cbv8.BackColor = Color.DarkOrange; }
            if (tıkgelVez9 == "Biliyor") { cbv9.Checked = true; cbv9.BackColor = Color.DarkOrange; }
            if (tıkgelVez10 == "Biliyor") { cbv10.Checked = true; cbv10.BackColor = Color.DarkOrange; }
            if (tıkgelVez11 == "Biliyor") { cbv11.Checked = true; cbv11.BackColor = Color.DarkOrange; }
            if (tıkgelVez12 == "Biliyor") { cbv12.Checked = true; cbv12.BackColor = Color.DarkOrange; }
            if (tıkgelVez13 == "Biliyor") { cbv13.Checked = true; cbv13.BackColor = Color.DarkOrange; }
            if (tıkgelVez14 == "Biliyor") { cbv14.Checked = true; cbv14.BackColor = Color.DarkOrange; }
            if (tıkgelVez15 == "Biliyor") { cbv15.Checked = true; cbv15.BackColor = Color.DarkOrange; }
            if (tıkgelVez16 == "Biliyor") { cbv16.Checked = true; cbv16.BackColor = Color.DarkOrange; }
            if (tıkgelVez17 == "Biliyor") { cbv17.Checked = true; cbv17.BackColor = Color.DarkOrange; }
            if (tıkgelVez18 == "Biliyor") { cbv18.Checked = true; cbv18.BackColor = Color.DarkOrange; }
            if (tıkgelVez19 == "Biliyor") { cbv19.Checked = true; cbv19.BackColor = Color.DarkOrange; }
            if (tıkgelVez20 == "Biliyor") { cbv20.Checked = true; cbv20.BackColor = Color.DarkOrange; }
            if (tıkgelVez21 == "Biliyor") { cbv21.Checked = true; cbv21.BackColor = Color.DarkOrange; }
            if (tıkgelVez22 == "Biliyor") { cbv22.Checked = true; cbv22.BackColor = Color.DarkOrange; }
            if (tıkgelVez23 == "Biliyor") { cbv23.Checked = true; cbv23.BackColor = Color.DarkOrange; }
            if (tıkgelVez24 == "Biliyor") { cbv24.Checked = true; cbv24.BackColor = Color.DarkOrange; }
            if (tıkgelVez25 == "Biliyor") { cbv25.Checked = true; cbv25.BackColor = Color.DarkOrange; }
            if (tıkgelVez26 == "Biliyor") { cbv26.Checked = true; cbv26.BackColor = Color.DarkOrange; }
            if (tıkgelVez27 == "Biliyor") { cbv27.Checked = true; cbv27.BackColor = Color.DarkOrange; }
            if (tıkgelVez28 == "Biliyor") { cbv28.Checked = true; cbv28.BackColor = Color.DarkOrange; }
            if (tıkgelVez29 == "Biliyor") { cbv29.Checked = true; cbv29.BackColor = Color.DarkOrange; }
            if (tıkgelVez30 == "Biliyor") { cbv30.Checked = true; cbv30.BackColor = Color.DarkOrange; }

        } //  TıkRenKontrol() END
       
       
    }
}
