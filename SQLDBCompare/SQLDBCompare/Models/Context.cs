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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

}
