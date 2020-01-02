using DBCHandling;
using OpStates;
using RBT.QcUtility;
using RBT.Universal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace CANTxGenerator
{

    [DataContract]
    [KnownType(typeof(DbcMessage))]
    [KnownType(typeof(DbcSignal))]
    //in order to feedback the backend changes to UI, we must inherient INotifyPropertyChanged
    public class ConfigurationManager : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #region private Fields
        private ObservableCollection<DbcSignal> _configuredSignals = new ObservableCollection<DbcSignal>();
        private ItemsChangeObservableCollection<CanTxSignalType> _configuredSigTypes = new ItemsChangeObservableCollection<CanTxSignalType>();
        private ObservableCollection<DbcMessage> _configuredMessages = new ObservableCollection<DbcMessage>();
        private ItemsChangeObservableCollection<OpState> _configuredOpStates = new ItemsChangeObservableCollection<OpState>();
        private ItemsChangeObservableCollection<Variant> _configuredVars = new ItemsChangeObservableCollection<Variant>();

        private ObservableCollection<DbcSignal> _signals = new ObservableCollection<DbcSignal>();
        private ItemsChangeObservableCollection<CanTxSignalType> _sigTypes = new ItemsChangeObservableCollection<CanTxSignalType>();
        private ObservableCollection<DbcMessage> _allMessages = new ObservableCollection<DbcMessage>();
        private ObservableCollection<DbcMessage> _messages = new ObservableCollection<DbcMessage>();
        private ItemsChangeObservableCollection<Connection> _connections = new ItemsChangeObservableCollection<Connection>();
        private ItemsChangeObservableCollection<OpState> _opStates = new ItemsChangeObservableCollection<OpState>();
        private ObservableCollection<string> _dbcNodes = new ObservableCollection<string>();
        private BusGeneric _busGenericConf = new BusGeneric();
        private NetworkPar _networkParConfig = new NetworkPar();

        private VLCGeneric _vlcGenericConf = new VLCGeneric();

        private HBAGeneric _hbaGenericConf = new HBAGeneric();

        private CDDGeneric _cddGenericConf = new CDDGeneric();

        private bool _isBusGenericIncluded = true;
        //private bool _isSpecificTxNodeUsed;
        private ItemsChangeObservableCollection<Variant> _variants = new ItemsChangeObservableCollection<Variant>();
        private string _selectedNode;
        private object _selectedCANTxItem;
        private int _noOfVar = 1;
        private ItemsChangeObservableCollection<Connection> _itemCollection = new ItemsChangeObservableCollection<Connection>();
        private string _projectDBC;
        private string _productType = "ESP";
        private QcImporter _qcImporter;

        private ItemsChangeObservableCollection<TestScript> _connectTestScript = new ItemsChangeObservableCollection<TestScript>();

        #endregion

        #region Constructor
        public ConfigurationManager()
        {
            InitOpStates();
            InitSigTypes();
            //subscribe for notification
            RegisterChildChangedEvent();
        }
        #endregion

        public void RegisterChildChangedEvent()
        {
            Connections.ChildElementPropertyChanged += ItemConnections_ChildElementPropertyChanged;

        }
        /// <summary>
        /// Handler for item change in observationCollection
        /// </summary>
        /// <param name="e"></param>
        private void ItemConnections_ChildElementPropertyChanged(ItemsChangeObservableCollection<Connection>.ChildElementPropertyChangedEventArgs e)
        {

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Connections)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConnectTestScript)));
        }

        /// <summary>
        /// Init Supported OpStates
        /// Todo: Read from XML files
        /// </summary>
        private void InitOpStates()
        {

            //init opstate
            Type[] allTypes = Assembly.GetAssembly(typeof(OpState)).GetExportedTypes();
            foreach (Type t in allTypes)
            {
                if (t.IsSubclassOf(typeof(OpState)) && t.IsSealed)

                    _opStates.Add((OpState)Activator.CreateInstance(t, true));

            }


        }

        /// <summary>
        /// Init Supported SignalTypes
        /// Todo: Read from XML files
        private void InitSigTypes()
        {
            //init signal types                   
            //init opstate
            Type[] allTypes = Assembly.GetAssembly(typeof(CanTxSignalType)).GetExportedTypes();
            foreach (Type t in allTypes)
            {
                if (t.IsSubclassOf(typeof(CanTxSignalType)) && t.IsSealed)

                    _sigTypes.Add((CanTxSignalType)Activator.CreateInstance(t, true));

            }


        }


        //Control switches
        [DataMember]
        public bool IsBusGenericTCIncluded
        {
            get
            {
                return _isBusGenericIncluded;
            }
            set
            {
                _isBusGenericIncluded = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.IsBusGenericTCIncluded)));
            }
        }


        //Properties
        [DataMember]
        public ObservableCollection<DbcSignal> ConfiguredSignals
        {
            get
            {
                IEnumerable<DbcSignal> confSig = (from x in _connections where x.SourceCANTxSignal is DbcSignal select (DbcSignal)x.SourceCANTxSignal).Distinct();
                return new ItemsChangeObservableCollection<DbcSignal>(confSig);
            }
            private set
            {
                _configuredSignals = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredSignals)));
            }
        }
        [DataMember]
        public ItemsChangeObservableCollection<CanTxSignalType> ConfiguredSigTypes
        {
            get
            {
                IEnumerable<CanTxSignalType> confType = (from x in _connections select x.TargetSignalType).Distinct();
                return new ItemsChangeObservableCollection<CanTxSignalType>(confType);
            }
            private set
            {
                _configuredSigTypes = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredSigTypes)));
            }
        }
        [DataMember]
        public ObservableCollection<DbcMessage> ConfiguredMessages
        {
            get
            {
                IEnumerable<DbcMessage> confMsg = (from x in _connections where x.SourceCANTxSignal is DbcMessage select (DbcMessage)x.SourceCANTxSignal).Distinct();
                return new ItemsChangeObservableCollection<DbcMessage>(confMsg);
            }
            private set
            {
                _configuredMessages = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredMessages)));
            }
        }
        [DataMember]
        public ItemsChangeObservableCollection<OpState> ConfiguredOpStates
        {
            get
            {
                IEnumerable<OpState> confOpState = (from x in _connections select x.ConnectedOpState).Distinct();
                return new ItemsChangeObservableCollection<OpState>(confOpState);
            }
            private set
            {
                _configuredOpStates = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredOpStates)));
            }
        }
        [DataMember]
        public ItemsChangeObservableCollection<Variant> ConfiguredVars
        {
            get
            {
                IEnumerable<Variant> confVar = (from x in _connections select x.Variant).Distinct();
                return new ItemsChangeObservableCollection<Variant>(confVar);
            }
            private set
            {
                _configuredVars = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredVars)));
            }
        }

        [DataMember]
        public ObservableCollection<DbcSignal> Signals
        {
            get
            {

                _signals.Clear();

                if (SelectedNode == "All" || SelectedNode == null)
                {
                    foreach (var msg in AllMessages)
                    {

                        foreach (var sig in msg.Signals)
                        {
                            _signals.Add(sig);

                        }

                    }
                }
                else
                {
                    foreach (var msg in Messages)
                    {
                        if (msg.Transmitters.Contains(SelectedNode))
                        {
                            foreach (var sig in msg.Signals)
                            {
                                _signals.Add(sig);

                            }

                        }
                    }


                }
                // order by Signals by Name asc
                return new ItemsChangeObservableCollection<DbcSignal>(_signals.OrderBy(cr => cr.Name));

            }

            private set
            {
                _signals = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Signals)));

            }

        }


        [DataMember]
        public ObservableCollection<DbcMessage> AllMessages
        {
            get
            {

                return new ObservableCollection<DbcMessage>(_allMessages);

            }

            set
            {
                _allMessages = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.AllMessages)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Messages)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Signals)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.DbcNodes)));
            }
        }

        [DataMember]
        public ObservableCollection<DbcMessage> Messages
        {
            get
            {
                if (SelectedNode == "All" || SelectedNode == null)
                {
                    return new ObservableCollection<DbcMessage>(_allMessages.OrderBy(cr => cr.Name));
                }
                else
                {
                    _messages = new ObservableCollection<DbcMessage>(from x in _allMessages where x.Transmitters.Contains(SelectedNode) select x);
                    return new ObservableCollection<DbcMessage>(_messages.OrderBy(cr => cr.Name));

                }

            }

            set
            {
                _messages = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Messages)));

            }
        }
        [DataMember]
        public ItemsChangeObservableCollection<CanTxSignalType> SigTypes
        {
            get
            {
                return _sigTypes;
            }
            set
            {
                _sigTypes = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.SigTypes)));
            }
        }
        [DataMember]
        public ItemsChangeObservableCollection<OpState> OpStates
        {
            get
            {
                return _opStates;
            }
            set
            {
                _opStates = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.OpStates)));

            }
        }



        [DataMember]
        public ObservableCollection<string> DbcNodes
        {
            get
            {

                //init nodes in  dbc
                _dbcNodes.Clear();
                _dbcNodes.Add("All");
                foreach (var msg in _allMessages)
                {
                    foreach (var node in msg.Transmitters)
                    {
                        if (!_dbcNodes.Contains(node))
                        { _dbcNodes.Add(node); }

                    }

                }
                return _dbcNodes;
            }
            set
            {
                _dbcNodes = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.DbcNodes)));

            }


        }

        [DataMember]
        public ItemsChangeObservableCollection<Connection> Connections
        {
            get { return _connections; }

            set
            {
                _connections = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Connections)));


            }


        }

        [DataMember]
        public ItemsChangeObservableCollection<Connection> ItemConnections
        {
            get
            {
                if (SelectedCANTxItem is DbcMessage)
                {

                    return _itemCollection = GetConnectionFromItem(SelectedCANTxItem as DbcMessage);

                }
                else if (SelectedCANTxItem is DbcSignal)
                {
                    return _itemCollection = GetConnectionFromItem(SelectedCANTxItem as DbcSignal);

                }
                return null;

            }
            set
            {
                _itemCollection = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ItemConnections)));

            }
        }

        [DataMember]
        public ItemsChangeObservableCollection<Variant> Variants
        {
            get
            {
                _variants.Clear();
                for (int i = 1; i <= NoOfVariant; i++)
                {
                    _variants.Add(new Variant(i));
                }
                return _variants;
            }
            private set
            {
                _variants = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Variants)));
            }
        }


        [DataMember]
        public int NoOfVariant
        {
            get
            {

                return _noOfVar;

            }
            set
            {
                _noOfVar = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.NoOfVariant)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Variants)));
            }
        }
        /// <summary>
        /// Current selected node
        /// </summary>
        [DataMember]
        public string SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.SelectedNode)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Messages)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Signals)));
            }
        }

        [DataMember]
        public object SelectedCANTxItem
        {
            get { return _selectedCANTxItem; }
            set
            {
                _selectedCANTxItem = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.SelectedCANTxItem)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ItemConnections)));


            }
        }

        [DataMember]
        public string ProjectDBC
        {
            get
            {
                return _projectDBC;
            }
            set
            {
                _projectDBC = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ProjectDBC)));
            }
        }

        /// <summary>
        /// To create Connection which later map to test case
        /// The connection is variant relavant
        /// </summary>
        public void AddConnection(Connection con)
        {
            _connections.Add(con);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ItemConnections)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Connections)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredSignals)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredMessages)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredOpStates)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredSigTypes)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredVars)));
        }

        public void RemoveConnection(Connection con)
        {
            if (_connections.Contains(con))
            {
                _connections.Remove(con);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ItemConnections)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Connections)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredSignals)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredMessages)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredOpStates)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredSigTypes)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConfiguredVars)));

            }


        }
        public ItemsChangeObservableCollection<Connection> GetConnectionFromItem(DbcSignal signal)
        {

            IEnumerable<Connection> tempList = from x in _connections where ((x.SourceCANTxSignal is DbcSignal) && (x.SourceCANTxSignal as DbcSignal).Name == signal.Name) select x;
            return new ItemsChangeObservableCollection<Connection>(tempList);
        }

        public ItemsChangeObservableCollection<Connection> GetConnectionFromItem(DbcMessage message)
        {
            IEnumerable<Connection> tempList = from x in _connections where ((x.SourceCANTxSignal is DbcMessage) && (x.SourceCANTxSignal as DbcMessage).Name == message.Name) select x;
            return new ItemsChangeObservableCollection<Connection>(tempList);
        }

        public bool IsConnectionExists(DbcSignal canSig, CanTxSignalType sigType, OpState opstate, Variant var)
        {
            return _connections.Any(p => (p.SourceCANTxSignal is DbcSignal) && ((DbcSignal)p.SourceCANTxSignal).Name == canSig.Name && p.TargetSignalType.Name == sigType.Name && p.ConnectedOpState.Name == opstate.Name && p.Variant.RbVariant == var.RbVariant);
        }

        public bool IsConnectionExists(DbcMessage canMsg, CanTxSignalType sigType, OpState opstate, Variant var)
        {
            return _connections.Any(p => (p.SourceCANTxSignal is DbcMessage) && ((DbcMessage)p.SourceCANTxSignal).Name == canMsg.Name && p.TargetSignalType.Name == sigType.Name && p.ConnectedOpState.Name == opstate.Name && p.Variant.RbVariant == var.RbVariant);
        }

        /// <summary>
        /// HHC Generic config
        /// </summary>
        [DataMember]
        public BusGeneric BusGenericConfig
        {
            get { return _busGenericConf; }
            set
            {
                _busGenericConf = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.BusGenericConfig)));
            }

        }

        /// <summary>
        /// VLC Generic Config
        /// </summary>
        [DataMember]
        public VLCGeneric VLCGenericConfig
        {
            get { return _vlcGenericConf; }
            set
            {
                _vlcGenericConf = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.VLCGenericConfig)));
            }
        }

        /// <summary>
        /// HBA Generic Config
        /// </summary>
        [DataMember]
        public HBAGeneric HBAGenericConfig
        {
            get { return _hbaGenericConf; }
            set
            {
                _hbaGenericConf = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.HBAGenericConfig)));
            }
        }

        /// <summary>
        /// CDD Generic Config
        /// </summary>
        [DataMember]
        public CDDGeneric CDDGenericConfig
        {
            get
            {
                return _cddGenericConf;
            }
            set
            {
                _cddGenericConf = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.CDDGenericConfig)));
            }
        }

        [DataMember]
        public NetworkPar NetworkParConfig
        {
            get { return _networkParConfig; }
            set
            {
                _networkParConfig = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.NetworkParConfig)));
            }

        }

        [DataMember]
        public string ProductType
        {
            get { return _productType; }
            set
            {
                _productType = value;
                //add Product Type change to corresponding property object ->20181008->Xia Jack
                BusGenericConfig.ProductType = value;
                NetworkParConfig.ProductType = value;
                ProductPar.ProductType = value;
            }

        }

        [DataMember]
        public QcImporter QcImporter
        {
            get { return _qcImporter; }
            set
            {
                _qcImporter = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.QcImporter)));

            }

        }

        [DataMember]
        public ItemsChangeObservableCollection<TestScript> ConnectTestScript
        {
            get
            {
                _connectTestScript.Clear();
                foreach (Connection con in Connections)
                {
                    _connectTestScript.Add(con.TestScript);
                }

                return _connectTestScript;
            }
            private set
            {
                _connectTestScript = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ConnectTestScript)));

            }

        }
    }

}