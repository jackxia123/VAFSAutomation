using RBT.Universal.Keywords;

namespace RBT.Universal.CanEvalParameters
{
    public enum CheckConditionMessage { TimeOut, DLCFail , Init,}
    public enum TriggerConditionSignal { Equal, Greater, Less }

    public interface ICanEvalParameter
    {
        DependentParameter EvalParameter { get; set; }
        bool ArgumentsValidated();    
    }


    public interface TriggerBase
    {
         int TriggerID { get; set; }
         string TriggerName { get; }

    }

    public interface ICondition
    {
        

    }

    public interface IStartStopTime
    {


    }



}
