using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class UndoChecksumFail : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairBefore = new ObservableCollection<string>();
        public UndoChecksumFail()
        {
            InitKeyword();
        }
        public UndoChecksumFail(string SigNameChecksum)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameChecksum")).ScalarValue = SigNameChecksum;
        }

        private void InitKeyword()
        {
            Name = "undo_Checksum_fail_";            
            Description = @"Undo the checksum failure which is done by ‘Checksum_fail_xxx’.

>>‘xxx’ = Signal name defined in CANmapping file.
>>Use along with ‘Checksum_fail_xxx’.


";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;
            AddDependentParameter(new ScalarDependentParameter("SigNameChecksum", "signal name of the checksum to be reset", "", ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (!String.IsNullOrEmpty(((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameChecksum")).Value))
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairBefore
        {
            get
            {

                _pairBefore = new ObservableCollection<string>() { ScriptName.Replace(Name, "Checksum_fail_") };
                RaisePropertyChanged();
                return _pairBefore;
            }

            protected set
            {
                _pairBefore = value;
                SetProperty(ref _pairBefore, value);
            }
        }


    }
}
