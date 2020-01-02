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
    public class StartContinousRequest : PairedDependentKeyword
    {
        public StartContinousRequest()
        {
            InitKeyword();
        }
        public StartContinousRequest(ObservableCollection<string> diagRequest)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Diag_Request_Continuous")).ValueList = diagRequest;
        }

        private void InitKeyword()
        {
            Name = "start_continuous_request";            
            Description = @"Send a diagnosis request command to ECU continuously (every 50ms).

>>Following settings are required in spa-file to get this keyword to work.
   - select “All responses” ( in “Extensions” -> “Receive mode”)
   - set  “False” for “Show background communication” (in “Extensions”)
>>Use along with ‘stop_continuous_request’.


";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new ListDependentParameter("Diag_Request_Continuous", @"Specify the commands to be sent to ECU continuously. 

e.g. test_sequence = @(…,’start_continuous_request’,…,’start_continuous_request’,…)
       Diag_Request_Continuous = @(‘18 00 FF 00’ , ‘14 FF 00’)

", null, "0"));

            AddPairAfter("stop_continuous_request");
            PairType = KeywordPairType.One2One;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "Diag_Request_Continuous")).ValueList.Count>0)
            {
                return true;

            }

            return false;

        }

       

    }
}
