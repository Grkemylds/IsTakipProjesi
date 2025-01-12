using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using is_takip_projesi.Entity;

namespace is_takip_projesi.Formlar
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();

        private void FrmAnaForm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGörevler
                                       select new
                                       {
                                           x.Açıklama,
                                           Personel = x.TblPersonel.Ad + " " + x.TblPersonel.Soyad,
                                           x.Durum
                                       }).Where(y => y.Durum == true).ToList();
            gridView1.Columns["Durum"].Visible = false;

            //Bugün Yapılan Görevler
            DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString());

            gridControl2.DataSource = (from x in db.TblGorevDetaylar
                                       select new
                                       {
                                           gorev = x.TblGörevler.Açıklama,
                                           x.Açıklama,
                                           x.Tarih
                                       }).Where(x => x.Tarih == bugun).ToList();
            // Aktif çağrı listesi
            gridControl3.DataSource = (from x in db.TblCagrilar
                                       select new
                                       {
                                           x.TblFirmalar.Ad,
                                           x.Konu,
                                           x.Tarih,
                                           x.Durum
                                       }).Where(x => x.Durum == true).ToList();
            gridView3.Columns["Durum"].Visible = false;

            //Fihrist Komutları
            gridControl5.DataSource = (from x in db.TblFirmalar
                                       select new
                                       {
                                           x.Ad,
                                           x.Telefon,
                                           x.Mail
                                       }).ToList();
            //Çağrı Grafikleri


            int Aktif_cagrılar = db.TblCagrilar.Where(x => x.Durum == true).Count();
            int Pasif_cagrılar = db.TblCagrilar.Where(x => x.Durum == false).Count();

            chartControl1.Series["Series 1"].Points.AddPoint("Aktif Çağrılar", Aktif_cagrılar);
            chartControl1.Series["Series 1"].Points.AddPoint("Pasif Çağrılar", Pasif_cagrılar);

        }
    }
}
