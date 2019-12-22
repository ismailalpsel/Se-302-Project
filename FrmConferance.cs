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
    public partial class FrmConferance : Form
    {
        int conferanceId = 0;
        public FrmConferance()
        {
            InitializeComponent();
        }
        public FrmConferance(int Id)
        {
            InitializeComponent();
            conferanceId = Id;
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
            string CommandText = $"SELECT Author,Title,Publisher,Year,Series,Address,Edition,Volume,Pages,Mounth,Url,Note FROM TblTReasurei Where Id = {conferanceId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtCoAuthor.Text = dr["Author"]?.ToString();
            TxtCoPages.Text = dr["Pages"]?.ToString();
            TxtCoVolume.Text = dr["Volume"]?.ToString();
            TxtCoPublisher.Text = dr["Publisher"]?.ToString();
            TxtCoYear.Text = dr["Year"]?.ToString();
            TxtCoSeries.Text = dr["Series"]?.ToString();
            TxtCoAddress.Text = dr["Address"]?.ToString();                     
            TxtCoUrl.Text = dr["Url"]?.ToString();
            TxtCoTitle.Text = dr["Title"]?.ToString();                   
            TxtCoEdition.Text = dr["Edition"]?.ToString();
            TxtCoMounth.Text = dr["Mounth"]?.ToString();
            TxtCoNote.Text = dr["Note"]?.ToString();
        }
        private void BtnCoSave_Click(object sender, EventArgs e)
        {
            if (TxtCoPublisher.Text == "" | TxtCoAuthor.Text == "" | TxtCoTitle.Text == "" | TxtCoYear.Text == "")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string txtquery = "";
                if (conferanceId == 0)
                    txtquery = "insert into TblTReasurei(Author,Title,Publisher,Year,Series,Address,Edition,Volume,Pages,Mounth,Url,Note,TReasureiType) values('" + TxtCoAuthor.Text + "','" + TxtCoTitle.Text + "','" + TxtCoPublisher.Text + "','" + TxtCoYear.Text + "','" + TxtCoSeries.Text + "','" + TxtCoAddress.Text + "','" + TxtCoEdition.Text + "','" + TxtCoVolume.Text + "','" + TxtCoPages.Text + "','" + TxtCoMounth.Text + "','" + TxtCoUrl.Text + "','" + TxtCoNote.Text + "','" + LblConfrance.Text + "')";
                else
                    txtquery = $"update TblTreasurei set Author='{TxtCoAuthor.Text }',Title='{ TxtCoTitle.Text }',Publisher='{ TxtCoPublisher.Text }',Year='{ TxtCoYear.Text }',Series='{TxtCoSeries.Text}',Address='{TxtCoAddress.Text }',Edition='{TxtCoEdition.Text }',Volume='{ TxtCoVolume.Text }',Pages='{ TxtCoPages.Text }',Mounth='{TxtCoMounth.Text }',Url='{TxtCoUrl.Text}',Note='{TxtCoNote.Text }' where Id = '{conferanceId}' ";
                ExecuteQuery(txtquery);
                (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                MessageBox.Show("Sucssesfully recorded!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnConferanceClear_Click(object sender, EventArgs e)
        {
            TxtCoAddress.Text = "";
            TxtCoAuthor.Text = "";
            TxtCoEdition.Text = "";
            TxtCoMounth.Text = "";
            TxtCoNote.Text = "";
            TxtCoPages.Text = "";
            TxtCoPublisher.Text = "";
            TxtCoSeries.Text = "";
            TxtCoTitle.Text = "";
            TxtCoUrl.Text = "";
            TxtCoVolume.Text = "";
            TxtCoYear.Text = "";
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + LblConfrance.Text);
                        await sw.WriteLineAsync("Author={" + TxtCoAuthor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtCoTitle.Text + "}");
                        await sw.WriteLineAsync("Publisher={" + TxtCoPublisher.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtCoYear.Text + "}");
                        await sw.WriteLineAsync("Series={" + TxtCoSeries.Text + "}");
                        await sw.WriteLineAsync("Address={" + TxtCoAddress.Text + "}");
                        await sw.WriteLineAsync("Pages={" + TxtCoPages.Text + "}");
                        await sw.WriteLineAsync("Mounth={" + TxtCoMounth.Text + "}");
                        await sw.WriteLineAsync("Volume={" + TxtCoVolume.Text + "}");
                        await sw.WriteLineAsync("Edition={" + TxtCoEdition.Text + "}");
                        await sw.WriteLineAsync("Url={" + TxtCoUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtCoNote.Text + "}");
                        await sw.WriteLineAsync("}");
                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
