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
    public class XflashAlgo : DependentKeyword
    {
        public XflashAlgo()
        {
            InitKeyword();
        }
        public XflashAlgo(ObservableCollection<string> xflashFiles)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "xflash_file_name")).ValueList = xflashFiles;
        }

        private void InitKeyword()
        {
            Name = "Xflash_Algo";            
            Description = @"Flash a hex file. 
Also does ECU OFF -> ECU ON after flash the hex file,
and reads the current version of the hex file available inside the ECU.

>>Need to put the hex file in …\STEPS_BBxxxxx_config\Tools\MM6



";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new ListDependentParameter("xflash_file_name", @"Specify the hex file name to be flashed.

e.g. test_sequence = @(…,’Xflash_Algo’,…)
       xflash_file_name = @(‘PRJ_Hexfile_BBxxxxx_Vxxxx_ECB_CSW.hex’)

", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "xflash_file_name")).ValueList.Count>0)
            {
                return true;

            }

            return false;

        }

       

    }
}
