using SQLDBCompare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private readonly System.ComponentModel.BackgroundWorker db1Schema = new System.ComponentModel.BackgroundWorker();
        private readonly System.ComponentModel.BackgroundWorker db2Schema = new System.ComponentModel.BackgroundWorker();


        private readonly System.ComponentModel.BackgroundWorker compareDB = new System.ComponentModel.BackgroundWorker();


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
            db1Schema.DoWork += db1Schema_DoWork;
            db1Schema.RunWorkerCompleted += db1Schema_RunWorkerCompleted;
            db1Schema.WorkerSupportsCancellation = true;
            db2Schema.DoWork += db2Schema_DoWork;
            db2Schema.RunWorkerCompleted += db2Schema_RunWorkerCompleted;
            db2Schema.WorkerSupportsCancellation = true;


            compareDB.DoWork += compareDB_DoWork;
            compareDB.RunWorkerCompleted += compareDB_RunWorkerCompleted;
            compareDB.WorkerSupportsCancellation = true;

            testDB2.DoWork += testDB2_DoWork;
            testDB2.RunWorkerCompleted += testDB2_RunWorkerCompleted;
            connectDB2.DoWork += connectDB2_DoWork;
            connectDB2.RunWorkerCompleted += connectDB2_RunWorkerCompleted;


            db1CompareGrid.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(onChanged));


            this.DataContext = this.Data;
        }
        private void onChanged(object sender, ScrollChangedEventArgs e)
        {
            var v = e.VerticalOffset;

            //db2CompareGrid.ScrollViewer.ScrollToVerticalOffset(v); 
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
                db1Schema.RunWorkerAsync();
                db2Schema.RunWorkerAsync();
            }
            else
            {
                db1Schema.CancelAsync();
                this.Data.CompareButton = "Canceling...";
                this.Data.Compare = false;
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
                this.Data.DB1Details = " " + this.Data.Database1.Host + ", " + this.Data.Database1.Port + " [" + this.Data.Database1.Database.FirstOrDefault(o => o.IsSelected).Name + "]";

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

        private void db1Schema_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Data.DataSet dat_set = this.Data.Database1.getTables();


            foreach (System.Data.DataRow dRow in dat_set.Tables[0].Rows)
            {
                if (db1Schema.CancellationPending )
                {
                    e.Cancel = true;
                    break;
                }
                SQLTables table = new SQLTables()
                {
                    TableName = dRow["TableName"].ToString(),
                    Fields = this.Data.Database1.getFields(dRow["TableName"].ToString())
                };
                this.Data.DB1Tables.Add(table);
            }
           
        }
        private void db1Schema_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Data.Database1.Status += "Schema Loaded Successfully.";
            this.Data.Database1.Status += Environment.NewLine;
            if (!db2Schema.IsBusy)
            {
                if (!compareDB.IsBusy)
                {
                    compareDB.RunWorkerAsync();
                }
            }
            //this.Data.Processing = Visibility.Hidden;
            //this.Data.CompareButton = "Compare";
            //this.Data.Compare = true;
            //MessageBox.Show("Comparison Complete","SUCCESS!",MessageBoxButton.OK,MessageBoxImage.Information);

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

                this.Data.DB2Details = " " +  this.Data.Database2.Host + ", " + this.Data.Database2.Port + " [" + this.Data.Database2.Database.FirstOrDefault(o => o.IsSelected).Name + "]";

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

        private void db2Schema_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Data.DataSet dat_set = this.Data.Database2.getTables();


            foreach (System.Data.DataRow dRow in dat_set.Tables[0].Rows)
            {
                if (db1Schema.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                SQLTables table = new SQLTables()
                {
                    TableName = dRow["TableName"].ToString(),
                    Fields = this.Data.Database2.getFields(dRow["TableName"].ToString())
                };
                this.Data.DB2Tables.Add(table);
            }

        }
        private void db2Schema_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Data.Database2.Status += "Schema Loaded Successfully.";
            this.Data.Database2.Status += Environment.NewLine;

            if(!db2Schema.IsBusy)
            {
                compareDB.RunWorkerAsync();
            }
            //this.Data.Processing = Visibility.Hidden;
            //this.Data.CompareButton = "Compare";
            //this.Data.Compare = true;
            //MessageBox.Show("Comparison Complete", "SUCCESS!", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void compareDB_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach( SQLTables tab in this.Data.DB1Tables )
            {
                CompareTable table = new CompareTable();
                table.TableName = tab.TableName;
                table.TableName1 = tab.TableName;
                table.TableName2 = "";
                table.DB1 = true;

                this.Data.CompareTables.Add(table);
            }
            foreach (SQLTables tab in this.Data.DB2Tables)
            {
                int cnt = this.Data.DB1Tables.FindAll(o => o.TableName == tab.TableName).Count;
                if (cnt == 0)
                {
                    CompareTable table = new CompareTable();
                    table.TableName = tab.TableName;
                    table.TableName1 = "";
                    table.TableName2 = tab.TableName;
                    table.DB2 = true;
                    this.Data.CompareTables.Add(table);
                }
                else
                {
                    try
                    {
                        this.Data.CompareTables.FirstOrDefault(o => o.TableName == tab.TableName).DB2 = true;
                        this.Data.CompareTables.FirstOrDefault(o => o.TableName == tab.TableName).TableName2 = tab.TableName;
                    }
                    catch (Exception ex) { }
                }
            }
            this.Data.CompareTables = this.Data.CompareTables.OrderBy(o=>o.TableName).ToList<CompareTable>();

        }
        private void compareDB_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Data.Processing = Visibility.Hidden;
            this.Data.CompareButton = "Compare";
            this.Data.CompareTab = true;
            this.Data.Compare = true;

            compareTab.IsSelected = true;

        }
    }
}
