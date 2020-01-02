using RBT.Universal.Keywords;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters
{
    [DataContract]
    [KnownType(typeof(ListDependentParameter))]
    public class AndTrigger : TriggerBase, INotifyPropertyChanged, ICanEvalParameter
    {
        private Trigger _trgA;
        private Trigger _trgB;
        private int _trgID;
        private static int _trgIdCnt = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public AndTrigger()
        {
            InitParameter();
            TriggerID = _trgIdCnt++;

        }

        public AndTrigger(Trigger trgA, Trigger trgB)
        {
            InitParameter();
            TriggerA = trgA;
            TriggerB = trgB;
            TriggerID = _trgIdCnt++;

        }

        [DataMember(Order = 3)]
#pragma warning disable CS0109 // The member 'AndTrigger.TriggerID' does not hide an accessible member. The new keyword is not required.
        public new int TriggerID
#pragma warning restore CS0109 // The member 'AndTrigger.TriggerID' does not hide an accessible member. The new keyword is not required.
        {
            get { return _trgID; }
            set
            {
                _trgID = value;
                ((ListDependentParameter)EvalParameter).Name = "and_trigger_" + value;
                OnPropertyChanged(new PropertyChangedEventArgs("EvalParameter"));
            }
        }
        /// <summary>
        /// To reset trigger ID increment, should be called at each new test script
        /// </summary>
        public static void ResetID()
        {

            _trgIdCnt = 1;
        }

        private void InitParameter()
        {

            EvalParameter = new ListDependentParameter("and_trigger", @"This parameter is used to set trigger point if user want to evaluate the delta time between two conditions or need a trigger point for start point.The parameter read_delta_time_xx (xx= 1,2,3,…), and mark of the trigger point when be used is “TRiggerxx”(xx=1,2,3….).
Four formats are supported.
e.g.
read_delta_time_1 = %( 'Check_condition' => 'TimeOut', 'Message' => 'ECT1S07' )
read_delta_time_2 = %( 'Check_condition' => ' DLCFail', 'Message' => 'ECT1S07' )
read_delta_time_3 = %( 'Check_condition' => ' Init', 'Message' => 'ECT1S07' )
read_delta_time_4    = %( 'can_signal' => 'Ch3C', 'signal_value' => 32752, 'Check_condition' => '==', ‘Start_Time’ => 20000, ‘delay’ => 2000)
and_trigger_1 = @(‘Trigger1’,’Trigger2’)
and_trigger_2 = @(‘Trigger3’,’Trigger4’)
Delta1 = @(‘AndTrigger1’,’AndTrigger2’,’500-600’)
Delta2 = @(‘AndTrigger1’,’Trigger2’,’500-600’)
Delta3 = @(‘Trigger1’,’Trigger2’,’500-600’)


", new ObservableCollection<string>() { "", "" }, null);

        }
        [DataMember(Order = 1)]
        public DependentParameter EvalParameter
        {
            get;
            set;

        }
        [DataMember(Order = 2)]
        public Trigger TriggerA
        {
            get { return _trgA; }
            set
            {
                _trgA = value;
                ((ListDependentParameter)EvalParameter).ValueList[0] = _trgA.TriggerName;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.EvalParameter)));
            }
        }
        [DataMember(Order = 4)]
        public Trigger TriggerB
        {
            get { return _trgB; }
            set
            {
                _trgB = value;
                ((ListDependentParameter)EvalParameter).ValueList[1] = _trgB.TriggerName;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.EvalParameter)));
            }
        }
        [DataMember(Order = 5)]
#pragma warning disable CS0109 // The member 'AndTrigger.TriggerName' does not hide an accessible member. The new keyword is not required.
        public new string TriggerName
#pragma warning restore CS0109 // The member 'AndTrigger.TriggerName' does not hide an accessible member. The new keyword is not required.
        {
            get
            {
                return "AndTrigger" + TriggerID;
            }

            set { }
        }

        public bool ArgumentsValidated()
        {
            if (TriggerA != null && TriggerB != null)
            {
                return true;
            }

            return false;
        }
    }
}
