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
    public class ReadFPC : DependentKeyword
    {
        public ReadFPC()
        {
            InitKeyword();
        }
        public ReadFPC(ObservableCollection<string> faults, FaultType faultType)
        {
            InitKeyword();
            if (faultType == FaultType.Mandatory)
            {
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_mandatory_faults")).ValueList = faults;
            }
            else if (faultType == FaultType.Optional)
            {
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_optional_faults")).ValueList = faults;
            } else if (faultType == FaultType.Disjuction)
            {
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_disjunction_faults")).ValueList = faults;
            }

        }

        public ReadFPC(ObservableCollection<string> mandatoryFaults, ObservableCollection<string> optionalFaults, ObservableCollection<string> disjuctionFaults)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_mandatory_faults")).ValueList = mandatoryFaults;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_optional_faults")).ValueList = optionalFaults;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_disjunction_faults")).ValueList = disjuctionFaults;
         

        }

        private void InitKeyword()
        {
            Name = "read_FPc";            
            Description = @"Read Customer failcode at any timing in a test sequence.

>>Delete and evaluation of failcode will be done after the sequence is finished.

";
            Delay = 0;
            UsageLimit = 3;

            AddDependentParameter(new ListDependentParameter("CU_mandatory_faults", @"Specify the expected fail word or code.

e.g. test_sequence = @(…,’read_FPC’,…,’read_FPC’,…,’read_FPC’,…)
       CU_mandatory_faults = @(‘1234XXXX’ , ‘5678XXXX’)
       CU_optional_faults = @(‘1111XXXX’ , ‘2222XXXX’)
       CU_disjunction_faults = @(‘ValveEvFlGeneric’ , ‘WssFLSupplyGnd’)
       CU_mandatory_faults_2 = @(‘1234XXXX’ , ‘5678XXXX’)
       CU_optional_faults_2 = @(‘1111XXXX’ , ‘2222XXXX’)
       CU_mandatory_faults_3 = @(‘1234XXXX’ , ‘5678XXXX’)


", null, "0"));
            AddDependentParameter(new ListDependentParameter("CU_optional_faults", @"Specify the expected fail word or code.

e.g. test_sequence = @(…,’read_FPC’,…,’read_FPC’,…,’read_FPC’,…)
       CU_mandatory_faults = @(‘1234XXXX’ , ‘5678XXXX’)
       CU_optional_faults = @(‘1111XXXX’ , ‘2222XXXX’)
       CU_disjunction_faults = @(‘ValveEvFlGeneric’ , ‘WssFLSupplyGnd’)
       CU_mandatory_faults_2 = @(‘1234XXXX’ , ‘5678XXXX’)
       CU_optional_faults_2 = @(‘1111XXXX’ , ‘2222XXXX’)
       CU_mandatory_faults_3 = @(‘1234XXXX’ , ‘5678XXXX’)


", null, "0"));
            AddDependentParameter(new ListDependentParameter("CU_disjunction_faults", @"Specify the expected fail word or code.

e.g. test_sequence = @(…,’read_FPC’,…,’read_FPC’,…,’read_FPC’,…)
       CU_mandatory_faults = @(‘1234XXXX’ , ‘5678XXXX’)
       CU_optional_faults = @(‘1111XXXX’ , ‘2222XXXX’)
       CU_disjunction_faults = @(‘ValveEvFlGeneric’ , ‘WssFLSupplyGnd’)
       CU_mandatory_faults_2 = @(‘1234XXXX’ , ‘5678XXXX’)
       CU_optional_faults_2 = @(‘1111XXXX’ , ‘2222XXXX’)
       CU_mandatory_faults_3 = @(‘1234XXXX’ , ‘5678XXXX’)


", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Combinatorial;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_mandatory_faults")).ValueList.Count == 0 &&
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_optional_faults")).ValueList.Count == 0 &&
                ((ListDependentParameter)DependentParameters.First(x => x.Name == "CU_disjunction_faults")).ValueList.Count ==0)
            {
                return false;

            }

            return true;

        }

       

    }
}
