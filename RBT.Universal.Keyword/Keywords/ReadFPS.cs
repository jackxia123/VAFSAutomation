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
    public class ReadFPS : DependentKeyword
    {
        public ReadFPS()
        {
            InitKeyword();
        }
        public ReadFPS(ObservableCollection<string> faults, FaultType faultType)
        {
            InitKeyword();
            if (faultType == FaultType.Mandatory)
            {
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_mandatory_faults")).ValueList = faults;
            }
            else if (faultType == FaultType.Optional)
            {
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_optional_faults")).ValueList = faults;
            } else if (faultType == FaultType.Disjuction)
            {
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_disjunction_faults")).ValueList = faults;
            }

        }

        public ReadFPS(ObservableCollection<string> mandatoryFaults, ObservableCollection<string> optionalFaults, ObservableCollection<string> disjuctionFaults)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_mandatory_faults")).ValueList = mandatoryFaults;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_optional_faults")).ValueList = optionalFaults;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_disjunction_faults")).ValueList = disjuctionFaults;
         

        }

        private void InitKeyword()
        {
            Name = "read_FPS";            
            Description = @"Read Bosch failcode at any timing in a test sequence.

>>Delete and evaluation of failcode will be done after the sequence is finished.

";
            Delay = 0;
            UsageLimit = 3;

            AddDependentParameter(new ListDependentParameter("RB_mandatory_faults", @"Specify the expected fail word or code.

e.g. test_sequence = @(…,’read_FPS’,…,’read_FPS’,…,’read_FPS’,…)
       RB_mandatory_faults = @(‘1234XXXX’ , ‘5678XXXX’)
       RB_optional_faults = @(‘1111XXXX’ , ‘2222XXXX’)
       RB_mandatory_faults_2 = @(‘1234XXXX’ , ‘5678XXXX’)
       RB_optional_faults_2 = @(‘1111XXXX’ , ‘2222XXXX’)
       RB_mandatory_faults_3 = @(‘1234XXXX’ , ‘5678XXXX’)

", null, "0"));
            AddDependentParameter(new ListDependentParameter("RB_optional_faults", @"Specify the expected fail word or code.

e.g. test_sequence = @(…,’read_FPS’,…,’read_FPS’,…,’read_FPS’,…)
       RB_mandatory_faults = @(‘1234XXXX’ , ‘5678XXXX’)
       RB_optional_faults = @(‘1111XXXX’ , ‘2222XXXX’)
       RB_mandatory_faults_2 = @(‘1234XXXX’ , ‘5678XXXX’)
       RB_optional_faults_2 = @(‘1111XXXX’ , ‘2222XXXX’)
       RB_mandatory_faults_3 = @(‘1234XXXX’ , ‘5678XXXX’)

", null, "0"));
           AddDependentParameter(new ListDependentParameter("RB_disjunction_faults", @"Specify the expected fail word or code.

e.g. test_sequence = @(…,’read_FPS’,…,’read_FPS’,…,’read_FPS’,…)
       RB_mandatory_faults = @(‘1234XXXX’ , ‘5678XXXX’)
       RB_optional_faults = @(‘1111XXXX’ , ‘2222XXXX’)
       RB_mandatory_faults_2 = @(‘1234XXXX’ , ‘5678XXXX’)
       RB_optional_faults_2 = @(‘1111XXXX’ , ‘2222XXXX’)
       RB_mandatory_faults_3 = @(‘1234XXXX’ , ‘5678XXXX’)

", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Combinatorial;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_mandatory_faults")).ValueList.Count == 0 &&
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_optional_faults")).ValueList.Count == 0 &&
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "RB_disjunction_faults")).ValueList.Count ==0)
            {
                return false;

            }

            return true;

        }

       

    }
}
