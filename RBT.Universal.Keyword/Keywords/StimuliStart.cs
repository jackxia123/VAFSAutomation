using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Start
    /// </summary>
    /// 
    [Serializable]
    public class StimuliStart : PairedDependentKeyword
    {
        public StimuliStart()
        {
            InitKeyword();
        }

        public StimuliStart(ObservableCollection<string> lcsFiles )
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "stimuli_file_name")).ValueList = lcsFiles;

        }

        private void InitKeyword()
        {

            Name = "stimuli_start";
            Description = @"Start running a LabCar stimuli file (lcs-file).

>>Need to put lcs-files in …\STEPS_BBxxxxx_config\Tools\LabCar
>>All stimuli channels given in lcs-file will be reset once (always start from 0).
>>Use along with ‘stimuli_stop’.

*Delay time depends on the number of channels used in stimuli file.

Dependent parameter -> stimuli_file_name, Signal_Generator_Mode

Specify the stimuli file (lcs-file) name.

e.g. test_sequence = @(…,’stimuli_start’,…,’stimuli_stop’,…)
       stimuli_file_name = @(‘AMR_regulation_test.lcs’)
       Signal_Generator_Mode = @(‘M+S’,’M*S’,’S’)


";
            Delay = 2000;
            UsageLimit = 255;
            AddPairAfter("stimuli_stop");
            PairType = KeywordPairType.One2One;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;

            ObservableCollection<string> lcsfiles = new ObservableCollection<string>();

            AddDependentParameter(new ListDependentParameter("stimuli_file_name", @"stimuli_file_name = @(‘AMR_regulation_test.lcs’)", lcsfiles, ""));



        }

        public override bool ParametersValidated()
        {

            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "stimuli_file_name")).ValueList.All(x=>x.EndsWith(".lcs", StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }
            return false;

        }

    }
}
