using RBT.QcUtility;
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
using System.Windows.Shapes;

namespace CANTxGenerator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private QcImporter _qcimp= new QcImporter();

        public Window1()
        {
            InitializeComponent();
        }

        public QcImporter QcImporter
        {
            get { return _qcimp; }
            set { _qcimp = value; }

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QcImporter.Login(pwdBox.Password);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            comboDomain.SelectedIndex = 0;
           
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QcImporter.ConnectProject(pwdBox.Password);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           

            for (int i = 1; i <= QcImporter.Test2Import.Count; i++)
            {
                try
                {
                    QcImporter.ImportQC(QcImporter.Test2Import[i - 1]);
                    double value = i * 100.0 / QcImporter.Test2Import.Count;
                    lbShow.Content = "Imported：" + i;
                    pbBar.Dispatcher.Invoke(new Action<System.Windows.DependencyProperty, object>(pbBar.SetValue), System.Windows.Threading.DispatcherPriority.Background, ProgressBar.ValueProperty, value);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               
            }

            this.Hide();
            pbBar.Value = 0;

        }

        private void WinQcLogin_Closed(object sender, EventArgs e)
        {
            QcImporter.Disconnect();
            QcImporter.Logout();
            QcImporter.ReleaseConnection();
        }
    }
}
