using DBCHandling;
using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class FirstFrameCheckAllMsg : Model, ICanEvalParameter
    {
        private ObservableCollection<DbcMessage> _messages = new ObservableCollection<DbcMessage>();
        private DependentParameter _evalParameter;
        private string _expValue;


        #region ctor
        public FirstFrameCheckAllMsg()
        {
            InitParameter();
        }

        public FirstFrameCheckAllMsg(ObservableCollection<DbcMessage> messages,string expValue)
        {
            InitParameter();
            Messages = messages;
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
            EvalParameter = new HashDependentParameter("First_Frame_Check_All_Msg", @"This two parameters are used for detecting time when the ECU send Messages are re-ceived for the First time after IG ON and detecting time when the ECU send the first frame(it could be any message) after IG ON.

ECU ON trigger should be given by user; otherwise CAN batch will LC INFO signal as default trigger. 
Format:
First_Frame_Check = ‘xx-yy’
First_Frame_Check_All_Msg = %(‘Msg1’ => ‘xx-yy’, ‘Msg2’ => ‘>aa’, ‘Msg3’ => ‘<aa’)
ECU_ON_Trigger = %(‘Sig1’ => ‘xx’)


", argKvPairs, null);

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
                Dictionary<string, string> tempKeyValuePairs = new Dictionary<string, string>();

                foreach (DbcMessage msg in Messages)
                {
                    //todo: tolerance of cycle time check need to be parameterized 
                    int cycleTolerance = 1;
                    int lowLimit = msg.CycleTime - cycleTolerance;
                    int highLimit = msg.CycleTime + cycleTolerance;

                    tempKeyValuePairs.Add(msg.Name, ExpectedValue); 
                }
                return new HashDependentParameter("First_Frame_Check_All_Msg", @"This two parameters are used for detecting time when the ECU send Messages are re-ceived for the First time after IG ON and detecting time when the ECU send the first frame(it could be any message) after IG ON.

ECU ON trigger should be given by user; otherwise CAN batch will LC INFO signal as default trigger. 
Format:
First_Frame_Check = ‘xx-yy’
First_Frame_Check_All_Msg = %(‘Msg1’ => ‘xx-yy’, ‘Msg2’ => ‘>aa’, ‘Msg3’ => ‘<aa’)
ECU_ON_Trigger = %(‘Sig1’ => ‘xx’)


", tempKeyValuePairs, null); 



            }

            set
            {
                _evalParameter = value;
                RaisePropertyChanged("EvalParameter");
            }
        }

        public bool ArgumentsValidated()
        {
            if (Messages.Count>0 && ExpectedValue !=null)
            {

                return true;
            }


            return false;
        }
    }
}
