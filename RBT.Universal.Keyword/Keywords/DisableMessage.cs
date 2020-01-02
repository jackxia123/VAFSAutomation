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
    public class DisableMessage : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public DisableMessage()
        {
            InitKeyword();
        }
        public DisableMessage(string msgName)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "message")).ScalarValue = msgName;
        }

        private void InitKeyword()
        {
            Name = "disableMessage_";            
            Description = @"Disable a particular CAN message in LabCar CAN modules. 

>>‘xxx’ = CAN message name defined in CAN mapping file.
>>Use along with ‘enableMessage_xxx’.

e.g. test_sequence = @(…,’disableMessage_Msg_A’,…)

";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2n;
            AddDependentParameter(new ScalarDependentParameter("message", "Message name to be disabled", "", ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (!String.IsNullOrEmpty(((ScalarDependentParameter)DependentParameters.First(x => x.Name == "message")).Value))
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter = new ObservableCollection<string>() { ScriptName.Replace(Name, "enableMessage_"), "reset_CAN_manipulation" };
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
