using RBT.Universal;
using RBT.Universal.Keywords;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CANTxGenerator
{
    public class TcLibVlcGeneric
    {
        #region SysT
        /// <summary>
        /// To verify VLC activation (without Stop and Go (VLC-BB))
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="acctgtax"></param>
        /// <param name="accbrakepreferred"></param>
        /// <param name="acctgtaxlowerlimit"></param>
        /// <param name="acctgtaxupperlimit"></param>
        /// <param name="acctgtaxlowercomftband"></param>
        /// <param name="ACCTgtAxUpperComftBand"></param>
        /// <param name="accMode"></param>
        /// <param name="accVlcShutdownReq"></param>
        /// <param name="vlcActive"></param>
        /// <param name="vlcAvailable"></param>
        /// <returns></returns>
        private static TestScript GetTScriptVlcBBActiveSysT(bool apbmiExists, string acctgtax, string accbrakepreferred, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string accVlcShutdownReq, string vlcActive, string vlcAvailable)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_VLC_0001__VLC_BB_Activation",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To verify VLC activation (without Stop and Go (VLC-BB))";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ acctgtax, "-3"},{ "model_Accelerator", "1"} ,{ accbrakepreferred, "1"} ,{accMode ,"0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(8000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(6000),
                // Add internal MM6 evaluation keywords

                new Wait(2000),
                new SetModelValues(new Dictionary<string, string>(){ { accMode, "0" },{ accVlcShutdownReq,"2" } }),
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
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ acctgtax, "-3"},{ "model_Accelerator", "1"} ,{ accbrakepreferred, "1"} ,{accMode ,"0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(8000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(6000),
                // Add internal MM6 evaluation keywords

                new Wait(2000),
                new SetModelValues(new Dictionary<string, string>(){ { accMode, "0" },{ accVlcShutdownReq,"2" } }),
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

        /// <summary>
        /// To verify VLC availability during Init, Normal and postrun
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="acctgtax"></param>
        /// <param name="accbrakepreferred"></param>
        /// <param name="acctgtaxlowerlimit"></param>
        /// <param name="acctgtaxupperlimit"></param>
        /// <param name="acctgtaxlowercomftband"></param>
        /// <param name="accTgtAxUpperComftBand"></param>
        /// <param name="accMode"></param>
        /// <param name="accVlcShutdownReq"></param>
        /// <param name="vlcActive"></param>
        /// <param name="vlcAvailable"></param>
        /// <returns></returns>
        private static TestScript GetTScriptVlcAvailabilitySysT(bool apbmiExists, string acctgtax, string accbrakepreferred, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string accVlcShutdownReq, string vlcActive, string vlcAvailable)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_VLC_0004__VLC_Availability",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To verify VLC availability during Init, Normal and postrun";

            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>(){ { "UZ1_switch", "0" } }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords
                
                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "UZ1_switch", "1"}}),
                new Wait(5000),
                // Add internal MM6 evaluation keywords

                new Wait(5000),
                new MM6Stop(),
                new TraceStop(),
                new UndoSetModelValues(),
                new EcuOff(),

            });

            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }
        #endregion

        #region SwT
        /// <summary>
        /// To verify VLC activation control (Brake Control)
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="acctgtax"></param>
        /// <param name="accbrakepreferred"></param>
        /// <param name="acctgtaxlowerlimit"></param>
        /// <param name="acctgtaxupperlimit"></param>
        /// <param name="acctgtaxlowercomftband"></param>
        /// <param name="accTgtAxUpperComftBand"></param>
        /// <param name="accMode"></param>
        /// <param name="accVlcShutdownReq"></param>
        /// <param name="vlcActive"></param>
        /// <param name="vlcAvailable"></param>
        /// <returns></returns>
        private static TestScript GetTScriptVlcNegTarAccSwT(bool apbmiExists, string acctgtax, string accbrakepreferred, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string accVlcShutdownReq, string vlcActive, string vlcAvailable)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_VLC_0001__VLC_Neg_Targt_Acceleration",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To verify VLC activation control (Brake Control)";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ acctgtax, "0"},{ "model_Accelerator", "1"} ,{accMode ,"0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(12000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(20000),
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
                new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ acctgtax, "0"},{ "model_Accelerator", "1"} ,{accMode ,"0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(12000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Accelerator", "0"} ,{ accMode, "3" } }),
                new Wait(20000),
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

        /// <summary>
        /// To verify VLC activation control (Engine Control)
        /// </summary>
        /// <param name="apbmiExists"></param>
        /// <param name="acctgtax"></param>
        /// <param name="accbrakepreferred"></param>
        /// <param name="acctgtaxlowerlimit"></param>
        /// <param name="acctgtaxupperlimit"></param>
        /// <param name="acctgtaxlowercomftband"></param>
        /// <param name="accTgtAxUpperComftBand"></param>
        /// <param name="accMode"></param>
        /// <param name="accVlcShutdownReq"></param>
        /// <param name="vlcActive"></param>
        /// <param name="vlcAvailable"></param>
        /// <returns></returns>
        private static TestScript GetTScriptVlcPosTarAccSwT(bool apbmiExists, string acctgtax, string accbrakepreferred, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string accVlcShutdownReq, string vlcActive, string vlcAvailable)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_VLC_0002__VLC_Pos_Targt_Acceleration",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To verify VLC activation control (Engine Control)";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ acctgtax, "0"},{ "model_Accelerator", "1"} ,{accMode ,"0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(6000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Accelerator", "0"} ,{ accMode, "3" },{acctgtax,"2" } }),
                new Wait(20000),
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
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ acctgtax, "0"},{ "model_Accelerator", "1"} ,{accMode ,"0"},{ acctgtaxlowerlimit ,"-5"},{ acctgtaxupperlimit ,"10"} ,{ acctgtaxlowercomftband ,"1"},{ accTgtAxUpperComftBand,"1" } }),
                new Wait(6000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Accelerator", "0"} ,{ accMode, "3" },{acctgtax,"2" } }),
                new Wait(20000),
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

        public static ObservableCollection<TestScript> GetTScriptVLC(bool apbmiExists, string acctgtax, string accbrakepreferred, string acctgtaxlowerlimit, string acctgtaxupperlimit, string acctgtaxlowercomftband, string accTgtAxUpperComftBand, string accMode, string accVlcShutdownReq, string vlcActive, string vlcAvailable)
        {
            ObservableCollection<TestScript> _tsVlcGenConf = new ObservableCollection<TestScript>()
            {
                GetTScriptVlcBBActiveSysT(apbmiExists, acctgtax, accbrakepreferred, acctgtaxlowerlimit, acctgtaxupperlimit, acctgtaxlowercomftband, accTgtAxUpperComftBand, accMode, accVlcShutdownReq, vlcActive, vlcAvailable),

                GetTScriptVlcAvailabilitySysT(apbmiExists, acctgtax, accbrakepreferred, acctgtaxlowerlimit, acctgtaxupperlimit, acctgtaxlowercomftband, accTgtAxUpperComftBand, accMode, accVlcShutdownReq, vlcActive, vlcAvailable),

                GetTScriptVlcNegTarAccSwT(apbmiExists, acctgtax, accbrakepreferred, acctgtaxlowerlimit, acctgtaxupperlimit, acctgtaxlowercomftband, accTgtAxUpperComftBand, accMode, accVlcShutdownReq, vlcActive, vlcAvailable),

                GetTScriptVlcPosTarAccSwT(apbmiExists, acctgtax, accbrakepreferred, acctgtaxlowerlimit, acctgtaxupperlimit, acctgtaxlowercomftband, accTgtAxUpperComftBand, accMode, accVlcShutdownReq, vlcActive, vlcAvailable),

            };
            return _tsVlcGenConf;
        }

    }
}
