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
    public partial class FrmBook : Form
    {
        int bookId = 0;
        public FrmBook()
        {
            InitializeComponent();
        }
        public FrmBook(int Id)
        {
            InitializeComponent();
            bookId = Id;
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
            string CommandText = $"SELECT Author,Title,Publisher,Year,Series,Address,Edition,BookTitle,Volume,Pages,Organization,Mounth,Url,Note FROM TblTReasurei Where Id = {bookId}";
            Da = new SQLiteDataAdapter(CommandText, Program.sql_con);
            Ds.Reset();
            Da.Fill(Ds);
            DataRow dr = Ds.Tables[0].Rows[0];
            Program.sql_con.Close();
            TxtBoAuthor.Text = dr["Author"]?.ToString();
            TxtBoPages.Text = dr["Pages"]?.ToString();
            TxtBoVolume.Text = dr["Volume"]?.ToString();
            TxtBoPublisher.Text = dr["Publisher"]?.ToString();
            TxtBoYear.Text = dr["Year"]?.ToString();
            TxtBoSeries.Text = dr["Series"]?.ToString();
            TxtBoAddress.Text = dr["Address"]?.ToString();
            TxtBoOrganization.Text = dr["Organization"]?.ToString();           
            TxtBoUrl.Text = dr["Url"]?.ToString();
            TxtBoTitle.Text = dr["Title"]?.ToString();         
            TxtBoBookTitle.Text = dr["BookTitle"]?.ToString();
            TxtBoEdition.Text = dr["Edition"]?.ToString();
            TxtBoMounth.Text = dr["Mounth"]?.ToString();
            TxtBoNote.Text = dr["Note"]?.ToString();
        }
        private void BtnBookSave_Click(object sender, EventArgs e)
        {
            if (TxtBoAuthor.Text == "" | TxtBoBookTitle.Text == "" |TxtBoTitle.Text==""|TxtBoYear.Text=="")
            {
                MessageBox.Show("Please fill in the exclamation points (!)", "WARNİNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                string txtquery = "";
                if (bookId == 0)
                    txtquery = "insert into TblTReasurei(Author,Title,Publisher,Year,Series,Address,Edition,BookTitle,Volume,Pages,Organization,Mounth,Url,Note,TReasureiType) values('" + TxtBoAuthor.Text + "','" + TxtBoTitle.Text + "','" + TxtBoPublisher.Text + "','" + TxtBoYear.Text + "','" + TxtBoSeries.Text + "','" + TxtBoAddress.Text + "','" + TxtBoEdition.Text + "','" + TxtBoBookTitle.Text + "','" + TxtBoVolume.Text + "','" + TxtBoPages.Text + "','" + TxtBoOrganization.Text + "','" + TxtBoMounth.Text + "','" + TxtBoUrl.Text + "','" + TxtBoNote.Text + "','" + LblBook.Text + "')";
                else
                    txtquery = $"update TblTreasurei set Author=' {TxtBoAuthor.Text }',Title=' {TxtBoBookTitle.Text }',Publisher='{ TxtBoPublisher.Text }',Year='{ TxtBoYear.Text }',Series='{ TxtBoSeries.Text }',Address='{ TxtBoAddress.Text} ',Edition='{ TxtBoEdition.Text }',BookTitle='{ TxtBoBookTitle.Text }',Volume='{ TxtBoVolume.Text}',Pages='{ TxtBoPages.Text }',Organization='{ TxtBoOrganization.Text}',Mounth='{ TxtBoMounth.Text }',Url='{TxtBoUrl.Text }',Note='{ TxtBoNote.Text }' where Id = '{bookId}'";
                
                ExecuteQuery(txtquery);

                (Application.OpenForms["FrmTReasurei"] as FrmTReasurei).ReflestList();
                MessageBox.Show("Sucssesfully recorded!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBookClear_Click(object sender, EventArgs e)
        {
            TxtBoAddress.Text = "";
            TxtBoAuthor.Text = "";
            TxtBoBookTitle.Text = "";
            TxtBoEdition.Text = "";
            TxtBoMounth.Text = "";
            TxtBoNote.Text = "";
            TxtBoOrganization.Text = "";
            TxtBoPages.Text = "";
            TxtBoPublisher.Text = "";
            TxtBoSeries.Text = "";
            TxtBoTitle.Text = "";
            TxtBoUrl.Text = "";
            TxtBoVolume.Text = "";
            TxtBoYear.Text = "";

        }

        private async void BtnBookWriteFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.bib", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        await sw.WriteLineAsync("Type={@" + LblBook.Text);
                        await sw.WriteLineAsync("Author={" + TxtBoAuthor.Text + "}");
                        await sw.WriteLineAsync("Title={" + TxtBoBookTitle.Text + "}");
                        await sw.WriteLineAsync("Publisher={" + TxtBoPublisher.Text + "}");
                        await sw.WriteLineAsync("Year={" + TxtBoYear.Text + "}");
                        await sw.WriteLineAsync("Series={" + TxtBoSeries.Text + "}");
                        await sw.WriteLineAsync("Address={" + TxtBoAddress.Text + "}");
                        await sw.WriteLineAsync("Edition={" + TxtBoEdition.Text + "}");
                        await sw.WriteLineAsync("Book Title={" + TxtBoBookTitle.Text + "}");
                        await sw.WriteLineAsync("Volume= {" + TxtBoVolume.Text + "}");
                        await sw.WriteLineAsync("Pages= {" + TxtBoPages.Text + "}");
                        await sw.WriteLineAsync("Organization= {" + TxtBoOrganization.Text + "}");
                        await sw.WriteLineAsync("Mounth= {" + TxtBoMounth.Text + "}");
                        await sw.WriteLineAsync("Url= {" + TxtBoUrl.Text + "}");
                        await sw.WriteLineAsync("Note= {" + TxtBoNote.Text + "}");
                        await sw.WriteLineAsync("}");

                        MessageBox.Show("You have been succesfully writed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}

