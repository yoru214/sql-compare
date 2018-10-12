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
        public MainWindow()
        {
            InitializeComponent();

            this.Data.Database1.Host = "database1host";


            this.DataContext = this.Data;
        }

        private void testConnect_Click(object sender, RoutedEventArgs e)
        {
            //this.Data.Database1.Connected = true;
        }
    }
}
