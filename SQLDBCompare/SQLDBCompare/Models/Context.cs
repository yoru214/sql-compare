using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

}
