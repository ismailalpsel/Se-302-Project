using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TReasure_i
{
    static class Program
    {

        public static SQLiteConnection sql_con;

        public static void Connection()
        {
            
                string calismaYolu = Application.StartupPath;
                Program.sql_con = new SQLiteConnection(@"Data Source = " + calismaYolu + @"\db.s3db;Read Only=False");
            
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmTReasurei());
        }
    }
}
