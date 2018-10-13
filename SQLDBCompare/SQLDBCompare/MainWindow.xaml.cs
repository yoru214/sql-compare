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

        String ErrorMessage = "";
        Boolean hasError = false;

        private readonly System.ComponentModel.BackgroundWorker testDB1 = new System.ComponentModel.BackgroundWorker();
        private readonly System.ComponentModel.BackgroundWorker connectDB1 = new System.ComponentModel.BackgroundWorker();

        private readonly System.ComponentModel.BackgroundWorker testDB2 = new System.ComponentModel.BackgroundWorker();
        private readonly System.ComponentModel.BackgroundWorker connectDB2 = new System.ComponentModel.BackgroundWorker();

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



            testDB1.DoWork += testDB1_DoWork;
            testDB1.RunWorkerCompleted += testDB1_RunWorkerCompleted;
            connectDB1.DoWork += connectDB1_DoWork;
            connectDB1.RunWorkerCompleted += connectDB1_RunWorkerCompleted;

            testDB2.DoWork += testDB2_DoWork;
            testDB2.RunWorkerCompleted += testDB2_RunWorkerCompleted;
            connectDB2.DoWork += connectDB2_DoWork;
            connectDB2.RunWorkerCompleted += connectDB2_RunWorkerCompleted;


            this.DataContext = this.Data;
        }

        private void testConnect_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.IsEnabled = false;
            testDB1.RunWorkerAsync();
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
                try
                {
                    String Selected = ((SQLDatabase)((ComboBox)sender).SelectedItem).Name;
                    this.Data.Database1.Database.ForEach(d => d.IsSelected = false);
                    foreach (var item in this.Data.Database1.Database.Where(w => w.Name == Selected))
                    {
                        item.IsSelected = true;
                        this.Data.Database1.Use = true;
                    }
                }
                catch(Exception ex)
                {

                }
            }


        }

        private void connectDatabase1_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.IsEnabled = false;
            this.Data.Processing = Visibility.Visible;
            this.Data.Compare = false;
            connectDB1.RunWorkerAsync();
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
            mainGrid.IsEnabled = false;
            this.Data.Processing = Visibility.Visible;
            this.Data.Compare = false;
            connectDB2.RunWorkerAsync();


        }

        private void db2testConnect_Click(object sender, RoutedEventArgs e)
        {
            testDB2.RunWorkerAsync();
        }

        private void compareButton_Click(object sender, RoutedEventArgs e)
        {
            this.Data.Processing = Visibility.Visible;
            if (this.Data.CompareButton == "Compare")
            {
                this.Data.CompareButton = "Cancel";
            }
            else
            {
                this.Data.Processing = Visibility.Hidden;
                this.Data.CompareButton = "Compare";
            }
        }

        private void ErrorPrompt()
        {
            if (this.hasError)
            {
                MessageBox.Show(this.ErrorMessage, "CONNECTION ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.hasError = false;
        }



        // worker for Geographical Tab
        private void testDB1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                hasError = true;
                ErrorMessage = this.Data.Database1.ErrorMessage;
            }



        }
        // worker done for Geographical Tab
        private void testDB1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ErrorPrompt();
            this.Data.Processing = Visibility.Hidden;
            mainGrid.IsEnabled = true;
        }

        private void connectDB1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (this.Data.Database1.Connect())
            {
                this.Data.Database1.Use = true;
                this.Data.Database1.Status += "Connected to " + this.Data.Database1.Host + ", " + this.Data.Database1.Port + " [" + this.Data.Database1.Database.FirstOrDefault(o => o.IsSelected).Name + "]";
                this.Data.Database1.Status += Environment.NewLine;
                this.Data.Database1.Use = false;
                this.Data.Database1.Ready = true;

                if (this.Data.Database2.Ready)
                {
                    this.Data.Compare = true;
                }
            }
            else
            {
                hasError = true;
                ErrorMessage = this.Data.Database1.ErrorMessage;
            }
        }
        private void connectDB1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            mainGrid.IsEnabled = true;
            this.Data.Processing = Visibility.Hidden;

        }

        // worker for Geographical Tab
        private void testDB2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                hasError = true;
                ErrorMessage = this.Data.Database2.ErrorMessage;
            }



        }
        // worker done for Geographical Tab
        private void testDB2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ErrorPrompt();
            this.Data.Processing = Visibility.Hidden;
            mainGrid.IsEnabled = true;
        }

        private void connectDB2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
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
                hasError = true;
                ErrorMessage = this.Data.Database2.ErrorMessage;
            }

        }
        private void connectDB2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            mainGrid.IsEnabled = true;
            this.Data.Processing = Visibility.Hidden;
        }
    }
}
