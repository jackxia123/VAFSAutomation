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
    public class UndoEditSignal : PairedDependentKeyword
    {
        ObservableCollection<string> _pairBefore = new ObservableCollection<string>();
        public UndoEditSignal()
        {
            InitKeyword();
        }
        public UndoEditSignal(string signalName)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SignalName")).ScalarValue = signalName;
            
        }

        private void InitKeyword()
        {
            Name = "undoEditSignal_";            
            Description = @"Reset the values of the CAN signals back to its original values which are manipulated by ‘editSignal_xxx’.

>>‘xxx’ = CAN signal name defined in CAN mapping file.
>>Use along with ‘editSignal_xxx’.



";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;
            AddDependentParameter(new ScalarDependentParameter("SignalName", "signal name to be reset editing", "", ""));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (!String.IsNullOrEmpty(((ScalarDependentParameter)DependentParameters.First(x => x.Name == "SignalName")).Value))
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairBefore
        {
            get
            {

                _pairBefore = new ObservableCollection<string>() { ScriptName.Replace(Name, "EditSignal_") };
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
