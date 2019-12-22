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
    public partial class FrmManual : Form
    {
        int manualId = 0;

        public FrmManual()
        {
            InitializeComponent();
        }
        public FrmManual(int Id)
        {
            InitializeComponent();
            manualId = Id;
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
            string CommandText = $"SELECT Author,Title,Year,Address,Edition,Organization,Mounth,Url,Note FROM TblTReasurei Where Id = {manualId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtMaAurhor.Text = dr["Author"]?.ToString();
            TxtMaYear.Text = dr["Year"]?.ToString();
            TxtMaAddress.Text = dr["Address"]?.ToString();
            TxtMaOrganization.Text = dr["Organization"]?.ToString();
            TxtMaUrl.Text = dr["Url"]?.ToString();
            TxtMaTitle.Text = dr["Title"]?.ToString();
            TxtMaEdition.Text = dr["Edition"]?.ToString();
            TxtMaMounth.Text = dr["Mounth"]?.ToString();
            TxtMaNote.Text = dr["Note"]?.ToString();
        }
        private void BtnMaSave_Click(object sender, EventArgs e)
        {
            if (TxtMaTitle.Text == "")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string txtquery = "";
                if (manualId == 0)
                    txtquery = "insert into TblTReasurei(Author,Title,Year,Address,Edition,Organization,Mounth,Url,Note,TReasureiType) values('" + TxtMaAurhor.Text + "','" + TxtMaTitle.Text + "','" + TxtMaYear.Text + "','" + TxtMaAddress.Text + "','" + TxtMaEdition.Text + "','" + TxtMaOrganization.Text + "','" + TxtMaMounth.Text + "','" + TxtMaUrl.Text + "','" + TxtMaNote.Text + "','" + LblManual.Text + "')";
                else
                    txtquery = $"update TblTreasurei set Author='{TxtMaAurhor.Text }',Title='{ TxtMaTitle.Text }',Year='{ TxtMaYear.Text }',Address='{ TxtMaAddress.Text }',Edition='{ TxtMaEdition.Text }',Organization='{TxtMaOrganization.Text }',Mounth='{ TxtMaMounth.Text }',Url='{TxtMaUrl.Text }',Note='{TxtMaNote.Text }' where Id = '{manualId}'"; ;
                ExecuteQuery(txtquery);
                (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                MessageBox.Show("Sucssesfully recorded!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnManualClear_Click(object sender, EventArgs e)
        {
            TxtMaAddress.Text = "";
            TxtMaAurhor.Text = "";
            TxtMaEdition.Text = "";
            TxtMaMounth.Text = "";
            TxtMaNote.Text = "";
            TxtMaOrganization.Text = "";
            TxtMaTitle.Text = "";
            TxtMaUrl.Text = "";
            TxtMaYear.Text = "";
        }
        private async void BtnManualWriteFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + LblManual.Text);
                        await sw.WriteLineAsync("Author={" + TxtMaAurhor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtMaTitle.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtMaYear.Text + "}");
                        await sw.WriteLineAsync("Organization={" + TxtMaOrganization.Text + "}");
                        await sw.WriteLineAsync("Address={" + TxtMaAddress.Text + "}");
                        await sw.WriteLineAsync("Mounth={" + TxtMaMounth.Text + "}");
                        await sw.WriteLineAsync("Url={" + TxtMaUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtMaNote.Text + "}");
                        await sw.WriteLineAsync("}");
                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
