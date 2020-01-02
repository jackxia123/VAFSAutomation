using RBT.Universal;
using RBT.Universal.Keywords;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace CANTxGenerator
{
    [DataContract]
    public class CDDGeneric : Model
    {
        #region Private Fields
        private bool _isapbMi = false;

        private bool _isIncludeCDDGeneric = false;

        private string _accTgtAx = "ACC_TgtAx";

        private string _accTgtAxLowerLim = "ACC_TgtAxLowerLim";

        private string _accTgtAxUpperLim = "ACC_TgtAxUpperLim";

        private string _accTgtAxLowerComftBand = "ACC_TgtAxLowerComftBand";

        private string _accTgtAxUpperComftBand = "ACC_TgtAxUpperComftBand";

        private string _accMode = "ACC_AccMode";


        private string _cddActive = "ESP_CDD_Intervention";

        private string _cddAvailable = "ESP_CDD_Available";

        private string _doorsLink = "Gen_RS_0307-601;Gen_RS_0307-793;Gen_RS_0307-606;Gen_RS_0307-674;Gen_RS_0307-590;Gen_RS_0307-672";

        #endregion
        private ObservableCollection<TestScript> _cddGenericTScript = new ObservableCollection<TestScript>();

        #region Constructor
        public CDDGeneric()
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
                RaisePropertyChanged(nameof(CDDGenericTScript));
            }
        }

        /// <summary>
        /// Whether support CDD 
        /// </summary>
        [DataMember]
        public bool IsIncludeCDDGeneric
        {
            get { return _isIncludeCDDGeneric; }
            set
            {
                SetProperty(ref _isIncludeCDDGeneric, value);
                RaisePropertyChanged(nameof(CDDGenericTScript));

            }

        }

        /// <summary>
        /// Requirements links to doors
        /// </summary>
        [DataMember]
        public string DoorsLink
        {
            get { return _doorsLink; }
            set { SetProperty(ref _doorsLink, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }

        }

        /// <summary>
        ///  Target Deceleration speed Net signal Name
        /// </summary>
        [DataMember]
        public string ACCTgtAx
        {
            get { return _accTgtAx; }
            set { SetProperty(ref _accTgtAx, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        /// <summary>
        /// ACC target Ax lower Limit net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxLowerLim
        {
            get { return _accTgtAxLowerLim; }
            set { SetProperty(ref _accTgtAxLowerLim, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        /// <summary>
        /// ACC target ax upper limit net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxUpperLim
        {
            get { return _accTgtAxUpperLim; }
            set { SetProperty(ref _accTgtAxUpperLim, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        /// <summary>
        /// ACC target ax lower comfort band net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxLowerComftBand
        {
            get { return _accTgtAxLowerComftBand; }
            set { SetProperty(ref _accTgtAxLowerComftBand, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        /// <summary>
        /// ACC target ax upper comfort band net signal name
        /// </summary>
        [DataMember]
        public string ACCTgtAxUpperComftBand
        {
            get { return _accTgtAxUpperComftBand; }
            set { SetProperty(ref _accTgtAxUpperComftBand, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        /// <summary>
        /// ACC Mode net signal name
        /// </summary>
        [DataMember]
        public string ACCMode
        {
            get { return _accMode; }
            set { SetProperty(ref _accMode, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }


        /// <summary>
        /// VLC active net signal name
        /// </summary>
        [DataMember]
        public string CDDActive
        {
            get { return _cddActive; }
            set { SetProperty(ref _cddActive, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        /// <summary>
        /// CDD available net signal name
        /// </summary>
        [DataMember]
        public string CDDAvailable
        {
            get { return _cddAvailable; }
            set { SetProperty(ref _cddAvailable, value); RaisePropertyChanged(nameof(CDDGenericTScript)); }
        }

        [DataMember]
        public ObservableCollection<TestScript> CDDGenericTScript
        {
            get
            {
                _cddGenericTScript.Clear();
                if (IsIncludeCDDGeneric)
                {
                    //var temp = TcLibVlcGeneric.GetTScriptVLC(IsApbMi, ACCTgtAx, ACCBrakePreferred, ACCTgtAxLowerLim, ACCTgtAxUpperLim, ACCTgtAxLowerComftBand, ACCTgtAxUpperComftBand, ACCMode, ACCVlcShutdownReq, VLCActive, VLCAvailable);
                    //_cddGenericTScript = new ObservableCollection<TestScript>(temp);
                    var temp = TcLibCddGeneric.GetTScriptCDD(IsApbMi, ACCTgtAx, ACCTgtAxLowerLim, ACCTgtAxUpperLim, ACCTgtAxLowerComftBand, ACCTgtAxUpperComftBand, ACCMode, CDDActive, CDDAvailable);
                    _cddGenericTScript = new ObservableCollection<TestScript>(temp);
                }
                foreach (var ts in _cddGenericTScript)
                {
                    ts.DoorsLink = DoorsLink;
                    ts.QcFolderPath = @"RBT_VAFS\CDDGeneric_" + DateTime.Now.ToString("yyyy_MMM_ddd_HHmmss");
                }
                return _cddGenericTScript;
            }
            set
            {
                SetProperty(ref _cddGenericTScript, value);
            }
        }

        #endregion

    }
}
