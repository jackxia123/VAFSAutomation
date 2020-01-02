using RBT.Universal;
using RBT.Universal.Keywords;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CANTxGenerator
{
    /// <summary>
    /// HHC Library
    /// </summary>
    static class TcLibBusGeneric
    {

        #region HHC SysT
        /// <summary>
        /// To activate HHC with all conditions satisfied and to check HHC is Activated
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript GetTScriptCanHIntrpt(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HHC_0001__HHC_Activation_Driving_Forward_UpHill",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To activate HHC with all conditions satisfied and to check HHC is Activated";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } ,{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(500),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(500),

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
        /// To activate HHC with all conditions satisfied and to check HHC is Activated
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript GetTScriptCanHSh2Uz(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {

            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HHC_0002__HHC_Activation_Driving_Reverse_UpHill",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To activate HHC with all conditions satisfied and to check HHC is Activated";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "-2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "-2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"},{ "model_gear", "gear_R" } }),
                new Wait(4000),
                // Add internal MM6 evaluation keywords

                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(500),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(3000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(500),

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
        /// To check HHC is de-activated once the Hold time is expired
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript GetTScriptCanHSh2Gnd(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HHC_0013__HHC_DeActivation_HoldTimeExpired_ManualTransmission",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is de-activated once the Hold time is expired";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "-2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "-2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
        /// To check HHC is de-activated once the Hold time is expired
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript GetTScriptCanLIntrpt(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HHC_0014__HHC_DeActivation_HoldTimeExpired_AutomaticTransmission",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is de-activated once the Hold time is expired";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "-2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "-2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
        /// To check HHC is De-Activated within hold time once drive off detected
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript GetTScriptCanLSh2Gnd(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {

            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SysT_Gen_HHC_0015__HHC_DeActivation_DriveOff_Detected",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is De-Activated within hold time once drive off detected";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" },{ "model_Accelerator", "1"} }),
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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "-2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "-2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(2000),
                // Add internal MM6 evaluation keywords

                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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

        #region HHC SwT
        /// <summary>
        /// To activate HHC with all conditions satisfied and to check HHC is Activated
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript CheckHHCActiveForwardUphillSwT(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_HHC_0001__HHC_Activation_Driving_Forward_UpHill",
                CAT = "Regression",
            };
            _script.Purpose.ScalarValue = @"To activate HHC with all conditions satisfied and to check HHC is Activated";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2" } , { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){{ "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
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
        /// To activate HHC with all conditions satisfied and to check HHC is Activated
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript CheckHHCActiveReverseUphillSwT(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {

            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_HHC_0002__HHC_Activation_Driving_Reverse_UpHill",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To activate HHC with all conditions satisfied and to check HHC is Activated";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "-2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "-2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"},{ "model_gear", "gear_R" } }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(500),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "-2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "-2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"},{ "model_gear", "gear_R" } }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(500),

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
        /// To check HHC is de-activated once the Hold time is expired
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript CheckHHCNotActiveHoldTimeExperiedMTSwT(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_HHC_0020__HHC_DeActivation_HoldTimeExpired_ManualTransmission",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is de-activated once the Hold time is expired";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(500),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(500),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
        /// To check HHC is de-activated once the Hold time is expired
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript CheckHHCNotActiveHoldTimeExperiedATSwT(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_HHC_0021__HHC_DeActivation_HoldTimeExpired_AutomaticTransmission",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is de-activated once the Hold time is expired";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(500),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(500),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" } }),
                new Wait(5000),

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
        /// To check HHC is De-Activated within hold time once drive off detected
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript CheckHHCNotActiveDriverOffSwT(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {

            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_HHC_0023__HHC_DeActivation_DriveOff_Detected",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is De-Activated within hold time once drive off detected";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" },{ "model_Accelerator", "1"} }),
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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){  { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" },{ "model_Accelerator", "1"} }),
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
        /// To check HHC is activated but the maximum pressure does not increase above 45bar
        /// </summary>
        /// <param name="productType">Which Product you test, e.g ESP,ESPHevx,Ibooster,IPB</param>
        /// <param name="apbmiExists" >Check whether your project has the APBMi</param>
        /// <param name="axExterior">Check whether ax is exterior or interior</param>
        /// <param name="axCanSignaName">bindind axExterior, if true, set your Ax Net signale name</param>
        /// <param name="hhcActiveSignalName">Check your project HHC active Net signal name</param>
        /// <param name="brakeLightSignalName"> Check your project brake light Net signal name </param>
        /// <returns></returns>
        private static TestScript CheckHHCActiveMaxPressureSwT(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {

            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.SwT_Gen_HHC_0031__HHC_Activation_Maxpressure",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check HHC is activated but the maximum pressure does not increase above 45bar";
            if (apbmiExists)
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new TraceStart(),
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){ { "APB_switchstate", "2"}, { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" },{ "model_Accelerator", "1"} }),
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
                axExterior==true? new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ axCanSignaName, "2"},{ "model_Accelerator", "0.2"} }):new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0"},{ "model_gear", "gear_D" },{ "ax", "2"},{ "model_Accelerator", "0.2"} }),
                new Wait(5000),
                new SetModelValues(new Dictionary<string, string>(){  { "model_Brake", "0.5" } ,{ "model_Accelerator", "0"} }),
                new Wait(3000),
                // Add internal MM6 evaluation keywords

                new Wait(1000),
                new SetModelValues(new Dictionary<string, string>(){ { "model_Brake", "0" },{ "model_Accelerator", "1"} }),
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

        /// <summary>
        /// Add all HHC Swt and SysT to to the List
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="apbmiExists"></param>
        /// <param name="axExterior"></param>
        /// <param name="axCanSignaName"></param>
        /// <param name="hhcActiveSignalName"></param>
        /// <param name="brakeLightSignalName"></param>
        /// <returns></returns>
        public static ObservableCollection<TestScript> GetTScriptUD(string productType, bool apbmiExists, bool axExterior, string axCanSignaName, string hhcActiveSignalName, string brakeLightSignalName)
        {
            ObservableCollection<TestScript> _tsBusGenUD = new ObservableCollection<TestScript>
            {
                GetTScriptCanHIntrpt(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                GetTScriptCanHSh2Gnd(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                GetTScriptCanHSh2Uz(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                GetTScriptCanLIntrpt(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                GetTScriptCanLSh2Gnd(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                CheckHHCActiveForwardUphillSwT(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                CheckHHCActiveReverseUphillSwT(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                CheckHHCNotActiveHoldTimeExperiedMTSwT(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                CheckHHCNotActiveHoldTimeExperiedATSwT(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                CheckHHCNotActiveDriverOffSwT(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),

                CheckHHCActiveMaxPressureSwT(productType,apbmiExists,axExterior,axCanSignaName,hhcActiveSignalName,brakeLightSignalName),
            };

            return _tsBusGenUD;

        }
    }
}