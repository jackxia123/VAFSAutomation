using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using DBCHandling;

namespace CANTxGenerator
{
    public class CANItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MessageTemplate { get; set; }
        public DataTemplate SignalTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item != null && item is DbcMessage)
            {
                return MessageTemplate;
            }
            if (item != null && item is DbcSignal)
            {
                return SignalTemplate;
            }

            return null;
        }
    }
}
