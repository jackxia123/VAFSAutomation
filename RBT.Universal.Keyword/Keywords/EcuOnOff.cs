using System;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord EcuOnOff
    /// </summary>
    /// 
    [Serializable]
    public class EcuOnOff : DependentKeyword
    {
        public EcuOnOff()
        {
            InitKeyword();
        }
        public EcuOnOff(int offTimeInMs,int onTimeInMs,int repeat)
        {
            InitKeyword();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "Off_Time")).ScalarValue =  offTimeInMs.ToString();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "On_Time")).ScalarValue = onTimeInMs.ToString();
            ((ScalarDependentParameter)DependentParameters.First(x => x.Name == "Repeat")).ScalarValue = repeat.ToString();

        }

        private void InitKeyword()
        {
            Name = "ECU_";            
            Description = @"Turn ECU OFF and ON for n-times.

>>‘xxxx’ = wait time in msec after ECU OFF
>>‘yyyy’ = wait time in msec after ECU ON
>>‘zzz’ = number of times ECU OFF->ON will be done.

e.g. test_sequence = @(…,’ECU_OFF_500_ON_300_20’,…)

";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new ScalarDependentParameter("Off_Time", "Time in Ecu Off", "", "0"));
            DependentParameters.Add(new ScalarDependentParameter("On_Time", "Time in Ecu On", "", "0"));
            DependentParameters.Add(new ScalarDependentParameter("Repeat", "Repeat times", "", "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override string ScriptName
        {
            get
            {
                return Name+"OFF_"+ DependentParameters.First(x=>x.Name == "Off_Time").Value+
                    "_"+"ON_" + DependentParameters.First(x => x.Name == "On_Time").Value +
                    "_" + DependentParameters.First(x => x.Name == "Repeat").Value;
            }

        }
        public override bool ParametersValidated()
        {
            int result;
            if (int.TryParse(DependentParameters[0].Value,out result) && int.TryParse(DependentParameters[1].Value, out result) && int.TryParse(DependentParameters[2].Value, out result))
            {
                return true;

            }

            return false;

        }

       

    }
}
