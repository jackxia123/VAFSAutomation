using RBT.Universal.Keywords;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters.MeasurementPoints
{
    [DataContract]
    public class ReadCanSignalOverTime : MeasurementPoint
    {
        private int _timeInTrace = 0;

        public ReadCanSignalOverTime()
        {
            InitParameter();
        }
        public ReadCanSignalOverTime(int time)
        {
            InitParameter();
            TimeInTrace = time;

        }

        private void InitParameter()
        {
            EvalParameter = new ScalarDependentParameter("read_CAN_signal_over_time", @"Note: Time = 0 corresponds to the time when CAN Trace was started

If the CAN signal values are to be read at specified time then this parameter is used. Here the time is given at which the CAN signals should be read. All the values of the CAN signals available in the signal_list will be read from the CAN trace file.
e.g. read_CAN_signal_over_time = '5000'     # msec
# Time at which the CAN signals should be read from the CAN trace file
# read_CAN_signal_over_time = '5000:6000:7000' this format is not recommended because of CAN-Tx not support multiple measurement points, and verdict will be IN-CONC.  
", TimeInTrace.ToString(), "0");

        }

        public int TimeInTrace
        {
            get { return _timeInTrace; }
            set
            {
                SetProperty(ref _timeInTrace, value);

                EvalParameter.ScalarValue = value.ToString();
                RaisePropertyChanged("EvalParameter");
            }

        }

        public override bool ArgumentsValidated()
        {
            if (SignalList.ValueList.Count > 0 && CanOpState.ValueList.Count > 0)
            {
                return true;
            }
            return false;
        }


    }
}
