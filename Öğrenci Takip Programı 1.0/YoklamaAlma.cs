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
    public partial class YoklamaAlma : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
        OleDbCommand komut = new OleDbCommand();

        OleDbDataReader dr;
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet dtst = new DataSet();
        DataSet ds = new DataSet();
      //  OleDbCommandBuilder cmdb;
        OleDbCommand komut2 = new OleDbCommand();
        DataGridViewCellStyle renk = new DataGridViewCellStyle();
        public YoklamaAlma()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void DataGridDoldur()
        {
            //OleDbDataAdapter adtr = new OleDbDataAdapter("SElect *from Ogrkaydet",baglanti);
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From Ogrkaydet", baglanti);
            adtr.Fill(dtst, "Ogrkaydet");
            dataGridViewYoklamaAlma.DataSource = dtst.Tables["Ogrkaydet"];
            adtr.Dispose();
            baglanti.Close();
        }//DataGridDoldur() END

        private void dataGridViewYoklamaAlma_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            labelAdı.Text = dataGridViewYoklamaAlma.Rows[e.RowIndex].Cells[2].Value.ToString();
            labelSoyad.Text = dataGridViewYoklamaAlma.Rows[e.RowIndex].Cells[3].Value.ToString();
        }//dataGridViewYoklamaAlma_CellClick END

        private void YoklamaAlma_Load(object sender, EventArgs e)
        {
            DevamsızlıkBılgıTablosuDoldur();
            //labelTarih.Text= string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);
            labelTarih.Text = DateTime.Now.ToShortDateString();
            labelgun.Text = String.Format("{0:D}", DateTime.Now);
            DataGridDoldur();
            labelTarih.Hide();
            panelDevamszlıkBilgi.Hide();
            labelTC.Hide();

        }//YoklamaAlma_Load END   

        private void dataGridViewYoklamaAlma_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                labelAdı.Text = dataGridViewYoklamaAlma.Rows[e.RowIndex].Cells[1].Value.ToString();
                labelSoyad.Text = dataGridViewYoklamaAlma.Rows[e.RowIndex].Cells[2].Value.ToString();
                label5.Text ="'"+ labelAdı.Text + " " + labelSoyad.Text+"'" + " için Devamsızlık Bilgisi";
                labelTC.Text = dataGridViewYoklamaAlma.Rows[e.RowIndex].Cells[4].Value.ToString();
                IstenılenDoldur();
            }
            catch (Exception)
            {
            }

        }//dataGridViewYoklamaAlma_CellClick_ END

        String Durum = "";
        private void buttonGeldi_Click(object sender, EventArgs e)
        {
            Durum = "Geldi";        
            YklmStm();
            IstenılenDoldur();
        }//buttonGeldi_Click END

        private void buttonGelmedi_Click(object sender, EventArgs e)
        {
            Durum = "Gelmedi";
            YklmStm();
            IstenılenDoldur();

        }//buttonGelmedi_Click END

        public void YklmStm()
        {
            if ((labelAdı.Text != "...") && (labelSoyad.Text != "..."))
            {
                try
                {
                    baglanti.Open();
                    //string sorgu = "insert into ogrenciler(OkulNo,Ad,Soyad,Resim) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + pictureBox1 .ImageLocation + "')";
                    string sorgu = "select OgrAd,OgrSoyad,DurumTarihi from YoklamaAl where OgrAd=@OgrAd and OgrSoyad=@OgrSoyad and DurumTarihi=@DurumTarihi";
                    komut = new OleDbCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@OgrAd", labelAdı.Text);
                    komut.Parameters.AddWithValue("@OgrSoyad", labelSoyad.Text);
                    komut.Parameters.AddWithValue("@DurumTarihi", labelTarih.Text);
                    dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Seçtiğiniz Öğrenci için Bugün Yoklama Alındı. Detaylardan Görebilirsiniz.", "Kişi İçin Yoklama Alındı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //  komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    else
                    {
                        string sorgum = "insert into YoklamaAl(OgrAd,OgrSoyad,OgrDurum,DurumTarihi,OgrTc) values('" + labelAdı.Text + "','" + labelSoyad.Text + "','" + Durum + "','" + labelTarih.Text + "','" + labelTC.Text + "')";
                        OleDbCommand komut2 = new OleDbCommand(sorgum, baglanti);
                        komut2.ExecuteNonQuery();
                        baglanti.Close();

                        string AdSoyad = labelAdı.Text + " " + labelSoyad.Text;
                        MessageBox.Show("( " + AdSoyad + " )" + " Bugün " + Durum + " . Kayıt Tamamlandı.", "İşlem Başarılı");
                    }
                }
                catch (Exception HATA)
                {
                    string hata = HATA.ToString();
                    MessageBox.Show("Kişi 'Geldi-Gelmedi' İle İlgili Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
                }
            }
            else
            {
                MessageBox.Show("Yoklama İçin Lütfen Öğrenci Seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }//YklmStm() END

        public void DevamsızlıkBılgıTablosuDoldur()
        {
            try
            {
                baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                adtr = new OleDbDataAdapter("SElect *from YoklamaAl", baglanti);
                dtst = new DataSet();
                baglanti.Open();
                adtr.Fill(dtst, "YoklamaAl");
                dataGridViewDevamsızklık.DataSource = dtst.Tables["YoklamaAl"];
                baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message, "Veri Tabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//DevamsızlıkBılgıTablosuDoldur() end

        private void buttonDevamsızlıkDetay_Click(object sender, EventArgs e)
        {
            if (labelAdı.Text == "..." && labelSoyad.Text == "...")
            {
                MessageBox.Show("Detaylar İçin Lütfen Öğrenci Seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                IstenılenDoldur();
                panelDevamszlıkBilgi.Show();
            }
        }//buttonDevamsızlıkDetay_Click end

        public void IstenılenDoldur()
        {
                         
                try
                {
                    baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
                    adtr = new OleDbDataAdapter("SElect *from YoklamaAl where OgrAd and OgrTc like'" + labelTC.Text + "%'", baglanti);
                    ds = new DataSet();
                    baglanti.Open();
                    adtr.Fill(ds, "YoklamaAl");
                    dataGridViewDevamsızklık.DataSource = ds.Tables["YoklamaAl"];
                    baglanti.Close();

                    int kayitsayisi = dataGridViewDevamsızklık.RowCount;
                    labelGeldigiGS.Text = kayitsayisi.ToString();
                    if (kayitsayisi == 0)
                    {
                        MessageBox.Show("Seçtiğiniz kişi için hiç yoklama alınmamış !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception Hata)
                {
                    MessageBox.Show(Hata.Message, "Arama Yapılırken Veritabanı Hatası !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void dataGridViewDevamsızklık_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString().Contains("Geldi"))
            {             
                e.CellStyle.BackColor = Color.Lime;               
            }
            else if (e.Value != null && e.Value.ToString().Contains("Gelmedi"))
            {
                e.CellStyle.BackColor = Color.Red;
            }
        } //dataGridViewDevamsızklık_CellFormatting END

        private void button1_Click(object sender, EventArgs e)
        {
            panelDevamszlıkBilgi.Hide();
        }

        private void buttonOgrKitapBılgı_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Günlük Bir Programdır. Gündelik Yoklama Alınması Gerekir. \n"+
                "Geçmişe veya Geleceğe Göre Yoklama Alamazsınız ve Değiştiremezsiniz. \n" +
                "1. Adım : Bir öğrenci seçin, \n" +
               "2. Adım : Seçtiğiniz öğrenci o gün geldi ise 'Geldi', gelmedi ise 'Gelmedi' Buttonuna Basınız  . \n"+
               "3. Adım : Öğrencinin geçmişteki yoklama bilgisine 'Öğrenci Devamsızlık Detayı' buttonuna basarak görebilirsiniz. "
               , "Bilgilendirme", MessageBoxButtons.OK);
        }

        //Random r = new Random();
        //int x, y;
        //public void Salla()
        //{
        //    int c = 0;
        //    Point l = this.Location;
        //    while (c < 50)
        //    {
        //        int x = r.Next(1, 10);
        //        int y = r.Next(1, 10);

        //        this.Location = new Point(l.X + x, l.Y + y);
        //        Thread.Sleep(10); // bu çalışan kod parçacığının belirtilen bir süre duraklatılmasını sağlar.
        //        c++;
        //    }
        //    this.Location = l;

        //}
    }
}
