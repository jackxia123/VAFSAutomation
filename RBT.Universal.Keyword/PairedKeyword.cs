using System;
using System.Collections.ObjectModel;

namespace RBT.Universal.Keywords
{
    [Serializable]
    public class PairedKeyword: Keyword,IPairedKeyword
    {
        KeywordPairType _pairType;
        bool _paired = false;
        private ObservableCollection<string> _pairBefore = new ObservableCollection<string> ();
        private ObservableCollection<string> _pairAfter=new ObservableCollection<string> ();

        protected PairedKeyword() { }
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
                SetProperty(ref _pairType,value);
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
                SetProperty(ref _paired,value);
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
    }
}
