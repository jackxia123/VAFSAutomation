using System;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class ECUMsgCondition : ICondition
    {
        public ECUMsgCondition(CheckConditionMessage cond, string msgName)
        {
            Condition = cond;
            MsgName = msgName;


        }
        public CheckConditionMessage Condition
        {
            get; set;
        }

        public string MsgName { get; set; }
    }
}
