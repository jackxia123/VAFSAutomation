using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace RBT.Universal.Keywords
{

    [DataContract]
    [KnownType(typeof(ScalarDependentParameter))]
    [KnownType(typeof(ListDependentParameter))]
    [KnownType(typeof(HashDependentParameter))]
    public class DependentParameter : INotifyPropertyChanged, IKeywordParameter
    {
        private string _name;
        private string _scalarvalue;
        private string _description;
        private KeywordParameterType _dataType;
        private string _defaultValue;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        /// <summary>
        /// parameterless ctor
        /// </summary>
        protected DependentParameter() { }
        /// <summary>
        /// ctor to instantiate a new dependent paramter of keyword, including inline and seperate
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="dataType"></param>
        protected DependentParameter(string name, string desc, string value, string defaultValue, KeywordParameterType dataType)
        {
            _name = name;
            _description = desc;
            _scalarvalue = value;
            _dataType = dataType;
            _defaultValue = defaultValue;

        }

        /// <summary>
        /// Name for the parameter,will be include in the script for hash type,but not for scalar and array type
        /// </summary>
        /// 
        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }

        /// <summary>
        ///  Description for the parameter,will not be include in the script,serve as reminder
        /// </summary>
        /// 
        [DataMember]
        public string Description
        {
            get
            {
                return _description;
            }
            private set
            {
                _description = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Description)));
            }
        }

        /// <summary>
        /// Input value of scalar parameter ,later will be used in script name
        /// </summary>
        /// 
        [DataMember]
        public string ScalarValue
        {
            get { return _scalarvalue; }
            set
            {
                _scalarvalue = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ScalarValue)));
            }
        }

        /// <summary>
        ///  Value for the parameter,will be include in the script for all type
        /// </summary>
        /// 
        [DataMember]
        public virtual string Value
        {
            get
            {
                return "'" + _scalarvalue + "'";
            }
            private set
            {

            }
        }

        /// <summary>
        /// Value for the parameter,serve as reminder 
        /// </summary>
        /// 
        [DataMember]
        public string DefaultValue
        {
            get
            {
                return _defaultValue;
            }
            private set
            {
                _defaultValue = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.DefaultValue)));
            }
        }
        /// <summary>
        /// Type of the keyword parameter, will be used to composite script par
        /// </summary>
        /// 
        [DataMember]
        public virtual KeywordParameterType DataType
        {
            get
            {
                return _dataType;
            }
            private set
            {
                _dataType = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.DataType)));
            }
        }

    }

    [DataContract]
    public class ScalarDependentParameter : DependentParameter
    {
        public ScalarDependentParameter() : base() { }
        public ScalarDependentParameter(string name, string desc, string value, string defaultValue) : base(name, desc, value, defaultValue, KeywordParameterType.Scalar)
        {


        }

    }
    [DataContract]
    public class ListDependentParameter : DependentParameter
    {

        ObservableCollection<string> _listOfValues = new ObservableCollection<string>();

        public ListDependentParameter() : base() { }
        public ListDependentParameter(string name, string desc, ObservableCollection<string> listOfValues, string defaultValue) : base(name, desc, "", defaultValue, KeywordParameterType.List)
        {
            _listOfValues = listOfValues;

        }
        [DataMember]
        public override string Value
        {
            get
            {
                List<string> templist = new List<string>();
                if (_listOfValues == null) return "";
                foreach (string value in _listOfValues)
                {
                    templist.Add(string.Format("'{0}'", value));

                }
                return "@(" + String.Join(",", templist) + ")";

            }
        }

        [DataMember]
        public ObservableCollection<string> ValueList
        {
            get
            {
                return _listOfValues;

            }
            set
            {
                _listOfValues = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ValueList)));


            }

        }
        public void AddValue(string value)
        {

            _listOfValues.Add(value);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ValueList)));
        }

        public void RemoveValue(string value)
        {

            _listOfValues.Remove(value);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.ValueList)));
        }


    }
    [DataContract]
    public class HashDependentParameter : DependentParameter
    {


        Dictionary<string, string> _listOfKeyValuePairs = new Dictionary<string, string>();
        public HashDependentParameter() : base() { }
        public HashDependentParameter(string name, string desc, Dictionary<string, string> listOfKeyValuePairs, string defaultValue) : base(name, desc, "", defaultValue, KeywordParameterType.Hash)
        {
            _listOfKeyValuePairs = listOfKeyValuePairs;

        }
        [DataMember]
        public override string Value
        {
            get
            {
                List<string> templist = new List<string>();
                foreach (KeyValuePair<string, string> kvp in _listOfKeyValuePairs)
                {
                    templist.Add(string.Format("'{0}' => '{1}'", kvp.Key, kvp.Value));

                }

                return "%(" + String.Join(",", templist) + ")";

            }


        }

        [DataMember]
        public Dictionary<string, string> KeyValuePairList
        {
            get
            {
                return _listOfKeyValuePairs;

            }

            set
            {
                _listOfKeyValuePairs = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.KeyValuePairList)));


            }


        }
        public void AddKeyValuePair(string key, string value)
        {
            _listOfKeyValuePairs.Add(key, value);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.KeyValuePairList)));

        }

        public void RemoveKeyValuePair(string key)
        {
            _listOfKeyValuePairs.Remove(key);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.KeyValuePairList)));

        }


    }
}