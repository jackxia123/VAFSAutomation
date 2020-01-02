using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RBT.Universal.Keywords
{
    [Serializable]
    public class PairedDependentKeyword:Keyword,IPairedKeyword,IDependentKeyword
    {
        KeywordPairType _pairType;
        bool _paired = false;

        DependentKeywordParameterizationType _parameterizationType;
        DependentKeywordParameterCompositionOption _parameterCompositionOption;
        ObservableCollection<DependentParameter> _dependentParameters = new ObservableCollection<DependentParameter>();
        private ObservableCollection<string> _pairBefore = new ObservableCollection<string> ();
        private ObservableCollection<string> _pairAfter=new ObservableCollection<string> ();

        protected PairedDependentKeyword() { }
        public virtual ObservableCollection<string> PairBefore
        {
            get
            {
                return _pairBefore;
            }
            protected set
            {
                SetProperty(ref _pairBefore, value);

            }

        }

        public virtual ObservableCollection<string> PairAfter
        {
            get
            {
                return _pairAfter;
            }
            protected set
            {
                SetProperty(ref _pairAfter, value);

            }

        }
        public KeywordPairType PairType
        {
            get
            {
                return _pairType;
            }

            set
            {
                SetProperty(ref _pairType, value);
            }
        }

        public bool Paired
        {
            get
            {
                return _paired;
            }

            set
            {
                SetProperty(ref _paired, value);
            }
        }
        public void AddPairBefore(string keywordName)
        {
            _pairBefore.Add(keywordName);
            RaisePropertyChanged(nameof(PairBefore));
        }

        public void RemovePairBefore(string keywordName)
        {
            _pairBefore.Remove(keywordName);
            RaisePropertyChanged(nameof(PairBefore));
        }

        public void AddPairAfter(string keywordName)
        {
            _pairAfter.Add(keywordName);
            RaisePropertyChanged(nameof(PairAfter));
        }

        public void RemovePairAfter(string keywordName)
        {
            _pairAfter.Remove(keywordName);
            RaisePropertyChanged(nameof(PairAfter));
        }


        public DependentKeywordParameterizationType ParametrizationType
        {
            get
            {
                return _parameterizationType;
            }

            protected set
            {
                SetProperty(ref _parameterizationType, value);                
            }

        }
        public DependentKeywordParameterCompositionOption ParameterCompositionOption
        {
            get
            {
                return _parameterCompositionOption;
            }
            protected set
            {
                SetProperty(ref _parameterCompositionOption, value);
            }
        }
        public ObservableCollection<DependentParameter> DependentParameters
        {
            get
            {
                return _dependentParameters;
            }
            set
            {
                SetProperty(ref _dependentParameters, value);
            }
        }

        public override string ScriptName
        {
            get
            {
                if (ParametrizationType == DependentKeywordParameterizationType.InlineParameterized)
                {
                    List<string> templist = new List<string>();
                    foreach (var parameter in DependentParameters)
                    {

                        templist.Add(parameter.ScalarValue);

                    }
                    return Name + String.Join("_", templist);

                }
                else if (ParametrizationType == DependentKeywordParameterizationType.CombineParameterized)
                {
                    List<string> templist = new List<string>();
                    foreach (var parameter in DependentParameters)
                    {

                        if (parameter is HashDependentParameter)
                        {
                            foreach (var pair in ((HashDependentParameter)parameter).KeyValuePairList)
                            {
                                templist.Add(pair.Key);
                            }


                        }
                        //else if (parameter is ListDependentParameter)
                        //{
                        //    return Name+_keywordIndex++;
                        //}

                    }
                    return Name + String.Join("_", templist);

                }
                else
                {
                    return Name;

                }
            }
        }

        public void AddDependentParameter(DependentParameter dependentParameter)
        {
            _dependentParameters.Add(dependentParameter);
            RaisePropertyChanged(nameof(DependentParameters));
        }

        public void RemoveDependentParameter(DependentParameter dependentParameter)
        {
            _dependentParameters.Remove(dependentParameter);
            RaisePropertyChanged(nameof(DependentParameters));
        }
        public virtual bool ParametersValidated() { return false; }

       
    }
}
