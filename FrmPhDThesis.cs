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
    public partial class FrmPhDThesis : Form
    {
        int phdthesisId = 0;
        public FrmPhDThesis()
        {
            InitializeComponent();
        }
        public FrmPhDThesis(int Id)
        {
            InitializeComponent();
            phdthesisId = Id;
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
            string CommandText = $"SELECT Author,Title,School,Year,Type,Address,Mounth,Url,Note FROM TblTReasurei Where Id = {phdthesisId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtPtAuthor.Text = dr["Author"]?.ToString();
            TxtPtSchool.Text = dr["School"]?.ToString();
            TxtPtYear.Text = dr["Year"]?.ToString();
            TxtPtAddress.Text = dr["Address"]?.ToString();
            TxtPtUrl.Text = dr["Url"]?.ToString();
            TxtPtTitle.Text = dr["Title"]?.ToString();
            TxtPtType.Text = dr["Type"]?.ToString();
            TxtPtMounth.Text = dr["Mounth"]?.ToString();
            TxtPtNote.Text = dr["Note"]?.ToString();
        }
        private void BtnPtSave_Click(object sender, EventArgs e)
        {
            if (TxtPtAuthor.Text == "" | TxtPtTitle.Text == "" | TxtPtSchool.Text == "" | TxtPtYear.Text == "")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string txtquery = "";
                if (phdthesisId == 0)
                    txtquery = "insert into TblTReasurei(Author,Title,School,Year,Type,Address,Mounth,Url,Note,TReasureiType) values('" + TxtPtAuthor.Text + "','" + TxtPtTitle.Text + "','" + TxtPtSchool.Text + "','" + TxtPtYear.Text + "','" + TxtPtType.Text + "','" + TxtPtAddress.Text + "','" + TxtPtMounth.Text + "','" + TxtPtUrl.Text + "','" + TxtPtNote.Text + "','" + LblPhDThesis.Text + "')";
                else
                    txtquery = $"update TblTreasurei set Author='{TxtPtAuthor.Text }',Title='{ TxtPtTitle.Text }',School='{TxtPtSchool.Text }',Year='{ TxtPtYear.Text }',Type='{ TxtPtType.Text }',Address='{TxtPtAddress.Text }',Mounth='{TxtPtMounth.Text }',Url='{ TxtPtUrl.Text }',Note='{ TxtPtNote.Text }' where Id = '{phdthesisId}' ";

      
                ExecuteQuery(txtquery);
                (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                MessageBox.Show("Sucssesfully recorded!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnPtClear_Click(object sender, EventArgs e)
        {
            TxtPtAddress.Text = "";
            TxtPtAuthor.Text = "";
            TxtPtMounth.Text = "";
            TxtPtNote.Text = "";
            TxtPtSchool.Text = "";
            TxtPtTitle.Text = "";
            TxtPtType.Text = "";
            TxtPtUrl.Text = "";
            TxtPtYear.Text = "";
        }
        private async void BtnPtWriteFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + LblPhDThesis.Text);
                        await sw.WriteLineAsync("Author={" + TxtPtAuthor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtPtTitle.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtPtYear.Text + "}");
                        await sw.WriteLineAsync("School={" + TxtPtSchool.Text + "}");
                        await sw.WriteLineAsync("Type={" + TxtPtType.Text + "}");
                        await sw.WriteLineAsync("Address={" + TxtPtAddress.Text + "}");
                        await sw.WriteLineAsync("Mounth={" + TxtPtMounth.Text + "}");
                        await sw.WriteLineAsync("Url={" + TxtPtUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtPtNote.Text + "}");
                        await sw.WriteLineAsync("}");
                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
