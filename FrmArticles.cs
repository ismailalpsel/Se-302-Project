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
        }
