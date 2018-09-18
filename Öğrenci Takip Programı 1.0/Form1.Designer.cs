namespace Öğrenci_Takip_Programı_1._0
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGİRİŞ = new System.Windows.Forms.Button();
            this.textBoxKullanıcıSoyadı = new System.Windows.Forms.TextBox();
            this.textBoxKullanıcıAdı = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTRH = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGİRİŞ);
            this.groupBox1.Controls.Add(this.textBoxKullanıcıSoyadı);
            this.groupBox1.Controls.Add(this.textBoxKullanıcıAdı);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(27, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kullanıcı";
            // 
            // buttonGİRİŞ
            // 
            this.buttonGİRİŞ.BackColor = System.Drawing.Color.CadetBlue;
            this.buttonGİRİŞ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonGİRİŞ.ForeColor = System.Drawing.Color.Black;
            this.buttonGİRİŞ.Location = new System.Drawing.Point(239, 152);
            this.buttonGİRİŞ.Name = "buttonGİRİŞ";
            this.buttonGİRİŞ.Size = new System.Drawing.Size(68, 35);
            this.buttonGİRİŞ.TabIndex = 4;
            this.buttonGİRİŞ.Text = "Giriş";
            this.buttonGİRİŞ.UseVisualStyleBackColor = false;
            this.buttonGİRİŞ.Click += new System.EventHandler(this.buttonGİRİŞ_Click);
            this.buttonGİRİŞ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonGİRİŞ_MouseDown);
            this.buttonGİRİŞ.MouseLeave += new System.EventHandler(this.buttonGİRİŞ_MouseLeave);
            // 
            // textBoxKullanıcıSoyadı
            // 
            this.textBoxKullanıcıSoyadı.BackColor = System.Drawing.Color.Silver;
            this.textBoxKullanıcıSoyadı.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxKullanıcıSoyadı.Location = new System.Drawing.Point(105, 108);
            this.textBoxKullanıcıSoyadı.Name = "textBoxKullanıcıSoyadı";
            this.textBoxKullanıcıSoyadı.Size = new System.Drawing.Size(202, 29);
            this.textBoxKullanıcıSoyadı.TabIndex = 3;
            // 
            // textBoxKullanıcıAdı
            // 
            this.textBoxKullanıcıAdı.BackColor = System.Drawing.Color.Silver;
            this.textBoxKullanıcıAdı.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxKullanıcıAdı.Location = new System.Drawing.Point(105, 52);
            this.textBoxKullanıcıAdı.Name = "textBoxKullanıcıAdı";
            this.textBoxKullanıcıAdı.Size = new System.Drawing.Size(202, 29);
            this.textBoxKullanıcıAdı.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(19, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Soyadı :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(41, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adı :";
            // 
            // labelTRH
            // 
            this.labelTRH.AutoSize = true;
            this.labelTRH.Location = new System.Drawing.Point(47, 224);
            this.labelTRH.Name = "labelTRH";
            this.labelTRH.Size = new System.Drawing.Size(35, 13);
            this.labelTRH.TabIndex = 1;
            this.labelTRH.Text = "label3";
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonGİRİŞ;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(400, 246);
            this.Controls.Add(this.labelTRH);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Öğrenci Takip Programına Giriş";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGİRİŞ;
        private System.Windows.Forms.TextBox textBoxKullanıcıSoyadı;
        private System.Windows.Forms.TextBox textBoxKullanıcıAdı;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTRH;
    }
}

