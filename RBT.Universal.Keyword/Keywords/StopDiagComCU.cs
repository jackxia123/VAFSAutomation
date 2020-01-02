using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class StopDiagComCU : PairedKeyword
    {


        public StopDiagComCU()
        {
            base.Name = "stop_diag_communication_CU";
            base.Description = @"Stop customer diagnosis communication corresponding to either ‘start_diag_communication_CU’ or start_diag_communication_CU_hideHeader’.

>>Use along with either
‘start_diag_communication_CU’ or start_diag_communication_CU_hideHeader’.

";
            Delay = 0;
            UsageLimit = 255;
            AddPairBefore("start_diag_communication_CU");
            AddPairBefore("start_diag_communication_CU_hideHeader");
            PairType = KeywordPairType.One2n;
        }

        
       

    }
}
