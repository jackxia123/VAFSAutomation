using RBT.Universal;
using RBT.Universal.Keywords;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CANTxGenerator
{
    public class TcLibHBAGeneric
    {
        #region SysT

        /// <summary>
        /// To verify that HBA is not active, when pMC failure in the system
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="abaInclude"></param>
        /// <param name="abaInvervention"></param>
        /// <param name="abarequest"></param>
        /// <param name="abalevel"></param>
        /// <returns></returns>
        private static TestScript GetTScriptHBADeactivepMCFailureSysT(bool apbmiExists, bool abaInclude, string abaInvervention, string abarequest, string abalevel)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HBA_0010__HBA_Deactivation_pMC_failure",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To verify that HBA is not active, when pMC failure in the system";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new DoLineManipulation(new ObservableCollection<string>() {"pHBZ_line"}),
                new Wait(3000),

                new SetModelValues(new Dictionary<string, string>() { { "APB_switchstate", "2" }, { "model_Brake", "0" }, { "model_gear", "gear_D" }, { "model_Accelerator", "1" } }),
                new Wait(8000),
                new SetModelValues(new Dictionary<string, string>() { { "model_Accelerator", "0" }, { "model_Brake", "0.5" } }),
                new Wait(6000),
                // Add internal MM6 evaluation keywords

                new Wait(5000),
                new MM6Stop(),
                new TraceStop(),
                new UndoSetModelValues(),
                new ResetLineManipulation(),
                new EcuOff(),

            });
            }
            else
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new DoLineManipulation(new ObservableCollection<string>() {"pHBZ_line"}),
                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>() {{ "model_Brake", "0" }, { "model_gear", "gear_D" }, { "model_Accelerator", "1" } }),
                new Wait(8000),
                new SetModelValues(new Dictionary<string, string>() { { "model_Accelerator", "0" }, { "model_Brake", "0.5" } }),
                new Wait(6000),
                // Add internal MM6 evaluation keywords

                new Wait(5000),
                new MM6Stop(),
                new TraceStop(),
                new UndoSetModelValues(),
                new ResetLineManipulation(),
                new EcuOff(),

            });

            }
            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBPressSent1LineHigh" };
            _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBPressSent1LineLow" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBPressSent1LineHigh" };
            _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBPressSent1LineLow" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// To verify VLC availability during Init, Normal and postrun
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="abaInclude"></param>
        /// <param name="abaInvervention"></param>
        /// <param name="abarequest"></param>
        /// <param name="abalevel"></param>
        /// <returns></returns>
        private static TestScript GetTScriptHBAactiveWithAbaRequestSysT(bool apbmiExists, bool abaInclude, string abaInvervention, string abarequest, string abalevel)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HBA_0014__ABA_activation_AbaRequest_2_with_HBA_activation",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To verify VLC availability during Init, Normal and postrun";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>() { { "APB_switchstate", "2" }, { "model_Brake", "0" }, { "model_gear", "gear_D" }, { "model_Accelerator", "1" } }),
                new Wait(8000),
                new SetModelValues(new Dictionary<string, string>() { { "model_Accelerator", "0" }, { abarequest, "1" },{abalevel,"1" } }),
                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>() { { abarequest, "0" }, { abalevel, "2" } }),
                new SetModelValues(new Dictionary<string, string>() { { abarequest, "1" }, { abalevel, "2" },{ "model_Brake", "0.5"} }),
                new Wait(1000),
                // Add internal MM6 evaluation keywords

                new Wait(5000),
                new MM6Stop(),
                new TraceStop(),
                new UndoSetModelValues(),
                new EcuOff(),

            });
            }
            else
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>() {  { "model_Brake", "0" }, { "model_gear", "gear_D" }, { "model_Accelerator", "1" } }),
                new Wait(8000),
                new SetModelValues(new Dictionary<string, string>() { { "model_Accelerator", "0" }, { abarequest, "1" },{abalevel,"1" } }),
                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>() { { abarequest, "0" }, { abalevel, "2" } }),
                new SetModelValues(new Dictionary<string, string>() { { abarequest, "1" }, { abalevel, "2" },{ "model_Brake", "0.5"} }),
                new Wait(1000),
                // Add internal MM6 evaluation keywords

                new Wait(5000),
                new MM6Stop(),
                new TraceStop(),
                new UndoSetModelValues(),
                new EcuOff(),

            });

            }
            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        #endregion

        public static ObservableCollection<TestScript> GetTScriptHBA(bool apbmiExists, bool abaInclude, string abaInvervention, string abarequest, string abalevel)
        {
            ObservableCollection<TestScript> _tsHBAGenConf = new ObservableCollection<TestScript>();
            if (abaInclude == true)
            {

                _tsHBAGenConf.Add(GetTScriptHBADeactivepMCFailureSysT(apbmiExists, abaInclude, abaInvervention, abarequest, abalevel));
                _tsHBAGenConf.Add(GetTScriptHBAactiveWithAbaRequestSysT(apbmiExists, abaInclude, abaInvervention, abarequest, abalevel));

            }
            else
            {
                _tsHBAGenConf.Add(GetTScriptHBADeactivepMCFailureSysT(apbmiExists, abaInclude, abaInvervention, abarequest, abalevel));
            }
            return _tsHBAGenConf;
        }


    }
}

