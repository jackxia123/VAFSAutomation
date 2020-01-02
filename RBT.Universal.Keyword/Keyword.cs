using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace RBT.Universal.Keywords
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public class Keyword:Model,IKeyword
    {
        private string _name;
        private string _description;
        private int _delay;
        int _usageLimit;

        //string _scriptName;
        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            protected set
            {
                SetProperty(ref _name, value);
            }

        }
        [DataMember]
        public string Description
        {
            get
            {
                return _description;
            }
            protected set
            {
                SetProperty(ref _description, value);
            }

        }
        [DataMember]
        public int Delay
        {
            get
            {
                return _delay;
            }
            protected set
            {
                SetProperty(ref _delay, value);
            }

        }
        [DataMember]
        public int UsageLimit
        {
            get
            {
                return _usageLimit;
            }
            protected set
            {
                SetProperty(ref _usageLimit, value);
            }

        }
        [DataMember]
        public virtual string ScriptName
        {
            get
            {                
                return _name;
            }

            protected set
            {
                SetProperty(ref _name, value);
            }

        }

        private static Type[] GetKnownTypes()
        {
            List<Type> type = new List<Type>();
            Type[] allTypes = Assembly.GetAssembly(typeof(Keyword)).GetExportedTypes();
            foreach (Type t in allTypes)
            {
                if (t.IsSubclassOf(typeof(Keyword)) && (t != typeof(DependentKeyword)) && (t != typeof(PairedKeyword)) && (t != typeof(PairedDependentKeyword)))
                    type.Add(t);

            }
            return type.ToArray();
        }
    }
}
