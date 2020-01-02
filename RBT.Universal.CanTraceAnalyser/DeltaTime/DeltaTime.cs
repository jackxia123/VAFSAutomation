using RBT.Universal.Keywords;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace RBT.Universal.CanEvalParameters
{
    [DataContract]
    [KnownType(typeof(Trigger))]
    [KnownType(typeof(AndTrigger))]
    [KnownType(typeof(ListDependentParameter))]
    public class DeltaTime : Model, ICanEvalParameter
    {
        private TriggerBase _trgA;
        private TriggerBase _trgB;

        private int _deltaID;
        private static int _deltaIdCnt = 1;


        private string _deltaTime;

        public DeltaTime()
        {
            InitParameter();
            DeltaID = _deltaIdCnt++;

        }

        public DeltaTime(TriggerBase trgA, TriggerBase trgB, string expDelta)
        {
            InitParameter();
            TriggerA = trgA;
            TriggerB = trgB;
            Delta = expDelta;
            DeltaID = _deltaIdCnt++;

        }

        [DataMember(Order = 3)]
        public int DeltaID
        {
            get { return _deltaID; }
            set
            {
                SetProperty(ref _deltaID, value);
                ((ListDependentParameter)EvalParameter).Name = "Delta" + value;
                RaisePropertyChanged("EvalParameter");
            }
        }
        /// <summary>
        /// To reset trigger ID increment, should be called at each new test script
        /// </summary>
        public static void ResetID()
        {

            _deltaIdCnt = 1;
        }

        private void InitParameter()
        {

            EvalParameter = new ListDependentParameter("Delta", @"This parameter is used to set trigger point if user want to evaluate the delta time between two conditions or need a trigger point for start point.The parameter read_delta_time_xx (xx= 1,2,3,…), and mark of the trigger point when be used is “TRiggerxx”(xx=1,2,3….).
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


", new ObservableCollection<string>() { "", "", "" }, null);

        }

        [DataMember(Order = 1)]
        public DependentParameter EvalParameter
        {
            get;
            set;

        }

        [DataMember(Order = 4)]
        public TriggerBase TriggerA
        {
            get { return _trgA; }
            set
            {
                SetProperty(ref _trgA, value);
                ((ListDependentParameter)EvalParameter).ValueList[0] = _trgA.TriggerName;
                RaisePropertyChanged("EvalParameter");
            }
        }
        [DataMember(Order = 5)]
        public TriggerBase TriggerB
        {
            get { return _trgB; }
            set
            {
                SetProperty(ref _trgB, value);
                ((ListDependentParameter)EvalParameter).ValueList[1] = _trgB.TriggerName;
                RaisePropertyChanged("EvalParameter");
            }
        }

        [DataMember(Order = 2)]
        public string Delta
        {
            get { return _deltaTime; }
            set
            {
                SetProperty(ref _deltaTime, value);
                ((ListDependentParameter)EvalParameter).ValueList[2] = _deltaTime;
                RaisePropertyChanged("EvalParameter");
            }
        }

        public bool ArgumentsValidated()
        {
            if (TriggerA != null && TriggerB != null && Regex.IsMatch(Delta, @"\d+\-\d+"))
            {
                return true;
            }

            return false;
        }
    }
}

