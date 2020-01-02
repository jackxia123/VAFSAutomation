using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class StopDiagComRB : PairedKeyword
    {


        public StopDiagComRB()
        {
            base.Name = "stop_diag_communication_RB";
            base.Description = @"Stop Bosch diagnosis communication corresponding to either ‘start_diag_communication_RB’ or ‘start_diag_communication_RB_hideHeader’.

>>Use along with either
‘start_diag_communication_RB’ or ‘start_diag_communication_RB_hideHeader’.

";
            Delay = 0;
            UsageLimit = 255;
            AddPairBefore("start_diag_communication_RB");
            AddPairBefore("start_diag_communication_RB_hideHeader");
            PairType = KeywordPairType.n2One;
        }

        
       

    }
}
