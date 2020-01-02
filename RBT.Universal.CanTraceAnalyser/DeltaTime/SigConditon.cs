using System;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class SigConditon : ICondition
    {
        public SigConditon(string sigName, decimal sigValue, TriggerConditionSignal cond)
        {
            Condition = cond;
            SigName = sigName;
            SigValue = sigValue;
        }
        public TriggerConditionSignal Condition
        {
            get; set;
        }

        public string SigName { get; set; }

        public decimal SigValue { get; set; }

        public string strCondition
        {

            get { return getStrFromEnum(Condition); }

        }

        private string getStrFromEnum(TriggerConditionSignal trg)
        {
            string result;
            switch (trg)
            {
                case TriggerConditionSignal.Equal:
                    result = "==";
                    break;
                case TriggerConditionSignal.Greater:
                    result = ">";
                    break;
                case TriggerConditionSignal.Less:
                    result = "<";
                    break;
                default:
                    result = "==";
                    break;
            }
            return result;
        }
    }
}
