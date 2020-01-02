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
    public class ReadCanSignals : DependentKeyword
    {
        public ReadCanSignals()
        {
            InitKeyword();
        }
        public ReadCanSignals(ObservableCollection<string> SignalStrategies)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "CAN_signals_expected")).ValueList = SignalStrategies;
        }

        private void InitKeyword()
        {
            Name = "read_CAN_signals";            
            Description = @"Read CAN signals and evaluate detected CAN signal values by the strategy defined in Project Default file.

>>Need to define strategy in Project Default file.



";
            Delay = 0;
            UsageLimit = 255;

           AddDependentParameter(new ListDependentParameter("CAN_signals_expected", @"Specify the strategy name defined in Project Default file.

>>Expected values defined in Project Default file can be only physical value. (decimal value with a factor/offset)

e.g. test_sequence = @(…,’read_CAN_signals’,…’read_CAN_signals’,…)
       CAN_signals_expected = @(‘Full_system’ , ‘ESP_off’)


", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "CAN_signals_expected")).ValueList.Count>0)
            {
                return true;

            }

            return false;

        }

       

    }
}
