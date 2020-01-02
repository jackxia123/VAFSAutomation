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
    public class RunDiagSeqRB : DependentKeyword
    {
        public RunDiagSeqRB()
        {
            InitKeyword();
        }
        public RunDiagSeqRB(ObservableCollection<string> diagSeqs)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "diag_sequences")).ValueList = diagSeqs;
        }

        private void InitKeyword()
        {
            Name = "run_Diag_seq_RB";            
            Description = @"Run a diag sequence file. (*.seq) in Bosch Mode.
>>Need to put seq-files in …\STEPS_BBxxxxx_config\Tools\Diag

* the delay while running seq file depend on the length if the seq file and the amount of wait time given in the seq file.

";
            Delay = 0;
            UsageLimit = 255;

            AddDependentParameter(new ListDependentParameter("vra_expected", @"Specify the seq-file name.

>>Be careful about the order of seq-file names in a par-file if using more than one seq-file since it’s common for both RB and CU mode. 

e.g. test_sequence = @(…,’run_Diag_seq_RB’,…,’run_Diag_seq_RB’,…)
       diag_sequences = @(‘AutoInit.seq’ , ‘EOL_test’)


", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            ObservableCollection<string> varExp = ((ListDependentParameter)DependentParameters.First(x => x.Name == "diag_sequences")).ValueList;

            if (varExp.Count>0 )
            {
                return true;

            }

            return false;

        }

       

    }
}
