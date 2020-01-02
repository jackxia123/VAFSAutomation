using RBT.Universal;
using RBT.Universal.Keywords;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CANTxGenerator
{
    public class TcLibCddGeneric
    {
        #region SwT&&SysT

        /// <summary>
        /// To create CDD de-activation with valve failure and to check CDD is inactive
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="acctgtax"></param>
        /// <param name="acctgtaxlowerlimit"></param>
        /// <param name="acctgtaxupperlimit"></param>
        /// <param name="acctgtaxlowercomftband"></param>
        /// <param name="accTgtAxUpperComftBand"></param>
        /// <param name="accMode"></param>
        /// <param name="cddActive"></param>
        /// <param name="cddAvailable"></param>
        /// <returns></returns>
        private static TestScript GetTScriptCddValveFailureAfterCddActiveSysT(bool apbmiExists, string acctgtax, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string cddActive, string cddAvailable)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.Gen_CDD_0006_CDD_Valve_Failure_After_CddActivation",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To create CDD de-activation with valve failure and to check CDD is inactive";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ "model_Accelerator", "1" },{ accMode, "0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(6000),
                new SetModelValues(new Dictionary<string, string>(){ { acctgtax,"-5"},{ "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(5000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new DoLineManipulation(new ObservableCollection<string>(){ "EVFL"}),
                new Wait(3000),

                // Add internal MM6 evaluation keywords

                new Wait(5000),

                new MM6Stop(),
                new TraceStop(),
                new ResetLineManipulation(),
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
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "model_Accelerator", "1" },{ accMode, "0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(6000),
                new SetModelValues(new Dictionary<string, string>(){ { acctgtax,"-5"},{ "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(5000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new DoLineManipulation(new ObservableCollection<string>(){ "EVFL"}),
                new Wait(3000),

                // Add internal MM6 evaluation keywords

                new Wait(5000),

                new MM6Stop(),
                new TraceStop(),
                new ResetLineManipulation(),
                new UndoSetModelValues(),
                new EcuOff(),

            });

            }
            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBVLV_MV1A_ValveCoilPathInterruptionFailure" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBVLV_MV1A_ValveCoilPathInterruptionFailure" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }


        /// <summary>
        /// To create CDD_S activation and to check CDD-S is active
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="acctgtax"></param>
        /// <param name="acctgtaxlowerlimit"></param>
        /// <param name="acctgtaxupperlimit"></param>
        /// <param name="acctgtaxlowercomftband"></param>
        /// <param name="accTgtAxUpperComftBand"></param>
        /// <param name="accMode"></param>
        /// <param name="cddActive"></param>
        /// <param name="cddAvailable"></param>
        /// <returns></returns>
        private static TestScript GetTScriptCddSActiveSysT(bool apbmiExists, string acctgtax, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string cddActive, string cddAvailable)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.Gen_CDD_0016_CDD_S_Activation",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To create CDD_S activation and to check CDD-S is active";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ "model_Accelerator", "1" },{ accMode, "0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(6000),
                new SetModelValues(new Dictionary<string, string>(){ { acctgtax,"-3.5"},{ "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new Wait(8000),
                 new SetModelValues(new Dictionary<string, string>(){ { acctgtax,"0"} ,{ accMode, "0" } }),
                new Wait(3000),

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
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "model_Accelerator", "1" },{ accMode, "0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(6000),
                new SetModelValues(new Dictionary<string, string>(){ { acctgtax,"-3.5"},{ "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new Wait(8000),
                 new SetModelValues(new Dictionary<string, string>(){ { acctgtax,"0"} ,{ accMode, "0" } }),
                new Wait(3000),

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


        public static ObservableCollection<TestScript> GetTScriptCDD(bool apbmiExists, string acctgtax, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string cddActive, string cddAvailable)
        {
            ObservableCollection<TestScript> _tsCddGenConf = new ObservableCollection<TestScript>()
            {
                GetTScriptCddValveFailureAfterCddActiveSysT( apbmiExists,acctgtax,acctgtaxlowerlimit,acctgtaxupperlimit,acctgtaxlowercomftband,accTgtAxUpperComftBand,accMode,cddActive, cddAvailable),

                GetTScriptCddSActiveSysT(apbmiExists,acctgtax,acctgtaxlowerlimit,acctgtaxupperlimit,acctgtaxlowercomftband,accTgtAxUpperComftBand,accMode,cddActive, cddAvailable)

            };
            return _tsCddGenConf;
        }
    }
}