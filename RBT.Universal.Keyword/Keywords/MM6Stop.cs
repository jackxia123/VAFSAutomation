using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Stop
    /// </summary>
    /// 
    [Serializable]
    public class MM6Stop : PairedKeyword
    {


        public MM6Stop()
        {
            Name = "MM6_stop";            
            Description = @"Stop MM6 measurement.

>>Not available for XMT measurement since it’s handled by ‘trace_time’ given at start MM6.
>>Use along with ‘MM6_start’.


";
            Delay = 0;
            UsageLimit = 1;
            AddPairBefore( "MM6_start");
            PairType = KeywordPairType.One2One;


        }

       

    }
}
