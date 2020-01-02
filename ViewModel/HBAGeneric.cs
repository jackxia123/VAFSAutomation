using RBT.Universal;
using RBT.Universal.Keywords;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace CANTxGenerator
{
    /// <summary>
    /// HBA generic parameters
    /// </summary>
    [DataContract]
    public class HBAGeneric : Model
    {
        #region Private fields

        private bool _isapbMi = false;

        private bool _isIncludeHBAGeneric = false;

        private bool _isABAInclude = false;

        private string _abaIntervention = "ESP_ABAIntervention";

        private string _abaRequest = "ACC_ABA_Req";

        private string _abaLevel = "ACC_ABA_Level";

        private string _doorsLink = "Gen_RS_0504-484;Gen_RS_0504-1608;Gen_RS_0504-1853;Gen_RS_0504-288;Gen_RS_0504-2085;Gen_SWFS_0057_SWCS_HRB-439;Gen_RS_0504-276;Gen_RS_0504-402;Gen_RS_0504-285;Gen_RS_0504-280";

        #endregion

        private ObservableCollection<TestScript> _hbaGenericTScript = new ObservableCollection<TestScript>();

        #region Constructor
        public HBAGeneric()
        {

        }
        #endregion

        #region Public Property
        /// <summary>
        /// Whether includes APBMi
        /// </summary>
        [DataMember]
        public bool IsApbMi
        {
            get { return _isapbMi; }
            set
            {
                SetProperty(ref _isapbMi, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }
        }

        /// <summary>
        /// Check whether include HBA VAFS
        /// </summary>
        [DataMember]
        public bool IsIncludeHBAGeneric
        {
            get { return _isIncludeHBAGeneric; }
            set
            {
                SetProperty(ref _isIncludeHBAGeneric, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }
        }

        /// <summary>
        /// Check whether your project include ABA
        /// </summary>
        [DataMember]
        public bool IsABAInclude
        {
            get { return _isABAInclude; }
            set
            {
                SetProperty(ref _isABAInclude, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }

        }

        /// <summary>
        /// Doors link requirment ID
        /// </summary>
        [DataMember]
        public string DoorsLink
        {
            get { return _doorsLink; }
            set
            {
                SetProperty(ref _doorsLink, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }

        }

        /// <summary>
        /// ABA active net signal name
        /// </summary>
        [DataMember]
        public string ABAIntervention
        {
            get { return _abaIntervention; }
            set
            {
                SetProperty(ref _abaIntervention, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }
        }

        /// <summary>
        /// ABA 
        /// </summary>
        [DataMember]
        public string ABARequest
        {
            get { return _abaRequest; }
            set
            {
                SetProperty(ref _abaRequest, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }
        }

        /// <summary>
        /// ABA level
        /// </summary>
        [DataMember]
        public string ABALevel
        {
            get { return _abaLevel; }
            set
            {
                SetProperty(ref _abaLevel, value);
                RaisePropertyChanged(nameof(HBAGenericTScript));
            }
        }


        [DataMember]
        public ObservableCollection<TestScript> HBAGenericTScript
        {
            get
            {
                _hbaGenericTScript.Clear();
                if (IsIncludeHBAGeneric)
                {
                    var temp = TcLibHBAGeneric.GetTScriptHBA(IsApbMi, IsABAInclude, ABAIntervention, ABARequest, ABALevel);
                    _hbaGenericTScript = new ObservableCollection<TestScript>(temp);

                }
                foreach (var ts in _hbaGenericTScript)
                {
                    ts.DoorsLink = DoorsLink;

                    ts.QcFolderPath = @"RBT_VAFS\HBAGeneric_" + DateTime.Now.ToString("yyyy_MMM_ddd_HHmmss");
                }
                return _hbaGenericTScript;
            }
            set
            {
                SetProperty(ref _hbaGenericTScript, value);
            }
        }
        #endregion
    }
}