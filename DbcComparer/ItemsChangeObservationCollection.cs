using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System;
using System.Collections.Generic;

namespace DbcComparer
{
    /// <summary>
    ///     This class adds the ability to refresh the list when any property of
    ///     the objects changes in the list which implements the INotifyPropertyChanged. 
    /// </summary>
    /// <typeparam name="T">
    
    public class ItemsChangeObservableCollection<T> :
            ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public ItemsChangeObservableCollection() { }
        public ItemsChangeObservableCollection(IEnumerable<T> list) : base(list){    }
        public class ChildElementPropertyChangedEventArgs : EventArgs
        {
            public object ChildElement { get; set; }
            public ChildElementPropertyChangedEventArgs(object item)
            {
                ChildElement = item;
            }
        }
        public delegate void ChildElementPropertyChangedEventHandler(ChildElementPropertyChangedEventArgs e);
        public event ChildElementPropertyChangedEventHandler ChildElementPropertyChanged;
        private void OnChildElementPropertyChanged(object childelement)
        {
            if (ChildElementPropertyChanged != null)
            {
                ChildElementPropertyChanged(new ChildElementPropertyChangedEventArgs(childelement));
            }
        }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RegisterPropertyChanged(e.NewItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                UnRegisterPropertyChanged(e.OldItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                UnRegisterPropertyChanged(e.OldItems);
                RegisterPropertyChanged(e.NewItems);
            }

            base.OnCollectionChanged(e);
        }

        protected override void ClearItems()
        {
            UnRegisterPropertyChanged(this);
            base.ClearItems();
        }

        private void RegisterPropertyChanged(IList items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
        }

        private void UnRegisterPropertyChanged(IList items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                {
                    item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChildElementPropertyChanged(sender);
        }
    }

}
