using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class ReadLamps : DependentKeyword
    {
        public ReadLamps()
        {
            InitKeyword();
        }
        public ReadLamps(ObservableCollection<string> lampStrategies)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "lamp_expected")).ValueList = lampStrategies;
        }

        private void InitKeyword()
        {
            Name = "read_lamps";            
            Description = @"Read SILAs status from LabCar. 

>>Need to specify the SILAs to be read in Project Default file.
>>Need to define the strategy in Project Default file.


";
            Delay = 0;
            UsageLimit = 255;

            AddDependentParameter(new ListDependentParameter("lamp_expected", @"Specify the strategy name defined in Project Default file.

>> Expected values defined in Project Default file can be ON, OFF or frequency value.

e.g.test_sequence = @(…,’read_Lamps’,…,’read_Lamps’,…)
       lamps_expected = @(‘EBV_off’ , ‘Full_system’)
",null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "lamp_expected")).ValueList.Count>0)
            {
                return true;

            }

            //throw new Exception("Liansheng Debugging");
            return false;

        }

       

    }
}
