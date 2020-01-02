using RBT.Universal.Keywords;
using System;


namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class OperationStatePatternStartTime : Model, ICanEvalParameter
    {
        private object _startTime;

        #region ctor
        public OperationStatePatternStartTime()
        {
            InitParameter();
        }

        public OperationStatePatternStartTime(int startTime)
        {
            InitParameter();
            StartTime = startTime;
        }

        public OperationStatePatternStartTime(TriggerBase startTime)
        {
            InitParameter();
            StartTime = startTime;
        }


        #endregion
        /// <summary>
        /// Start time can be integer or trigger name
        /// </summary>
        public object StartTime
        {
            get { return _startTime; }
            set
            {
                SetProperty(ref _startTime, value);
                if (_startTime is int)
                {
                    ((ScalarDependentParameter)EvalParameter).ScalarValue = _startTime.ToString();

                }
                else if (_startTime is TriggerBase)
                {

                    ((ScalarDependentParameter)EvalParameter).ScalarValue = ((TriggerBase)_startTime).TriggerName;
                }
                else
                {
                    throw new Exception("Only trigger and int is supported as Start_Time in Operation_State_Pattern_start_time");

                }

                RaisePropertyChanged("EvalParameter");
            }


        }


        /// <summary>
        /// Init parameter set up with empty key
        /// </summary>
        private void InitParameter()
        {

            EvalParameter = new ScalarDependentParameter("Operation_state_pattern_start_time", @"These three keywords are used to detect signal value change pattern between two trig-ger points, signals which need to be detected should be defined by user in keyword “signal_list”

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
            if (StartTime is int && (int)StartTime >= 0)
            {
                return true;

            }
            else if (StartTime is TriggerBase && StartTime !=null)
            {
                return true;
            }
            return false;
        }
    }
}
