using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class LastFrameCheck : Model, ICanEvalParameter
    {
       
        private DependentParameter _evalParameter;
        private string _expValue;


        #region ctor
        public LastFrameCheck()
        {
            InitParameter();
        }

        public LastFrameCheck(string expValue)
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
            EvalParameter = new ScalarDependentParameter("Last_Frame_Check", @"This parameter can be used to detect time when ECU send last frame (it could be any message) after IG ON.

Expected time should be given by user as a range.

Format:
Last_Message_Check = ‘10000-12000’
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

                return new ScalarDependentParameter("Last_Frame_Check", @"This parameter can be used to detect time when ECU send last frame (it could be any message) after IG ON.

Expected time should be given by user as a range.

Format:
Last_Message_Check = ‘10000-12000’
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
