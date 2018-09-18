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

namespace Öğrenci_Takip_Programı_1._0
{
    public partial class Form1 : Form
    {

        OleDbConnection baglanti2 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\OgrTkpVrtbn.accdb");
        OleDbCommand komut2 = new OleDbCommand();
        Anasayfa anasayfa = new Anasayfa();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelTRH.Text = DateTime.Today.ToShortDateString();
            labelTRH.Hide();
        }

        private void buttonGİRİŞ_Click(object sender, EventArgs e)
        {
            if (textBoxKullanıcıAdı.Text == "")
            {
                MessageBox.Show("Lütfen 'Kullanıcı Adı' giriniz.","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBoxKullanıcıSoyadı.Text == "")
            {
                MessageBox.Show("Lütfen 'Kullanıcı Soyadı' giriniz.","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string sorgu = "Insert into KullBilgi (kul_adı,kul_soyad,grs_trh) values (@ad,@soyad,@trh)";
                    komut2 = new OleDbCommand(sorgu, baglanti2);

                    komut2.Parameters.AddWithValue("@ad", textBoxKullanıcıAdı.Text);
                    komut2.Parameters.AddWithValue("@soyad", textBoxKullanıcıSoyadı.Text);
                    komut2.Parameters.AddWithValue("@trh", labelTRH.Text);

                    baglanti2.Open();
                    komut2.ExecuteNonQuery();
                    baglanti2.Close();
     
                    Form1.ActiveForm.Hide();
                    anasayfa.ShowDialog();
                    
                }
                catch (Exception HATA)
                {
                    string hata = HATA.ToString();
                    MessageBox.Show("Giriş Yapılırken Veritabanı hatası oluştu ! " + HATA.Message, hata.Substring(0, hata.IndexOf(':')));
                }

            }
        }

        private void buttonGİRİŞ_MouseDown(object sender, MouseEventArgs e)
        {
           
            buttonGİRİŞ.BackColor = Color.White;
            buttonGİRİŞ.ForeColor = Color.Black;
        }

        private void buttonGİRİŞ_MouseLeave(object sender, EventArgs e)
        {
            buttonGİRİŞ.BackColor = Color.CadetBlue;
            buttonGİRİŞ.ForeColor = Color.Black;
        }
    }
}
