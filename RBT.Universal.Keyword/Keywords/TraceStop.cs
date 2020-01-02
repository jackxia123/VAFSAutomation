using System;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Stop
    /// </summary>
    /// 
    [Serializable]
    public class TraceStop : PairedKeyword
    {
             

        public TraceStop()
        {
            Name = "trace_stop";
            Description = @"Stop running a LabCar stimuli file (lcs-file).

>>All stimuli channels given in lcs-file will be reset (all channels back to 0 or to the values which has been set by the parameter ‘initialize_model_values’).
>>Use along with ‘stimuli_start’.


";
            Delay = 2000;
            UsageLimit = 1;
            AddPairBefore ("trace_start");
            PairType = KeywordPairType.One2One;
        }
               

    }
}
