using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace RBT.Universal.Keywords
{
    [DataContract]
    public class DependentKeyword:Keyword,IDependentKeyword
    {
        DependentKeywordParameterizationType _parameterizationType;
        DependentKeywordParameterCompositionOption _parameterCompositionOption;
        ObservableCollection<DependentParameter> _dependentParameters = new ObservableCollection<DependentParameter>();

        protected DependentKeyword() { }
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
                    return Name + String.Join("_",templist);

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
                        //Todo: find solution if the parameter type is not hash type

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
