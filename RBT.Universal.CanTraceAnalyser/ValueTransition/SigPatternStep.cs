using System;

namespace RBT.Universal.CanEvalParameters.ValueTransition
{
    [Serializable]
    public class SigPatternStep
    {
        public SigPatternStep() { }
        public SigPatternStep(int value)
        {
            Value = value;
        }

        public SigPatternStep(int value,int duration)
        {
            Value = value;
            Duration = duration;
        }
        public int Value { get; set; }
        public int Duration { get; set; }        

    }
}
