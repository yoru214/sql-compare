using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDBCompare.Utils
{
    public class SQLConnector
    {
        private String sql_string;
        private String strcon;
        public String ErrorMessage = "";
        System.Data.SqlClient.SqlDataAdapter da_1;

        public int DebugLevel = 0;

        public String DebugTable = "tblSLType";

        public String[] DebugWrite = new String[4];


        public Int64 InsertProgress = 0;

        public String Host { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Port { get; set; }
        public String Database { get; set; }



        private void setConnectionString()
        {
            this.strcon = "Data Source=" + this.Host + ";User ID=" + this.Username + ";Password=" + this.Password + ";connection timeout=0;Max Pool Size = 999999;Pooling = True;";
            if (this.Database != null)
            {
                this.strcon += "Database=" + this.Database;
            }
        }

        public String getConnectionString()
        {
            return this.strcon;
        }

        public Boolean Connect()
        {
            this.setConnectionString();
            //Console.WriteLine(strcon);
            try
            {
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strcon);
                con.Open();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// SQL Query Command you wish to load.
        /// </summary>
        /// <value>String sql query</value>
        public String Sql
        {
            set
            {
                sql_string = value;
            }
        }
        /// <summary>
        /// Set the Connection String of the SQL Connection
        /// </summary>
        public String ConnectionString { set { strcon = value; } }
        /// <summary>
        /// The inserted Primary Key of your insert statement
        /// Note:This is best for Select statments, for inserts, use the insert function. 
        ///      For statements that has no returns, it is adviced to use the Query function instead.
        /// </summary>
        public int LastInsertID { get; set; }
        /// <summary>
        /// The result of the select statment query
        /// </summary>
        /// <returns>
        /// returns DataSet and to retrieve data, just the Tables[0].Rows
        /// </returns>
        public System.Data.DataSet Results() { return MyDataSet(); }
        /// <summary>
        /// Use the function to query Insert statments to your SQL Database
        /// </summary>
        /// <returns>
        /// Returns the inserted AutoIncremented Primary Key of your Statement
        /// </returns>
        public int Insert() { return InsertFunction(); }
        /// <summary>
        /// Function to query statements that has no return values such as Update and Delete
        /// </summary>
        public void Query() { UpdateFunction(); }



        private System.Data.DataSet MyDataSet()
        {
            System.Data.DataSet dat_set = new System.Data.DataSet();
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strcon))
            {
                con.Open();
                using (da_1 = new System.Data.SqlClient.SqlDataAdapter(sql_string, con))
                {
                    da_1.Fill(dat_set, "Table_Data_1");
                }
            }
            return dat_set;
        }


        private int InsertFunction()
        {
            int newId;
            using (System.Data.SqlClient.SqlConnection thisConnection = new System.Data.SqlClient.SqlConnection(this.strcon))
            {
                using (System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(this.sql_string + ";SELECT CAST(scope_identity() AS int)", thisConnection))
                {
                    thisConnection.Open();
                    sqlCmd.CommandTimeout = 0;
                    newId = (int)sqlCmd.ExecuteScalar();
                    thisConnection.Close();
                }
            }
            this.LastInsertID = newId;
            return newId;
        }
        private Boolean UpdateFunction()
        {
            using (System.Data.SqlClient.SqlConnection thisConnection = new System.Data.SqlClient.SqlConnection(this.strcon))
            {
                using (System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(this.sql_string + ";SELECT CAST(scope_identity() AS int)", thisConnection))
                {
                    thisConnection.Open();
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.ExecuteNonQuery();
                    thisConnection.Close();
                }
            }

            return true;
        }


    }

}
