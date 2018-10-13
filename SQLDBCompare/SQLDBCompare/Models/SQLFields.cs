using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDBCompare.Models
{
    public class SQLFields : INotifyPropertyChanged
    {
        String _FieldName;
        String _FieldType;
        String _FieldLength;
        String _Nullable;


        public String FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
                OnPropertyChanged("FieldName");
            }
        }

        public String FieldType
        {
            get
            {
                return _FieldType;
            }
            set
            {
                _FieldType = value;
                OnPropertyChanged("FieldType");
            }
        }

        public String FieldLength
        {
            get
            {
                return _FieldLength;
            }
            set
            {
                _FieldLength = value;
                OnPropertyChanged("FieldLength");
            }
        }

        public String IsNullable
        {
            get
            {
                return _Nullable;
            }
            set
            {
                _Nullable = value;
                OnPropertyChanged("IsNullable");
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
