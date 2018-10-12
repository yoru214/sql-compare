using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDBCompare.Models
{
    public class SQLDBConnection : INotifyPropertyChanged
    {

        String _Host;
        public String Host
        {
            get
            {
                return _Host;
            }
            set
            {
                _Host = value;
                OnPropertyChanged("Host");
            }
        }
        String _Port;
        public String Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
                OnPropertyChanged("Port");
            }
        }
        String _User;
        public String User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }
        String _Password;
        public String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }
        String _Status;
        public String Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }

        List<SQLDatabase> _Database;
        public List<SQLDatabase> Database
        {
            get
            {
                return _Database;
            }
            set
            {
                _Database = value;
                OnPropertyChanged("Database");
            }
        }

        Boolean _Connected;
        public Boolean Connected
        {
            get
            {
                return _Connected;
            }
            set
            {
                _Connected = value;
                OnPropertyChanged("Connected");
            }
        }
        Boolean _Use;
        public Boolean Use
        {
            get
            {
                return _Use;
            }
            set
            {
                _Use = value;
                OnPropertyChanged("Use");
            }
        }
        Boolean _Ready;
        public Boolean Ready
        {
            get
            {
                return _Ready;
            }
            set
            {
                _Ready = value;
                OnPropertyChanged("Ready");
            }
        }


        private String _ConnectionString;
        public String ErrorMessage = "";



        private void setConnectionString(int timeout = 0)
        {
            this._ConnectionString = "Data Source=" + this._Host + ";User ID=" + this._User + ";Password=" + this._Password + ";connection timeout=" + timeout + ";Max Pool Size = 999999;Pooling = True;";
        }
        public Boolean Connect()
        {
            this.setConnectionString(5);
            try
            {
                if(this._Database.FindAll(o => o.IsSelected).Count == 1)
                {
                    this._ConnectionString += "Database=" + this._Database.FirstOrDefault(o => o.IsSelected).Name;
                }

                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(this._ConnectionString);




                con.Open();
                con.Close();

                if(this._Database.Count == 0 || this._Database.FindAll(o=> o.IsSelected).Count == 0)
                {
                    this.SelectDatabases();
                }

                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }

        private void SelectDatabases()
        {
            this._Database = new List<SQLDatabase>();
            String sqlQuery = "SELECT name as 'Name', 0 as 'IsSelected'  FROM sys.databases ORDER BY name ";
            using (System.Data.DataSet dat_set = new System.Data.DataSet())
            {
                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(this._ConnectionString))
                {
                    con.Open();
                    using (System.Data.SqlClient.SqlDataAdapter da_1 = new System.Data.SqlClient.SqlDataAdapter(sqlQuery, con))
                    {
                        da_1.Fill(dat_set, "Table_Data_1");
                    }
                }

                foreach (System.Data.DataRow dRow in dat_set.Tables[0].Rows)
                {
                    this._Database.Add(new SQLDatabase() { Name = dRow["Name"].ToString(), IsSelected = false });
                }
            }
            OnPropertyChanged("Database");
        }

        public String getConnectionString()
        {
            return this._ConnectionString;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
