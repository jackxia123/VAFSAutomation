using DBCHandling;
using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class CycleTimeCheck : Model, ICanEvalParameter
    {
        private ObservableCollection<DbcMessage> _messages = new ObservableCollection<DbcMessage>();
        private DependentParameter _evalParameter;


        #region ctor
        public CycleTimeCheck()
        {
            InitParameter();
        }

        public CycleTimeCheck(string name, string range)
        {
            InitParameter();
            ((HashDependentParameter)EvalParameter).KeyValuePairList = new Dictionary<string, string>() { { name,range}, };
            
        }
        #endregion


        /// <summary>
        /// Init parameter set up with empty key
        /// </summary>
        private void InitParameter()
        {
            Dictionary<string, string> argKvPairs = new Dictionary<string, string>()
            {
                
            };
            EvalParameter = new HashDependentParameter("Cycle_Time_Check", @"This parameter is used for detecting variation of some message's cycle time all through a CAN trace file. 
And evaluate the actual cycle time detected form CAN trace  FAIL or PASS if cycle time is over a range(user defined) or not.


", argKvPairs, null);

        }
        public ObservableCollection<DbcMessage> Messages
        {
            get { return _messages; }
            set
            {
                SetProperty(ref _messages, value);
                
                RaisePropertyChanged("EvalParameter");

            }

        }
        public DependentParameter EvalParameter
        {
            get
            {  

                return _evalParameter;
            }

            set
            {
                _evalParameter = value;
                RaisePropertyChanged("EvalParameter");
            }
        }

        public bool ArgumentsValidated()
        {


            return true;
        }
    }
}
