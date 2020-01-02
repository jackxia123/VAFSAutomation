using System.Runtime.Serialization;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Start
    /// </summary>
    /// 
    [DataContract]
    public class TraceStart : PairedKeyword
    {
        
        public TraceStart()
        {
            Name = "trace_start";
            Description = @"Start CAN measurement by CANalyser/CANoe.

>>Use along with ‘trace_stop’.

e.g. test_sequence = @(…,’trace_start’,…,’trace_stop’,…)

*Delay time depends on the performance of CANalyzer PC (x = 200,500,1500,5000,…)

";
            Delay = 2000;
            UsageLimit = 1;
            AddPairAfter("trace_stop");
            PairType = KeywordPairType.One2One;


        }

      
    }
}
