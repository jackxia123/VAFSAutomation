using DBCHandling;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace DbcComparer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _tvExpanded = false;
        private bool _tvExpandedB = false;
        private ViewModelDbcComparer _dbcComp = new ViewModelDbcComparer();

        DBCReader dbcParserA;
        DBCReader dbcParserB;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        public ViewModelDbcComparer DbcComparer
        {
            get { return _dbcComp; }
            private set
            {
                _dbcComp = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.DbcComparer)));
            }
        }



        private void tvExpand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (object tvItem in treeViewBase.Items)
            {
                TreeViewItem item = treeViewBase.ItemContainerGenerator.ContainerFromItem(tvItem) as TreeViewItem;
                item.IsExpanded = true;
            }
            _tvExpanded = true;
        }
        private void tvCollapse_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (object tvItem in treeViewBase.Items)
            {
                TreeViewItem item = treeViewBase.ItemContainerGenerator.ContainerFromItem(tvItem) as TreeViewItem;
                item.IsExpanded = false;

            }
            _tvExpanded = false;
        }


        private void tvCollapse_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _tvExpanded;
        }
        private void tvExpand_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_tvExpanded;
        }

        private void tvCopyName_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(treeViewBase.SelectedItem is DbcMessage ? ((DbcMessage)treeViewBase.SelectedItem).Name : ((DbcSignal)treeViewBase.SelectedItem).Name);
        }
        private void tvCopyName_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = treeViewBase.SelectedItem != null;
        }

        private void tvFindMatch_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (treeViewBase.SelectedItem is DbcMessage)
            {
                ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>(DbcComparer.DbcB.Where(x => (x.ID == ((DbcMessage)treeViewBase.SelectedItem).ID)));
                TreeViewItem container = (TreeViewItem)treeViewTarget.ItemContainerGenerator.ContainerFromItem(founds[0]);
                if (container != null)
                {
                    container.IsSelected = true;

                }

            }
            else if (treeViewBase.SelectedItem is DbcSignal)
            {
                ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>(DbcComparer.DbcB.Where(x =>
                                                        (x.Signals.Where(
                                                        y => (y.StartBit == ((DbcSignal)treeViewBase.SelectedItem).StartBit &&
                                                        y.Length == ((DbcSignal)treeViewBase.SelectedItem).Length &&
                                                        y.InMessage == ((DbcSignal)treeViewBase.SelectedItem).InMessage
                                                        ) ||
                                                       y.Name == ((DbcSignal)treeViewBase.SelectedItem).Name
                                                       ).Count() > 0
                                )
                       ));
                TreeViewItem container = (TreeViewItem)treeViewTarget.ItemContainerGenerator.ContainerFromItem(founds[0]);
                if (container != null)
                {

                    container.IsExpanded = true;

                    if (container.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                    {
                        container.UpdateLayout();
                    }
                    container.Focus();


                    var childitemsControl = container as ItemsControl;

                    ObservableCollection<DbcSignal> signals = new ObservableCollection<DbcSignal>(founds[0].Signals.Where(
                                                          y => (y.StartBit == ((DbcSignal)treeViewBase.SelectedItem).StartBit &&
                                                        y.Length == ((DbcSignal)treeViewBase.SelectedItem).Length &&
                                                        y.InMessage == ((DbcSignal)treeViewBase.SelectedItem).InMessage
                                                        ) ||
                                                       y.Name == ((DbcSignal)treeViewBase.SelectedItem).Name
                                                       ));

                    TreeViewItem childcontainer = (TreeViewItem)childitemsControl.ItemContainerGenerator.ContainerFromItem(signals[0]);

                    if (childcontainer != null)
                    {
                        childcontainer.IsSelected = true;
                    }

                }


            }

        }
        private void tvFindMatch_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (treeViewBase.SelectedItem is DbcMessage)
            {
                e.CanExecute = DbcComparer.DbcB.Where(x => (x.ID == ((DbcMessage)treeViewBase.SelectedItem).ID)).Count() > 0;

            }
            else if (treeViewBase.SelectedItem is DbcSignal)
            {
                e.CanExecute = DbcComparer.DbcB.Where(x => (x.Signals.Where(
                                                        y => (y.StartBit == ((DbcSignal)treeViewBase.SelectedItem).StartBit &&
                                                        y.Length == ((DbcSignal)treeViewBase.SelectedItem).Length &&
                                                        y.InMessage == ((DbcSignal)treeViewBase.SelectedItem).InMessage
                                                        ) ||
                                                       y.Name == ((DbcSignal)treeViewBase.SelectedItem).Name
                                                       ).Count() > 0
                                )
                       ).Count() > 0;
            }
        }

        //FOR Treeview B
        private void tvExpandB_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (object tvItem in treeViewTarget.Items)
            {
                TreeViewItem item = treeViewTarget.ItemContainerGenerator.ContainerFromItem(tvItem) as TreeViewItem;
                item.IsExpanded = true;
            }
            _tvExpandedB = true;
        }
        private void tvCollapseB_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (object tvItem in treeViewTarget.Items)
            {
                TreeViewItem item = treeViewTarget.ItemContainerGenerator.ContainerFromItem(tvItem) as TreeViewItem;
                item.IsExpanded = false;

            }
            _tvExpandedB = false;
        }
        private void tvCollapseB_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _tvExpandedB;
        }
        private void tvExpandB_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_tvExpandedB;
        }
        private void tvCopyNameB_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(treeViewTarget.SelectedItem is DbcMessage ? ((DbcMessage)treeViewTarget.SelectedItem).Name : ((DbcSignal)treeViewTarget.SelectedItem).Name);
        }
        private void tvCopyNameB_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = treeViewTarget.SelectedItem != null;
        }

        private void tvFindMatchB_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (treeViewTarget.SelectedItem is DbcMessage)
            {
                ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>(DbcComparer.DbcA.Where(x => (x.ID == ((DbcMessage)treeViewTarget.SelectedItem).ID)));
                TreeViewItem container = (TreeViewItem)treeViewBase.ItemContainerGenerator.ContainerFromItem(founds[0]);
                if (container != null)
                {
                    container.IsSelected = true;

                }
            }
            else if (treeViewTarget.SelectedItem is DbcSignal)
            {
                ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>(DbcComparer.DbcA.Where(
                                                    x => (x.Signals.Where(y =>
                                                     (y.StartBit == ((DbcSignal)treeViewTarget.SelectedItem).StartBit &&
                                                        y.Length == ((DbcSignal)treeViewTarget.SelectedItem).Length &&
                                                        y.InMessage == ((DbcSignal)treeViewTarget.SelectedItem).InMessage) ||

                                                       y.Name == ((DbcSignal)treeViewTarget.SelectedItem).Name
                                                       ).Count() > 0
                                )
                       ));

                TreeViewItem container = (TreeViewItem)treeViewBase.ItemContainerGenerator.ContainerFromItem(founds[0]);
                if (container != null)
                {


                    container.IsExpanded = true;

                    if (container.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                    {
                        container.UpdateLayout();
                    }
                    container.Focus();


                    var childitemsControl = container as ItemsControl;

                    ObservableCollection<DbcSignal> signals = new ObservableCollection<DbcSignal>(founds[0].Signals.Where(y =>
                                                      (y.StartBit == ((DbcSignal)treeViewTarget.SelectedItem).StartBit &&
                                                        y.Length == ((DbcSignal)treeViewTarget.SelectedItem).Length &&
                                                        y.InMessage == ((DbcSignal)treeViewTarget.SelectedItem).InMessage) ||

                                                       y.Name == ((DbcSignal)treeViewTarget.SelectedItem).Name
                                                       ));

                    TreeViewItem childcontainer = (TreeViewItem)childitemsControl.ItemContainerGenerator.ContainerFromItem(signals[0]);

                    if (childcontainer != null)
                    {
                        childcontainer.IsSelected = true;
                    }

                }

            }
        }
        private void tvFindMatchB_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (treeViewTarget.SelectedItem is DbcMessage)
            {
                e.CanExecute = DbcComparer.DbcA.Where(x => (x.ID == ((DbcMessage)treeViewTarget.SelectedItem).ID)).Count() > 0;

            }
            else if (treeViewTarget.SelectedItem is DbcSignal)
            {
                e.CanExecute = DbcComparer.DbcA.Where(x => (x.Signals.Where(y => (
                                                        y.StartBit == ((DbcSignal)treeViewTarget.SelectedItem).StartBit &&
                                                        y.Length == ((DbcSignal)treeViewTarget.SelectedItem).Length &&
                                                        y.InMessage == ((DbcSignal)treeViewTarget.SelectedItem).InMessage) ||

                                                       y.Name == ((DbcSignal)treeViewTarget.SelectedItem).Name
                                                       ).Count() > 0
                                )
                       ).Count() > 0;
            }
        }

        /// <summary>
        /// Load DBC A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dbcBase_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDBC = new OpenFileDialog();
            openDBC.FileName = "DBC File";
            openDBC.DefaultExt = ".dbc";
            openDBC.Filter = "CAN db files (.dbc)|*.dbc";



            if (openDBC.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                dbcParserA = new DBCReader(openDBC.FileName);
                DbcComparer.DbcA = dbcParserA.GetAllMessages();
                DbcComparer.PathA = openDBC.FileName;

                //binding to controls
                treeViewBase.ItemsSource = null;
                treeViewBase.Items.Clear();
                treeViewBase.ItemsSource = DbcComparer.DbcA;
            }

        }

        /// <summary>
        /// Load DBC B
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dbcTarget_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDBC = new OpenFileDialog();
            openDBC.FileName = "DBC File";
            openDBC.DefaultExt = ".dbc";
            openDBC.Filter = "CAN db files (.dbc)|*.dbc";

            if (openDBC.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                dbcParserB = new DBCReader(openDBC.FileName);
                DbcComparer.DbcB = dbcParserB.GetAllMessages();
                DbcComparer.PathB = openDBC.FileName;

                //binding to controls
                treeViewTarget.ItemsSource = null;
                treeViewTarget.Items.Clear();
                treeViewTarget.ItemsSource = DbcComparer.DbcB;
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("V1.0.0.0\nAuthor: Test Automation Team\nCC-AS/ESW3-CN", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MergeBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveMergedDBC = new SaveFileDialog();
            saveMergedDBC.FileName = "Merged_DBC_File";
            saveMergedDBC.DefaultExt = ".dbc";
            saveMergedDBC.Filter = "CAN db files (.dbc)|*.dbc";

            if (saveMergedDBC.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                if (dbcParserA == null || dbcParserB == null) { return; }

                try
                {
                    DBCWriter dbcWriter = new DBCWriter(saveMergedDBC.FileName);
                    ObservableCollection<DbcMessage> mergedMsg = MergeDBC(dbcParserA, dbcParserB);
                    dbcWriter.writeDBC(dbcParserA.DbVersion, dbcParserA.DbCustomer, dbcParserA.DbName + dbcParserB.DbName, String.IsNullOrEmpty(dbcParserA.DbBaudrate) ? "500000" : dbcParserA.DbBaudrate, mergedMsg);
                    dbcWriter.close();
                    System.Windows.Forms.MessageBox.Show("DBC File Merged successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    System.Windows.Forms.MessageBox.Show("Cannot merge dbc file\n" + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private ObservableCollection<DbcMessage> MergeDBC(DBCReader dbcA, DBCReader dbcB)
        {
            ObservableCollection<DbcMessage> mergedMsg = dbcA.GetAllMessages();

            foreach (var msg in dbcParserB.GetAllMessages())
            {

                if (dbcParserA.GetAllMessages().Any(p => p.ID == msg.ID))
                {
                    //not found in dbcA
                }
                else
                {
                    mergedMsg.Add(msg);
                }


            }
            return mergedMsg;
        }

    }
}
