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
    public partial class FrmBooklet : Form
    {
        int bookletId = 0;
        public FrmBooklet()
        {
            InitializeComponent();
        }
        public FrmBooklet(int Id)
        {
            InitializeComponent();
            bookletId = Id;
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
            Program.sql_con.Close(); ;
        }
        private void LoadData()
        {
            Program.Connection();
            Program.sql_con.Open();
            cmd = Program.sql_con.CreateCommand();
            string CommandText = $"SELECT Author,Title,HowPublished,Year,Address,Mounth,Url,Note FROM TblTReasurei Where Id = {bookletId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtBookletAddress.Text = dr["Address"]?.ToString();
            TxtBookletAuthor.Text = dr["Author"]?.ToString();
            TxtBookletHowPublished.Text = dr["HowPublished"]?.ToString();
            TxtBookletMounth.Text = dr["Mounth"]?.ToString();
            TxtBookletNote.Text = dr["Note"]?.ToString();
            TxtBookletTitle.Text = dr["Title"]?.ToString();
            TxtBookletUrl.Text = dr["Url"]?.ToString();
            TxtBookletYear.Text = dr["Year"]?.ToString();
        }
        private void BtnBookletSave_Click(object sender, EventArgs e)
        {
            if (TxtBookletHowPublished.Text == "" | TxtBookletTitle.Text == "")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string txtquery = "";
                if (bookletId == 0)
                    txtquery = "insert into TblTReasurei(Author,Title,HowPublished,Year,Address,Mounth,Url,Note,TReasureiType) values('" + TxtBookletAuthor.Text + "','" + TxtBookletTitle.Text + "','" + TxtBookletHowPublished.Text + "','" + TxtBookletYear.Text + "','" + TxtBookletAddress.Text + "','" + TxtBookletMounth.Text + "','" + TxtBookletUrl.Text + "','" + TxtBookletNote.Text + "','" + LblBooklet.Text + "')";
                else
                    txtquery = $"update TblTreasurei set Author='{ TxtBookletAuthor.Text }',Title='{ TxtBookletTitle.Text }',HowPublished='{ TxtBookletHowPublished.Text }',Year='{ TxtBookletYear.Text }',Address='{TxtBookletAddress.Text }',Mounth='{TxtBookletMounth.Text }',Url='{TxtBookletUrl.Text }',Note='{TxtBookletNote.Text }' where Id = '{bookletId} '";

                ExecuteQuery(txtquery);
                (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                MessageBox.Show("Sucssesfully recorded!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnBookletClear_Click(object sender, EventArgs e)
        {
            TxtBookletAddress.Text = "";
            TxtBookletAuthor.Text = "";
            TxtBookletHowPublished.Text = "";
            TxtBookletMounth.Text = "";
            TxtBookletNote.Text = "";
            TxtBookletTitle.Text = "";
            TxtBookletUrl.Text = "";
            TxtBookletYear.Text = "";
        }
        private async void BtnBookletWriteFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + LblBooklet.Text);
                        await sw.WriteLineAsync("Author={" + TxtBookletAuthor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtBookletTitle.Text + "}");
                        await sw.WriteLineAsync("HowPublished={" + TxtBookletHowPublished.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtBookletYear.Text + "}");
                        await sw.WriteLineAsync("Volume={" + TxtBookletMounth.Text + "}");
                        await sw.WriteLineAsync("Address={" + TxtBookletAddress.Text + "}");
                        await sw.WriteLineAsync("Url={" + TxtBookletUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtBookletNote.Text + "}");
                        await sw.WriteLineAsync("}");
                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
