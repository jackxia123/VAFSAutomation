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
    public class UndoDamageDLC : PairedDependentKeyword
    {
        private string _msgName;
        private ObservableCollection<string> _pairBefore = new ObservableCollection<string>();

        public UndoDamageDLC()
        {
            InitKeyword();
        }
        public UndoDamageDLC( string msgName)
        {
            InitKeyword();
            _msgName = msgName;
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "message")).ScalarValue = msgName;
                   

        }


        private void InitKeyword()
        {
            Name = "undoDamageDLC_";            
            Description = @"Set back to the normal DLC which is damaged by ‘damageDLC_xxx’?

>>‘xxx’ = CAN message name defined in CAN mapping file.
>>Use along with ‘damageDLC_xxx’.

e.g. test_sequence = @(…,’undoDamageDLC_Msg_A’,…)

";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;

            AddDependentParameter(new ScalarDependentParameter("message", "Message name to be damage DLC", "", ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {

            if ((!String.IsNullOrEmpty(MsgName)))
            {
                return true;

            }


                return false;

        }

        public string MsgName
        {

            get
            { return _msgName; }
            set
            {
                _msgName = value;

                SetProperty(ref _msgName, value);
            }


        }

        public override ObservableCollection<string> PairBefore
        {
            get
            {

                _pairBefore = new ObservableCollection<string>() { ScriptName.Replace(Name, "damageDLC_") };
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
