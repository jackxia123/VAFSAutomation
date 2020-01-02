using RBT.Universal.Keywords;
using System;


namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class OperationStatePatternStopTime : Model, ICanEvalParameter
    {
        private object _stopTime;

        #region ctor
        public OperationStatePatternStopTime()
        {
            InitParameter();
        }

        public OperationStatePatternStopTime(int stopTime)
        {
            InitParameter();
            StopTime = stopTime;
        }

        public OperationStatePatternStopTime(TriggerBase stopTime)
        {
            InitParameter();
            StopTime = stopTime;
        }


        #endregion
        /// <summary>
        /// Start time can be integer or trigger name
        /// </summary>
        public object StopTime
        {
            get { return _stopTime; }
            set
            {
                SetProperty(ref _stopTime, value);
                if (_stopTime is int)
                {
                    ((ScalarDependentParameter)EvalParameter).ScalarValue = _stopTime.ToString();

                }
                else if (_stopTime is TriggerBase)
                {

                    ((ScalarDependentParameter)EvalParameter).ScalarValue = ((TriggerBase)_stopTime).TriggerName;
                }
                else
                {
                    throw new Exception("Only trigger and int is supported as StopTime in Operation_State_Pattern_stop_time");

                }

                RaisePropertyChanged("EvalParameter");
            }


        }


        /// <summary>
        /// Init parameter set up with empty key
        /// </summary>
        private void InitParameter()
        {

            EvalParameter = new ScalarDependentParameter("Operation_state_pattern_stop_time", @"These three keywords are used to detect signal value change pattern between two trig-ger points, signals which need to be detected should be defined by user in keyword “signal_list”

Format:
CAN_operation_state_Pattern     = @('init1', 'init2', 'normal', 'speedX')
Operation_state_pattern_start_time = ‘Trigger1’
Operation_state_pattern_stop_time = ‘10000’
evaluate_as_range = @('Sig1','Sig2','Sig3','Sig4')

‘TriggerX’ or number can be used in “Operation_state_pattern_start_time” and “Opera-tion_state_pattern_stop_time”.


", "", null);

        }

        /// <summary>
        /// To implement ICanEvalParameter
        /// </summary>
        public DependentParameter EvalParameter
        {
            get; set;
        }

        public bool ArgumentsValidated()
        {
            if (StopTime is int && (int)StopTime >= 0)
            {
                return true;

            }
            else if (StopTime is TriggerBase && StopTime != null)
            {
                return true;
            }
            return false;
        }
    }
}
