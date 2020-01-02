using System;
using System.Collections.Generic;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    [Serializable]
    public class CheckCanSignal : DependentKeyword
    {
        public CheckCanSignal()
        {
            InitKeyword();
        }
        public CheckCanSignal(Dictionary<string,string> SignalStrategies, Dictionary<string, string> moreSignalStrategies)
        {
            InitKeyword();
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "Check_CAN_signals1")).KeyValuePairList = SignalStrategies;
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "Check_CAN_signals2")).KeyValuePairList = moreSignalStrategies;
        }

        private void InitKeyword()
        {
            Name = "check_CAN_signal";            
            Description = @"Read CAN signals which names and expected values are specified in a par-file.
";
            Delay = 0;
            UsageLimit = 2;

            DependentParameters.Add(new HashDependentParameter("Check_CAN_signals1", @"Specify the signal names and expected values.

>>Maximum 10 CAN signals can be read.
>> Expected values can be only physical value (decimal value with a factor/offset).
>>This parameter name has been renamed (from CAN_signals_expected1/2) in V1.7

e.g. test_sequence = @(…,’check_CAN_signal’,…,’ check_CAN_signal’,…)
       Check_CAN_signals1 = %(‘sig_A’ =>  ‘0’ , ‘sig_B’ =>  ‘!=-1.5’ , ‘sig_C’ =>  ‘<2.25’ )
       Check_CAN_signals2 = %(‘sig_A’ =>  ‘2-5’ , ‘sig_D’ =>  ’10%5’ , ‘sig_E’ =>  ‘100?5’)

Options to specify expected value
'sig_A' => '!=x'	not equal to x
'sig_A' => '<x'	less than x
'sig_A' => '>x'	more than x
'sig_A' => 'x-y'	range ( from x to y )
'sig_A' => 'x%y' relative tolerance ( from x*(1-(y/100)) to x*(1+(y/100)) )
'sig_A' => 'x?y'	absolute tolerance ( from (x-y) to (x+y) )

", null, "0"));

            DependentParameters.Add(new HashDependentParameter("Check_CAN_signals2", @"Specify the signal names and expected values.

>>Maximum 10 CAN signals can be read.
>> Expected values can be only physical value (decimal value with a factor/offset).
>>This parameter name has been renamed (from CAN_signals_expected1/2) in V1.7

e.g. test_sequence = @(…,’check_CAN_signal’,…,’ check_CAN_signal’,…)
       Check_CAN_signals1 = %(‘sig_A’ =>  ‘0’ , ‘sig_B’ =>  ‘!=-1.5’ , ‘sig_C’ =>  ‘<2.25’ )
       Check_CAN_signals2 = %(‘sig_A’ =>  ‘2-5’ , ‘sig_D’ =>  ’10%5’ , ‘sig_E’ =>  ‘100?5’)

Options to specify expected value
'sig_A' => '!=x'	not equal to x
'sig_A' => '<x'	less than x
'sig_A' => '>x'	more than x
'sig_A' => 'x-y'	range ( from x to y )
'sig_A' => 'x%y' relative tolerance ( from x*(1-(y/100)) to x*(1+(y/100)) )
'sig_A' => 'x?y'	absolute tolerance ( from (x-y) to (x+y) )

", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Combinatorial;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((HashDependentParameter)DependentParameters.First(x => x.Name == "Check_CAN_signals1")).KeyValuePairList.Count==0 &&
                ((HashDependentParameter)DependentParameters.First(x => x.Name == "Check_CAN_signals2")).KeyValuePairList.Count == 0)
            {
                return false;

            }

            return true;

        }

       

    }
}
