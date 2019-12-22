using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TReasure_i.Pages
{
    public partial class FrmMasterThesis : Form
    {
        int masterthesisId = 0;
        public FrmMasterThesis()
        {
            InitializeComponent();
        }
        public FrmMasterThesis(int Id)
        {
            InitializeComponent();
            masterthesisId = Id;
            LoadData();
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
        private void LoadData()
        {
            Program.Connection();
            Program.sql_con.Open();
            cmd = Program.sql_con.CreateCommand();
            string CommandText = $"SELECT Author,Title,School,Year,Type,Address,Mounth,Url,Note FROM TblTReasurei Where Id = {masterthesisId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtMtAuthor.Text = dr["Author"]?.ToString();
            TxtMtSchool.Text = dr["School"]?.ToString();
            TxtMtYear.Text = dr["Year"]?.ToString();
            TxtMtAddress.Text = dr["Address"]?.ToString();
            TxtMtUrl.Text = dr["Url"]?.ToString();
            TxtMtTitle.Text = dr["Title"]?.ToString();
            TxtMtType.Text = dr["Type"]?.ToString();
            TxtMtMounth.Text = dr["Mounth"]?.ToString();
            TxtMtNote.Text = dr["Note"]?.ToString();
        }
        private void BtnMtSave_Click(object sender, EventArgs e)
        {
            if (TxtMtAuthor.Text == "" | TxtMtTitle.Text == "" | TxtMtSchool.Text == "" | TxtMtYear.Text == "")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string txtquery = "";
                if (masterthesisId == 0)
                    txtquery = "insert into TblTReasurei(Author,Title,School,Year,Type,Address,Mounth,Url,Note,TReasureiType) values('" + TxtMtAuthor.Text + "','" + TxtMtTitle.Text + "','" + TxtMtSchool.Text + "','" + TxtMtYear.Text + "','" + TxtMtType.Text + "','" + TxtMtAddress.Text + "','" + TxtMtMounth.Text + "','" + TxtMtUrl.Text + "','" + TxtMtNote.Text + "','" + LblMasterThesis.Text + "')";
                else                 
                      txtquery = $"update TblTreasurei set Author='{TxtMtAuthor.Text }',Title='{ TxtMtTitle.Text }',School='{TxtMtSchool.Text }',Year='{ TxtMtYear.Text }',Type='{ TxtMtType.Text }',Address='{TxtMtAddress.Text }',Mounth='{TxtMtMounth.Text }',Url='{ TxtMtUrl.Text }',Note='{ TxtMtNote.Text }' where Id = '{masterthesisId}' ";

                ExecuteQuery(txtquery);
                (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                MessageBox.Show("Sucssesfully recorded!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnMtClear_Click(object sender, EventArgs e)
        {
            TxtMtAddress.Text = "";
            TxtMtAuthor.Text = "";
            TxtMtMounth.Text = "";
            TxtMtNote.Text = "";
            TxtMtSchool.Text = "";
            TxtMtTitle.Text = "";
            TxtMtType.Text = "";
            TxtMtUrl.Text = "";
            TxtMtYear.Text = "";
        }
        private async void BtnMtWriteFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + LblMasterThesis.Text);
                        await sw.WriteLineAsync("Author={" + TxtMtAuthor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtMtTitle.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtMtYear.Text + "}");
                        await sw.WriteLineAsync("School={" + TxtMtSchool.Text + "}");
                        await sw.WriteLineAsync("Type={" + TxtMtType.Text + "}");
                        await sw.WriteLineAsync("Address={" + TxtMtAddress.Text + "}");
                        await sw.WriteLineAsync("Mounth={" + TxtMtMounth.Text + "}");
                        await sw.WriteLineAsync("Url={" + TxtMtUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtMtNote.Text + "}");
                        await sw.WriteLineAsync("}");
                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

       
    }
}
