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

        public List<SQLDatabase> Database { get; set; }

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



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
