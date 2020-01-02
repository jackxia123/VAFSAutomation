using RBT.Universal;
using RBT.Universal.CanEvalParameters;
using RBT.Universal.CanEvalParameters.MeasurementPoints;
using RBT.Universal.Keywords;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CANTxGenerator
{
    static class TcLibNetworkPar
    {

        #region Postrun
        /// <summary>
        /// Get Test Script if postrun check in standstill
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptPostrunStandstillTimeCheck(int postrunTime, int delayPostrun, string doorsLink, string indSig, int indValue, string productType, int toleranceMs = 3000)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.Postrun_TimeCheck_StandStill",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check postrun time during standstill, as well as postrun delay time if requested";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\Postrun";

            if (productType.StartsWith("ABS"))
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),
                
                new Wait(3000),
                new EcuOff(),
                new Wait(postrunTime+1000),
                new Wait(2000),
                new MM6Stop(),
                new TraceStop(),

                 });

                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                };

            }
            else
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),
                
                new Wait(3000),
                new EcuOff(),
                new Wait(delayPostrun+1000),
                new ReadCanLamps(new ObservableCollection<string> { "ESP_off"}),
                new ReadCanSignals(new ObservableCollection<string> { "ESP_off"}),
                new Wait(postrunTime-delayPostrun+1000),
                new Wait(2000),
                new MM6Stop(),
                new TraceStop(),

            });

                Trigger lcUzOff = new Trigger(new SigConditon("LCI_UZ1", 0, TriggerConditionSignal.Equal)); //hardcode LC_Info signal
                Trigger lampOn = new Trigger(new SigConditon(indSig, indValue, TriggerConditionSignal.Equal));
                DeltaTime postrunDelay;
                if (delayPostrun == 0)
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun + "-" + (delayPostrun + toleranceMs));
                }
                else
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun - toleranceMs + "-" + (delayPostrun + toleranceMs));
                }


                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                    Triggers = new ObservableCollection<TriggerBase>() { lcUzOff, lampOn },
                    DeltaTimes = new ObservableCollection<DeltaTime>() { postrunDelay },

                };

            }


            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// Get Test Script if postrun check in vehicle running
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptPostrunningTimeCheck(int postrunTime, int delayPostrun, string doorsLink, string indSig, int indValue, string productType, int toleranceMs = 3000)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.Postrun_TimeCheck_Running",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check postrun time during vehicle running, 10kph by default, as well as postrun delay time if requested";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\Postrun";
            Dictionary<string, string> modelValuePstrSpd = new Dictionary<string, string>()
            {
                { "v_Wheel_LF","10"},
                { "v_Wheel_RF","10"},
                { "v_Wheel_LR","10"},
                {"v_Wheel_RR" ,"10"},
            };

            if (productType.StartsWith("ABS"))
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),
                new Wait(3000),              
                 new SetModelValues(modelValuePstrSpd),
                new Wait(5000),
                new EcuOff(),
                new Wait(postrunTime+1000),
                new Wait(2000),
                //new StimuliStop(),
                new UndoSetModelValues(),
                new MM6Stop(),
                new TraceStop(),

                 });

                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                };

            }
            else
            {

                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),
                new Wait(3000),
                new SetModelValues(modelValuePstrSpd),
                new Wait(5000),
                new EcuOff(),
                new Wait(delayPostrun+1000),

                new ReadCanLamps(new ObservableCollection<string> { "ESP_off"}),
                new ReadCanSignals(new ObservableCollection<string> { "ESP_off"}),
                new Wait(postrunTime-delayPostrun+1000),
                new Wait(2000),
                //new StimuliStop(),
                new UndoSetModelValues(),
                new MM6Stop(),
                new TraceStop(),

            });

                Trigger lcUzOff = new Trigger(new SigConditon("LCI_UZ1", 0, TriggerConditionSignal.Equal)); //hardcode LC_Info signal
                Trigger lampOn = new Trigger(new SigConditon(indSig, indValue, TriggerConditionSignal.Equal));
                DeltaTime postrunDelay;
                if (delayPostrun == 0)
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun + "-" + (delayPostrun + toleranceMs));
                }
                else
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun - toleranceMs + "-" + (delayPostrun + toleranceMs));
                }


                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                    Triggers = new ObservableCollection<TriggerBase>() { lcUzOff, lampOn },
                    DeltaTimes = new ObservableCollection<DeltaTime>() { postrunDelay },

                };

            }


            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// Get Test Script of postrun speed threshold check
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptPostrunSpdThresholdUpperCheck(string spdTh, int postrunTime, int delayPostrun, string doorsLink, string indSig, int indValue, string productType, int toleranceMs = 3000)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.Postrun_Speed_ThresholdUpperCheck",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check postrun time speed threshold, default value is 1 kph";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\Postrun";


            Dictionary<string, string> modelValuePstrSpd = new Dictionary<string, string>();



            modelValuePstrSpd.Add("v_Wheel_LF", ((double.Parse(spdTh) + 1) / 3.6).ToString("F1"));
            modelValuePstrSpd.Add("v_Wheel_RF", ((double.Parse(spdTh) + 1) / 3.6).ToString("F1"));
            modelValuePstrSpd.Add("v_Wheel_LR", ((double.Parse(spdTh) + 1) / 3.6).ToString("F1"));
            modelValuePstrSpd.Add("v_Wheel_RR", ((double.Parse(spdTh) + 1) / 3.6).ToString("F1"));


            if (productType.StartsWith("ABS"))
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),               
                new Wait(3000),
                new SetModelValues(modelValuePstrSpd),
                new Wait(5000),
                new EcuOff(),
                new Wait(postrunTime+1000),
                new Wait(2000),
                new UndoSetModelValues(),
                new MM6Stop(),
                new TraceStop(),

                 });

                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                };

            }
            else
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),
                new Wait(3000),
                new SetModelValues(modelValuePstrSpd),
                new Wait(5000),
                new EcuOff(),
                new Wait(delayPostrun+5000),
                new ReadCanLamps(new ObservableCollection<string> { "ESP_off"}),
                new ReadCanSignals(new ObservableCollection<string> { "ESP_off"}),
                new Wait(postrunTime-delayPostrun+1000),
                new Wait(2000),
                new UndoSetModelValues(),
                new MM6Stop(),
                new TraceStop(),

            });

                Trigger lcUzOff = new Trigger(new SigConditon("LCI_UZ1", 0, TriggerConditionSignal.Equal)); //hardcode LC_Info signal
                Trigger lampOn = new Trigger(new SigConditon(indSig, indValue, TriggerConditionSignal.Equal));
                DeltaTime postrunDelay;
                if (delayPostrun == 0)
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun + "-" + (delayPostrun + toleranceMs));
                }
                else
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun - toleranceMs + "-" + (delayPostrun + toleranceMs));
                }


                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                    Triggers = new ObservableCollection<TriggerBase>() { lcUzOff, lampOn },
                    DeltaTimes = new ObservableCollection<DeltaTime>() { postrunDelay },

                };

            }


            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// Get Test Script of postrun speed threshold check
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptPostrunSpdThresholdLowerCheck(string spdTh, int postrunTime, int delayPostrun, string doorsLink, string indSig, int indValue, string productType, int toleranceMs = 3000)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.Postrun_Speed_ThresholdLowerCheck",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check postrun time speed threshold, default value is 1 kph";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\Postrun";


            Dictionary<string, string> modelValuePstrSpd = new Dictionary<string, string>();



            modelValuePstrSpd.Add("v_Wheel_LF", ((double.Parse(spdTh) - 1) / 3.6).ToString("F1"));
            modelValuePstrSpd.Add("v_Wheel_RF", ((double.Parse(spdTh) - 1) / 3.6).ToString("F1"));
            modelValuePstrSpd.Add("v_Wheel_LR", ((double.Parse(spdTh) - 1) / 3.6).ToString("F1"));
            modelValuePstrSpd.Add("v_Wheel_RR", ((double.Parse(spdTh) - 1) / 3.6).ToString("F1"));


            if (productType == "ABS")
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),
                new Wait(3000),

                new SetModelValues(modelValuePstrSpd),
                new Wait(5000),
                new EcuOff(),
                new Wait(postrunTime+1000),
                new Wait(2000),
                new UndoSetModelValues(),
                new MM6Stop(),
                new TraceStop(),

                 });

                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                };

            }
            else
            {
                _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {
                new EcuOn(),
                new Wait(3000),
                new MM6Start("9","Normal"),
                new TraceStart(),

                new Wait(3000),
                new SetModelValues(modelValuePstrSpd),
                new Wait(5000),
                new EcuOff(),
                new Wait(delayPostrun+1000),
                // Bug:342 -> Xia Jack  05/26/2018
                new ReadCanLamps(new ObservableCollection<string> { "ESP_off"}),
                new ReadCanSignals(new ObservableCollection<string> { "ESP_off"}),
                new Wait(postrunTime-delayPostrun+1000),
                new Wait(2000),
                new UndoSetModelValues(),
                new MM6Stop(),
                new TraceStop(),

            });

                Trigger lcUzOff = new Trigger(new SigConditon("LCI_UZ1", 0, TriggerConditionSignal.Equal)); //hardcode LC_Info signal
                Trigger lampOn = new Trigger(new SigConditon(indSig, indValue, TriggerConditionSignal.Equal));
                DeltaTime postrunDelay;
                if (delayPostrun == 0)
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun + "-" + (delayPostrun + toleranceMs));
                }
                else
                {
                    postrunDelay = new DeltaTime(lcUzOff, lampOn, delayPostrun - toleranceMs + "-" + (delayPostrun + toleranceMs));
                }


                _script.CanTraceAnalyser = new CanTraceAnalyser()
                {
                    LastFrameCheck = new LastFrameCheck(postrunTime - toleranceMs + "-" + (postrunTime + toleranceMs)),
                    Triggers = new ObservableCollection<TriggerBase>() { lcUzOff, lampOn },
                    DeltaTimes = new ObservableCollection<DeltaTime>() { postrunDelay },

                };

            }


            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        #endregion Postrun


        #region Init
        /// <summary>
        /// Get Test Script if Ta timing check
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptTxTimingInit(int lowLimit, int highLimit, int lampInit, int lampInitTor, string doorsLink, ObservableCollection<NameValue> lampSigs)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.InitTiming_FirstFrameAndLampInit",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the first frame transmit time and lamp init time after IG On";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\InitTiming";

            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new Wait(2000),
            new MM6Start("9","Normal"),
            new EcuOn(),
            new Wait(lampInit+2000),
            new EcuOff(),
            new Wait(3000),
            new MM6Stop(),
            new TraceStop(),

             });

            ObservableCollection<TriggerBase> tempTriggerCollection = new ObservableCollection<TriggerBase>();
            ObservableCollection<DeltaTime> tempDeltaCollection = new ObservableCollection<DeltaTime>();
            Trigger lcUzOn = new Trigger(new SigConditon("LCI_UZ1", 1, TriggerConditionSignal.Equal)); //hardcode LC_Info signal
            tempTriggerCollection.Add(lcUzOn);

            foreach (var sigLamp in lampSigs)
            {
                var tempLamp = new Trigger(new SigConditon(sigLamp.Name, sigLamp.Value, TriggerConditionSignal.Equal));
                tempTriggerCollection.Add(tempLamp);
                tempDeltaCollection.Add(new DeltaTime(lcUzOn, tempLamp, lampInit - lampInitTor + "-" + (lampInit + lampInitTor)));
            }

            _script.CanTraceAnalyser = new CanTraceAnalyser()
            {
                FirstFrameCheck = new FirstFrameCheck(lowLimit + "-" + highLimit),
                Triggers = tempTriggerCollection,
                DeltaTimes = tempDeltaCollection,

            };


            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }
        /// <summary>
        /// Tx signal init value check
        /// </summary>
        /// <param name="lowLimit"></param>
        /// <param name="highLimit"></param>
        /// <param name="lampInit"></param>
        /// <param name="lampInitTor"></param>
        /// <param name="doorsLink"></param>
        /// <param name="lampSigs"></param>
        /// <returns></returns>
        public static TestScript GetTScriptTxInitValues(string doorsLink, ObservableCollection<NameValue> sigs)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.InitTiming_ReadInitialValues",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the initial values of all Tx signals";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\InitTiming";

            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new Wait(2000),
            new EcuOn(),
            new Wait(2000),
            new MM6Start("9","Normal"),
            new EcuOff(),
            new Wait(1000),
            new MM6Stop(),
            new TraceStop(),

             });

            ReadInitialValues readInit = new ReadInitialValues();
            readInit.SignalList.ValueList = new ObservableCollection<string>(sigs.Select(x => x.Name));  //Todo: The expected value of init signals need to provide in a smart ways
            readInit.CanOpState.ValueList = new ObservableCollection<string>() { "init" };

            _script.CanTraceAnalyser = new CanTraceAnalyser()
            {

                MeasurementPoint = readInit,

            };


            _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NoFault" };
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        #endregion Init


        #region voltage
        /// <summary>
        /// Get Test Script of net undervoltage voltage set situations
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptNetUnderVoltageSet(string underVoltSet, string doorsLink, string productType, string spdLimit, string toleranceV = "0.5", double toleranceSpd = 1)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.NetVolt_Threshold_NetUnderVoltage_Set",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the under voltage set threhold and speed limit";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\VoltageThreshold";

            Dictionary<string, string> modelValueUnderSet = new Dictionary<string, string>();
            modelValueUnderSet.Add("BatteryVoltage", (double.Parse(underVoltSet) - double.Parse(toleranceV)).ToString("F1"));



            Dictionary<string, string> modelValueUnderSetNo = new Dictionary<string, string>();
            modelValueUnderSetNo.Add("BatteryVoltage", (double.Parse(underVoltSet) + double.Parse(toleranceV)).ToString("F1"));
            if (double.Parse(spdLimit) > 0)
            {

                modelValueUnderSetNo.Add("v_Wheel_LF", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
                modelValueUnderSetNo.Add("v_Wheel_RF", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
                modelValueUnderSetNo.Add("v_Wheel_LR", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
                modelValueUnderSetNo.Add("v_Wheel_RR", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
            }


            Dictionary<string, string> modelValueNormal = new Dictionary<string, string>();
            modelValueNormal.Add("BatteryVoltage", "12");


            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new EcuOn(),
            new Wait(5000),
            new MM6Start("9","Normal"),
            new SetModelValues(modelValueUnderSetNo),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "ABS_off"}),
            new ReadCanSignals(new ObservableCollection<string> { "ABS_off"}),
            new SetModelValues(modelValueUnderSet),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "ABS_off"}),
            new ReadCanSignals(new ObservableCollection<string> { "ABS_off"}),
            new Wait(3000),
            new SetModelValues(modelValueNormal),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system"}),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system"}),
            new Wait(3000),
            new EcuOff(),
            new MM6Stop(),
            new TraceStop(),
            new UndoSetModelValues(),

            });

            if (productType == "Gen93" || productType == "ABS_Gen93")
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Undervoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Undervoltage" };
                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicUndervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicUndervoltage" };
            }
            else
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Undervoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Undervoltage" };
                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage" };
            }


            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// Get Test Script of net undervoltage voltage reset situations
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptNetUnderVoltageReset(string underVoltSet, string underVoltReset, string doorsLink, string productType, string spdLimit, string toleranceV = "0.5", double toleranceSpd = 1)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.NetVolt_Threshold_NetUnderVoltage_Reset",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the under voltage reset threhold and speed limit";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\VoltageThreshold";

            Dictionary<string, string> modelValueUnderSet = new Dictionary<string, string>();
            modelValueUnderSet.Add("BatteryVoltage", (double.Parse(underVoltSet) - double.Parse(toleranceV)).ToString("F1"));
            if (double.Parse(spdLimit) > 0)
            {
                modelValueUnderSet.Add("v_Wheel_LF", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
                modelValueUnderSet.Add("v_Wheel_RF", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
                modelValueUnderSet.Add("v_Wheel_LR", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));
                modelValueUnderSet.Add("v_Wheel_RR", ((double.Parse(spdLimit) + toleranceSpd) / 3.6).ToString("F1"));

            }



            Dictionary<string, string> modelValueUnderSetNo = new Dictionary<string, string>();
            modelValueUnderSetNo.Add("BatteryVoltage", (double.Parse(underVoltSet) + double.Parse(toleranceV)).ToString("F1"));

            Dictionary<string, string> modelValueUnderReset = new Dictionary<string, string>();
            modelValueUnderReset.Add("BatteryVoltage", (double.Parse(underVoltReset) + double.Parse(toleranceV)).ToString("F1"));

            Dictionary<string, string> modelValueUnderResetNo = new Dictionary<string, string>();
            modelValueUnderResetNo.Add("BatteryVoltage", (double.Parse(underVoltReset) - double.Parse(toleranceV)).ToString("F1"));

            Dictionary<string, string> modelValueNormal = new Dictionary<string, string>();
            modelValueNormal.Add("BatteryVoltage", "12");

            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new EcuOn(),
            new Wait(5000),
            new MM6Start("9","Normal"),
            new SetModelValues(modelValueUnderSet),
            new Wait(3000),
            new SetModelValues(modelValueUnderResetNo),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system"}),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system"}),
            new Wait(3000),
            new SetModelValues(modelValueUnderReset),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system"}),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system"}),
            new Wait(3000),
            new MM6Stop(),
            new EcuOff(),
            new TraceStop(),
            new UndoSetModelValues(),

            });

            if (productType == "Gen93" || productType == "ABS_Gen93")
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Undervoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Undervoltage" };
                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicSupplyUndervoltage", "RBApbMotorSupplyUndervoltage", "RBHydraulicSupplyHardUndervoltage", "RBApbButtonSupplyUndervolt", "RBHydraulicUndervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicSupplyUndervoltage", "RBApbMotorSupplyUndervoltage", "RBHydraulicSupplyHardUndervoltage", "RBApbButtonSupplyUndervolt", "RBHydraulicUndervoltage" };
            }
            else
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Undervoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Undervoltage" };
                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "ApbMotorSupplyUndervoltage", "HydraulicSupplyHardUndervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "ApbMotorSupplyUndervoltage", "HydraulicSupplyHardUndervoltage" };
            }

            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// Get Test Script of net overvoltage voltage set situations
        /// </summary>
        /// <returns></returns>
        /// 
        public static TestScript GetTScriptNetOverVoltageSet(string overVoltSet, string doorsLink, string productType, double spdLimit, string toleranceV = "0.5", double toleranceSpd = 1)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.NetVolt_Threshold_NetOverVoltage_Set",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the over voltage set threhold and speed limit";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\VoltageThreshold";

            Dictionary<string, string> modelValueOverSet = new Dictionary<string, string>();
            modelValueOverSet.Add("BatteryVoltage", (double.Parse(overVoltSet) + double.Parse(toleranceV)).ToString("F1"));

            Dictionary<string, string> modelValueOverSetNo = new Dictionary<string, string>();
            modelValueOverSetNo.Add("BatteryVoltage", (double.Parse(overVoltSet) - double.Parse(toleranceV)).ToString("F1"));
            if (spdLimit > 0)
            {
                modelValueOverSetNo.Add("v_Wheel_LF", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
                modelValueOverSetNo.Add("v_Wheel_RF", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
                modelValueOverSetNo.Add("v_Wheel_LR", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
                modelValueOverSetNo.Add("v_Wheel_RR", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));

            }

            Dictionary<string, string> modelValueNormal = new Dictionary<string, string>();
            modelValueNormal.Add("BatteryVoltage", "12");

            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new EcuOn(),
            new Wait(5000),
            new MM6Start("9","Normal"),
            new SetModelValues(modelValueOverSetNo),
            new Wait(3000),
            new SetModelValues(modelValueOverSet),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "ABS_off"}),
            new ReadCanSignals(new ObservableCollection<string> { "ABS_off"}),
            new Wait(3000),
            new SetModelValues(modelValueNormal),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system"}),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system"}),
            new Wait(3000),
            new MM6Stop(),
            new EcuOff(),
            new TraceStop(),
            new UndoSetModelValues(),

            });
            if (productType == "Gen93" || productType == "ABS_Gen93")
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Overvoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Overvoltage" };

                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBSupplyOvervoltage", "RBApbMotorSupplyOvervoltage", "RBOvervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBSupplyOvervoltage", "RBApbMotorSupplyOvervoltage", "RBOvervoltage" };
            }
            else
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Overvoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Overvoltage" };

                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "SupplyOvervoltage", "ApbMotorSupplyOvervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "SupplyOvervoltage", "ApbMotorSupplyOvervoltage" };
            }

            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// Get Test Script of net overvoltage voltage reset situations
        /// </summary>
        /// <returns></returns>
        public static TestScript GetTScriptNetOverVoltageReset(string overVoltSet, string overVoltReset, string doorsLink, string productType, double spdLimit, string toleranceV = "0.5", double toleranceSpd = 1)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.NetVolt_Threshold_NetOverVoltage_Reset",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the over voltage reset threhold and speed limit";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\VoltageThreshold";
            Dictionary<string, string> modelValueOverSet = new Dictionary<string, string>();
            modelValueOverSet.Add("BatteryVoltage", (double.Parse(overVoltSet) + double.Parse(toleranceV)).ToString("F1"));

            if (spdLimit > 0)
            {
                modelValueOverSet.Add("v_Wheel_LF", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
                modelValueOverSet.Add("v_Wheel_RF", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
                modelValueOverSet.Add("v_Wheel_LR", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
                modelValueOverSet.Add("v_Wheel_RR", ((spdLimit + toleranceSpd) / 3.6).ToString("F1"));
            }


            Dictionary<string, string> modelValueOverSetNo = new Dictionary<string, string>();
            modelValueOverSetNo.Add("BatteryVoltage", (double.Parse(overVoltSet) - double.Parse(toleranceV)).ToString("F1"));


            Dictionary<string, string> modelValueOverReset = new Dictionary<string, string>();
            modelValueOverReset.Add("BatteryVoltage", (double.Parse(overVoltReset) + double.Parse(toleranceV)).ToString("F1"));

            Dictionary<string, string> modelValueOverResetNo = new Dictionary<string, string>();
            modelValueOverResetNo.Add("BatteryVoltage", (double.Parse(overVoltReset) - double.Parse(toleranceV)).ToString("F1"));


            Dictionary<string, string> modelValueNormal = new Dictionary<string, string>();
            modelValueNormal.Add("BatteryVoltage", "12");

            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new EcuOn(),
            new Wait(5000),
            new MM6Start("9","Normal"),
            new SetModelValues(modelValueOverSet),
            new Wait(3000),
            new SetModelValues(modelValueOverResetNo),
            new Wait(4000),
             new ReadCanLamps(new ObservableCollection<string> { "Full_system"}),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system"}),
            new Wait(3000),
            new SetModelValues(modelValueOverReset),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system"}),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system"}),
            new Wait(3000),
            new MM6Stop(),
            new EcuOff(),
            new TraceStop(),
            new UndoSetModelValues(),

            });

            if (productType == "Gen93" || productType == "ABS_Gen93")
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Overvoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "RBNET_Overvoltage" };

                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBOvervoltage", "RBApbMotorSupplyOvervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBOvervoltage", "RBApbMotorSupplyOvervoltage" };
            }

            else
            {
                _script.RBMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Overvoltage" };
                _script.CUMandatoryFaults.ValueList = new ObservableCollection<string>() { "NET_Overvoltage" };

                _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "SupplyOvervoltage", "ApbMotorSupplyOvervoltage" };
                _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "SupplyOvervoltage", "ApbMotorSupplyOvervoltage" };
            }
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="underVoltSet"></param>
        /// <param name="doorsLink"></param>
        /// <param name="productType"></param>
        /// <param name="spdLimit"></param>
        /// <param name="toleranceV"></param>
        /// <param name="toleranceSpd"></param>
        /// <returns></returns>
        public static TestScript GetTScriptNetUnderVoltageSpdNotReached(string underVoltSet, string doorsLink, string productType, string spdLimit, string toleranceV = "0.5", double toleranceSpd = 1)
        {
            TestScript _script = new TestScript
            {
                Name = @"RB_UNIVERSAL_01J.NetVolt_NetUnderVoltage_SpeedNotReached",
                CAT = "Regression"
            };
            _script.Purpose.ScalarValue = @"To check the over voltage reset threhold and speed limit";
            _script.DoorsLink = doorsLink;
            _script.QcFolderPath = @"RBT_CANTx\VoltageThreshold";


            Dictionary<string, string> modelValueUnderSet = new Dictionary<string, string>();
            modelValueUnderSet.Add("BatteryVoltage", (double.Parse(underVoltSet) - double.Parse(toleranceV)).ToString("F1"));
            modelValueUnderSet.Add("v_Wheel_LF", ((double.Parse(spdLimit) - toleranceSpd) / 3.6).ToString("F1"));
            modelValueUnderSet.Add("v_Wheel_RF", ((double.Parse(spdLimit) - toleranceSpd) / 3.6).ToString("F1"));
            modelValueUnderSet.Add("v_Wheel_LR", ((double.Parse(spdLimit) - toleranceSpd) / 3.6).ToString("F1"));
            modelValueUnderSet.Add("v_Wheel_RR", ((double.Parse(spdLimit) - toleranceSpd) / 3.6).ToString("F1"));


            Dictionary<string, string> modelValueUnderSetNo = new Dictionary<string, string>();
            modelValueUnderSetNo.Add("BatteryVoltage", (double.Parse(underVoltSet) + double.Parse(toleranceV)).ToString("F1"));


            Dictionary<string, string> modelValueNormal = new Dictionary<string, string>();
            modelValueNormal.Add("BatteryVoltage", "12");


            _script.TestSequence = new TestSequence(new ObservableCollection<Keyword>() {

            new TraceStart(),
            new EcuOn(),
            new Wait(5000),
            new MM6Start("9","Normal"),
            new SetModelValues(modelValueUnderSet),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system" }),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system" }),

            new Wait(3000),
            new SetModelValues(modelValueNormal),
            new Wait(4000),
            new ReadCanLamps(new ObservableCollection<string> { "Full_system" }),
            new ReadCanSignals(new ObservableCollection<string> { "Full_system" }),
            new Wait(3000),
            new MM6Stop(),
            new EcuOff(),
            new TraceStop(),
            new UndoSetModelValues(),

            });

            switch (productType)
            {
                case "APBMi5064":
                    _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage", "ApbMotorSupplyUndervoltage" };

                    _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage", "ApbMotorSupplyUndervoltage" };
                    break;

                case "APBMi5065":
                    _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage", "ApbMotorSupplyUndervoltage" };

                    _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage", "ApbMotorSupplyUndervoltage" };
                    break;

                case "ABS_Gen93":
                    _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicUndervoltage", "RBHydraulicHardUndervoltage" };

                    _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicUndervoltage", "RBHydraulicHardUndervoltage" };
                    break;
                case "ABS":

                    _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage", "ApbMotorSupplyUndervoltage" };

                    _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage", "ApbMotorSupplyUndervoltage" };
                    break;
                case "Gen93":

                    _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicUndervoltage", "RBHydraulicHardUndervoltage" };

                    _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "RBHydraulicUndervoltage", "RBHydraulicHardUndervoltage" };
                    break;
                default:

                    _script.RBOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage", "HydraulicSupplyHardUndervoltage" };

                    _script.CUOptionalFaults.ValueList = new ObservableCollection<string>() { "HydraulicSupplyUndervoltage" };
                    break;
            }
            _script.InitDiagSequence.ValueList = new ObservableCollection<string>() { "Initial.seq" };
            _script.InitModelValues.KeyValuePairList = new Dictionary<string, string>() { { "BLS", "0" }, { "p_C1", "0" }, { "p_MBC", "0" }, };

            return _script;
        }
        #endregion
    }
}