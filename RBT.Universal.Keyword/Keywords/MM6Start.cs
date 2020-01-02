using System;
using System.Collections.Generic;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Start
    /// </summary>
    /// 
    [Serializable]
    public class MM6Start : PairedDependentKeyword
    {
        
        public MM6Start()
        {
            InitKeyword();


        }

        public MM6Start(string generation,string measurementType)
        {
            InitKeyword();
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "Measurement_Info")).KeyValuePairList["Generation"] = generation;
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "Measurement_Info")).KeyValuePairList["MeasureType"] = measurementType;

        }

        private void InitKeyword()
        {
            Name = "MM6_start";            
            Description = @"Start MM6 measurement.

>>Need to specify the path of the a2l, cfg and dbc-file in Project Config file.
>>Need to put the a2l, cfg and dbc-file in …\STEPS_BBxxxxx_config\Tools\MM6
   a2l- file is mandatory, cfg-file and dbc-file are optional
>>This can be used along with ‘MM6_stop’ to stop measurement 
    or will stop after 120 sec automatically.
>>In test bench file …\STEPS_BBxxxxx_config\Testbenches_config_xxx.pm,
            'MM6x' => {   'Hostname' => 'YHxxxx' ,
            'Interface' => 'xx', # measurement interface: XMT or XCP or XCPonCAN
            'Type' => 'xx',      # XCPBox or CANCaseXL or VX1121

*Delay time depends on the performance of MM6 PC and on the size of cfg-file


";
            Delay = 2000;//X
            UsageLimit = 1;
            AddPairAfter("MM6_stop");
            PairType = KeywordPairType.One2One;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;

            string paraDesc = @"Specify the type of measurement.

>>‘Generation’ supports 8 and 9. 
>>‘MeasureType’ supports ‘Normal’, ‘XCPonCAN’, ‘CAN’, ‘powerON’, ‘powerON+CAN’ 

e.g. test_sequence = @(…,’ecu_on’,…,’MM6_start’,…,’MM6_stop’,…,’ecu_off’,…)
       Measurement_Info   = %(‘Generation’ => ‘9’ , ‘MeasureType’ => ‘powerON+CAN’ )
";
            string defaultValue = @"%(‘Generation’ => ‘9’ , ‘MeasureType’ => ‘Normal’)";

            Dictionary<string, string> paraItems = new Dictionary<string, string>();

            paraItems.Add("Generation", "");
            paraItems.Add("MeasureType", "");

           AddDependentParameter(new HashDependentParameter("Measurement_Info", paraDesc, paraItems, defaultValue));

        }

        public override bool ParametersValidated()
        {            
            if ((base.DependentParameters.First(x => x.Name == "Measurement_Info").Value =="9"|| base.DependentParameters.First(x => x.Name == "Measurement_Info").Value == "8")||
                (base.DependentParameters.First(x => x.Name == "MeasureType").Value == "Normal"|| base.DependentParameters.First(x => x.Name == "MeasureType").Value == "CAN" || 
                base.DependentParameters.First(x => x.Name == "MeasureType").Value == "XCPonCAN" || base.DependentParameters.First(x => x.Name == "MeasureType").Value == "powerON"||
                base.DependentParameters.First(x => x.Name == "MeasureType").Value == "powerON+CAN"))
            {
                return true;
            }

            return false;

        }

    }
}
