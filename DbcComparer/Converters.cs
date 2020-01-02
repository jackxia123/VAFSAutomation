using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;

using System.Windows.Media.Imaging;
using System.Windows.Media;
using DBCHandling;


namespace DbcComparer
{

    /// <summary>
    /// Value converter between bool and IsChecked property value (bool?)
    /// </summary>    
    public class IsCheckedConverter : IValueConverter
    {
        #region IValueConverter Members
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            
            return (bool)value;
        }

        /// <summary>
        /// Converts the value back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool? isChecked = (bool?)value;
            if (isChecked == null|| isChecked ==false)
            {
                return false;
            }
            else
            {
                return isChecked == true;
            }
        }
        #endregion IValueConverter Members
    }
    public class BaseImageConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            object CANTxItem = (value[0] as TreeViewItem).DataContext;
            ObservableCollection<DbcMessage> BaseDBC = (ObservableCollection<DbcMessage>)value[1];

            if (CANTxItem is DbcMessage)
            {

                if (BaseDBC.Where(x => (x.Name == ((DbcMessage)CANTxItem).Name) && (x.ID == ((DbcMessage)CANTxItem).ID) && (x.DLC == ((DbcMessage)CANTxItem).DLC) && (x.CycleTime == ((DbcMessage)CANTxItem).CycleTime)).Count() > 0)
                {
                    DbcMessage seletectMessage = (DbcMessage)CANTxItem;
                    ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>( BaseDBC.Where(x => (x.ID == ((DbcMessage)CANTxItem).ID) && (x.DLC == ((DbcMessage)CANTxItem).DLC) && (x.CycleTime == ((DbcMessage)CANTxItem).CycleTime)));
                    bool layoutSame = true;

                    foreach (var sig in founds[0].Signals)
                    {
                        if (seletectMessage.Signals.Where(x=>x.StartBit ==sig.StartBit && x.Length == sig.Length).Count() ==0)
                        {
                            layoutSame = false;
                        }

                    }

                    foreach (var sig in seletectMessage.Signals)
                    {
                        if (founds[0].Signals.Where(x => x.StartBit == sig.StartBit && x.Length == sig.Length).Count() == 0)
                        {
                            layoutSame = false;
                        }

                    }

                    if (layoutSame)
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/Icon/Ok.png", UriKind.Absolute));
                    }
                    else
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/Icon/layoutChanged.png", UriKind.Absolute));
                    }
                    
                }
                else if (BaseDBC.Where(x => x.ID == ((DbcMessage)CANTxItem).ID).Count() == 0)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/removed.png", UriKind.Absolute));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/modified.png", UriKind.Absolute));
                }
                

            }
            else if (CANTxItem is DbcSignal)
            {

                if ((BaseDBC.Where(x => (x.Signals.Where(y => y.StartBit == ((DbcSignal)CANTxItem).StartBit &&
                                                          y.Length == ((DbcSignal)CANTxItem).Length &&
                                                       y.Offset == ((DbcSignal)CANTxItem).Offset &&
                                                          y.Factor == ((DbcSignal)CANTxItem).Factor &&
                                                       y.ValueType == ((DbcSignal)CANTxItem).ValueType &&
                                                       y.ByteOrder == ((DbcSignal)CANTxItem).ByteOrder &&
                                                          y.Name == ((DbcSignal)CANTxItem).Name &&
                                                           y.InMessage == ((DbcSignal)CANTxItem).InMessage).Count() > 0
                                  )
                         ).Count() > 0
                   )
                 )
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/Ok.png", UriKind.Absolute));
                }
                else if ((BaseDBC.Where(x => (x.Signals.Where(y => y.StartBit == ((DbcSignal)CANTxItem).StartBit &&
                                                                   y.Length == ((DbcSignal)CANTxItem).Length &&
                                                                    y.InMessage == ((DbcSignal)CANTxItem).InMessage).Count() > 0

                                  )
                         ).Count() == 0))
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/removed.png", UriKind.Absolute));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/modified.png", UriKind.Absolute));
                }



            }
            else {
                return new BitmapImage(new Uri("pack://application:,,,/Icon/Nok.png", UriKind.Absolute));
            }






        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class ImageVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter,CultureInfo culture)
        {

            object CANTxItem = (value[0] as TreeViewItem).DataContext;
            ObservableCollection<DbcMessage> BaseDBC = (ObservableCollection <DbcMessage>)value[1];

            if (CANTxItem is DbcMessage)
            {

                if (BaseDBC.Where(x => (x.Name == ((DbcMessage)CANTxItem).Name) && (x.ID == ((DbcMessage)CANTxItem).ID) && (x.DLC == ((DbcMessage)CANTxItem).DLC) && (x.CycleTime == ((DbcMessage)CANTxItem).CycleTime) ).Count() > 0  )
                {
                    DbcMessage seletectMessage = (DbcMessage)CANTxItem;
                    ObservableCollection<DbcMessage> founds = new ObservableCollection<DbcMessage>(BaseDBC.Where(x => (x.ID == ((DbcMessage)CANTxItem).ID) && (x.DLC == ((DbcMessage)CANTxItem).DLC) && (x.CycleTime == ((DbcMessage)CANTxItem).CycleTime)));
                    bool layoutSame = true;

                    foreach (var sig in founds[0].Signals)
                    {
                        if (seletectMessage.Signals.Where(x => x.StartBit == sig.StartBit && x.Length == sig.Length).Count() == 0)
                        {
                            layoutSame = false;
                        }

                    }

                    foreach (var sig in seletectMessage.Signals)
                    {
                        if (founds[0].Signals.Where(x => x.StartBit == sig.StartBit && x.Length == sig.Length).Count() == 0)
                        {
                            layoutSame = false;
                        }

                    }

                    if (layoutSame)
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/Icon/Ok.png", UriKind.Absolute));
                    }
                    else
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/Icon/layoutChanged.png", UriKind.Absolute));
                    }
                }
                else if (BaseDBC.Where(x => x.ID == ((DbcMessage)CANTxItem).ID).Count() == 0)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/added.png", UriKind.Absolute));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/modified.png", UriKind.Absolute));
                }
               

            }
            else if (CANTxItem is DbcSignal)
            {

                if ((BaseDBC.Where(x => (x.Signals.Where(y => y.StartBit == ((DbcSignal)CANTxItem).StartBit &&
                                                          y.Length == ((DbcSignal)CANTxItem).Length &&
                                                       y.Offset == ((DbcSignal)CANTxItem).Offset &&
                                                          y.Factor == ((DbcSignal)CANTxItem).Factor &&
                                                       y.ValueType == ((DbcSignal)CANTxItem).ValueType &&
                                                        y.ByteOrder == ((DbcSignal)CANTxItem).ByteOrder &&
                                                          y.Name == ((DbcSignal)CANTxItem).Name &&
                                                           y.InMessage == ((DbcSignal)CANTxItem).InMessage).Count() > 0
                                  )
                         ).Count() > 0
                   )
                 )
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/Ok.png", UriKind.Absolute));
                }
                else if ((BaseDBC.Where(x => (x.Signals.Where(y => y.StartBit == ((DbcSignal)CANTxItem).StartBit &&
                                                                   y.Length == ((DbcSignal)CANTxItem).Length &&
                                                                    y.InMessage == ((DbcSignal)CANTxItem).InMessage).Count() > 0
                                        
                                  )
                         ).Count() == 0))
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/added.png", UriKind.Absolute));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Icon/modified.png", UriKind.Absolute));
                }
                


            }
            else {
                return new BitmapImage(new Uri("pack://application:,,,/Icon/Nok.png", UriKind.Absolute));
            }          


            
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class Connection2ForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            object CANTxItem = (value[0] as TreeViewItem).DataContext;
            ObservableCollection<DbcMessage> BaseDBC = (ObservableCollection<DbcMessage>)value[1];


            if (CANTxItem is DbcMessage)
            {

                if (BaseDBC.Where(x => (x.Name == ((DbcMessage)CANTxItem).Name)&&(x.ID == ((DbcMessage)CANTxItem).ID) && (x.DLC == ((DbcMessage)CANTxItem).DLC) && (x.CycleTime == ((DbcMessage)CANTxItem).CycleTime)).Count() > 0)
                {
                    return  Brushes.Black;
                }
                return Brushes.Red;

            }
            else if (CANTxItem is DbcSignal)
            {

                if ((BaseDBC.Where(x => (x.Signals.Where(y => y.StartBit == ((DbcSignal)CANTxItem).StartBit &&
                                                          y.Length == ((DbcSignal)CANTxItem).Length &&
                                                       y.Offset == ((DbcSignal)CANTxItem).Offset &&
                                                          y.Factor == ((DbcSignal)CANTxItem).Factor &&
                                                       y.ValueType == ((DbcSignal)CANTxItem).ValueType &&
                                                       y.ByteOrder == ((DbcSignal)CANTxItem).ByteOrder &&
                                                          y.Name == ((DbcSignal)CANTxItem).Name &&
                                                           y.InMessage == ((DbcSignal)CANTxItem).InMessage).Count() > 0
                                  )
                         ).Count() > 0
                   )
                 )
                {
                    return Brushes.Black;
                }
                return Brushes.Red;


            }
            else { return Brushes.Red; }





        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class Collection2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> input = value as ObservableCollection<string>;             
            return String.Join(" , ", input.ToArray<string>());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }

    public class Bool2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = (bool)value;
            return input ? "Yes":"No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }

    public class Bool2ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = (bool)value;
            return input?new BitmapImage(new Uri("pack://application:,,,/Icon/Ok.png",UriKind.Absolute)): new BitmapImage(new Uri("/Icon/NOk.png",UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }
    public class Bool2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = (bool)value;
            return input ? Colors.Green:Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }

    public class SignalCodingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "none";
            }
            ObservableCollection<KeyValuePair<double,string>> input = value as ObservableCollection<KeyValuePair<double, string>>;
            List<string> temp = new List<string>();
            foreach (KeyValuePair<double,string> pair in input)
            {

                temp.Add(pair.Key+":"+pair.Value);

            }
            return String.Join(" , ", temp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }

   

    public class Select2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;

            if (input == "ABS")
            {
                return Visibility.Hidden;

            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }   

}
