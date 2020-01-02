using System;
using System.Collections.Generic;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// Keywords eval_ECU_signals
    /// </summary>
    [Serializable]
    public class EvalECUSignals : PairedDependentKeyword
    {
        public EvalECUSignals()
        {
            InitKeyword();
        }
        public EvalECUSignals(Dictionary<string, string> sigValuePairs)
        {
            InitKeyword();
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "Eval_ECU_signals")).KeyValuePairList = sigValuePairs;
        }

        private void InitKeyword()
        {
            Name = "eval_ECU_signals";
            Description = @"Evaluate ECU signals with MM6.
";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new HashDependentParameter("eval_ECU_signals", @"Specify the variable names and values to be set.

>>The same variable names as used in LabCar have to be given.
    Values have to be physical.

e.g. test_sequence = @(…,’eval_ECU_signals’,…,’eval_ECU_signals’,…)
         Eval_ECU_signals1 = %(‘ABS_active_120ms’ => ‘1’, ‘HmiDevice_EbdFault’ => ‘0’)
         Eval_ECU_signals2 = %(‘ABS_active_120ms’ => ‘0’, ‘HmiDevice_EbdFault’ => ‘1’)

", null, "0"));

            // PairType = KeywordPairType.One2One;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {

            if (((HashDependentParameter)DependentParameters.First(x => x.Name == "eval_ECU_signals")).KeyValuePairList.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
