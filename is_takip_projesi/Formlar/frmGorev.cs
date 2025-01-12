using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using is_takip_projesi.Entity;

namespace is_takip_projesi.Formlar
{
    public partial class frmGorev : Form
    {
        public frmGorev()
        {
            InitializeComponent();
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void BtnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DbisTakipEntities db = new DbisTakipEntities();

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TblGörevler t = new TblGörevler();
            t.Açıklama = TxtAcıklama.Text;
            t.Durum = true;
            t.GörevAlan=int.Parse(lookUpEdit1.EditValue.ToString());
            t.Tarih = DateTime.Parse(TxtTarih.Text);
            t.GörevVeren = int.Parse(TxtGorevVeren.Text);
            db.TblGörevler.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Görev Tanımlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmGorev_Load(object sender, EventArgs e)
        {
            var degerler = (from x in db.TblPersonel
                                select new
                                {
                                    x.ID,
                                    adsoyad=x.Ad +" "+ x.Soyad
                                }).ToList();
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "adsoyad";
            lookUpEdit1.Properties.DataSource = degerler;
        }
    }
}
