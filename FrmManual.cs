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
