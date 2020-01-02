using System;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class MsgCondition : ICondition
    {
        public MsgCondition(CheckConditionMessage cond, string msgName)
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
