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
    public class SendRequest : DependentKeyword
    {
        public SendRequest()
        {
            InitKeyword();
        }
        public SendRequest(ObservableCollection<string> diagRequest, ObservableCollection<string> expResponse)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Diag_Request")).ValueList = diagRequest;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Exp_Response")).ValueList = expResponse;
        }

        private void InitKeyword()
        {
            Name = "send_request";            
            Description = @"Send a diagnosis request command to ECU.

>>Diagnosis communication has to be started by giving either,
    ‘start_diag_communication_RB/CU’
or ‘start_diag_communication_RB/CU_hideHeader’ ahead of this keyword.

*200ms delay only for the first time after ‘start_diag_communication_RB/CU’


";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new ListDependentParameter("Diag_Request", @"send_request	No*	Send a diagnosis request command to ECU.

>>Diagnosis communication has to be started by giving either,
    ‘start_diag_communication_RB/CU’
or ‘start_diag_communication_RB/CU_hideHeader’ ahead of this keyword.

*200ms delay only for the first time after ‘start_diag_communication_RB/CU’
	 Dependent parameter -> Diag_Request
	Specify the commands sent to ECU.

>>If use ‘start_diag_communication_RB/CU’ -> Header is NOT required.
>>If use ‘start_diag_communication_RB/CU_hideHeader’ -> 12bytes header is required.
    - 1st 4bytes for request ID
    - 2nd 4bytes for response ID
    - 3rd 4bytes for  functional ID
    - The rest for request commands
>>Checksum is not required in either case.

e.g.1 test_sequence = @(…,’start_diag_communication_RB’,’send_request’,…
                                          ,’send_request’, …,’stop_diag_communication_RB’,…)
          Diag_Request  = @(‘18 00 40 00’, ‘14 40 00’)
e.g.2 test_sequence = @(…,’start_diag_communication_RB_hideHeader’,’send_request’,…
                                          ,’send_request’,…,’stop_diag_communication_RB_hideHeader’,…)
          Diag_Request  = @(‘00 00 02 61 00 00 06 61 00 00 00 00 18 00 40 00’,
                                           ‘00 00 02 61 00 00 06 61 00 00 00 00 14 40 00’)
         #request ID = 261h, response ID = 661h, functional ID = not used

", null, "0"));

            DependentParameters.Add(new ListDependentParameter("Exp_Response", @"Specify expected responses for each sent command for off-line evaluation.
Evaluation is not done on-line except the only case of no response.

>>If no response is expected, it’s required to give ‘quiet’ as an expected value.
>>If use “_hideHeader”, 4 bytes are required for Response ID at the beginning.

e.g.1 test_sequence = @(…,’start_diag_communication_RB’,’send_request’,…,’send_request’
                                       ,…,’send_request’,…,’stop_diag_communication_RB’,…)
         Diag_Request = @(‘10 80’ , ‘12 34 56’ , ‘14 40 00’)
         Exp_Response = @(‘50 80’ , ‘quiet’ , ‘54 00’)
e.g.2 test_sequence = @(…,’start_diag_communication_RB_hideHeader’,’send_request’,…
                                         ,’stop_diag_communication_RB’,…)
         Diag_Request = @(‘00 00 02 61 00 00 06 61 00 00 00 00 18 00 FF 00’)
         Exp_Response = @(‘00 00 06 61 58 01 50 17 E0’)
         #request ID = 261h, response ID = 661h, functional ID = not used


", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Parallel;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "Diag_Request")).ValueList.Count>0 
                && ((ListDependentParameter)DependentParameters.First(x => x.Name == "Exp_Response")).ValueList.Count > 0)
            {
                return true;

            }

            return false;

        }

       

    }
}
