using RBT.QcUtility;
using System;
using System.ComponentModel;
using System.Windows;

namespace CANTxGenerator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private QcImporter _qcimp = new QcImporter();
        private BackgroundWorker backgroundWorker;


        public Window1()
        {
            InitializeComponent();
            backgroundWorker = ((BackgroundWorker)this.FindResource("backgroundWorker"));
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
            //Task471->20181009
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();

            }

            //try
            //{
            //    QcImporter.ConnectProject(pwdBox.Password);
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //for (int i = 1; i <= QcImporter.Test2Import.Count; i++)
            //{
            //    try
            //    {
            //        QcImporter.ImportQC(QcImporter.Test2Import[i - 1]);
            //        double value = i * 100.0 / QcImporter.Test2Import.Count;
            //        lbShow.Content = "Imported：" + i;
            //        // backgroundWorker.ReportProgress(Int32.Parse(value.ToString()));
            //        pbBar.Dispatcher.Invoke(new Action<System.Windows.DependencyProperty, object>(pbBar.SetValue), System.Windows.Threading.DispatcherPriority.Background, ProgressBar.ValueProperty, value);

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;
            //    }

            //}



        }

        private void WinQcLogin_Closed(object sender, EventArgs e)
        {
            QcImporter.Disconnect();
            QcImporter.Logout();
            QcImporter.ReleaseConnection();
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
            int interationcount = QcImporter.Test2Import.Count;
            //int interation = interationcount / 100;
            for (int i = 1; i <= interationcount; i++)
            {
                try
                {
                    QcImporter.ImportQC(QcImporter.Test2Import[i - 1]);
                    // double value = i * 100.0 / QcImporter.Test2Import.Count;
                    //lbShow.Content = "Imported：" + i;
                    backgroundWorker.ReportProgress(i * 100 / interationcount);
                    //pbBar.Dispatcher.Invoke(new Action<System.Windows.DependencyProperty, object>(pbBar.SetValue), System.Windows.Threading.DispatcherPriority.Background, ProgressBar.ValueProperty, value);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }


        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Hide();
            pbBar.Value = 0;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbBar.Value = e.ProgressPercentage;
        }
    }
}
