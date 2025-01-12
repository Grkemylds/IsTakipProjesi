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
    public partial class FrmGorevListesi : Form
    {
        public FrmGorevListesi()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmGorevListesi_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGörevler
                                       select new
                                       {
                                           x.ID,
                                           x.Açıklama
                                       }).ToList();
            LblAktifGorevSayisi.Text = db.TblGörevler.Where(x => x.Durum == true).Count().ToString();
            LblPasifGorevSayisi.Text = db.TblGörevler.Where(x => x.Durum == false).Count().ToString();
            LblToplamDepartman.Text = db.TblDepartmanlar.Count().ToString();



            chartControl1.Series["Durum"].Points.AddPoint("Aktif Görevler",int.Parse(LblAktifGorevSayisi.Text));
            chartControl1.Series["Durum"].Points.AddPoint("Pasif Görevler", int.Parse(LblPasifGorevSayisi.Text));
        }
    }
}
