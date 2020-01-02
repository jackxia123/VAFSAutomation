using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class MessageChecksumCheck : Model, ICanEvalParameter
    {
        private string _signal;
        private string _algo;

        private object _startTime;
        private object _stopTime;



#pragma warning disable CS0169 // The field 'MessageChecksumCheck._evalParameter' is never used
        private DependentParameter _evalParameter;
#pragma warning restore CS0169 // The field 'MessageChecksumCheck._evalParameter' is never used


        #region ctor
        public MessageChecksumCheck()
        {
            InitParameter();
        }
        public MessageChecksumCheck(string sig, string algo )
        {
            InitParameter();
            Signal = sig;
            Algo = algo;
        }

        public MessageChecksumCheck(string sig, string algo,int startTime =0,int stopTime=0)
        {
            InitParameter();
            Signal = sig;
            Algo = algo;
            if (startTime != 0)
            {
                StartTime = startTime;
            }
            if (stopTime !=0)
            {
                StopTime = stopTime;

            }
            
        }

        public MessageChecksumCheck(string sig, string algo, TriggerBase startTime = null, TriggerBase stopTime =null)
        {
            InitParameter();

            Algo = algo;
            if (startTime !=null)
            {
                StartTime = startTime;
            }
            if (stopTime !=null)
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
            EvalParameter = new HashDependentParameter("Check_Message_Checksum", @"This parameter can be used to monitor checksum

Check_Message_Checksum = %(‘signal’ => ‘sig’, ‘StartTime’ => 2000, ‘StopTime’ => 5000, ‘algorithm’ => 'CRC_ECM1')

## TriggerX can be used in StartTime and StopTime.
## StartTime, StopTime are optional;
## Algo is the algrithm provided in project defaults
 

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

       

        public string Algo
        {
            get { return _algo; }
            set
            {
                SetProperty(ref _algo, value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["algorithm"] = _algo;
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
            if (Signal !=null && Algo!=null)
            {

                return true;
            }


            return false;
        }
    }
}
