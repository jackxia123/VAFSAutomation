using RBT.Universal.Keywords;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters.MeasurementPoints
{
    [DataContract]
    public class ReadInitialValues : MeasurementPoint
    {
        public ReadInitialValues()
        {
            InitParameter();
        }


        private void InitParameter()
        {
            EvalParameter = new ScalarDependentParameter("read_initial_values", @"If the initial values of the signals should be recorded then the value of this parameter must be set to true.
e.g. read_initial_values            = 'True' 

", "True", "True");

        }

        public override bool ArgumentsValidated()
        {
            if (SignalList.ValueList.Count >= 0 && CanOpState.ValueList.Count >= 0)
            {
                return true;
            }
            return false;
        }


    }
}
