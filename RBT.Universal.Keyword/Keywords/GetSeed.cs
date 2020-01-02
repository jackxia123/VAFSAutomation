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
    public class GetSeed : PairedDependentKeyword
    {
        private static int _index=1;
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public GetSeed()
        {
            InitKeyword();
        }

        public GetSeed( ObservableCollection<string> diagRequest, ObservableCollection<string> expResponse)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Diag_Request")).ValueList = diagRequest;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Exp_Response")).ValueList = expResponse;
        }
        public GetSeed(int index,ObservableCollection<string> diagRequest, ObservableCollection<string> expResponse)
        {
            InitKeyword();
            _index = index;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Diag_Request")).ValueList = diagRequest;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Exp_Response")).ValueList = expResponse;
        }

        private void InitKeyword()
        {
            Name = "get_seed_";            
            Description = @"Send a request to get a seed from ECU and calculate a key.
This is for field reprogramming. Available customers are Toyota, Honda, Kawasaki and Harley-Davidson.

>>Need to specify customer, expected responses and some more information if required in Project Default file.
>>Give ‘n’ as the number of ‘get_seed’ used in a sequence.
>>Diagnosis communication has to be started by giving either
    ‘start_diag_communication_RB/CU’
or ‘start_diag_communication_RB/CU_hideHeader’ ahead of this keyword.

*200ms delay only for the first time after ‘start_diag_communication_RB/CU’

";
            Delay = 0;
            UsageLimit = 255;
            PairType = KeywordPairType.One2One;
            AddDependentParameter(new ListDependentParameter("Diag_Request", @"Specify a command to get a seed from ECU.

>>If use “_hideHeader”, 4 bytes are required for request ID at the beginning.

e.g.1 test_sequence = @(…,’start_diag_communication_RB’,’get_seed_1’,…,’send_key_1’,
          …, ’get_seed_2’,…,’send_key_2’,…,’stop_diag_communication_RB’,…)
         Diag_Request = @( ‘7E 05 91 02’,’ 7E 07 91 03’, ‘7E 05 91 02’,’7E 07 91 03’)

e.g.2 test_sequence = @(…,’start_diag_communication_RB_hideHeader’,’get_seed_1’,…
                                         ’send_key_1’,…,’stop_diag_communication_RB’,…)
         Diag_Request = @(‘00 00 07 B0 00 00 07 B8 00 00 00 00 27 01 00’,
                                         ’00 00 07 B0 00 00 07 B8 00 00 00 00 27 02’)
         #request ID = 7B0h, response ID = 7B8h, functional ID = not used

", null, "0"));

            AddDependentParameter(new ListDependentParameter("Exp_Response", @"Specify expected responses only in following cases.
 - If offline analysis is required (Give ‘xx’ as a expected byte for seed value).
 - If expected responses are different from what are defined in Project Default file. 

>>If use “_hideHeader”, 4 bytes are required for response ID at the beginning.

e.g.1 test_sequence = @(…,’start_diag_communication_RB’,’get_seed_1’,’send_key_1’,
                                         …,’send_request’,’send_request’,’stop_diag_communication_RB’,…)
         Diag_Request = @( ‘7E 05 91 02’,’ 7E 07 91 03’,‘A2 05 73 00’,’A1 04 01’)
         
e.g.2 test_sequence = @(…,’start_diag_communication_RB_hideHeader’,’get_seed_1’,…
                                         ’send_key_1’,…,’stop_diag_communication_RB’,…)
         Diag_Request = @(‘00 00 07 B0 00 00 07 B8 00 00 00 00 27 01 00’,
                                         ’00 00 07 B0 00 00 07 B8 00 00 00 00 27 02’)
         Exp_Response = @(‘00 00 07 B8 67 01’,‘00 00 07 B8 67 02 xx xx xx xx’)
         #request ID = 7B0h, response ID = 7B8h, functional ID = not used

", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Parallel;
            ParametrizationType = DependentKeywordParameterizationType.CombineParameterized;
            

        }

        public override string ScriptName
        {
            get
            {
                return Name + _index++;
            }
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


        public int Index
        {
            get
            {
                return _index;
            }

            set
            {                
                SetProperty(ref _index,value);
            }

        }

        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter = new ObservableCollection<string>() { ScriptName.Replace(Name, "send_key_") };
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
