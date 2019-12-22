using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace TReasure_i
{
    public partial class FrmArticles : Form
    {
        int ArticlesId = 0;
        public FrmArticles()
        {
            InitializeComponent();
        }
        public FrmArticles(int Id)
        {
            InitializeComponent();
            ArticlesId = Id;
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
            
            
            private void LoadData()
        {
            Program.Connection();
            Program.sql_con.Open();
            cmd = Program.sql_con.CreateCommand();
            string CommandText = $"SELECT Author,Title,Journal,Year,Volume,Number,Pages,Mounth,Url,Note FROM TblTReasurei Where Id = {ArticlesId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtArAuthor.Text = dr["Author"]?.ToString();
            TxtArTitle.Text = dr["Title"]?.ToString();
            TxtArJournal.Text = dr["Journal"]?.ToString();
            TxtArYear.Text = dr["Year"]?.ToString();
            TxtArNumber.Text = dr["Number"]?.ToString();
            TxtArVolume.Text = dr["Volume"]?.ToString();
            TxtArPages.Text = dr["Pages"]?.ToString();
            TxtArMounth.Text = dr["Mounth"]?.ToString();
            TxtArUrl.Text = dr["Url"]?.ToString();
            TxtArNote.Text = dr["Note"]?.ToString();
        }
        private void BtnArticlesSave_Click(object sender, EventArgs e)
        {
            if (TxtArAuthor.Text == "" | TxtArTitle.Text == "" | TxtArJournal.Text == "" | TxtArYear.Text == "")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string txtquery = "";
                    if (ArticlesId == 0)
                        txtquery = "insert into TblTReasurei(Author,Title,Journal,Year,Volume,Number,Pages,Mounth,Url,Note,TReasureiType) values('" + TxtArAuthor.Text + "','" + TxtArTitle.Text + "','" + TxtArJournal.Text + "','" + TxtArYear.Text + "','" + TxtArVolume.Text + "','" + TxtArNumber.Text + "','" + TxtArPages.Text + "','" + TxtArMounth.Text + "','" + TxtArUrl.Text + "','" + TxtArNote.Text + "','" + lblArticles.Text + "')";
                    else
                        txtquery = $"update TblTreasurei set Author='{TxtArAuthor.Text}',Title='{ TxtArTitle.Text}',Journal='{TxtArJournal.Text}',Number='{TxtArNumber.Text }',Year='{ TxtArYear.Text }',Volume='{TxtArVolume.Text }',Pages='{ TxtArPages.Text }',Mounth='{ TxtArMounth.Text}',Url='{ TxtArUrl.Text }',Note='{ TxtArNote.Text }'  where Id = '{ ArticlesId}'";
                      
                    ExecuteQuery(txtquery);
                    (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                    MessageBox.Show("Sucssesfully recorded!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void BtnArticlesClear_Click(object sender, EventArgs e)
        {
            TxtArAuthor.Text = "";
            TxtArJournal.Text = "";
            TxtArMounth.Text = "";
            TxtArNote.Text = "";
            TxtArNumber.Text = "";
            TxtArPages.Text = "";
            TxtArTitle.Text = "";
            TxtArUrl.Text = "";
            TxtArVolume.Text = "";
            TxtArYear.Text = "";
        }      
        private async void BtnArticlesWriteFile_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd=new SaveFileDialog() { Filter="Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using(StreamWriter sw=new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + lblArticles.Text);
                        await sw.WriteLineAsync("Author={"+TxtArAuthor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtArTitle.Text + "}");
                        await sw.WriteLineAsync("Journal={" + TxtArJournal.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtArYear.Text + "}");
                        await sw.WriteLineAsync("Volume={" + TxtArVolume.Text + "}");
                        await sw.WriteLineAsync("Pages={" + TxtArPages.Text + "}");
                        await sw.WriteLineAsync("Mounth={" + TxtArMounth.Text + "}");
                        await sw.WriteLineAsync("Url={" + TxtArUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtArNote.Text+"}");
                        await sw.WriteLineAsync("}");

                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
     
    }
}
        }
