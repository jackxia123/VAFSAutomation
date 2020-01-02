using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters
{
    [DataContract]
    public class SignalCountupCalculate : Model, ICanEvalParameter
    {
        private string _signal;

        private string _expValue;

        private object _startTime;
        private object _stopTime;



#pragma warning disable CS0169 // The field 'SignalCountupCalculate._evalParameter' is never used
        private DependentParameter _evalParameter;
#pragma warning restore CS0169 // The field 'SignalCountupCalculate._evalParameter' is never used


        #region ctor
        public SignalCountupCalculate()
        {
            InitParameter();
        }
        public SignalCountupCalculate(string sig, string expvalue)
        {
            InitParameter();

            Signal = sig;
            ExpectedValue = expvalue;


        }
        public SignalCountupCalculate(string sig, string expvalue, int startTime=0,int stopTime=0)
        {
            InitParameter();

            Signal = sig;
            ExpectedValue = expvalue;

            if (startTime != 0)
            {
                StartTime = startTime;
            }
            if(stopTime != 0)
            {
                StopTime = stopTime;
            }           
            
        }

        public SignalCountupCalculate(string sig,  string expvalue, TriggerBase startTime = null, TriggerBase stopTime =null)
        {
            InitParameter();
            Signal = sig;

            ExpectedValue = expvalue;

            if (startTime != null) { 
                StartTime = startTime;
             }

            if (stopTime != null)
            {
                StopTime = stopTime;
            }

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
            EvalParameter = new HashDependentParameter("Calculate_Signal_Countup", @"This parameter can be used to calculate signal count up.

Calculate_Signal_Countup = %(‘signal’ => ‘sig’, ‘StartTime’ => 2000, ‘StopTime’ => 5000,’Expected’ =>’500-600’)
Calculate_Signal_Countup = %(‘signal’ => ‘sig’, ‘StartTime’ => 2000, ‘Duration’ => 3000, ,’Expected’ =>’500-600’)

## TriggerX can be used in StartTime and StopTime.

 

", argKvPairs, null);

        }

        public string Signal
        {
            get { return _signal; }
            set
            {
                SetProperty(ref _signal, value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["signal"] = _signal;
                RaisePropertyChanged("EvalParameter");

            }

        }


        public string ExpectedValue
        {
            get { return _expValue; }
            set
            {
                SetProperty(ref _expValue, value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["Expected"] = _expValue;
                RaisePropertyChanged("EvalParameter");

            }

        }

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
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = _startTime.ToString();

                }
                else if (_startTime is TriggerBase)
                {

                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = ((TriggerBase)_startTime).TriggerName;
                }
                else
                {
                    throw new Exception("Only trigger and int is supported as Start_Time in Calculate_Signal_Countup");

                }

                RaisePropertyChanged("EvalParameter");
            }


        }

        /// <summary>
        /// Stop time can be integer or trigger name
        /// </summary>
        public object StopTime
        {
            get { return _stopTime; }
            set
            {
                SetProperty(ref _stopTime, value);
                if (_stopTime is int)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Stop_Time"] = _stopTime.ToString();

                }
                else if (_stopTime is TriggerBase)
                {

                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Stop_Time"] = ((TriggerBase)_stopTime).TriggerName;
                }
                else
                {
                    throw new Exception("Only trigger and int is supported as Stop_Time in Calculate_Signal_Countup");

                }

                RaisePropertyChanged("EvalParameter");
            }


        }
        public DependentParameter EvalParameter
        {
            get; set;
        }

        public bool ArgumentsValidated()
        {
            if (Signal !=null && ExpectedValue !=null)
            {

                return true;
            }


            return false;
        }
    }
}
