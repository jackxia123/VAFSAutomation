using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters
{
    [DataContract]
    [KnownType(typeof(ECUSigConditon))]
    [KnownType(typeof(ECUMsgCondition))]
    [KnownType(typeof(HashDependentParameter))]
    [KnownType(typeof(ECUTrigger))]
    [KnownType(typeof(ECUAndTrigger))]
    [KnownType(typeof(int))]
    public class ECUTrigger : TriggerBase, INotifyPropertyChanged, ICanEvalParameter
    {

        private object _condition;

        //optional
        private object _startTime;
        private int _delay;

        private int _trgID;
        private static int _trgIdCnt = 1;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        //ctor
        public ECUTrigger()
        {
            InitParameter();
            TriggerID = _trgIdCnt++;
        }

        public ECUTrigger(ICondition cond)
        {
            InitParameter();

            Condition = cond;
            TriggerID = _trgIdCnt++;

        }
        public ECUTrigger(ICondition cond, int startTime = 0, int delay = 0)
        {
            InitParameter();

            Condition = cond;

            if (startTime != 0)
            {
                StartTime = startTime;
            }

            Delay = delay;
            TriggerID = _trgIdCnt++;

        }




        /// <summary>
        /// Trigger ID as and incrementing integer
        /// </summary>
        [DataMember(Order = 6)]
#pragma warning disable CS0109 // The member 'Trigger.TriggerID' does not hide an accessible member. The new keyword is not required.
        public new int TriggerID
#pragma warning restore CS0109 // The member 'Trigger.TriggerID' does not hide an accessible member. The new keyword is not required.
        {
            get { return _trgID; }
            set
            {
                _trgID = value;
                ((HashDependentParameter)EvalParameter).Name = "read_ecu_time_" + value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.EvalParameter)));
            }
        }

        public static void ResetID()
        {

            _trgIdCnt = 1;
        }

        /// <summary>
        /// Init parameter set up with a mandatory key "Check_condition"
        /// </summary>
        private void InitParameter()
        {
            Dictionary<string, string> argKvPairs = new Dictionary<string, string>()
            {
                { "Check_condition", "" },
            };
            EvalParameter = new HashDependentParameter("read_ecu_time", @"This parameter is used to set trigger point if user want to evaluate the delta time between two conditions or need a trigger point for start point.The parameter read_delta_time_xx (xx= 1,2,3,…), and mark of the trigger point when be used is “TRiggerxx”(xx=1,2,3….).
Four formats are supported.
e.g.
read_ecu_time_1 = %( 'Check_condition' => 'TimeOut', 'Message' => 'ECT1S07' )
read_ecu_time_2 = %( 'Check_condition' => ' DLCFail', 'Message' => 'ECT1S07' )
read_ecu_time_3 = %( 'Check_condition' => ' Init', 'Message' => 'ECT1S07' )
read_ecu_time_4    = %( 'can_signal' => 'Ch3C', 'signal_value' => 32752, 'Check_condition' => '==', ‘Start_Time’ => 20000, ‘delay’ => 2000)
ECU_signal_Deltatime_1 = @(’500-600’)
ECU_signal_Deltatime_2 = @(’500-600’)
ECU_signal_Deltatime_3 = @(’500-600’)



", argKvPairs, null);

        }

        /// <summary>
        /// To implement ICanEvalParamter
        /// </summary>
        [DataMember(Order = 1)]
        public DependentParameter EvalParameter
        {
            get;
            set;

        }

        /// <summary>
        /// Condition in tigger can be Signal Condition or message condition, which have different parameter syntax
        /// </summary>
        [DataMember(Order = 2)]
        public object Condition
        {
            get { return _condition; }
            set
            {
                _condition = value;
                if (_condition is SigConditon)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["can_signal"] = ((SigConditon)_condition).SigName;
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Check_condition"] = ((SigConditon)_condition).strCondition;
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["signal_value"] = ((SigConditon)_condition).SigValue.ToString();
                }
                else if (_condition is MsgCondition)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Message"] = ((MsgCondition)_condition).MsgName;
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Check_condition"] = ((MsgCondition)_condition).Condition.ToString();


                }
                else
                {
                    throw new Exception("Only message and signal condition is supported as Check_condition in Trigger");

                }


                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.EvalParameter)));
            }

        }

        /// <summary>
        /// Start time can be integer or trigger name
        /// </summary>
        [DataMember(Order = 3)]
        public object StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                if (_startTime is int)
                {
                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = value.ToString();

                }
                else if (_startTime is TriggerBase)
                {

                    ((HashDependentParameter)EvalParameter).KeyValuePairList["Start_Time"] = ((TriggerBase)_startTime).TriggerName;
                }


                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.EvalParameter)));
            }


        }
        /// <summary>
        /// Delay as integer in parameter
        /// </summary>
        [DataMember(Order = 4)]
        public int Delay
        {
            get { return _delay; }
            set
            {
                _delay = value;

                ((HashDependentParameter)EvalParameter).KeyValuePairList["delay"] = value.ToString();

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.EvalParameter)));
            }

        }
        /// <summary>
        /// Tigger Name to used in ReadSignalOverCondition and Trigger/ReadDeltaTime
        /// </summary>
        [DataMember(Order = 5)]
#pragma warning disable CS0109 // The member 'Trigger.TriggerName' does not hide an accessible member. The new keyword is not required.
        public new string TriggerName
#pragma warning restore CS0109 // The member 'Trigger.TriggerName' does not hide an accessible member. The new keyword is not required.
        {
            get
            {
                return "Trigger" + TriggerID;
            }

            private set { }
        }

        public bool ArgumentsValidated()
        {
            if (((HashDependentParameter)EvalParameter).KeyValuePairList["Check_condition"] != null)
            {
                return true;
            }
            return false;
        }

    }
}