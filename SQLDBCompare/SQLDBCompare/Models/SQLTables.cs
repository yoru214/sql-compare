using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDBCompare.Models
{
    public class SQLTables : INotifyPropertyChanged
    {
        String _TableName;
        List<SQLFields> _Fields;

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

        public List<SQLFields> Fields
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
