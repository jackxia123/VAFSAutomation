using RBT.Universal;
using RBT.Universal.Keywords;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace CANTxGenerator
{
    [DataContract]
    public class NetworkPar : Model
    {

        private int _tfirstframehigh = 200;
        private int _tfirstframelow = 0;
        private int _tlampinit = 3000;
        private int _tlampinitTor = 500;
        private ItemsChangeObservableCollection<NameValue> _initLamps = new ItemsChangeObservableCollection<NameValue>();
        private ItemsChangeObservableCollection<NameValue> _initSignals = new ItemsChangeObservableCollection<NameValue>();

        private int _tpostrunstdstl = 600;
        private int _tpostrunning = 1800;
        private int _delaypostrun = 0;
        private string _spdthrpstrkph = "1";

        private string _thNUV = "9";
        private string _thOV = "16";
        private string _doorsLinkPostrun = "";
        private string _doorsLinkTiming = "";
        private string _doorsLinkVolt = "";
        private string _thNUVReset = "10";
        private string _thOVReset = "15";

        private string _postrunLamp = "ESP_Fault";
        private int _postrunLampValue = 1;
        private string _productType = "ESP";

        private bool _isSelectedPostrun = false;
        private bool _isSelectedTxTiming = false;
        private bool _isSelectedVoltage = false;
        private string _nuvSpdLimitKph = "6";
        private double _nuvSpdTor = 1;
        private string _VoltTor = "0.5";

        // add for Regression ->jack add it 20171206
        private bool _isRegression = false;

        private ObservableCollection<TestScript> _networkTScript = new ObservableCollection<TestScript>();
        public NetworkPar()
        {
            RegisterChildChangedEvent();
        }

        public void RegisterChildChangedEvent()
        {
            InitLamps.ChildElementPropertyChanged += ItemConnections_ChildElementPropertyChanged;
        }

        private void ItemConnections_ChildElementPropertyChanged(ItemsChangeObservableCollection<NameValue>.ChildElementPropertyChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(InitLamps)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(InitSignals)));

            RaisePropertyChanged(nameof(NetworkParTScript));
        }

        [DataMember]
        public int TFirstFrameHigh
        {
            get { return _tfirstframehigh; }
            set { SetProperty(ref _tfirstframehigh, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public int TFirstFrameLow
        {
            get { return _tfirstframelow; }
            set { SetProperty(ref _tfirstframelow, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public int TLampInit
        {
            get { return _tlampinit; }
            set { SetProperty(ref _tlampinit, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public int TLampInitTolerance
        {
            get { return _tlampinitTor; }
            set { SetProperty(ref _tlampinitTor, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public ItemsChangeObservableCollection<NameValue> InitLamps
        {
            get { return _initLamps; }
            set { SetProperty(ref _initLamps, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        public void AddInitLamps(string key, int val)
        {
            InitLamps.Add(new NameValue(key, val));
            RaisePropertyChanged(nameof(InitLamps));
            RaisePropertyChanged(nameof(NetworkParTScript));
        }

        public void RemoveInitLamps(NameValue nv)
        {
            if (InitLamps.Contains(nv))
            {
                InitLamps.Remove(nv);
                RaisePropertyChanged(nameof(InitLamps));
                RaisePropertyChanged(nameof(NetworkParTScript));

            }

        }

        [DataMember]
        public ItemsChangeObservableCollection<NameValue> InitSignals
        {
            get { return _initSignals; }
            set { SetProperty(ref _initSignals, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        public void AddInitSig(string key, int val)
        {
            InitSignals.Add(new NameValue(key, val));
            RaisePropertyChanged(nameof(InitSignals));
            RaisePropertyChanged(nameof(NetworkParTScript));
        }

        public void RemoveInitSig(NameValue nv)
        {
            if (InitSignals.Contains(nv))
            {
                InitSignals.Remove(nv);
                RaisePropertyChanged(nameof(InitSignals));
                RaisePropertyChanged(nameof(NetworkParTScript));

            }

        }

        #region postrun config

        [DataMember]
        public int TPostrunStdStl
        {
            get { return _tpostrunstdstl; }
            set { SetProperty(ref _tpostrunstdstl, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public int TPostrunning
        {
            get { return _tpostrunning; }
            set { SetProperty(ref _tpostrunning, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public int DelayPostrun
        {
            get { return _delaypostrun; }
            set { SetProperty(ref _delaypostrun, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public string SpdThrPstrKph
        {
            get { return _spdthrpstrkph; }
            set { SetProperty(ref _spdthrpstrkph, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }


        #endregion

        #region Voltage config

        [DataMember]
        public string ThresholdNUV
        {
            get { return _thNUV; }
            set { SetProperty(ref _thNUV, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public string ThresholdOV
        {
            get { return _thOV; }
            set { SetProperty(ref _thOV, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public string ThresholdNUVReset
        {
            get { return _thNUVReset; }
            set { SetProperty(ref _thNUVReset, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public string ThresholdOVReset
        {
            get { return _thOVReset; }
            set { SetProperty(ref _thOVReset, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        #endregion

        [DataMember]
        public string PostrunLamp
        {
            get { return _postrunLamp; }
            set { SetProperty(ref _postrunLamp, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public string ProductType
        {
            get { return _productType; }
            set { SetProperty(ref _productType, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }


        [DataMember]
        public int PostrunLampValue
        {
            get { return _postrunLampValue; }
            set { SetProperty(ref _postrunLampValue, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }


        [DataMember]
        public string DoorsLinkPostRun
        {
            get { return _doorsLinkPostrun; }
            set { SetProperty(ref _doorsLinkPostrun, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public string DoorsLinkTxTiming
        {
            get { return _doorsLinkTiming; }
            set { SetProperty(ref _doorsLinkTiming, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public string DoorsLinkVoltage
        {
            get { return _doorsLinkVolt; }
            set { SetProperty(ref _doorsLinkVolt, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public bool IsSelectPostrun
        {
            get { return _isSelectedPostrun; }
            set { SetProperty(ref _isSelectedPostrun, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public bool IsSelectTxTiming
        {
            get { return _isSelectedTxTiming; }
            set { SetProperty(ref _isSelectedTxTiming, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public bool IsSelectVolt
        {
            get { return _isSelectedVoltage; }
            set { SetProperty(ref _isSelectedVoltage, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public string NuvSpdLimitKph
        {
            get { return _nuvSpdLimitKph; }
            set { SetProperty(ref _nuvSpdLimitKph, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public double NuvSpdTolerance
        {
            get { return _nuvSpdTor; }
            set { SetProperty(ref _nuvSpdTor, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        [DataMember]
        public string VoltTolerance
        {
            get { return _VoltTor; }
            set { SetProperty(ref _VoltTor, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }
        // add for Regression Test, when User select Regression, it will add RB_CAT property to R
        [DataMember]
        public bool IsRegression
        {
            get { return _isRegression; }
            set { SetProperty(ref _isRegression, value); RaisePropertyChanged(nameof(NetworkParTScript)); }

        }

        [DataMember]
        public ObservableCollection<TestScript> NetworkParTScript
        {

            get
            {
                _networkTScript.Clear();

                //get postrun test case
                if (IsSelectPostrun)
                {
                    if (IsRegression)
                    {
                        //it will add RB_CAT property to R
                    }
                    else
                    {
                        _networkTScript.Add(TcLibNetworkPar.GetTScriptPostrunningTimeCheck(TPostrunning, DelayPostrun, DoorsLinkPostRun, PostrunLamp, PostrunLampValue, ProductType));
                        _networkTScript.Add(TcLibNetworkPar.GetTScriptPostrunStandstillTimeCheck(TPostrunStdStl, DelayPostrun, DoorsLinkPostRun, PostrunLamp, PostrunLampValue, ProductType));
                        _networkTScript.Add(TcLibNetworkPar.GetTScriptPostrunSpdThresholdUpperCheck(SpdThrPstrKph, TPostrunStdStl, DelayPostrun, DoorsLinkPostRun, PostrunLamp, PostrunLampValue, ProductType));
                        _networkTScript.Add(TcLibNetworkPar.GetTScriptPostrunSpdThresholdLowerCheck(SpdThrPstrKph, TPostrunStdStl, DelayPostrun, DoorsLinkPostRun, PostrunLamp, PostrunLampValue, ProductType));
                    }
                }

                if (IsSelectTxTiming)
                {
                    _networkTScript.Add(TcLibNetworkPar.GetTScriptTxTimingInit(TFirstFrameLow, TFirstFrameHigh, TLampInit, TLampInitTolerance, DoorsLinkTxTiming, InitLamps));
                    _networkTScript.Add(TcLibNetworkPar.GetTScriptTxInitValues(DoorsLinkTxTiming, InitSignals));

                }

                if (IsSelectVolt)
                {
                    if (double.Parse(NuvSpdLimitKph) > 0)
                    {
                        _networkTScript.Add(TcLibNetworkPar.GetTScriptNetUnderVoltageSpdNotReached(ThresholdNUV, DoorsLinkVoltage, ProductType, NuvSpdLimitKph, VoltTolerance, NuvSpdTolerance));

                    }
                    _networkTScript.Add(TcLibNetworkPar.GetTScriptNetUnderVoltageSet(ThresholdNUV, DoorsLinkVoltage, ProductType, NuvSpdLimitKph, VoltTolerance, NuvSpdTolerance));
                    _networkTScript.Add(TcLibNetworkPar.GetTScriptNetUnderVoltageReset(ThresholdNUV, ThresholdNUVReset, DoorsLinkVoltage, ProductType, NuvSpdLimitKph, VoltTolerance, NuvSpdTolerance));
                    _networkTScript.Add(TcLibNetworkPar.GetTScriptNetOverVoltageSet(ThresholdOV, DoorsLinkVoltage, ProductType, 0, VoltTolerance, NuvSpdTolerance));
                    _networkTScript.Add(TcLibNetworkPar.GetTScriptNetOverVoltageReset(ThresholdOV, ThresholdOVReset, DoorsLinkVoltage, ProductType, 0, VoltTolerance, NuvSpdTolerance));

                }


                return _networkTScript;

            }
            set { SetProperty(ref _networkTScript, value); }

        }


    }
}
