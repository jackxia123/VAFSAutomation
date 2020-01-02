using RBT.Universal.Keywords;

namespace CANTxGenerator
{
    public class NameValue: Model
    {
        private string _name;
        private int _value;

        public NameValue() { }
        public NameValue(string key, int val)
        {
            Name = key;
            Value = val;
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public int Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
    }
}