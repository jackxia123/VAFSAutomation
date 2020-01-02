using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCHandling;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace DbcComparer
{
    /// <summary>
    /// viewmodel of the dbccomparer
    /// </summary>
    public class ViewModelDbcComparer : Model
    {
        private ObservableCollection<DbcMessage> _dbcA = new ObservableCollection<DbcMessage>();
        private ObservableCollection<DbcMessage> _dbcB = new ObservableCollection<DbcMessage>();
        private ObservableCollection<DbcMessage> _msgAdded = new ObservableCollection<DbcMessage>();
        private ObservableCollection<DbcMessage> _msgRemoved = new ObservableCollection<DbcMessage>();
        private ObservableCollection<DbcMessage> _msgModified = new ObservableCollection<DbcMessage>();
        private ObservableCollection<DbcMessage> _msgLayoutChanged = new ObservableCollection<DbcMessage>();

        private ObservableCollection<DbcSignal> _sigAdded = new ObservableCollection<DbcSignal>();       
        private ObservableCollection<DbcSignal> _sigRemoved = new ObservableCollection<DbcSignal>();
        private ObservableCollection<DbcSignal> _sigModified = new ObservableCollection<DbcSignal>();

        private string _pathA = "";
        private string _pathB = "";

        private object _selectedCANTxItemA;
        private object _selectedCANTxItemB;

        bool _isDisplayAdded = false;
        bool _isDisplayRemoved = false;
        bool _isDisplayModified = false;
        bool _isDisplayLayoutChanged = false;

        public ObservableCollection<DbcMessage> DbcA
        {
            get { return _dbcA; }
            set
            {
                SetProperty(ref _dbcA, value);
                OnPropertyChanged(new PropertyChangedEventArgs("DbcB"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageAdded"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageRemoved"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageModified"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageLayoutChanged"));
                OnPropertyChanged(new PropertyChangedEventArgs("SignalAdded"));
                OnPropertyChanged(new PropertyChangedEventArgs("SignalRemoved"));
                OnPropertyChanged(new PropertyChangedEventArgs("SignalModified"));
            }

        }

        public ObservableCollection<DbcMessage> DbcB
        {
            get { return _dbcB; }
            set
            {
                SetProperty(ref _dbcB, value);
                OnPropertyChanged(new PropertyChangedEventArgs("DbcA"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageAdded"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageRemoved"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageModified"));
                OnPropertyChanged(new PropertyChangedEventArgs("MessageLayoutChanged"));
                OnPropertyChanged(new PropertyChangedEventArgs("SignalAdded"));
                OnPropertyChanged(new PropertyChangedEventArgs("SignalRemoved"));
                OnPropertyChanged(new PropertyChangedEventArgs("SignalModified"));
            }

        }

        public ObservableCollection<DbcMessage> MessageAdded
        {
            get
            {
                _msgAdded.Clear();

                foreach (var msg in _dbcB)
                {
                    if (DbcA.Where(x => (x.ID == msg.ID)).Count() == 0)
                    {

                        _msgAdded.Add(msg);
                    }

                }
                return _msgAdded;
            }
            set
            {
                SetProperty(ref _msgAdded, value);
                OnPropertyChanged(new PropertyChangedEventArgs("MessageAdded"));
            }

        }

        public ObservableCollection<DbcMessage> MessageRemoved
        {
            get
            {
                _msgRemoved.Clear();

                foreach (var msg in _dbcA)
                {
                    if (DbcB.Where(x => (x.ID == msg.ID)).Count() == 0)
                    {

                        _msgRemoved.Add(msg);
                    }

                }
               
                return _msgRemoved;
            }
            set
            {
                SetProperty(ref _msgRemoved, value);
                OnPropertyChanged(new PropertyChangedEventArgs("MessageRemoved"));
            }

        }
        public ObservableCollection<DbcMessage> MessageModified
        {
            get
            {
                _msgModified.Clear();

                foreach (var msg in _dbcA)
                {                    

                    if ((DbcB.Where(x => (x.Name == msg.Name) && (x.ID == msg.ID) && (x.DLC == msg.DLC) && (x.CycleTime == msg.CycleTime)).Count() == 0 )&& (DbcB.Where(x => (x.ID == msg.ID)).Count()>0))
                    {

                        _msgModified.Add(msg);
                    }



                }
                return _msgModified;
            }
            set
            {
                SetProperty(ref _msgModified, value);
                OnPropertyChanged(new PropertyChangedEventArgs("MessageModified"));
            }

        }

        public ObservableCollection<DbcMessage> MessageLayoutChanged
        {
            get
            {
                _msgLayoutChanged.Clear();

                foreach (var msg in _dbcA)
                {
                    bool layoutSame = true;

                    if (DbcB.Where(x => (x.Name == msg.Name) && (x.ID == msg.ID) && (x.DLC == msg.DLC) && (x.CycleTime == msg.CycleTime)).Count() > 0)
                    {                        
                        ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>(DbcB.Where(x => (x.Name == msg.Name) && (x.ID == msg.ID) && (x.DLC == msg.DLC) && (x.CycleTime == msg.CycleTime)));
                        foreach (var sig in msg.Signals)
                        {
                            if (founds[0].Signals.Where(x => x.StartBit == sig.StartBit && x.Length == sig.Length).Count() == 0)
                            {
                                layoutSame = false;
                                continue;
                            }

                        }
                        foreach (var sig in founds[0].Signals)
                        {
                            if (msg.Signals.Where(x => x.StartBit == sig.StartBit && x.Length == sig.Length).Count() == 0)
                            {
                                layoutSame = false;
                                continue;
                            }

                        }

                    }

                    if (!layoutSame)
                    {
                        _msgLayoutChanged.Add(msg);

                    }
                   
                }
                return _msgLayoutChanged;
            }
            set
            {
                SetProperty(ref _msgLayoutChanged, value);
                OnPropertyChanged(new PropertyChangedEventArgs("MessageLayoutChanged"));
            }

        }

        public ObservableCollection<DbcSignal> SignalAdded
        {
            get
            {
                _sigAdded.Clear();

                foreach (var msg in _dbcB)
                {
                    foreach (var sig in msg.Signals)
                    {
                        if (DbcA.Where(x => (x.Signals.Where(y => y.StartBit == sig.StartBit &&y.Length == sig.Length &&y.InMessage == sig.InMessage).Count() > 0

                                  )).Count()==0)
                        {

                            _sigAdded.Add(sig);
                        }
                    }
                   

                }
                return _sigAdded;
            }
            set
            {
                SetProperty(ref _sigAdded, value);
                OnPropertyChanged(new PropertyChangedEventArgs("SignalAdded"));
            }

        }
        public ObservableCollection<DbcSignal> SignalRemoved
        {
            get
            {

                _sigRemoved.Clear();

                foreach (var msg in _dbcA)
                {
                    foreach (var sig in msg.Signals)
                    {
                        if (DbcB.Where(x => (x.Signals.Where(y => y.StartBit == sig.StartBit && y.Length == sig.Length && y.InMessage == sig.InMessage).Count() > 0

                                  )).Count() == 0)
                        {

                            _sigRemoved.Add(sig);
                        }
                    }


                }
                
                return _sigRemoved;
            }
            set
            {
                SetProperty(ref _sigRemoved, value);
                OnPropertyChanged(new PropertyChangedEventArgs("SignalRemoved"));
            }

        }
        public ObservableCollection<DbcSignal> SignalModified
        {
            get
            {
                _sigModified.Clear();

                foreach (var msg in _dbcA)
                {
                    foreach (var sig in msg.Signals)
                    {
                        if (DbcB.Where(x => (x.Signals.Where(y => y.StartBit == sig.StartBit && y.Length == sig.Length && y.InMessage == sig.InMessage).Count() > 0 )).Count() > 0)
                        {
                            if (DbcB.Where(x => (
                                                      x.Signals.Where(y => y.StartBit == sig.StartBit && y.Length == sig.Length && y.InMessage == sig.InMessage &&
                                                             y.Offset == sig.Offset &&
                                                          y.Factor == sig.Factor &&
                                                       y.ValueType == sig.ValueType &&
                                                        y.ByteOrder == sig.ByteOrder &&
                                                          y.Name == sig.Name ).Count() > 0
                            )).Count() == 0)
                            {
                                _sigModified.Add(sig);

                            }

                            
                        }
                    }


                }
                return _sigModified;
            }
            set
            {
                SetProperty(ref _sigModified, value);
                OnPropertyChanged(new PropertyChangedEventArgs("SignalModified"));
            }

        }


        public string PathA
        {
            get { return _pathA; }
            set { SetProperty(ref _pathA, value); }
        }

        public string PathB
        {
            get { return _pathB; }
            set {
                SetProperty(ref _pathB, value);
            }
        }


        public object SelectedCANTxItemA
        {
            get { return _selectedCANTxItemA; }
            set
            {
                _selectedCANTxItemA = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedCANTxItemA"));  

            }
        }
        public object SelectedCANTxItemB
        {
            get { return _selectedCANTxItemB; }
            set
            {
                _selectedCANTxItemB = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedCANTxItemB"));

            }
        }

        public bool  IsDisplayAdded
        {
            get { return _isDisplayAdded; }
            set
            {
                _isDisplayAdded = value;
                FilterFunction();
                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayAdded"));


            }
        }
        public bool IsDisplayRemoved
        {
            get { return _isDisplayRemoved; }
            set
            {
                _isDisplayRemoved = value;
                FilterFunction();

                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayRemoved"));



            }
        }
        public bool IsDisplayModified
        {
            get { return _isDisplayModified; }
            set
            {
                _isDisplayModified = value;
                FilterFunction();

                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayModified"));



            }
        }
        public bool IsDisplayLayoutChanged
        {
            get { return _isDisplayLayoutChanged; }
            set
            {
                _isDisplayLayoutChanged = value;
                FilterFunction();
                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayAdded"));
                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayRemoved"));
                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayModified"));
                OnPropertyChanged(new PropertyChangedEventArgs("IsDisplayLayoutChanged"));


            }
        }


        private void FilterFunction()
        {
            ICollectionView messageDataSourceViewA  = CollectionViewSource.GetDefaultView(DbcA);

            ICollectionView messageDataSourceViewB =  CollectionViewSource.GetDefaultView(DbcB);

            messageDataSourceViewA.Filter = (msgModel =>
            {
                ICollectionView signalDataSourceView = CollectionViewSource.GetDefaultView(((DbcMessage)msgModel).Signals);

                Predicate<object> predRemoved;
                Predicate<object> predModified;


                if (_isDisplayRemoved == true)
                {
                    predRemoved = (sigModel => SignalRemoved.Where(x => x.Name == ((DbcSignal)sigModel).Name).Count() > 0);
                }
                else
                {
                    predRemoved = null;
                }

                if (_isDisplayModified == true)
                {
                    predModified = (sigModel => SignalModified.Where(x => x.StartBit == ((DbcSignal)sigModel).StartBit && x.Length == ((DbcSignal)sigModel).Length && x.InMessage == ((DbcSignal)sigModel).InMessage).Count() > 0);
                }
                else
                {
                    predModified =null;
                }


                //update treeview

                signalDataSourceView.Filter = PredicateExtensions.Or(predRemoved, predModified);



                return !signalDataSourceView.IsEmpty;
            });




            messageDataSourceViewB.Filter = (msgModel =>
            {
                ICollectionView signalDataSourceView = CollectionViewSource.GetDefaultView(((DbcMessage)msgModel).Signals);
               

                Predicate<object> predAdded;
                Predicate<object> predModified;


                if (_isDisplayAdded == true)
                {
                    predAdded = (sigModel => SignalAdded.Where(x => x.Name == ((DbcSignal)sigModel).Name).Count() > 0);
                }
                else
                {
                    predAdded = null;
                }

                if (_isDisplayModified == true)
                {
                    predModified = (sigModel => SignalModified.Where(x => x.StartBit == ((DbcSignal)sigModel).StartBit && x.Length == ((DbcSignal)sigModel).Length && x.InMessage == ((DbcSignal)sigModel).InMessage).Count() > 0);
                }
                else
                {
                    predModified = null;
                }


                //update treeview
                signalDataSourceView.Filter = PredicateExtensions.Or(predAdded, predModified);

                return !signalDataSourceView.IsEmpty;
            });
        }


    }
}
