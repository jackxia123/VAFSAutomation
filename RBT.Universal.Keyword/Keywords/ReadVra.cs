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
    public class ReadVra : DependentKeyword
    {
        public ReadVra()
        {
            InitKeyword();
        }
        public ReadVra(ObservableCollection<string> vraStates)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "vra_expected")).ValueList = vraStates;
        }

        private void InitKeyword()
        {
            Name = "read_VRA";            
            Description = @"Read Valve Relay status.

>>Need to define the strategy in Project Default file.
>>Not available for a MMC (a card in LabCar) with older (<3.0) version.

";
            Delay = 0;
            UsageLimit = 255;

           AddDependentParameter(new ListDependentParameter("vra_expected", @"Specify the strategy name in Project Default file.

e.g. test_sequence = @(…,’read_VRA’,…,’read_VRA’,…)
        vra_expected = @(‘EBV_off’ , ‘Full_system’)
   (or vra_expected = @(‘ON’ , ‘OFF’) )



", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            ObservableCollection<string> varExp = ((ListDependentParameter)DependentParameters.First(x => x.Name == "vra_expected")).ValueList;

            if (varExp.Count>0 && varExp.All(x=>(x=="ON")||(x == "OFF")))
            {
                return true;

            }

            return false;

        }

       

    }
}
