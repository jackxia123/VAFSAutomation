using RBT.Universal.Keywords;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters.MeasurementPoints
{
    [DataContract]
    public class ReadSignalsDlcFailure : MeasurementPoint
    {
        private string _msgName;
        private int _startTime = 0;

        public ReadSignalsDlcFailure()
        {

        }

        public ReadSignalsDlcFailure(string msgName, int startTime = 0)
        {
            MsgName = msgName;
            StartTime = startTime;
        }

        public override DependentParameter EvalParameter
        {
            get
            {
                if (StartTime == 0)
                {
                    return new ScalarDependentParameter("read_signals_DLC_failure", @"The values of the CAN signals are recorded when the message specified by this pa-rameter times out.
e.g. 1. read_signals_timeout       = 'ECT1S07'
2. read_signals_timeout   =  %('Message' => 'ENG1S01', 'Start_Time' => 17000)
#Message name must be same as the name defined in CAN mapping file.

", MsgName, "");

                }
                else
                {
                    Dictionary<string, string> kvPair = new Dictionary<string, string>() { { "Message", MsgName }, { "Start_Time", StartTime.ToString() } };
                    return new HashDependentParameter("read_signals_DLC_failure", @"The values of the CAN signals are recorded when the message specified by this pa-rameter times out.
e.g. 1. read_signals_timeout       = 'ECT1S07'
2. read_signals_timeout   =  %('Message' => 'ENG1S01', 'Start_Time' => 17000)
#Message name must be same as the name defined in CAN mapping file.

", kvPair, null);
                }


            }

        }

        public string MsgName
        {
            get { return _msgName; }
            set
            {
                SetProperty(ref _msgName, value);
                if (StartTime != 0)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Message"] = value;
                }
                RaisePropertyChanged("EvalParameter");
            }

        }

        public int StartTime
        {
            get { return _startTime; }
            set
            {
                SetProperty(ref _startTime, value);
                if (StartTime != 0)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = value.ToString();
                }
                RaisePropertyChanged("EvalParameter");
            }

        }
        public override bool ArgumentsValidated()
        {
            if (SignalList.ValueList.Count > 0 && CanOpState.ValueList.Count > 0 &&
                StartTime >= 0 && string.IsNullOrEmpty(MsgName))
            {
                return true;
            }
            return false;
        }


    }
}
