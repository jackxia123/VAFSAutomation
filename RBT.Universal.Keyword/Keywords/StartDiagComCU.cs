using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class StartDiagComCU : PairedKeyword
    {


        public StartDiagComCU()
        {
            base.Name = "start_diag_communication_CU";
            base.Description = @"Start customer diagnosis communication.

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
