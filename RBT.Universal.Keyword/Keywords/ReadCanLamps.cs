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
    public class ReadCanLamps : DependentKeyword
    {
        public ReadCanLamps()
        {
            InitKeyword();
        }
        public ReadCanLamps(ObservableCollection<string> lampStrategies)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "CAN_lamps_expected")).ValueList = lampStrategies;
        }

        private void InitKeyword()
        {
            Name = "read_CAN_lamps";            
            Description = @"Read CAN signals corresponding to CAN lamps.

>>Need to define the strategy in Project Default file.



";
            Delay = 0;
            UsageLimit = 255;

            AddDependentParameter(new ListDependentParameter("CAN_lamps_expected", @"Specify the strategy name defined in Project Default file.

>>Expected values defined in Project Default file can be only physical value. (decimal value with a factor/offset)

e.g. test_sequence = @(…,’read_CAN_Lamps’,…,’read_CAN_Lamps’,…)
       CAN_lamps_expected = @(‘EBV_off’ , ‘Full_system’)

", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "CAN_lamps_expected")).ValueList.Count>0)
            {
                return true;

            }

            return false;

        }

       

    }
}
