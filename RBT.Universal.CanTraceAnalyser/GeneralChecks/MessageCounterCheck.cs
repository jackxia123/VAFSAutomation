using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class MessageCounterCheck : Model, ICanEvalParameter
    {
        private string _signal;
        private int _min;
        private int _max;
        private int _step;

        private object _startTime;
        private object _stopTime;



#pragma warning disable CS0169 // The field 'MessageCounterCheck._evalParameter' is never used
        private DependentParameter _evalParameter;
#pragma warning restore CS0169 // The field 'MessageCounterCheck._evalParameter' is never used


        #region ctor
        public MessageCounterCheck()
        {
            InitParameter();
        }
        public MessageCounterCheck(string sig, int min, int max )
        {
            InitParameter();

            Signal = sig;
            Min = min;
            Max = max;


        }
        public MessageCounterCheck(string sig,int min,int max,int startTime=0,int stopTime=0, int step = 1)
        {
            InitParameter();

            Signal = sig;
            Min = min;
            Max = max;
            Step = step;

            if (startTime != 0)
            {
                StartTime = startTime;
            }
            if(stopTime != 0)
            {
                StopTime = stopTime;
            }           
            
        }

        public MessageCounterCheck(string sig, int min, int max, TriggerBase startTime = null, TriggerBase stopTime =null, int step = 1)
        {
            InitParameter();
            Signal = sig;
            Min = min;
            Max = max;
            Step = step;

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
            EvalParameter = new HashDependentParameter("Check_Message_Counter", @"This parameter can be used to monitor live counter.

Check_Message_Counter = %(‘signal’ => ‘sig’, ‘Min’ => 0, ‘Max’ => 15, ‘StartTime’ => 2000, ‘StopTime’ => 5000, ‘Step’ => 1)

## TriggerX can be used in StartTime and StopTime.
## StartTime, StopTime and Step are optional; default value for Step is 1.
 

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

        public int Min
        {
            get { return _min; }
            set
            {
                SetProperty(ref _min, value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["Min"] = _min.ToString();
                RaisePropertyChanged("EvalParameter");

            }

        }

        public int Max
        {
            get { return _max; }
            set
            {
                SetProperty(ref _max, value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["Max"] = _max.ToString();
                RaisePropertyChanged("EvalParameter");

            }

        }

        public int Step
        {
            get { return _step; }
            set
            {
                SetProperty(ref _step, value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["Step"] = _step.ToString();
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
                    throw new Exception("Only trigger and int is supported as Start_Time in Check_Message_Counter");

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
                    throw new Exception("Only trigger and int is supported as Stop_Time in Check_Message_Counter");

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
            if (Signal !=null && Min>=0 && Max>0)
            {

                return true;
            }


            return false;
        }
    }
}
