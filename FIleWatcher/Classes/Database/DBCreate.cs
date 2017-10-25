using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Windows.Forms;
using FileWatcher.Classes.Logging;
using System.IO;

namespace FileWatcher.Classes.Database
{

     class DBCreate
    {
        private static Logger logger = new Logger();
        private static string pfad = logger.GetPath();

        Logger log = new Logger();
        private static string sqlcommand = " create table logger ( Name varchar ( 50 ), Types nvarchar ( 100 ), CreationTime nvarchar ( 100 ), Owner nvarchar ( 100 ), MSGroup nvarchar ( 100 ) )";

        ///<summary>
        ///Erstellt die benötigte SQLLite DB fürs Loggen
        ///</summary>
        public void CreateDatabase ()
        {
            try
            {
                
                
                SQLiteConnection.CreateFile(pfad + @"\logger.db");
                SQLiteConnection m_con = new SQLiteConnection(" Data Source=" + pfad + @"\logger.db" + ";Version=3;");
                SQLiteCommand m_cmd = new SQLiteCommand(sqlcommand, m_con);

                m_con.Open();
                m_cmd.ExecuteNonQuery();
                m_con.Close();

                m_cmd.Dispose();

                MessageBox.Show("Database Creation and Table Creation finished...");



            }
            catch ( Exception ex)
            {
                MessageBox.Show( ex.Message, "Database Creation failed");
            }


      }

        public void InsertMessage ( string message, WatcherChangeTypes types, DateTime CreationTime, string Owner, string MSGroup)
        {
            //string sql = " insert into logger ( Name, Types, CreationTime, Owner, MSGroup ) values ( '" + message + "','" + types + "',' " + CreationTime + "', '" + Owner + "', '" + MSGroup + "');";



            SQLiteConnection con = new SQLiteConnection("Data Source=" + pfad + @"\logger.db");
            SQLiteCommand cmd = new SQLiteCommand(" insert into logger ( Name, Types, CreationTime, Owner, MSGroup ) values (@Name,@Types,@CreationTime,@Owner,@MSGroup)", con);

            string dbtypes = types.ToString();
            cmd.Parameters[0].Value = message;
            cmd.Parameters[1].Value = types;
            cmd.Parameters[2].Value = CreationTime;
            cmd.Parameters[3].Value = Owner;
            cmd.Parameters[4].Value = MSGroup;
/*           
 *          cmd.Parameters.Add(dbtypes, System.Data.DbType.String);
            cmd.Parameters.Add(CreationTime.ToString(), System.Data.DbType.Time);
            cmd.Parameters.Add(Owner, System.Data.DbType.String);
            cmd.Parameters.Add(MSGroup, System.Data.DbType.String);
            cmd.Prepare();

            Gotta hate SQLite >_>

*/

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                con.Close();

            }


        }
    
    }
}
