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
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using DBCHandling;
using System.Collections.ObjectModel;
using RBT.Universal;
using System.IO;
using System.Xml;
using RBT.Universal.Keywords;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using OpStates;
using System.Diagnostics;

namespace CANTxGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Window1 WinQcLogin;

        private int lampCount = 1;

        private int signalsCount = 1;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


        ConfigurationManager _confManager = new ConfigurationManager();
        bool _tvExpanded = false;
        DBCReader dbcParser;

        public MainWindow()
        {
            InitializeComponent();


            //TreeViewItem initTvItem = new TreeViewItem();
            //initTvItem.MouseDoubleClick += buttonOpenDBC_Click;
            //initTvItem.Header = "Browse dbc to configure...";

            // treeView.Items.Add(initTvItem);


            //expdBusGeneric.IsExpanded = false;
            //expdNetworkPar.IsExpanded = false;
        }

        public ConfigurationManager ConfManager
        {
            get { return _confManager; }
            private set
            {
                _confManager = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfManager)));
            }
        }


        private void buttonOpenDBC_Click(object sender, RoutedEventArgs e)
        {
            //init all controls to empty

            treeView.ItemsSource = null;


            OpenFileDialog openDBC = new OpenFileDialog();
            openDBC.FileName = "DBC File";
            openDBC.DefaultExt = ".dbc";
            openDBC.Filter = "CAN db files (*.dbc)|*.dbc";

            if (openDBC.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                ObservableCollection<DbcMessage> AllMsg;

                dbcParser = new DBCReader(openDBC.FileName);
                AllMsg = dbcParser.ExtGetAllMessages();



                ConfManager.ProjectDBC = openDBC.FileName;
                ConfManager.AllMessages = AllMsg;
                ConfManager.SelectedNode = "All";

                //binding to controls
                treeView.ItemsSource = null;
                treeView.Items.Clear();
                treeView.ItemsSource = ConfManager.Messages;


            }


        }


        private void btnAddConnection_Click(object sender, RoutedEventArgs e)
        {
            if ((treeView.SelectedItem != null) && (comboConnect.SelectedItem != null) && (comboOpState.SelectedItem != null) && (comboVar.SelectedItem != null))
            {


                if (treeView.SelectedItem is DbcMessage)
                {
                    if (!ConfManager.IsConnectionExists(treeView.SelectedItem as DbcMessage, comboConnect.SelectedItem as OpStates.CanTxSignalType, comboOpState.SelectedItem as OpStates.OpState, comboVar.SelectedItem as Variant))
                    {
                        Connection newConnection = new Connection(treeView.SelectedItem as DbcMessage, comboConnect.SelectedItem as OpStates.CanTxSignalType, comboOpState.SelectedItem as OpStates.OpState, comboVar.SelectedItem as Variant);
                        ConfManager.AddConnection(newConnection);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Same connection already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else if (treeView.SelectedItem is DbcSignal)
                {
                    if (!ConfManager.IsConnectionExists(treeView.SelectedItem as DbcSignal, comboConnect.SelectedItem as OpStates.CanTxSignalType, comboOpState.SelectedItem as OpStates.OpState, comboVar.SelectedItem as Variant))
                    {
                        Connection newConnection = new Connection(treeView.SelectedItem as DbcSignal, comboConnect.SelectedItem as OpStates.CanTxSignalType, comboOpState.SelectedItem as OpStates.OpState, comboVar.SelectedItem as Variant);
                        ConfManager.AddConnection(newConnection);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Same connection already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("All options must be provided!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void comboNodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfManager != null)
            {
                treeView.ItemsSource = null;
                treeView.Items.Clear();
                treeView.ItemsSource = ConfManager.Messages;
                _tvExpanded = false;

                //ConfManager.SelectedNode = comboNodes.SelectedValue.ToString();

            }



        }

        private void tvExpand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (object tvItem in treeView.Items)
            {
                TreeViewItem item = treeView.ItemContainerGenerator.ContainerFromItem(tvItem) as TreeViewItem;
                item.IsExpanded = true;
            }
            _tvExpanded = true;
        }
        private void tvCollapse_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (object tvItem in treeView.Items)
            {
                TreeViewItem item = treeView.ItemContainerGenerator.ContainerFromItem(tvItem) as TreeViewItem;
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
            System.Windows.Clipboard.SetText(treeView.SelectedItem is DbcMessage ? ((DbcMessage)treeView.SelectedItem).Name : ((DbcSignal)treeView.SelectedItem).Name);
        }
        private void tvCopyName_CanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = treeView.SelectedItem != null;
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button RemoveButton = e.OriginalSource as System.Windows.Controls.Button;
            ConfManager.RemoveConnection(RemoveButton.DataContext as Connection);
        }

        private void dgConnection_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();

        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();

        }

        private void btnSaveDBC_Click(object sender, RoutedEventArgs e)
        {
            if (dbcParser == null)
            {
                System.Windows.Forms.MessageBox.Show("Please load DBC before save!\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog saveDBC = new SaveFileDialog();
            saveDBC.FileName = "DBC File";
            saveDBC.DefaultExt = ".dbc";
            saveDBC.Filter = "CAN db files (.dbc)|*.dbc";

            if (saveDBC.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                try
                {
                    DBCWriter dbcWriter = new DBCWriter(saveDBC.FileName);
                    dbcWriter.writeDBC(dbcParser.DbVersion, dbcParser.DbCustomer, dbcParser.DbName, String.IsNullOrEmpty(dbcParser.DbBaudrate) ? "500000" : dbcParser.DbBaudrate, dbcParser.ExtGetAllMessages());
                    dbcWriter.close();
                    System.Windows.Forms.MessageBox.Show("DBC File Saved successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    System.Windows.Forms.MessageBox.Show("Cannot save dbc file\n" + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }
        }

        private void btnSaveTestScript_Click(object sender, RoutedEventArgs e)
        {
            //get fileSaveDialog
            SaveFileDialog saveTestScript = new SaveFileDialog();
            saveTestScript.FileName = "RBT_CANTx_Universal.par";
            saveTestScript.DefaultExt = ".par";
            saveTestScript.Filter = "RBT Universal Parameter files (.par)|*.par";

            if (saveTestScript.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string localFilePath = saveTestScript.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                string newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;

                FileStream fsScript = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter swScript = new StreamWriter(fsScript);
                swScript.WriteLine("#Autogenerated at " + DateTime.Now.ToString());

                //print default signal list according to required
                ListDependentParameter defaultSigList = new ListDependentParameter("### default_signal_list", @"This is the signal list that will be used by default if no signal list is men-tioned in the parameter set. The default signal list should be written as shown in the example below. The signal will be printed in the same order in which they appear in the default signal list. This should come at the beginning before the parameter sets start. The names used must be same as in the CAN mapping file. This parameter is mandatory
  e.g.
### default_signal_list                    = @('DR0','DR1','DR2','B_ABS')
", new ObservableCollection<string>(), null);

                defaultSigList.ValueList = new ObservableCollection<string>(ConfManager.Signals.Select(X => X.Name));
                swScript.WriteLine(defaultSigList.Name + " = " + defaultSigList.Value);

                //loop connection to get each testcase script
                string isGenerationFail = "";
                try
                {
                    swScript.WriteLine("\n#Generate signal based test par");
                    foreach (TestScript ts in ConfManager.ConnectTestScript)
                    {
                        swScript.WriteLine(ts.TestCaseScript);

                    }

                    swScript.WriteLine("\n#Generate Bus Generic or line fault test par");
                    foreach (TestScript ts in ConfManager.BusGenericConfig.BusGenericTScript)
                    {
                        swScript.WriteLine(ts.TestCaseScript);

                    }
                    swScript.WriteLine("\n#Generate Network parameter, eg. postrun timing and voltage behavior test par");
                    foreach (TestScript ts in ConfManager.NetworkParConfig.NetworkParTScript)
                    {
                        swScript.WriteLine(ts.TestCaseScript);

                    }

                }
                catch (Exception exp)
                {
                    isGenerationFail = exp.Message;
                }
                finally
                {
                    swScript.Close();

                }

                if (isGenerationFail == "")
                {
                    System.Windows.MessageBox.Show("Project Script Saved Successfully", "Script", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Exception happened Saving Script:\n" + isGenerationFail, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }


        }

        private void btnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            //Get filesave dialog
            SaveFileDialog saveTestConfig = new SaveFileDialog();
            saveTestConfig.FileName = "RBT_CANTx_Configuration.xml";
            saveTestConfig.DefaultExt = ".xml";
            saveTestConfig.Filter = "RBT CANTx Configuration files (.xml)|*.xml";

            if (saveTestConfig.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string localFilePath = saveTestConfig.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                string newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;


                //get xmlformatter object

                DataContractSerializer formatter = new DataContractSerializer(typeof(ConfigurationManager));
                //Stream stream = new FileStream(localFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                XmlWriter stream = new XmlTextWriter(localFilePath, Encoding.UTF8);

                string result = "";
                try
                {
                    formatter.WriteObject(stream, ConfManager);

                }
                catch (Exception exp)
                {
                    result = exp.Message;

                }
                finally
                {
                    stream.Close();
                }


                if (result == "")
                {
                    System.Windows.MessageBox.Show("Project Configuration Saved Successfully", "Configuration", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Exception happened Saving Configuration:\n" + result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }


        }

        private void btnLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            //get openFileDialog
            OpenFileDialog openTestConfig = new OpenFileDialog();
            openTestConfig.FileName = "RBT CANTx Configuration File";
            openTestConfig.DefaultExt = ".xml";
            openTestConfig.Filter = "RBT CANTx Configuration files (.xml)|*.xml";

            if (openTestConfig.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string result = "";

                DataContractSerializer formatter = new DataContractSerializer(typeof(ConfigurationManager));
                Stream destream = new FileStream(openTestConfig.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                XmlReader reader = new XmlTextReader(destream);
                try
                {
                    treeView.ItemsSource = null;
                    treeView.Items.Clear();

                    var temp = formatter.ReadObject(reader);
                    ConfManager = (ConfigurationManager)temp;

                    ConfManager.SelectedNode = "All";

                    ConfManager.Connections.ToList().ForEach(x => x.RegisterChildChangeEvent());
                    ConfManager.RegisterChildChangedEvent();

                }
                catch (Exception exp)
                {

                    result = exp.Message;

                }
                finally
                {

                    destream.Close();
                }

                if (result == "")
                {
                    System.Windows.MessageBox.Show("Project Configuration Loaded Successfully", "Configuration", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                    System.Windows.MessageBox.Show("Exception happened Loading Configuration:\n" + result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }

            // OnPropertyChanged(new PropertyChangedEventArgs("ConfManager"));

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfManager)));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // add supposed lamps one time
            if (lampCount == 1)
            {
                ConfManager.NetworkParConfig.AddInitLamps("ESP_Fault", 0);
                ConfManager.NetworkParConfig.AddInitLamps("ABS_Fault", 0);
                ConfManager.NetworkParConfig.AddInitLamps("TCS_Fault", 0);
                ConfManager.NetworkParConfig.AddInitLamps("VDC_Fault", 0);
                ConfManager.NetworkParConfig.AddInitLamps("EBD_Fault", 0);
                lampCount++;
            }
            else
            {
                System.Windows.MessageBox.Show("You have added supposed lamps, don't need to add it again");
            }



        }

        private void btnImpBus_Click(object sender, RoutedEventArgs e)
        {
            if (WinQcLogin == null || !WinQcLogin.IsLoaded)
            {
                WinQcLogin = new Window1();
            }

            WinQcLogin.QcImporter.Test2Import = ConfManager.BusGenericConfig.BusGenericTScript;
            WinQcLogin.Show();

            //temperary solution to refresh the datagrid display
            BindingExpression bdExp = dgBus.GetBindingExpression(System.Windows.Controls.DataGrid.ItemsSourceProperty);
            bdExp.UpdateTarget();

        }

        private void btnImpNetwork_Click(object sender, RoutedEventArgs e)
        {
            if (WinQcLogin == null || !WinQcLogin.IsLoaded)
            {
                WinQcLogin = new Window1();
            }


            WinQcLogin.QcImporter.Test2Import = ConfManager.NetworkParConfig.NetworkParTScript;
            WinQcLogin.Show();
        }

        private void lbTxSigs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbTxSigs.SelectedItem == null) return;
            if (e.ClickCount >= 2)
            {
                ConfManager.NetworkParConfig.AddInitLamps(((DbcSignal)lbTxSigs.SelectedItem).Name, 0);
            }

        }

        private void btnImpQC_Click(object sender, RoutedEventArgs e)
        {

            if (WinQcLogin == null || !WinQcLogin.IsLoaded)
            {
                WinQcLogin = new Window1();
            }



            WinQcLogin.QcImporter.Test2Import = new ObservableCollection<TestScript>((ConfManager.NetworkParConfig.NetworkParTScript.Concat(ConfManager.BusGenericConfig.BusGenericTScript)).Concat(ConfManager.ConnectTestScript));

            WinQcLogin.Show();

            //temperary solution to refresh the datagrid display
            BindingExpression bdExp = dgBus.GetBindingExpression(System.Windows.Controls.DataGrid.ItemsSourceProperty);
            bdExp.UpdateTarget();

        }

        private void btnImpConnection_Click(object sender, RoutedEventArgs e)
        {
            if (WinQcLogin == null || !WinQcLogin.IsLoaded)
            {
                WinQcLogin = new Window1();
            }

            WinQcLogin.Show();
            WinQcLogin.QcImporter.Test2Import = ConfManager.ConnectTestScript;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ConfManager.NetworkParConfig.AddInitSig("sigA", 0);
        }

        private void btnAddInit_Click(object sender, RoutedEventArgs e)
        {
            // add propposed signals one time 
            if (signalsCount == 1)
            {
                // Only add transmitter is ESP and remove unused signals(which looks like NotUsed_***)
                //Bug 344 -> Xia Jack 05/30/2018, add sum, counter reg pattern 
                Regex reg = new Regex("^NotUsed.*", RegexOptions.IgnoreCase);
                Regex reg1 = new Regex(".*sum.*", RegexOptions.IgnoreCase);
                Regex reg2 = new Regex(".*counter.*", RegexOptions.IgnoreCase);
                foreach (DbcMessage message in ConfManager.Messages.Where(dr => dr.Transmitters[0] == "ESP" || dr.Transmitters[0] == "ESC" || dr.Transmitters[0] == "EBCM_PB" || dr.Transmitters[0] == "IBOOST_PB"))
                {
                    foreach (DbcSignal sig in message.Signals.Where(dr => dr.Name != reg.Match(dr.Name).Value).Where(dr => dr.Name != reg1.Match(dr.Name).Value).Where(dr => dr.Name != reg2.Match(dr.Name).Value))
                    {
                        ConfManager.NetworkParConfig.AddInitSig(sig.Name, (int)((sig.InitValue - sig.Offset) / sig.Factor));
                    }

                }
                signalsCount++;
            }
            else
            {
                System.Windows.MessageBox.Show("You have added all the signals in your checklist, don't need add add again");
            }
        }

        private void lbSigs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (dgConnection.SelectedItem != null)
            {
                ((Connection)dgConnection.SelectedItem).ConnectedOpState.AddSig2OpState(((DbcSignal)lbSigs.SelectedItem).Name, "");
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgConnection.SelectedItem != null && dgOpStateConf.SelectedItem != null)
            {
                System.Windows.Controls.Button RemoveButton = e.OriginalSource as System.Windows.Controls.Button;

                ((Connection)dgConnection.SelectedItem).ConnectedOpState.RemoveSig2OpState(RemoveButton.DataContext as CANTxParameter);
            }
        }

        private void btnSaveOpState_Click(object sender, RoutedEventArgs e)
        {
            //get fileSaveDialog
            SaveFileDialog saveTestScript = new SaveFileDialog();
            saveTestScript.FileName = "Operation_States.pm";
            saveTestScript.DefaultExt = ".pm";
            saveTestScript.Filter = "RBT CAN Tx OperationStates file (.pm)|*.pm";

            if (saveTestScript.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string localFilePath = saveTestScript.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                string newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;

                FileStream fsScript = new FileStream(localFilePath, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter swScript = new StreamWriter(fsScript);
                swScript.WriteLine("#Autogenerated at " + DateTime.Now.ToString());


                //loop connection to get each operation state
                string isGenerationFail = "";
                try
                {
                    swScript.WriteLine("package CAN_Operation_States;");
                    swScript.WriteLine("");
                    swScript.WriteLine("$CAN_signals = {");
                    swScript.WriteLine("");

                    Dictionary<string, Dictionary<string, string>> dicOpstate = new Dictionary<string, Dictionary<string, string>>();
                    //write init lamps and init value into operation states dictionary

                    foreach (var sig in ConfManager.NetworkParConfig.InitSignals)
                    {
                        // fix duplicate key issues
                        if (dicOpstate.ContainsKey(sig.Name))
                        {
                            dicOpstate.Remove(sig.Name);
                            // dicOpstate.Add(sig.Name, new Dictionary<string, string>() { { "init", sig.Value.ToString() } });
                        }
                        else
                        {
                            dicOpstate.Add(sig.Name, new Dictionary<string, string>() { { "init", sig.Value.ToString() } });
                        }

                    }
                    //write connection operation states into operation state dictionary
                    foreach (OpState op in ConfManager.ConfiguredOpStates)
                    {

                        foreach (var sig in op.OperationStateSignals)
                        {
                            if (dicOpstate.ContainsKey(sig.Name))
                            {
                                dicOpstate[sig.Name].Add(op.Name, sig.Value);
                            }
                            else
                            {
                                dicOpstate.Add(sig.Name, new Dictionary<string, string>() { { op.Name, sig.Value } });


                            }


                        }


                    }


                    foreach (string sig in dicOpstate.Keys)
                    {
                        swScript.WriteLine("\t\t\t\t" + "'" + sig + "' =>{");
                        foreach (string op in dicOpstate[sig].Keys)
                        {
                            swScript.WriteLine("\t\t\t\t\t\t\t\t'" + op + "'\t =>\t" + "'" + dicOpstate[sig][op] + "',");

                        }
                        swScript.WriteLine("\t\t\t\t}, #End of signal " + sig);
                    }

                    swScript.WriteLine("};#  END OF OperationStates");
                    swScript.WriteLine("1;#  END OF package");

                }
                catch (Exception exp)
                {
                    isGenerationFail = exp.Message;
                }
                finally
                {
                    swScript.Close();

                }

                if (isGenerationFail == "")
                {
                    System.Windows.MessageBox.Show("Project OperationStates file Saved Successfully", "OperationStates", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Exception happened Saving OperationStates:\n" + isGenerationFail, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void btnCompareDBC_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            Process.Start(@"DbcComparer.exe");
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lbTxSigs2.SelectedItem == null) return;
            if (e.ClickCount >= 2)
            {
                ConfManager.NetworkParConfig.AddInitSig(((DbcSignal)lbTxSigs2.SelectedItem).Name, 0);
            }
        }

        private void btnAddDefaultConnection_Click(object sender, RoutedEventArgs e)
        {
            // to implement suggested/proposed connections based on target signal type,eg. Speed signal should have minimum,typical and maximum speed value check.


        }

        private void lbTxSigs2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //to do -> when User enter any keys in the listbox, direct to it in signals lists

        }
    }
}