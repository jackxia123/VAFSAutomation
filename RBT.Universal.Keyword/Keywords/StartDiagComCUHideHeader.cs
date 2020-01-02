using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class StartDiagComCUHideHeader : PairedKeyword
    {


        public StartDiagComCUHideHeader()
        {
            base.Name = "start_diag_communication_CU_hideHeader";
            base.Description = @"Start customer diagnosis communication. 

>>Header is required if send commands by ‘send_request’. 
>>The wakeup pattern & start comm. command is sent by Samtec.
>>Use along with ‘stop_diag_communication_CU’

*2s delay just after running in Bosch diagnosis communication.

";
            Delay = 0;
            UsageLimit = 255;
            AddPairAfter("stop_diag_communication_CU");
            PairType = KeywordPairType.n2One;

        }

        
       

    }
}
