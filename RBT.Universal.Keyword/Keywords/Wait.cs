using System;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class Wait : DependentKeyword
    {
        public Wait()
        {
            InitKeyword();
        }
        public Wait(int duration)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "duration")).ScalarValue = duration.ToString();
        }

        private void InitKeyword()
        {
            Name = "wait_";            
            Description = @"Wait a given time in millisecond. 

>>‘xxxx’ = wait time value. 

e.g. test_sequence = @(…,’wait_5000’,…,’wait_10000’,…,’wait_15000’,…)

";
            Delay = 0;
            UsageLimit = 255;
            DependentParameters.Add(new ScalarDependentParameter("duration", "Time to wait in sequence", "", "0"));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {
            int result;
            if (int.TryParse(DependentParameters[0].ScalarValue,out result))
            {
                return true;

            }

            return false;

        }

       

    }
}
