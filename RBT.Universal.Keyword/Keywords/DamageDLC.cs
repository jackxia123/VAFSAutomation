using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class DamageDLC : PairedDependentKeyword
    {
        private string _msgName;
        private string _reduction;
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public DamageDLC()
        {
            InitKeyword();
        }
        public DamageDLC( string msgName)
        {
            InitKeyword();
            _msgName = msgName;
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "message")).ScalarValue = msgName;
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "reduction")).ScalarValue = @"-1";

        }
        public DamageDLC(string reduction,string msgName)
        {
            InitKeyword();
            _msgName = msgName;
            _reduction = reduction;
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "message")).ScalarValue = msgName;
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "reduction")).ScalarValue = reduction;
        }

        private void InitKeyword()
        {
            Name = "damageDLC_";            
            Description = @"Damage DLC of a particular CAN message in LabCar CAN modules.
The number of reduction of DLC can be given. (-n / +n)

>>‘xxx’ = CAN message name defined in CAN mapping file.
>>‘n’ = the number of reduction of DLC.
>>Use along with ‘undoDamageDLC_xxx’.

e.g.1 test_sequence = @(…,’damageDLC_Msg_A’,…)
e.g.2 test_sequence = @(…,’damageDLC_-3_Msg_A’,…)
e.g.3 test_sequence = @(…,’damageDLC_+2_Msg_A’,…)
";
            Delay = 0;
            UsageLimit = 255;

            PairType = KeywordPairType.One2n;

           
            AddDependentParameter(new ScalarDependentParameter("reduction", "number of DLC to be reduced or increased with +/-,can be omitted", "", "-1"));
            AddDependentParameter(new ScalarDependentParameter("message", "Message name to be Enabled", "", ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {

            if ((!String.IsNullOrEmpty(MsgName)) && (Reduction == null || (Regex.IsMatch(Reduction, @"[+-]\d+"))))
            {
                return true;

            }
            else if ((!String.IsNullOrEmpty(MsgName)) && (Reduction == null))
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

        public string Reduction
        {

            get
            { return _reduction; }
            set
            {
                _reduction = value;

                SetProperty(ref _reduction, value);
            }


        }
        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter = new ObservableCollection<string>() { ScriptName.Replace(Name, "undoDamageDLC_"), "reset_CAN_manipulation" };
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
