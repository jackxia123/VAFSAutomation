using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Stop
    /// </summary>
    /// 
    [Serializable]
    public class ResetCanManipulation : PairedKeyword
    {
        public ResetCanManipulation()
        {
            Name = "reset_CAN_manipulation";
            Description = @"Undo all CAN manipulations below.
‘editSignal_xxx’, ‘disableMessage_xxx’, and ‘damageDLC_xxx’.

>>Use along with
either ‘editSignal_xxx’, ‘disableMessage_xxx’, or ‘damageDLC_xxx’.


";
            Delay = 0;
            UsageLimit = 255;
            AddPairBefore("disableMessage_");
            AddPairBefore("damageDLC_");
            AddPairBefore("editSignal_");

            PairType = KeywordPairType.One2n;


        }

      

    }
}
