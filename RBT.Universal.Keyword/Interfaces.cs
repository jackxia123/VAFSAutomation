using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// enum of keyword paramter type, for script composing
    /// </summary>
    public enum KeywordParameterType
    {
        Scalar,List,Hash

    }
    /// <summary>
    /// Keyword pair type to support plausibility check
    /// </summary>
    public enum KeywordPairType
    {
        One2One,One2n,n2One
    }
    /// <summary>
    /// enum for Keyword type
    /// </summary>
    public enum DependentKeywordParameterizationType
    {
        InlineParameterized, SeperateParameterized,CombineParameterized
    }
    /// <summary>
    /// Composition type of parameter, which defines how to composite the parameter for keyword
    /// </summary>
    public enum DependentKeywordParameterCompositionOption
    {
        Unique, Parallel,Disjunctive, Combinatorial
    }

    public enum FaultType
    {
        Mandatory,Optional,Disjuction

    }
    /// <summary>
    /// Interface for keyword basic type, no parameter,no pairing
    /// </summary>
    public interface IKeyword:INotifyPropertyChanged
    {

        string Name{ get;   }       
        int Delay { get;   }
        string Description { get;  }
        int UsageLimit { get;  } 
        string ScriptName { get; }       

    }
    /// <summary>
    /// Interface for keyword paired type, need to sepecify the before keywords and after keywords
    /// </summary>
    public interface IPairedKeyword:IKeyword
    {
        ObservableCollection<string> PairBefore { get; }
        ObservableCollection<string> PairAfter { get; }
        void AddPairBefore(string keywordName);
        void RemovePairBefore(string keywordName);
        void AddPairAfter(string keywordName);
        void RemovePairAfter(string keywordName);
        KeywordPairType PairType { get; set; }
        bool Paired { get; set; }

    }
    /// <summary>
    /// Interface for keyword with parameters, including inline ,seperate and combined
    /// </summary>
    public interface IDependentKeyword: IKeyword
    {
        DependentKeywordParameterizationType ParametrizationType { get; }
        DependentKeywordParameterCompositionOption ParameterCompositionOption { get; }
        
         ObservableCollection<DependentParameter> DependentParameters { get; set; }

        void AddDependentParameter(DependentParameter dependentParameter);
        void RemoveDependentParameter(DependentParameter dependentParameter);

        bool ParametersValidated();
    }

    /// <summary>
    /// Interface for Keyword Parameter
    /// </summary>
    public interface IKeywordParameter
    {
        string Name { get;  }
        string Value { get;  }
        string Description { get; }
        string DefaultValue { get;}
        KeywordParameterType DataType { get; }
    }
 
}