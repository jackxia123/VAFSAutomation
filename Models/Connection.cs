using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using OpStates;
using DBCHandling;
using RBT.Universal;
using RBT.Universal.Keywords;
using RBT.Universal.CanEvalParameters;
using System.Runtime.Serialization;

namespace CANTxGenerator
{
    [DataContract]
    [KnownType(typeof(DbcSignal))]
    [KnownType(typeof(DbcMessage))]
    public class Connection : IEqualityComparer<Connection>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        //fields
        private ItemsChangeObservableCollection<OpState> _connectedOpStates = new ItemsChangeObservableCollection<OpState>();
        private string _comment = "";
        private string _expectedResult = "";
        private TestScript _testScript = new TestScript();
        private ItemsChangeObservableCollection<CANTxParameter> _clonedParameters = new ItemsChangeObservableCollection<CANTxParameter>();
        private TestSequence _clonedTestSequence;
        private CanTraceAnalyser _clonedCanTraceAnalyser;
        private bool _parameterized = false;
        private OpState _connectedOp;

        //properties
        //todo ->proposal
        //[DataMember]
        //public ItemsChangeObservableCollection<CanTxSignalType> affectedSigs { get; private set; }
        [DataMember]
        public object SourceCANTxSignal { get; private set; }
        [DataMember]
        public CanTxSignalType TargetSignalType { get; private set; }
        [DataMember]
        public OpState ConnectedOpState
        {
            get
            {
                return _connectedOp;

            }
            private set
            {
                _connectedOp = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestScript)));
            }
        }
        [DataMember]
        public Variant Variant { get; private set; }

        /// <summary>
        /// Comment added by user
        /// </summary>
        [DataMember]
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Comment)));
            }
        }
        public string ExpectedResult
        {
            get
            {
                return _expectedResult;
            }
            set
            {
                _expectedResult = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ExpectedResult)));
            }
        }

        /// <summary>
        /// parameterless ctor to support serialization
        /// </summary>
        public Connection()
        {
            RegisterChildChangeEvent();
        }

        /// <summary>
        /// used in deserialization
        /// </summary>
        public void RegisterChildChangeEvent()
        {
            Parameters.ChildElementPropertyChanged += Parameter_ChildElementChanged;
            ConnectedOpState.PropertyChanged += ConnectedOpState_PropertyChanged;
        }

        private void ConnectedOpState_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestScript)));
        }

        //todo ->proposal
        //public Connection(DbcSignal canSig, ItemsChangeObservableCollection<CanTxSignalType> affectSigs, OpState opstate, Variant var)
        //{
        //    SourceCANTxSignal = canSig;
        //    affectedSigs = affectSigs;
        //    Variant = var;

        //    ConnectedOpState = opstate;

        //    //PrepareParameters();
        //    cloneArtifacts();
        //    //listen to parameter change
        //    RegisterChildChangeEvent();

        //}

        /// <summary>
        /// ctor for signal
        /// </summary>
        /// <param name="canSig"></param>
        /// <param name="sigType"></param>
        /// <param name="opstate"></param>
        /// <param name="var"></param>
        public Connection(DbcSignal canSig, CanTxSignalType sigType, OpState opstate, Variant var)
        {
            SourceCANTxSignal = canSig;
            TargetSignalType = sigType;
            Variant = var;

            ConnectedOpState = opstate;

            PrepareParameters();
            cloneArtifacts();
            //listen to parameter change
            RegisterChildChangeEvent();

        }
        private void Parameter_ChildElementChanged(ItemsChangeObservableCollection<CANTxParameter>.ChildElementPropertyChangedEventArgs e)
        {

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Parameterized)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestScript)));
        }
        /// <summary>
        /// ctor for message
        /// </summary>
        /// <param name="canMsg"></param>
        /// <param name="sigType"></param>
        /// <param name="opstate"></param>
        /// <param name="var"></param>
        public Connection(DbcMessage canMsg, CanTxSignalType sigType, OpState opstate, Variant var)
        {
            SourceCANTxSignal = canMsg;
            TargetSignalType = sigType;
            Variant = var;
            ConnectedOpState = opstate;


            PrepareParameters();
            cloneArtifacts();
            RegisterChildChangeEvent();
        }

        [DataMember]
        public bool Parameterized
        {
            get
            {
                if (TestScript == null || Parameters.Any(x => x.Value == ""))
                {
                    _parameterized = false;
                }
                else
                {
                    _parameterized = true;

                }
                return _parameterized;
            }
            private set
            {

                _parameterized = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Parameterized)));
            }



        }
        /// <summary>
        /// Reserved method to add opstate, eg. from a OpState composer
        /// </summary>
        /// <param name="opstate"></param>
        public void AddOpState(OpState opstate)
        {
            if (!_connectedOpStates.Contains(opstate))
            {
                _connectedOpStates.Add(opstate);

            }


        }
        /// <summary>
        /// Reserved method to delete opstate, eg. from a OpState composer
        /// </summary>
        /// <param name="opstate"></param>
        public void RemoveOpState(OpState opstate)
        {
            if (_connectedOpStates.Contains(opstate))
            {
                _connectedOpStates.Remove(opstate);

            }
        }
        /// <summary>
        /// To get cloned test sequence and CanTraceAnalyser from cloned objects
        /// To parameterize test sequence and CanTraceAnalyser by user-input values
        /// To set test purpose 
        /// To display in GUI
        /// </summary>
        [DataMember]
        public TestScript TestScript
        {
            get
            {

                _testScript.TestSequence = TestSequenceCloned;
                _testScript.CanTraceAnalyser = CanTraceAnalyserCloned;

                //To add operation state evaluation by measurementpoint or operation state releated parameters
                if (ConnectedOpState.OperationStateSignals.Count > 0 && _testScript.CanTraceAnalyser.MeasurementPoint == null)
                {
                    _testScript.CanTraceAnalyser.MeasurementPoint = ConnectedOpState.GetMeasurementPoint(Parameters, ref _testScript);

                }


                //Assign default fault evaluation, detained fault evaluation
                // Jack add the default RB Mandatory, CU Mandatory, RB Optional, CU Optional failure
                if (_testScript.RBMandatoryFaults.ValueList.Count == 0)
                {
                    _testScript.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };

                }

                if (_testScript.RBOptionalFaults.ValueList.Count == 0)
                {
                    _testScript.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
                }

                if (_testScript.CUMandatoryFaults.ValueList.Count == 0)
                {
                    _testScript.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
                }

                if (_testScript.CUOptionalFaults.ValueList.Count == 0)
                {
                    _testScript.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
                }


                _testScript.QcFolderPath = @"RBT_CANTx\ConnectionBased_" + DateTime.Now.ToString("yyyy_MMM_ddd_HHmmss");
                _testScript.Purpose.ScalarValue = string.Format("Testing CAN item {0} as type {1} during opstate={2},and anlyse other related signals by user selection", (SourceCANTxSignal is DbcMessage) ? ((DbcMessage)SourceCANTxSignal).Name : ((DbcSignal)SourceCANTxSignal).Name, TargetSignalType.Name, ConnectedOpState.Name);
                _testScript.Name = "RB_UNIVERSAL_01J." + ((SourceCANTxSignal is DbcMessage) ? "Msg_" + ((DbcMessage)SourceCANTxSignal).Name : "Sig_" + ((DbcSignal)SourceCANTxSignal).Name) + "_" + TargetSignalType.Name + "_During_" + ConnectedOpState.Name + "_Var" + Variant.RbVariant;
                return _testScript;

            }
            private set
            {

                _testScript = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestScript)));
            }
        }
        /// <summary>
        ///  Clone TestSequence Property from selected OpState
        /// </summary>
        /// 
        [DataMember]
        public TestSequence TestSequenceCloned
        {
            get
            {

                _clonedTestSequence = ConnectedOpState.ParameterizeTestSequence(Parameters, ref _testScript);
                return _clonedTestSequence;
            }

            private set
            {

                _clonedTestSequence = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestSequenceCloned)));
            }

        }
        /// <summary>
        /// Clone CanTraceAnalyser Property from selected TargetType
        /// </summary>
        /// 
        [DataMember]
        public CanTraceAnalyser CanTraceAnalyserCloned
        {
            get
            {
                _clonedCanTraceAnalyser = TargetSignalType.ParameterizeCanTraceAnalyser(Parameters);
                return _clonedCanTraceAnalyser;
            }

            private set
            {

                _clonedCanTraceAnalyser = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.CanTraceAnalyserCloned)));
            }
        }
        /// <summary>
        /// Concat the parameters from CANTx Item and Operation States
        /// To be provided by end user and configured into script
        /// </summary>
        /// 
        [DataMember]
        public ItemsChangeObservableCollection<CANTxParameter> Parameters
        {
            get
            {
                return _clonedParameters;

            }
            set
            {
                _clonedParameters = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Parameters)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestScript)));
            }

        }

        public void AddParameters(CANTxParameter par)
        {
            if (Parameters.FirstOrDefault(x => x.Name == par.Name) == null)
            {
                Parameters.Add(par);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Parameters)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.TestScript)));
            }

        }
        /// <summary>
        /// clone test sequence from opstate ,clone cantraceanalyser from targetSignalType and Parameters from both
        /// </summary>
        private void cloneArtifacts()
        {

            //_clonedTestSequence= (TestSequence)ConnectedOpState.TestSequence.Clone();
            //_clonedCanTraceAnalyser = (CanTraceAnalyser)TargetSignalType.CanTraceAnalyser.Clone();

            //clone the parameters
            //_clonedParameters.Clear();
            var tempCANTxParCollection = new ObservableCollection<CANTxParameter>(TargetSignalType.Parameters.Concat(ConnectedOpState.Parameters));
            foreach (CANTxParameter par in tempCANTxParCollection)
            {
                _clonedParameters.Add((CANTxParameter)par.Clone());
            }

        }
        /// <summary>
        /// Prefill Parameter values eg.signal name,only in the ctor
        /// </summary>
        private void PrepareParameters()
        {
            //trick for same sigtype and op to add the default signal value for a particular operation state
            if (TargetSignalType.Name == ConnectedOpState.Name)
            {
                CANTxParameter _sigOnValOpstate = new CANTxParameter("OpSigValue_" + ConnectedOpState.Name, "Signal value particular for Opstate " + ConnectedOpState.Name, "to be updated", typeof(int));
                AddParameters(_sigOnValOpstate);
            }

            //add signal name to operation state if the signal type is value target type

            if (TargetSignalType is ValueTargetSignalType)
            {
                CANTxParameter _sigOnValOpstate = new CANTxParameter("OpSigValue_" + ConnectedOpState.Name, "Signal value particular for Opstate " + ConnectedOpState.Name + ",ignore if value calculated automatically", "to be updated", typeof(int));
                AddParameters(_sigOnValOpstate);
                if (TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "Offset") != null && SourceCANTxSignal is DbcSignal)
                {

                    TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "Offset").Value = ((DbcSignal)SourceCANTxSignal).Offset.ToString();

                }
                if (TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "Factor") != null && SourceCANTxSignal is DbcSignal)
                {

                    TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "Factor").Value = ((DbcSignal)SourceCANTxSignal).Factor.ToString();

                }
            }

            if (TargetSignalType is MessageTargetType)
            {

                if (TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "CycleTime") != null && SourceCANTxSignal is DbcMessage)
                {

                    TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "CycleTime").Value = ((DbcMessage)SourceCANTxSignal).CycleTime.ToString();

                }

            }
            //init target signal name value
            if (TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "SourceCANTxSignal") != null)
            {

                TargetSignalType.Parameters.FirstOrDefault(x => x.Name == "SourceCANTxSignal").Value = SourceCANTxSignal is DbcMessage ? ((DbcMessage)SourceCANTxSignal).Name : ((DbcSignal)SourceCANTxSignal).Name;

            }

        }

        //to implement interface for contains check
        public int GetHashCode(Connection obj)
        {
            return obj.GetHashCode();
        }
        public bool Equals(Connection x, Connection y)
        {
            return x.SourceCANTxSignal.Equals(y.SourceCANTxSignal) && (x.TargetSignalType.Equals(y.TargetSignalType)) && (x.Variant.Equals(y.Variant));
        }

    }
}