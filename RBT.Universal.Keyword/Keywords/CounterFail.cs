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
    public class CounterFail : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public CounterFail()
        {
            InitKeyword();
        }
        public CounterFail(string SigNameCounter)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameCounter")).ScalarValue = SigNameCounter;
        }

        private void InitKeyword()
        {
            Name = "Counter_fail_";            
            Description = @"Make counter failure.

>>‘xxx’ = Signal name defined in CANmapping file.
>>Failure manipulation function has to be implemented for counter signals in LabCar CAN module.
>>Use along with ‘undo_Counter_fail_xxx’.

e.g. test_sequence = @(…,’Counter_fail_SigA’,…,’undo_Counter_fail_SigA’,…)

";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;
            AddDependentParameter(new ScalarDependentParameter("SigNameCounter", "signal name of counter to be made fail", "", ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (!String.IsNullOrEmpty(((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameCounter")).Value))
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter = new ObservableCollection<string>() { ScriptName.Replace(Name, "undo_Counter_fail_") };
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
