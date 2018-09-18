using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Öğrenci_Takip_Programı_1._0
{
    public partial class ProgramHakkında : Form
    {
        public ProgramHakkında()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProgramHakkında_Load(object sender, EventArgs e)
        {
            labelyazı.Text = "'Öğrenci Takip Programı' bir tür öğrenci takip programıdır. Yurtlarda veya Medreselerde bulunan öğrencilerin kayıt altına alınması veya gelişimlerinin takip \n" +
                "edilmesi için tasarlanmış bir programdır. \n \n" +
                " Öğrenci Takip Programı 1.0 versiyonunda olup içeriğinde 'Öğrenci Ekle' , 'Öğrenciler' , 'Yoklama Al' adında 3 bölümden oluşur. \n \n" +
                "1.BÖLÜM: 'Öğrenci Ekle' bölümünde ilk defa kayıt yapılacak öğrenciye ait bilgiler (ADI, SOYADI, BABA ADI, TC NUMARASI, OKULU, SINIFI, BABA TELEFONU,\n" +
                "ANNE TELEFONU, BİR YAKINININ TELEFONU, EV ADRESİ, GRUBU, FOTOĞRAFI ) tutularak kayıt altına alınır. \n\n"+
                "2.BÖLÜM: 'Öğrenciler' bölümnde kayıt altındaki öğrencilerin tümü görülebilir. Bu bölümde öğrencilerin tümü veya grubuna göre bütün öğrencilerin (talebelerin) \n" +
                "bilgileri yazdırılabilir. İstenilen öğrenciye tıklanarak gerek bilgileri güncellenebilir, gerek de o öğrencinin gelişimini ( Bildiği Dua ve Sureler, Bildiği Vecizler ve \n" +
                "Okuduğu Kitaplar ) görülebilir, eksik olduğu yönleri bulunup gelişmesi için üzerinde durulabilir. \n\n" +
                "3.BÖLÜM: 'Yoklama Al' adı üstünde bu bölümde ise kayıtlı olan öğrencilerin belirlenen programalara (Kur-an Kursu Programı vb) gelip gelmediğini öğrenmek \n"+
                "veya kurs süresi boyunca bir öğrencinin kaç gün gelip kaç gün gelmediğini öğrenmek için kullanılır.";


            labelKisi.Text = "'Öğrenci Takip Programı' SERHAT ŞOS tarafından hazırlanmıştır.\n"+
                              "                              Tüm Hakkları Saklıdır.(2017-2018)";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/serhat-sos/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.koddunyam.com/");
        }
    }
}
