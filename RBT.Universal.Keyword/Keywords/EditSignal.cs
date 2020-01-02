using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord EditSignal
    /// </summary>
    /// 
    [Serializable]
    public class EditSignal : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public EditSignal()
        {
            InitKeyword();
        }
        public EditSignal(Dictionary<string,string> dictSigValues)
        {
            InitKeyword();
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "edit_CAN_signal")).KeyValuePairList = dictSigValues;
        }

        private void InitKeyword()
        {
            Name = "editSignal_";            
            Description = @"Edit a CAN signal in LabCar CAN module. 

>>‘xxx’ = CAN signal name defined in CAN mapping file.
>>Edit only signals in LabCar transmit message.
>>If a signal is mapped to LabCar variables (e.g. engine torque, wheel speed),    edit mode has to be implemented in LabCar module.
>>Use along with ‘undoEditSignal_xxx’.


";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2n;
            AddDependentParameter(new HashDependentParameter("edit_CAN_signal", @"Specify the signal name and the value to be set. 

>> The value can be either physical or hexadecimal.
If physical and negative, give ‘-’ at the beginning of a value. (e.g. - 10.5(kph), -1.25(m / s))
If hexadecimal, add ‘0x’ at the beginning of a value. (e.g. 0x10(= 16), 0xFF(= 255))

e.g.1 test_sequence = @(…,’editSignal_SigA ‘,…,’undoEditSignal_SigA’,…)
         edit_CAN_signal = % (‘SigA’ => ‘26’)
  (or  edit_CAN_signal = % (‘SigA’=> ‘0x1A’) ) 
  (or  edit_CAN_signal = % (‘SigA’=> ‘++1’) ) 

If edit more than once, value_list_(CAN signal name) has to be given like below.
e.g.2 test_sequence = @(…,’editSignal_SigA ‘,… ,’undoEditSignal_SigA’,…)
         edit_CAN_signal = % (‘SigA’ => ‘value_list_SigA’) 
         SigA = @(‘1’ , ‘0x2’ )
", null, ""));
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.CombineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((HashDependentParameter)DependentParameters.First(x => x.Name == "edit_CAN_signal")).KeyValuePairList.Count==1)
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter = new ObservableCollection<string>() { ScriptName.Replace(Name, "undoEditSignal_"), "reset_CAN_manipulation" };
                
                RaisePropertyChanged();
                return _pairAfter;
            }

            protected set
            {
                _pairAfter = value;
                SetProperty(ref _pairAfter, value);
            }
        }


    }
}
