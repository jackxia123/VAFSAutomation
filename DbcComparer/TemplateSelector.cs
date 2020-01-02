using DBCHandling;
using System.Windows;
using System.Windows.Controls;

namespace DbcComparer
{
    public class CANItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MessageTemplate { get; set; }
        public DataTemplate SignalTemplate { get; set; }       

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            
            if (item is DbcMessage)
            {
                return MessageTemplate;
            }
            if (item is DbcSignal)
            {
                return SignalTemplate;
            }

            return null;
        }
    }
}
