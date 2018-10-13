using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDBCompare.Models
{
    public class CompareTable : INotifyPropertyChanged
    {
        String _TableName;
        public String TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
                OnPropertyChanged("TableName");
            }

        }
        String _TableName1;
        public String TableName1
        {
            get
            {
                return _TableName1;
            }
            set
            {
                _TableName1 = value;
                OnPropertyChanged("TableName1");
            }

        }

        String _TableName2;
        public String TableName2
        {
            get
            {
                return _TableName2;
            }
            set
            {
                _TableName2 = value;
                OnPropertyChanged("TableName2");
            }

        }

        Boolean _DB1 = false;
        public Boolean DB1
        {
            get
            {
                return _DB1;
            }
            set
            {
                _DB1 = value;
                OnPropertyChanged("DB1");
            }

        }


        Boolean _DB2 = false;
        public Boolean DB2
        {
            get
            {
                return _DB2;
            }
            set
            {
                _DB2 = value;
                OnPropertyChanged("DB2");
            }

        }
        List<CompareFields> _Fields;
        public List<CompareFields> Fields
        {
            get
            {
                return _Fields;
            }
            set
            {
                _Fields = value;
                OnPropertyChanged("Fields");
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
