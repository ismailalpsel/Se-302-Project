using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Data.SQLite;

using System.Drawing;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;

using TReasure_i.Pages;



namespace TReasure_i

{

    public partial class FrmDataBase : Form

    {

        public FrmDataBase()

        {

            InitializeComponent();

        }

        SQLiteCommand cmd;

        SQLiteDataAdapter Da;

        DataSet Ds = new DataSet();

        DataTable Dt = new DataTable();

        

        private void ExecuteQuery(string txtquery)

        {

            Program.Connection();

            Program.sql_con.Open();

            cmd = Program.sql_con.CreateCommand();

            cmd.CommandText = txtquery;

            cmd.ExecuteNonQuery();

            Program.sql_con.Close();

        }

        public void LoadData()

        {

            Program.Connection();

            Program.sql_con.Open();

            cmd = Program.sql_con.CreateCommand();

            string CommandText = "SELECT Id,TReasureiType,Title,Author,Year,BookTitle,Journal FROM TblTReasurei";

            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);

            Ds.Reset();

            Da.Fill(Ds);

            Dt = Ds.Tables[0];

            GrdDataBase.DataSource = Dt;

            Program.sql_con.Close();

        }

        private void FrmDataBase_Load(object sender, EventArgs e)

        {

            LoadData();

        }    

        private void GrdDataBase_MouseDoubleClick(object sender, MouseEventArgs e)

        {

            string TreasureiType = gridView1.GetFocusedRowCellValue("TreasureiType")?.ToString();

            string ID = gridView1.GetFocusedRowCellValue("Id")?.ToString();



            if (TreasureiType != null)

            {

                if (TreasureiType == "Book")

                {

                    Pages.FrmBook frmBook = new Pages.FrmBook(int.Parse(ID));

                    frmBook.ShowDialog();                   

                }

                else if (TreasureiType == "Articles")

                {

                    FrmArticles frmArticles = new FrmArticles(int.Parse(ID));

                    frmArticles.ShowDialog();

                }

                else if (TreasureiType == "Booklet")

                {

                    FrmBooklet  frmBooklet= new FrmBooklet(int.Parse(ID));

                    frmBooklet.ShowDialog();                    

                }

                else if (TreasureiType == "Conferance")

                {

                    FrmConferance frmConferance = new FrmConferance(int.Parse(ID));

                    frmConferance.ShowDialog();

                }

                else if (TreasureiType == "Manual")

                {

                    FrmManual frmManual = new FrmManual(int.Parse(ID));

                    frmManual.ShowDialog();

                }

                else if (TreasureiType == "Master Thesis")

                {

                    FrmMasterThesis frmMasterThesis = new FrmMasterThesis(int.Parse(ID));

                    frmMasterThesis.ShowDialog();

                }

                else

                {

                    FrmPhDThesis frmPhDThesis = new FrmPhDThesis(int.Parse(ID));

                    frmPhDThesis.ShowDialog();

                }

            }

    

        }



        private void BtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)

        {

            string ID = gridView1.GetFocusedRowCellValue("Id")?.ToString();

            string txtdeletequery = $"delete from TblTReasurei where Id={ ID }";

            ExecuteQuery(txtdeletequery);

            LoadData();

        }



        private void GrdDataBase_Click(object sender, EventArgs e)

        {



        }

    }

}
