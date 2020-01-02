using RBT.Universal.CanEvalParameters.ValueTransition;
using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RBT.Universal.CanEvalParameters
{
    [Serializable]
    public class SigPatternAnalyser: Model,ICanEvalParameter         
    {
        private object _startTime;
        private object _stopTime;
        private string _signalName;
        private int _tolerance;
        private ObservableCollection<SigPatternStep> _patternSteps = new ObservableCollection<SigPatternStep>();


        #region ctor
        public SigPatternAnalyser()
        {
            InitParameter();
        }

        public SigPatternAnalyser(string signame, ObservableCollection<SigPatternStep> patterStep)
        {
            InitParameter();
            SignalName = signame;
            PatternSteps = patterStep;

        }
        public SigPatternAnalyser(string signame, string patterString)
        {
            InitParameter();
            SignalName = signame;
            ((HashDependentParameter)EvalParameter).KeyValuePairList["pattern"] = patterString;

        }
        public SigPatternAnalyser(string signame, ObservableCollection<SigPatternStep> patterStep, int startTime = 0, int stopTime = 0,int tolerance =0 )
        {
            InitParameter();
            SignalName = signame;
            PatternSteps = patterStep;
            if (startTime != 0)
            {
                StartTime = startTime;
            }
            if (stopTime != 0)
            {
                StopTime = stopTime;
            }
            if (tolerance != 0)
            {
                Tolerance = tolerance;
            }
        }

        public SigPatternAnalyser(string signame, ObservableCollection<SigPatternStep> patterStep, object startTime=null, object stopTime=null, int tolerance = 0)
        {
            InitParameter();


            SignalName = signame;
            PatternSteps = patterStep;
            if (startTime != null)
            {
                StartTime = startTime;
            }
            if (stopTime != null)
            {
                StopTime = stopTime;
            }
            if (tolerance != 0)
            {
                Tolerance = tolerance;
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
                { "signal_name","" }, {"pattern","" }
            };
            EvalParameter = new HashDependentParameter("analyze_signal_pattern", @"This parameter is used to check signal patterns. Start condition, pattern value plus delta time for each value are supported (length of the pattern is open). As well as a range for the delta time. Function will also work if no delta time is needed.
Result will be added in the general STEPS report.

Format: 
analyze_signal_pattern = %('signal_name'=>'name' ,‘pattern’ => ‘value1->value2(deltatime2)->value3(deltatime3)->value4’, ’tolerance’=>’range of delta time’, 'Start_Time' => time,  'Duration' => time or 'Stop_Time'=>time)

range of delta time:  evaluation range shoud be deltatime +- range.
'Start_Time' => 'Trigger1' or 'Start_Time' => '500' used as same as used in 'read_delta_time_'
'Stop_Time='1000' or ' 'Duration' => '1000' (unit:ms)
‘deltatimex’,'Start_Time','Stop_Time' and 'Duration' are options.
# The first value and last value in pattern should not contain the delta time.
# the expected pattern can be only one value. ‘pattern’ => ‘value1’, this means signal value will not change in whole scope.


", argKvPairs, null);

        }

        public string SignalName
        {
            get { return _signalName; }
            set
            {
                SetProperty(ref _signalName,value);
                ((HashDependentParameter)EvalParameter).KeyValuePairList["signal_name"] = _signalName;
                RaisePropertyChanged("EvalParameter");
            }


       }

       public ObservableCollection<SigPatternStep> PatternSteps
       {
            get { return _patternSteps; }
            set
            {
                SetProperty(ref _patternSteps, value);
                string tempPattern="";

                foreach (var step in _patternSteps)
                {
                    if (_patternSteps.IndexOf(step) == 0)
                    {
                        tempPattern = tempPattern + step.Value+"->";

                    }
                    else if (_patternSteps.IndexOf(step) == _patternSteps.Count - 1)
                    {

                        tempPattern = tempPattern + step.Value;
                    }
                    else
                    {
                        if (step.Duration ==0)
                        {
                            tempPattern = tempPattern + step.Value  + "->";
                        }
                        else
                        {
                            tempPattern = tempPattern + step.Value + @"(" + step.Duration.ToString() + @")" + "->";
                        }
                    }

                }

                ((HashDependentParameter)EvalParameter).KeyValuePairList["pattern"] = tempPattern;
                RaisePropertyChanged("EvalParameter");
            }

        }

        /// <summary>
        /// Tolerance of Pattern step value 
        /// </summary>
        public int Tolerance
        {
            get { return _tolerance; }
            set
            {
                SetProperty(ref _tolerance, value);

                ((HashDependentParameter)EvalParameter).KeyValuePairList["tolerance"] = _tolerance.ToString();
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
                    throw new Exception("Only trigger and int is supported as Start_Time in Trigger");

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
                    throw new Exception("Only trigger and int is supported as Stop_Time in Trigger");

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
            if (SignalName != null && (((HashDependentParameter)EvalParameter).KeyValuePairList["pattern"] !=""))
            {

                return true;
            }
            return false;
        }
    }
}
