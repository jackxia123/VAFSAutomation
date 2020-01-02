using RBT.Universal;
using RBT.Universal.Keywords;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace CANTxGenerator
{
    [DataContract]
    public class BusGeneric : Model
    {
        private bool _isNoFaultCanHUbattSh = false;
        private bool _isNoFaultCanLGndSh = false;
        private bool _isIncludeBusGeneric = false;
        private bool _IsNoFaultErrorPassive = false;
        private bool _IsNoFaultBusOffReset = false;
        private int _tDiagstartMs = 1500;
        private bool _isRegression = false;
        private string _thNUV = "Ax";
        private string _thHydrUV = "ESP_HHCIntervention";
        private string _thHydrHardUV = "ESP_BLRequestController";
        private string _thOV = "16.5";
        private string _doorsLink = "Gen_SWFS_0061_SWCS_HHC-628;Gen_SWFS_0061_SWCS_HHC-383;Gen_SWFS_0061_SWCS_HHC-390;Gen_SWFS_0061_SWCS_HHC-388;Gen_SWFS_0061_SWCS_HHC-492;Gen_SWFS_0061_SWCS_HHC-398;Gen_SWFS_0061_SWCS_HHC-397;Gen_SWFS_0061_SWCS_HHC-534;Gen_SWFS_0061_SWCS_HHC-255;Gen_SWFS_0061_SWCS_HHC-483;Gen_SWFS_0061_SWCS_HHC-624;Gen_SWFS_0061_SWCS_HHC-625;Gen_SWFS_0061_SWCS_HHC-633;Gen_SWFS_0061_SWCS_HHC-461;Gen_SWFS_0061_SWCS_HHC-635;Gen_SWFS_0061_SWCS_HHC-610;Gen_SWFS_0061_SWCS_HHC-269;Gen_SWFS_0061_SWCS_HHC-264;Gen_SWFS_0061_SWCS_HHC-646;Gen_RS_0061-352;Gen_RS_0061-383;Gen_RS_0061-355;Gen_RS_0061-368;Gen_RS_0061-641;Gen_RS_0061-443;Gen_RS_0061-907;Gen_SWFS_0061_SWCS_HHC-628;Gen_SWFS_0061_SWCS_HHC-383;Gen_SWFS_0061_SWCS_HHC-390;Gen_SWFS_0061_SWCS_HHC-388;Gen_SWFS_0061_SWCS_HHC-492;Gen_SWFS_0061_SWCS_HHC-398;Gen_SWFS_0061_SWCS_HHC-397;Gen_SWFS_0061_SWCS_HHC-534;Gen_SWFS_0061_SWCS_HHC-255;Gen_SWFS_0061_SWCS_HHC-483;Gen_SWFS_0061_SWCS_HHC-624;Gen_SWFS_0061_SWCS_HHC-625;Gen_SWFS_0061_SWCS_HHC-633;Gen_SWFS_0061_SWCS_HHC-461;Gen_SWFS_0061_SWCS_HHC-635;Gen_SWFS_0061_SWCS_HHC-610;Gen_SWFS_0061_SWCS_HHC-269;Gen_SWFS_0061_SWCS_HHC-264;Gen_RS_0061-423;Gen_RS_0061-647;Gen_RS_0061-907";
        private string _productType = "ESP";

        //when any property changed, it will notify testscript object.-> Jack comments->20180927
        private ObservableCollection<TestScript> _busGenericTScript = new ObservableCollection<TestScript>();


        public BusGeneric()
        {


        }

        [DataMember]
        public bool IsNoFaultCanHUbattSh
        {
            get { return _isNoFaultCanHUbattSh; }
            set { SetProperty(ref _isNoFaultCanHUbattSh, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }

        [DataMember]
        public bool IsNoFaultCanLGndSh
        {
            get { return _isNoFaultCanLGndSh; }
            set { SetProperty(ref _isNoFaultCanLGndSh, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }

        [DataMember]
        public bool IsIncludeBusGeneric
        {
            get { return _isIncludeBusGeneric; }
            set
            {
                SetProperty(ref _isIncludeBusGeneric, value);
                RaisePropertyChanged(nameof(BusGenericTScript));

            }

        }

        // Special Projects have special requirements
        [DataMember]
        public bool IsNoFaultErrorPassive
        {
            get
            {
                return _IsNoFaultErrorPassive;
            }
            set
            {
                SetProperty(ref _IsNoFaultErrorPassive, value);
                RaisePropertyChanged(nameof(BusGenericTScript));
            }
        }

        // Special Projects have special requirements
        [DataMember]
        public bool IsNoFaultBusOffReset
        {
            get
            {
                return _IsNoFaultBusOffReset;
            }
            set
            {
                SetProperty(ref _IsNoFaultBusOffReset, value);
                RaisePropertyChanged(nameof(BusGenericTScript));
            }
        }

        // try to change double to string
        [DataMember]
        public int TdiagstartMs
        {
            get { return _tDiagstartMs; }
            set { SetProperty(ref _tDiagstartMs, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }

        [DataMember]
        public bool IsRegression
        {
            get { return _isRegression; }
            set { SetProperty(ref _isRegression, value); }

        }
        // try to change double to string
        [DataMember]
        public string ThresholdNUV
        {
            get { return _thNUV; }
            set
            {
                SetProperty(ref _thNUV, value); RaisePropertyChanged(nameof(BusGenericTScript));
            }

        }
        // try to change double to string
        [DataMember]
        public string ThresholdHydrUV
        {
            get { return _thHydrUV; }
            set { SetProperty(ref _thHydrUV, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }
        // try to change double to string
        [DataMember]
        public string ThresholdHydrHardUV
        {
            get { return _thHydrHardUV; }
            set { SetProperty(ref _thHydrHardUV, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }
        // try to change double to string
        [DataMember]
        public string ThresholdOV
        {
            get { return _thOV; }
            set { SetProperty(ref _thOV, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }
        [DataMember]
        public string DoorsLink
        {
            get { return _doorsLink; }
            set { SetProperty(ref _doorsLink, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }

        [DataMember]
        public string ProductType
        {
            get { return _productType; }
            set { SetProperty(ref _productType, value); RaisePropertyChanged(nameof(BusGenericTScript)); }

        }

        [DataMember]
        public ObservableCollection<TestScript> BusGenericTScript
        {

            get
            {
                _busGenericTScript.Clear();
                if (IsIncludeBusGeneric)
                {
                    if (IsRegression)
                    {
                        //Todo: define the regression test
                    }
                    else
                    {
                        var temp = TcLibBusGeneric.GetTScriptUD(ProductType, IsNoFaultCanHUbattSh, IsNoFaultCanLGndSh, ThresholdNUV, ThresholdHydrUV, ThresholdHydrHardUV);
                        _busGenericTScript = new ObservableCollection<TestScript>(
                            temp);

                    }

                    foreach (var ts in _busGenericTScript)
                    {
                        ts.DoorsLink = DoorsLink;
                        ts.QcFolderPath = @"RBT_VAFS\HHCGeneric_" + DateTime.Now.ToString("yyyy_MMM_ddd_HHmmss");
                    }
                }

                return _busGenericTScript;

            }
            set { SetProperty(ref _busGenericTScript, value); }

        }

    }
}
