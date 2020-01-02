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
    public class ChecksumFail : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public ChecksumFail()
        {
            InitKeyword();
        }
        public ChecksumFail(string SigNameChecksum)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameChecksum")).ScalarValue = SigNameChecksum;
        }

        private void InitKeyword()
        {
            Name = "Checksum_fail_";            
            Description = @"Make checksum failure.

>>‘xxx’ = Signal name defined in CANmapping file.
>>Failure manipulation function has to be implemented for checksum signals in LabCar CAN module.
>>Use along with ‘undo_Checksum_fail_xxx’.

e.g. test_sequence = @(…,’Checksum_fail_SigA’ ,…,’undo_Checksum_fail_SigA’,…)

";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;
            AddDependentParameter(new ScalarDependentParameter("SigNameChecksum", "Signal name of checksum to be made fail", "", ""));
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

        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter = new ObservableCollection<string>() { ScriptName.Replace(Name, "undo_Checksum_fail_") };
                RaisePropertyChanged();
                return _pairAfter;
            }

            protected set
            {
                _pairAfter = value;
                SetProperty(ref _pairAfter, value);
            }
        }

    }
}
