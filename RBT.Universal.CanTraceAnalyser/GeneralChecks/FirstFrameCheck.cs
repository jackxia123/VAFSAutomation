using DBCHandling;
using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class FirstFrameCheck : Model, ICanEvalParameter
    {
        private ObservableCollection<DbcMessage> _messages = new ObservableCollection<DbcMessage>();
        private DependentParameter _evalParameter;
        private string _expValue;


        #region ctor
        public FirstFrameCheck()
        {
            InitParameter();
        }

        public FirstFrameCheck(string expValue)
        {
            InitParameter();
            
            ExpectedValue = expValue;
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
            EvalParameter = new ScalarDependentParameter("First_Frame_Check", @"This two parameters are used for detecting time when the ECU send Messages are re-ceived for the First time after IG ON and detecting time when the ECU send the first frame(it could be any message) after IG ON.

ECU ON trigger should be given by user; otherwise CAN batch will LC INFO signal as default trigger. 
Format:
First_Frame_Check = ‘xx-yy’
First_Frame_Check_All_Msg = %(‘Msg1’ => ‘xx-yy’, ‘Msg2’ => ‘>aa’, ‘Msg3’ => ‘<aa’)
ECU_ON_Trigger = %(‘Sig1’ => ‘xx’)


", "", null);

        }

        public string ExpectedValue
        {
            get
            {
                return _expValue;
            }
            set
            {

                SetProperty(ref _expValue, value);

            }

        }

        public DependentParameter EvalParameter
        {
            get
            {

                return new ScalarDependentParameter("First_Frame_Check", @"This two parameters are used for detecting time when the ECU send Messages are re-ceived for the First time after IG ON and detecting time when the ECU send the first frame(it could be any message) after IG ON.

ECU ON trigger should be given by user; otherwise CAN batch will LC INFO signal as default trigger. 
Format:
First_Frame_Check = ‘xx-yy’
First_Frame_Check_All_Msg = %(‘Msg1’ => ‘xx-yy’, ‘Msg2’ => ‘>aa’, ‘Msg3’ => ‘<aa’)
ECU_ON_Trigger = %(‘Sig1’ => ‘xx’)


", ExpectedValue, null); 



            }

            set
            {
                _evalParameter = value;
                RaisePropertyChanged("EvalParameter");
            }
        }

        public bool ArgumentsValidated()
        {
            if (ExpectedValue != null)
            {

                return true;
            }


            return false;
        }
    }
}
