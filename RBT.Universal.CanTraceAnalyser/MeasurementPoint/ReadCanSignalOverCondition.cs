using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters.MeasurementPoints
{
    [DataContract]
    public class ReadCanSignalOverCondition : MeasurementPoint
    {
#pragma warning disable CS0169 // The field 'ReadCanSignalOverCondition._canSignal' is never used
        private string _canSignal;
#pragma warning restore CS0169 // The field 'ReadCanSignalOverCondition._canSignal' is never used
        private object _condition;
#pragma warning disable CS0169 // The field 'ReadCanSignalOverCondition._sigValue' is never used
        private string _sigValue;
#pragma warning restore CS0169 // The field 'ReadCanSignalOverCondition._sigValue' is never used

        //optional
        private object _startTime;
        private int _delay;



        public ReadCanSignalOverCondition()
        {
            InitParameter();
        }
        public ReadCanSignalOverCondition(SigConditon cond)
        {
            InitParameter();
            Condition = cond;

            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("can_signal","");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("signal_value", "");
        }
        public ReadCanSignalOverCondition(SigConditon cond, int startTime, int delay)
        {
            InitParameter();
            Condition = cond;

            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("can_signal", "");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("signal_value", "");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("Start_Time", "0");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("delay", "0");

            StartTime = startTime;
            Delay = delay;

        }

        public ReadCanSignalOverCondition(SigConditon cond, TriggerBase startTime, int delay)
        {
            InitParameter();
            Condition = cond;

            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("can_signal", "");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("signal_value", "");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("Start_Time", "0");
            //((HashDependentParameter)EvalParameter).KeyValuePairList.Add("delay", "0");

            StartTime = startTime;
            Delay = delay;

        }
        public ReadCanSignalOverCondition(TriggerBase trg)
        {
            InitParameter();
            Condition = trg;

        }


        private void InitParameter()
        {
            Dictionary<string, string> argKvPairs = new Dictionary<string, string>()
            {
                 { "Check_condition", "" },
            };
            EvalParameter = new HashDependentParameter("read_CAN_signal_over_condition", @"If the CAN signals are to be read only at a particular point then this parameter is used. For this parameter the condition is given. For example, if you want to read the CAN sig-nals at the point where the ESP CAN lamp is set to failure value then, the CAN lamp name and the failure value should be given.(Note: only decimal value is supported in ‘signal_value’) 
e.g.1. read_CAN_signal_over_condition = %('can_signal' => 'VSA_WARN_STATUS_VSA', 'signal_value' => 1, 'Check_condition' => '==')
2. read_CAN_signal_over_condition = %('can_signal' => 'VSA_WARN_STATUS_VSA', 'signal_value' => 1, 'Check_condition' => '==' , ‘Start_Time’ => 20000, ‘delay’ => 2000)
3. read_CAN_signal_over_condition   =   %(check_condition => 'AndTriggerX')
# The name of the CAN signal should be the same as in dbc file.
# Start_Time and delay are optional
# TriggerX can not be used as Start_Time. TriggerX can be used from v1_16.

", argKvPairs, null);

        }

        public object Condition
        {
            get { return _condition; }
            set
            {
                SetProperty(ref _condition, value);
                if (_condition is SigConditon)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["can_signal"] = ((SigConditon)_condition).SigName;
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Check_condition"] = ((SigConditon)_condition).strCondition;
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["signal_value"] = ((SigConditon)_condition).SigValue.ToString();
                }
                else if (_condition is TriggerBase)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Check_condition"] = ((TriggerBase)_condition).TriggerName;

                }
                else
                {
                    throw new Exception("Only trigger and signal condition is supported as Check_condition in ReadSignalOverCondition");

                }

                RaisePropertyChanged("EvalParameter");
            }

        }

        //public string SigValue
        //{
        //    get { return _sigValue; }
        //    set
        //    {
        //        SetProperty(ref _sigValue, value);

        //        ((HashDependentParameter)EvalParameter).KeyValuePairList["signal_value"] = value;
        //        RaisePropertyChanged("EvalParameter");
        //    }

        //}
        //public string CanSignal
        //{
        //    get { return _canSignal; }
        //    set
        //    {
        //        SetProperty(ref _canSignal, value);

        //        ((HashDependentParameter)EvalParameter).KeyValuePairList["can_signal"] = value;
        //        RaisePropertyChanged("EvalParameter");
        //    }

        //}

        public object StartTime
        {
            get { return _startTime; }
            set
            {
                SetProperty(ref _startTime, value);
                if (_startTime is int)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = value.ToString();

                }
                else if (_startTime is TriggerBase)
                {

                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = ((TriggerBase)_startTime).TriggerName;
                }
                else
                {
                    throw new Exception("Only trigger and int is supported as Start_Time in ReadSignalOverCondition");

                }

                RaisePropertyChanged("EvalParameter");
            }

        }

        public int Delay
        {
            get { return _delay; }
            set
            {
                SetProperty(ref _delay, value);

                ((HashDependentParameter)EvalParameter).KeyValuePairList["delay"] = value.ToString();
                RaisePropertyChanged("EvalParameter");
            }

        }

        public override bool ArgumentsValidated()
        {
            if (SignalList.ValueList.Count > 0 && CanOpState.ValueList.Count > 0 &&
                ((HashDependentParameter)EvalParameter).KeyValuePairList["Check_condition"] != null
                )
            {
                return true;
            }
            return false;
        }

    }
}
