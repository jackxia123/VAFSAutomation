using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class UndoShortLines : PairedDependentKeyword
    {

        private ObservableCollection<string> _pairBefore = new ObservableCollection<string>();

        private UndoShortLines() // default ctor is forbidden to use because the parameter can not be determined without a parameter
        {
            InitKeyword();
        }
        public UndoShortLines(Dictionary<string, string> lineInfo)
        {
            InitKeyword();
            foreach(var pair in lineInfo)
            {
                AddDependentParameter(new ScalarDependentParameter(pair.Value, @"Specify the line names for line1, line2...",pair.Key , ""));
            }
           
        }

        private void InitKeyword()
        {
            Name = "undo_short_";            
            Description = @"Undo the line short which is done by  ‘short_line1_line2…’

>>Use along with ‘short_line1_line2…’

";
            Delay = 250;
            UsageLimit = 255;

            PairType = KeywordPairType.One2One;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.InlineParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (DependentParameters.Count>0)
            {
                return true;

            }

            return false;

        }

        public override ObservableCollection<string> PairBefore
        {
            get
            {

                _pairBefore= new ObservableCollection<string>(){ ScriptName.Replace(Name, "short_")};
                RaisePropertyChanged();
                return _pairBefore;
            }

            protected set
            {
                _pairBefore = value;
                SetProperty(ref _pairBefore, value);
            }
        }


    }
}
