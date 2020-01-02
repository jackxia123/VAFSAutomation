using RBT.Universal.Keywords;
using System;
using System.Collections.ObjectModel;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class OperationStatePattern : Model, ICanEvalParameter
    {

        ObservableCollection<string> _operationStates = new ObservableCollection<string>();

        #region ctor
        public OperationStatePattern()
        {
            InitParameter();
        }

        public OperationStatePattern(ObservableCollection<string> opStates)
        {
            InitParameter();
            OperationStates = opStates;
        }


        #endregion

        public ObservableCollection<string> OperationStates
        {
            get { return _operationStates; }
            set
            {
                SetProperty(ref _operationStates, value);

                ((ListDependentParameter)EvalParameter).ValueList = _operationStates;
                RaisePropertyChanged("EvalParameter");
            }

        }
        /// <summary>
        /// Init parameter set up with empty key
        /// </summary>
        private void InitParameter()
        {
            ObservableCollection<string> opStateList = new ObservableCollection<string>();
            EvalParameter = new ListDependentParameter("CAN_operation_state_Pattern", @"These three keywords are used to detect signal value change pattern between two trig-ger points, signals which need to be detected should be defined by user in keyword “signal_list”

Format:
CAN_operation_state_Pattern     = @('init1', 'init2', 'normal', 'speedX')
Operation_state_pattern_start_time = ‘Trigger1’
Operation_state_pattern_stop_time = ‘10000’
evaluate_as_range = @('Sig1','Sig2','Sig3','Sig4')

‘TriggerX’ or number can be used in “Operation_state_pattern_start_time” and “Opera-tion_state_pattern_stop_time”.


", opStateList, null);

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
            if (OperationStates.Count >0)
            {
                return true;

            }
            return false;
        }
    }
}
