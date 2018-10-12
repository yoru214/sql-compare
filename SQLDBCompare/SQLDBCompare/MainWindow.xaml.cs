using SQLDBCompare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLDBCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context Data = new Context();
        String DB1Status = "";
        public MainWindow()
        {
            InitializeComponent();

            this.Data.Database1.Port = "1433";
            this.Data.Database2.Port = "1433";

            this.Data.Database1.Status = "";
            this.Data.Database2.Status = "";

            this.Data.Compare = false;
            this.Data.CompareButton = "Compare";
            this.Data.Processing = Visibility.Hidden;
            


            this.DataContext = this.Data;
        }

        private void testConnect_Click(object sender, RoutedEventArgs e)
        {
            this.Data.Processing = Visibility.Visible;
            this.Data.Compare = false;

            this.Data.Database1.Database = new List<SQLDatabase>();
            this.Data.Database1.Use = false;
            this.Data.Database1.Connected = false;
            if (this.Data.Database1.Connect() == true)
            {
                this.Data.Database1.Connected = true;
                
            }
            else
            {
                MessageBox.Show(this.Data.Database1.ErrorMessage, "CONNECTION ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Data.Processing = Visibility.Hidden;
        }

        private void db1Password_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            this.Data.Database1.Password = ((PasswordBox)sender).Password;
        }

        private void db1UseDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Data.Compare = false;
            if (this.Data.Database1.Database.Count() > 0)
            {
                String Selected = ((SQLDatabase)((ComboBox)sender).SelectedItem).Name;
                this.Data.Database1.Database.ForEach(d => d.IsSelected = false);
                foreach (var item in this.Data.Database1.Database.Where(w => w.Name == Selected))
                {
                    item.IsSelected = true;
                    this.Data.Database1.Use = true;
                }
            }


        }

        private void connectDatabase1_Click(object sender, RoutedEventArgs e)
        {
            this.Data.Processing = Visibility.Visible;
            this.Data.Compare = false;
            if (this.Data.Database1.Connect())
            {
                this.Data.Database1.Use = true;
                this.Data.Database1.Status += "Connected to " + this.Data.Database1.Host + ", " + this.Data.Database1.Port + " [" + this.Data.Database1.Database.FirstOrDefault(o => o.IsSelected).Name + "]";
                this.Data.Database1.Status += Environment.NewLine;
                this.Data.Database1.Use = false;
                this.Data.Database1.Ready = true;

                if(this.Data.Database2.Ready)
                {
                    this.Data.Compare = true;
                }
            }
            else
            {
                MessageBox.Show(this.Data.Database1.ErrorMessage, "CONNECTION ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Data.Processing = Visibility.Hidden;
        }

        private void db2Password_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            this.Data.Database2.Password = ((PasswordBox)sender).Password;
        }

        private void db2UseDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Data.Compare = false;

            if (this.Data.Database2.Database.Count() > 0)
            {
                String Selected = ((SQLDatabase)((ComboBox)sender).SelectedItem).Name;
                this.Data.Database2.Database.ForEach(d => d.IsSelected = false);
                foreach (var item in this.Data.Database2.Database.Where(w => w.Name == Selected))
                {
                    item.IsSelected = true;
                    this.Data.Database2.Use = true;
                }
            }
        }

        private void connectDatabase2_Click(object sender, RoutedEventArgs e)
        {
            this.Data.Processing = Visibility.Visible;
            this.Data.Compare = false;
            if (this.Data.Database2.Connect())
            {
                this.Data.Database2.Use = true;
                this.Data.Database2.Status += "Connected to " + this.Data.Database2.Host + ", " + this.Data.Database2.Port + " [" + this.Data.Database2.Database.FirstOrDefault(o => o.IsSelected).Name + "]";
                this.Data.Database2.Status += Environment.NewLine;
                this.Data.Database2.Use = false;
                this.Data.Database2.Ready = true;

                if (this.Data.Database1.Ready)
                {
                    this.Data.Compare = true;
                }
            }
            else
            {
                MessageBox.Show(this.Data.Database2.ErrorMessage, "CONNECTION ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Data.Processing = Visibility.Hidden;

        }

        private void db2testConnect_Click(object sender, RoutedEventArgs e)
        {

            this.Data.Processing = Visibility.Visible;
            this.Data.Compare = false;

            this.Data.Database2.Database = new List<SQLDatabase>();
            this.Data.Database2.Use = false;
            this.Data.Database2.Connected = false;
            if (this.Data.Database2.Connect() == true)
            {
                this.Data.Database2.Connected = true;

            }
            else
            {
                MessageBox.Show(this.Data.Database2.ErrorMessage, "CONNECTION ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Data.Processing = Visibility.Hidden;
        }

        private void compareButton_Click(object sender, RoutedEventArgs e)
        {
            this.Data.Processing = Visibility.Visible;
            if(this.Data.CompareButton == "Compare")
            {
                this.Data.CompareButton = "Cancel";
            }
            else
            {
                this.Data.Processing = Visibility.Hidden;
                this.Data.CompareButton = "Compare";
            }
        }
    }
}
