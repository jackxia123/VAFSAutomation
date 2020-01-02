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
    public class UndoCounterFail : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairBefore = new ObservableCollection<string>();
        public UndoCounterFail()
        {
            InitKeyword();
        }
        public UndoCounterFail(string SigNameCounter)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameCounter")).ScalarValue = SigNameCounter;
        }

        private void InitKeyword()
        {
            Name = "undo_Counter_fail_";            
            Description = @"Undo the counter failure which is done by ‘Counter_fail_xxx’.

>>‘xxx’ = Signal name defined in CANmapping file.
>>Use along with ‘Counter_fail_xxx’.


";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;

            AddDependentParameter(new ScalarDependentParameter("SigNameCounter", "signal name of counter to be reset", "", ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }
        /// <summary>
        /// only the name of signal for counter is not empty
        /// </summary>
        /// <returns></returns>
        public override bool ParametersValidated()
        {
            
            if (!String.IsNullOrEmpty(((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SigNameCounter")).Value))
            {
                return true;

            }

            return false;

        }


        public override ObservableCollection<string> PairBefore
        {
            get
            {

                _pairBefore = new ObservableCollection<string>() { ScriptName.Replace(Name, "Counter_fail_") };
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
