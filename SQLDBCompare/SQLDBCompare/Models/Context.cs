using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLDBCompare.Models
{
    public class Context : INotifyPropertyChanged
    {

        SQLDBConnection _Database1 = new SQLDBConnection();
        public SQLDBConnection Database1
        {
            get
            {
                return _Database1;
            }
            set
            {
                _Database1 = value;
                OnPropertyChanged("Database1");
            }
        }

        SQLDBConnection _Database2 = new SQLDBConnection();
        public SQLDBConnection Database2
        {
            get
            {
                return _Database2;
            }
            set
            {
                _Database2 = value;
                OnPropertyChanged("Database2");
            }
        }

        List<SQLTables> _DB1Tables = new List<SQLTables>();
        public List<SQLTables> DB1Tables
        {
            get
            {
                return _DB1Tables;
            }
            set
            {
                _DB1Tables = value;
                OnPropertyChanged("DB1Tables");
            }
        }

        List<SQLTables> _DB2Tables = new List<SQLTables>();
        public List<SQLTables> DB2Tables
        {
            get
            {
                return _DB2Tables;
            }
            set
            {
                _DB2Tables = value;
                OnPropertyChanged("DB2Tables");
            }
        }


        List<CompareTable> _CompareTables = new List<CompareTable>();
        public List<CompareTable> CompareTables
        {
            get
            {
                return _CompareTables;
            }
            set
            {
                _CompareTables = value;
                OnPropertyChanged("CompareTables");
            }
        }

        Boolean _Compare;
        public Boolean Compare
        {
            get
            {
                return _Compare;
            }
            set
            {
                _Compare = value;
                OnPropertyChanged("Compare");
            }

        }
        
        String _CompareButton;
        public String CompareButton
        {
            get
            {
                return _CompareButton;
            }
            set
            {
                _CompareButton = value;
                OnPropertyChanged("CompareButton");
            }

        }

        Visibility _Processing;
        public Visibility Processing
        {
            get
            {
                return _Processing;
            }
            set
            {
                _Processing = value;
                OnPropertyChanged("Processing");
            }

        }

        Boolean _CompareTab;
        public Boolean CompareTab
        {
            get
            {
                return _CompareTab;
            }
            set
            {
                _CompareTab = value;
                OnPropertyChanged("CompareTab");
            }

        }

        String _DB1Details;
        public String DB1Details
        {
            get
            {
                return _DB1Details;
            }
            set
            {
                _DB1Details = value;
                OnPropertyChanged("DB1Details");
            }

        }


        String _DB2Details;
        public String DB2Details
        {
            get
            {
                return _DB2Details;
            }
            set
            {
                _DB2Details = value;
                OnPropertyChanged("DB2Details");
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
