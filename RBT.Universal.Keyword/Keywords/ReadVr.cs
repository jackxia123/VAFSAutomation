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
    public class ReadVr : DependentKeyword
    {
        public ReadVr()
        {
            InitKeyword();
        }
        public ReadVr(ObservableCollection<string> vrStates)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "vr_expected")).ValueList = vrStates;
        }

        private void InitKeyword()
        {
            Name = "read_VR";            
            Description = @"Read Valve Relay status.

>>Need to define the strategy in Project Default file.
";
            Delay = 0;
            UsageLimit = 255;

            AddDependentParameter(new ListDependentParameter("vr_expected", @"Specify the strategy name defined in Project Default file.

e.g. test_sequence = @(…,’read_VR’,…,’read_VR’,…)
       vr_expected = @(‘EBV_off’ , ‘Full_system’)
  (or vr_expected = @(‘ON’ , ‘OFF’) )


", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {

            ObservableCollection<string> varExp = ((ListDependentParameter)DependentParameters.First(x => x.Name == "vr_expected")).ValueList;

            if (varExp.Count > 0 && varExp.All(x => (x == "ON") || (x == "OFF")))
            {
                return true;

            }

            return false;

        }

       

    }
}
