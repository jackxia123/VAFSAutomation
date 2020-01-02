using System;
using System.Collections.Generic;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class SetModelValues : PairedDependentKeyword
    {
        public SetModelValues()
        {
            InitKeyword();
        }
        public SetModelValues(Dictionary<string, string> sigValuePairs)
        {
            InitKeyword();
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "set_model_value_to_constant")).KeyValuePairList = sigValuePairs;
        }

        private void InitKeyword()
        {
            Name = "set_model_values";
            Description = @"Edit LabCar model variables available in VDM2EID gui in open loop.

>>Use along with ‘undo_set_model_values’.

";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new HashDependentParameter("set_model_value_to_constant", @"Specify the variable names and values to be set.

>>The same variable names as used in LabCar have to be given.
    Values have to be physical.

e.g. test_sequence = @(…,’set_model_values’,…,’set_model_values’,…,’undo_set_model_values’,…)
         set_model_value_to_constant = %(‘ax’ => ‘3.0’, ‘BatteryVoltage’ => ‘15.0’)
         set_model_value_to_constant_2 = %(‘ax’ => ‘5.0’, ‘ay’ => ‘1.5’)

", null, "0"));

            AddPairAfter("undo_set_model_values");
            PairType = KeywordPairType.One2One;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {

            if (((HashDependentParameter)DependentParameters.First(x => x.Name == "set_model_value_to_constant")).KeyValuePairList.Count > 0)
            {
                return true;

            }

            return false;

        }

    }
}
