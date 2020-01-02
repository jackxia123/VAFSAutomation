using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class ShortLines : PairedDependentKeyword
    {
        private ObservableCollection<string> _pairAfter = new ObservableCollection<string>();
        public ShortLines()
        {
            InitKeyword();
        }
        public ShortLines(Dictionary<string, string> lineInfo)
        {
            InitKeyword();
            ((HashDependentParameter)DependentParameters.First(x => x.Name == "line_information")).KeyValuePairList = lineInfo;

        }
        private void InitKeyword()
        {
            Name = "short_";            
            Description = @"Short lines.

>>Use along with ‘undo_short_line1_line2…’.



";
            Delay = 250;
            UsageLimit = 255;
            
            AddDependentParameter(new HashDependentParameter("line_information ", @"Specify the line names for line1, line2,…

>>Line names have to be the labels defined in LabCar mapping file or ‘Kxxx’.
>>‘xxx’ in ‘Kxxx’ is the number either on LabCar break out box or ES4441.

e.g. test_sequence = @(…,’short_line1_line2’,…,’undo_short _line1_line2’,…)
       line_information = %(‘line1’ => ‘WSSFL_line’ , ‘line2’ => ‘WSSFR_line’)
or    line_information = %(‘line1’ => ‘K67’ , ‘line2’ => ‘K72’)



", null, ""));
            PairType = KeywordPairType.One2One;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.CombineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((HashDependentParameter)DependentParameters.First(x => x.Name == "line_information")).KeyValuePairList.Count>0)
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairAfter
        {
            get
            {

                _pairAfter= new ObservableCollection<string>() { ScriptName.Replace(Name, "undo_short_") };
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
