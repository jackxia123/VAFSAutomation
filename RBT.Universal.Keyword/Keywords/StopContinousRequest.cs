using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class StopContinousRequest : PairedKeyword
    {


        public StopContinousRequest()
        {
            base.Name = "stop_continuous_request";
            base.Description = @"Stop sending a diagnosis request command to ECU continuously.

>>Use along with ‘start_continuous_request’.

";
            Delay = 0;
            UsageLimit = 255;
            AddPairBefore("start_continuous_request");
            PairType = KeywordPairType.One2One;


        }

        
       

    }
}
