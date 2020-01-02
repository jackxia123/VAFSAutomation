using RBT.Universal.Keywords;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace RBT.Universal.CanEvalParameters
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public class MeasurementPoint : Model, ICanEvalParameter
    {
        private DependentParameter _parameter;

        private ListDependentParameter _signalList =new ListDependentParameter("signal_list", @"This will contain a list of signals whose values should be printed in the CAN Tx Report file. If this parameter is given then the default signal list will be ignored otherwise the signals present in the default signal list will be used. 
The names used must be same as in the CAN mapping file.
e.g.  signal_list                    = @('DR0','DR1')
",null,"");
        private ListDependentParameter _canOpState = new ListDependentParameter ("CAN_operation_state", @"CAN_operation_state = @(‘init’,’normal’)
CAN operation state gives strategy of signals which defined in CAN operation state file.
#This parameter is mandatory if any of measurement parameter is given. 
#If you don’t have CAN operation state file, just give a dummy state, and expected sig-nal values will be set to 0. 
", null,"");

        [DataMember]
        public virtual DependentParameter EvalParameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                SetProperty(ref _parameter, value);
            }
        }
        [DataMember]
        public ListDependentParameter SignalList
        {
            get
            {
                return _signalList;
            }
            set
            {
                SetProperty(ref _signalList, value);
            }
        }

        [DataMember]
        public ListDependentParameter CanOpState
        {
            get
            {
                return _canOpState;
            } set
            {
                SetProperty(ref _canOpState, value);
            }
        }

        public virtual bool ArgumentsValidated()
        {
            return false;
        }

        private static Type[] GetKnownTypes()
        {
            List<Type> type = new List<Type>();
            Type[] allTypes = Assembly.GetAssembly(typeof(MeasurementPoint)).GetExportedTypes();
            foreach (Type t in allTypes)
            {
                if (t.IsSubclassOf(typeof(MeasurementPoint)))
                    type.Add(t);

            }
            return type.ToArray();
        }

    }
}
