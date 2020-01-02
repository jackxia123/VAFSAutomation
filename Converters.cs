using DBCHandling;
using OpStates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CANTxGenerator
{

    /// <summary>
    /// Value converter between bool and IsChecked property value (bool?)
    /// </summary>  
    [ValueConversion(typeof(object), typeof(bool))]
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
            if (isChecked == null || isChecked == false)
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


    public class ImageVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {

            object CANTxItem = (value[0] as TreeViewItem).DataContext;
            ObservableCollection<Connection> AllConnection = (ObservableCollection<Connection>)value[1];

            if (CANTxItem is DbcMessage)
            {

                if (AllConnection.Any(x => (x.SourceCANTxSignal is DbcMessage) && ((DbcMessage)(x.SourceCANTxSignal)).Name == ((DbcMessage)CANTxItem).Name))
                {
                    return Visibility.Visible;
                }
                return Visibility.Hidden;

            }
            else if (CANTxItem is DbcSignal)
            {

                if (AllConnection.Any(x => (x.SourceCANTxSignal is DbcSignal) && ((DbcSignal)(x.SourceCANTxSignal)).Name == ((DbcSignal)CANTxItem).Name))
                {
                    return Visibility.Visible;
                }
                return Visibility.Hidden;


            }
            else { return Visibility.Hidden; }

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
            ObservableCollection<Connection> AllConnection = (ObservableCollection<Connection>)value[1];


            if (CANTxItem is DbcMessage)
            {

                if (AllConnection.Where(x => (x.SourceCANTxSignal is DbcMessage) && ((DbcMessage)(x.SourceCANTxSignal)).Name == ((DbcMessage)CANTxItem).Name).Count() > 0
                        && AllConnection.Where(x => (x.SourceCANTxSignal is DbcMessage) && ((DbcMessage)(x.SourceCANTxSignal)).Name == ((DbcMessage)CANTxItem).Name).All(x => x.Parameterized == true))
                {
                    return Brushes.Black;
                }
                return Brushes.Red;

            }
            else if (CANTxItem is DbcSignal)
            {

                if ((AllConnection.Where(x => (x.SourceCANTxSignal is DbcSignal) && ((DbcSignal)(x.SourceCANTxSignal)).Name == ((DbcSignal)CANTxItem).Name).Count() > 0
                        && AllConnection.Where(x => (x.SourceCANTxSignal is DbcSignal) && ((DbcSignal)(x.SourceCANTxSignal)).Name == ((DbcSignal)CANTxItem).Name).All(x => x.Parameterized == true))
                        || (AllConnection.Where(X => X.ConnectedOpState.OperationStateSignals.Any(Z => Z.Name == ((DbcSignal)CANTxItem).Name)).Count() > 0)
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
            return input ? "Yes" : "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;
            if (input == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    public class Bool2ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = (bool)value;
            return input ? new BitmapImage(new Uri("pack://application:,,,/Icon/Ok.png", UriKind.Absolute)) : new BitmapImage(new Uri("/Icon/NOk.png", UriKind.Relative));
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
            return input ? Colors.Green : Colors.Red;
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
            ObservableCollection<KeyValuePair<double, string>> input = value as ObservableCollection<KeyValuePair<double, string>>;
            List<string> temp = new List<string>();
            foreach (KeyValuePair<double, string> pair in input)
            {

                temp.Add(pair.Key + ":" + pair.Value);

            }
            return String.Join(" , ", temp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }

    public class CombGrpViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "none";
            }

            if (value is ObservableCollection<CanTxSignalType>)
            {
                ObservableCollection<CanTxSignalType> input = value as ObservableCollection<CanTxSignalType>;

                ListCollectionView lcv = new ListCollectionView(input);

                lcv.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                //lcv.GroupDescriptions.Add(new PropertyGroupDescription("SubCategory"));
                return lcv;

            }
            else if (value is ObservableCollection<OpState>)
            {

                ObservableCollection<OpState> input = value as ObservableCollection<OpState>;

                ListCollectionView lcv = new ListCollectionView(input);

                lcv.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                //lcv.GroupDescriptions.Add(new PropertyGroupDescription("SubCategory"));
                return lcv;

            }
            return null;
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
            //add ABS_Gen93 selection
            if (input.StartsWith("ABS"))
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
