using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TReasure_i.Pages;

namespace TReasure_i
{
    public partial class FrmTReasurei : Form
    {
        public FrmTReasurei()
        {
            InitializeComponent();
        }

        FrmArticles frmArticles;
        FrmDataBase frmDataBase;
        FrmBook frmBook;
        FrmBooklet frmBooklet;
        FrmConferance frmConferance;
        FrmManual frmManual;
        FrmMasterThesis frmMasterThesis;
        FrmPhDThesis frmPhDThesis;

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
                frmArticles = new FrmArticles();
                frmArticles.MdiParent = this;
                frmArticles.Show();            
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDataBase = new FrmDataBase();
            frmDataBase.MdiParent = this;
            frmDataBase.Show();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBook = new FrmBook();
            frmBook.MdiParent = this;
            frmBook.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBooklet = new FrmBooklet();
            frmBooklet.MdiParent = this;
            frmBooklet.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmConferance = new FrmConferance();
            frmConferance.MdiParent = this;
            frmConferance.Show();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmManual = new FrmManual();
            frmManual.MdiParent = this;
            frmManual.Show();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmMasterThesis = new FrmMasterThesis();
            frmMasterThesis.MdiParent = this;
            frmMasterThesis.Show();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhDThesis = new FrmPhDThesis();
            frmPhDThesis.MdiParent = this;
            frmPhDThesis.Show();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmArticles = new FrmArticles();
            frmArticles.Show();
        }
        public void ReflestList()
        {
            foreach (var frm in xtraTabbedMdiManager1.Pages.ToList().Where(w => w.MdiChild.Name == "FrmDataBase").ToList())
            {
                (frm.MdiChild as FrmDataBase).LoadData();
            }
        }
    }
}
