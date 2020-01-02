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
    public class ReadEcuSignals : DependentKeyword
    {
        public ReadEcuSignals()
        {
            InitKeyword();
        }
        public ReadEcuSignals(ObservableCollection<string> SignalStrategies)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "ECU_signals_expected")).ValueList = SignalStrategies;
        }

        private void InitKeyword()
        {
            Name = "read_ECU_signals";            
            Description = @"Read and evaluate ECU signal values by the strategy defined in Project Default file. 
Evaluation is done by analyzing log (*.d97), so strictly not online evaluation and there could be an error of plus or minus 1 second.

>>Need to start MM6 measurement. Use along with ‘MM6_start’ and ‘MM6_stop’
>>Need to define strategy in Project Default file.
>>Need to specify the path of the a2l-file and cfg-file in Project Config file.
>>Need to put the a2l-file and cfg-file in …\STEPS_BBxxxxx_config\Tools\MM6




";
            Delay = 0;
            UsageLimit = 255;

            AddDependentParameter(new ListDependentParameter("ECU_signals_expected", @"Specify the strategy name defined in Project Default file.

>>The value has to be hexadecimal(raw value, without either a factor or a offset)

e.g. test_sequence = @(…,’MM6_start’,…,’read_ECU _signals’,…
                                       ’read_ECU_signals’,…,’MM6_stop’,…)
        ECU_signals_expected = @(‘Full_system’ , ‘ESP_off’)



", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "ECU_signals_expected")).ValueList.Count>0)
            {
                return true;

            }

            return false;

        }

       

    }
}
