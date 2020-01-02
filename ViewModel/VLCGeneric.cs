using RBT.Universal;
using RBT.Universal.Keywords;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
namespace CANTxGenerator
{
    /// <summary>
    /// It is for VLC generic paramter
    /// </summary>
    [DataContract]
    public class VLCGeneric : Model
    {
        #region Private fields
        private bool _isapbMi = false;

        private bool _isIncludeVLCGeneric = false;

        private string _accTgtAx = "ACC_TgtAx";

        private string _accBrakePreferred = "ACC_BrakePreferred";

        private string _accTgtAxLowerLim = "ACC_TgtAxLowerLim";

        private string _accTgtAxUpperLim = "ACC_TgtAxUpperLim";

        private string _accTgtAxLowerComftBand = "ACC_TgtAxLowerComftBand";

        private string _accTgtAxUpperComftBand = "ACC_TgtAxUpperComftBand";

        private string _accMode = "ACC_AccMode";

        private string _accVlcShutdownReq = "ACC_VLC_ShutdownReq";

        private string _vlcActive = "VLCActive";

        private string _vlcAvailable = "ESP_VLC_Available";

        private string _doorsLink = "Gen_SWRS_ASW_VLC-157;Gen_SWRS_ASW_VLC-123;Gen_SWRS_ASW_VLC-162;Gen_SWRS_ASW_VLC-163;Gen_RS_1050-26;Gen_SWRS_ASW_VLC-157;Gen_SWRS_ASW_VLC-162;Gen_SWRS_ASW_VLC-160;Gen_RS_1050-26;Gen_RS_1050-111";

        #endregion


        private ObservableCollection<TestScript> _vlcGenericTScript = new ObservableCollection<TestScript>();

        #region Constructor
        public VLCGeneric()
        {

        }
        #endregion

        #region Public Property
        /// <summary>
        /// Whether has Apbmi 
        /// </summary>
        [DataMember]
        public bool IsApbMi
        {
            get
            {
                return _isapbMi;
            }
            set
            {
                SetProperty(ref _isapbMi, value);
                RaisePropertyChanged(nameof(VLCGenericTScript));
            }
        }

        /// <summary>
        /// Whether support VLC 
        /// </summary>
        [DataMember]
        public bool IsIncludeVLCGeneric
        {
            get { return _isIncludeVLCGeneric; }
            set
            {
                SetProperty(ref _isIncludeVLCGeneric, value);
                RaisePropertyChanged(nameof(VLCGenericTScript));

            }

        }

        /// <summary>
        /// Requirements links to doors
        /// </summary>
        [DataMember]
        public string DoorsLink
        {
            get { return _doorsLink; }
            set { SetProperty(ref _doorsLink, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }

        }

        /// <summary>
        ///  Target Deceleration speed Net signal Name
        /// </summary>
        [DataMember]
        public string ACCTgtAx
        {
            get { return _accTgtAx; }
            set { SetProperty(ref _accTgtAx, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// Brake Preferred Net signal Name
        /// </summary>
        [DataMember]
        public string ACCBrakePreferred
        {
            get { return _accBrakePreferred; }
            set { SetProperty(ref _accBrakePreferred, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }

        }

        /// <summary>
        /// ACC target Ax lower Limit net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxLowerLim
        {
            get { return _accTgtAxLowerLim; }
            set { SetProperty(ref _accTgtAxLowerLim, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// ACC target ax upper limit net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxUpperLim
        {
            get { return _accTgtAxUpperLim; }
            set { SetProperty(ref _accTgtAxUpperLim, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// ACC target ax lower comfort band net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxLowerComftBand
        {
            get { return _accTgtAxLowerComftBand; }
            set { SetProperty(ref _accTgtAxLowerComftBand, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// ACC target ax upper comfort band net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxUpperComftBand
        {
            get { return _accTgtAxUpperComftBand; }
            set { SetProperty(ref _accTgtAxUpperComftBand, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// ACC Mode net signal name
        /// </summary>
        [DataMember]
        public string ACCMode
        {
            get { return _accMode; }
            set { SetProperty(ref _accMode, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// ACC vlc shutdown Request net signal name
        /// </summary>
        [DataMember]
        public string ACCVlcShutdownReq
        {
            get { return _accVlcShutdownReq; }
            set { SetProperty(ref _accVlcShutdownReq, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// VLC active net signal name
        /// </summary>
        [DataMember]
        public string VLCActive
        {
            get { return _vlcActive; }
            set { SetProperty(ref _vlcActive, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        /// <summary>
        /// VLC available net signal name
        /// </summary>
        [DataMember]
        public string VLCAvailable
        {
            get { return _vlcAvailable; }
            set { SetProperty(ref _vlcAvailable, value); RaisePropertyChanged(nameof(VLCGenericTScript)); }
        }

        [DataMember]
        public ObservableCollection<TestScript> VLCGenericTScript
        {
            get
            {
                _vlcGenericTScript.Clear();
                if (IsIncludeVLCGeneric)
                {
                    var temp = TcLibVlcGeneric.GetTScriptVLC(IsApbMi, ACCTgtAx, ACCBrakePreferred, ACCTgtAxLowerLim, ACCTgtAxUpperLim, ACCTgtAxLowerComftBand, ACCTgtAxUpperComftBand, ACCMode, ACCVlcShutdownReq, VLCActive, VLCAvailable);
                    _vlcGenericTScript = new ObservableCollection<TestScript>(temp);
                }
                foreach (var ts in _vlcGenericTScript)
                {
                    ts.DoorsLink = DoorsLink;
                    ts.QcFolderPath = @"RBT_VAFS\VLCGeneric_" + DateTime.Now.ToString("yyyy_MMM_ddd_HHmmss");
                }
                return _vlcGenericTScript;
            }
            set
            {
                SetProperty(ref _vlcGenericTScript, value);
            }
        }

        #endregion

    }
}