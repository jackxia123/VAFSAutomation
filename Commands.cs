using System.Windows.Input;

namespace CANTxGenerator
{
    public class treeViewCommands
    {
        private readonly static RoutedUICommand _expand;
        private readonly static RoutedUICommand _collapse;
        private readonly static RoutedUICommand _copyName;
        //Design Pattern: Command design pattern
        static treeViewCommands()
        {
            InputGestureCollection inputsExpand = new InputGestureCollection();
            inputsExpand.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            _expand = new RoutedUICommand(
            "Expand", "Expand", typeof(treeViewCommands), inputsExpand);

            InputGestureCollection inputsCollapse = new InputGestureCollection();
            inputsCollapse.Add(new KeyGesture(Key.X, ModifierKeys.Control, "Ctrl+X"));
            _collapse = new RoutedUICommand(
            "Collapse", "Collapse", typeof(treeViewCommands), inputsCollapse);

            InputGestureCollection inputsCopyName = new InputGestureCollection();
            inputsCopyName.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Ctrl+C"));
            _copyName = new RoutedUICommand(
            "CopyName", "CopyName", typeof(treeViewCommands), inputsCopyName);
        }
        public static RoutedUICommand Expand
        {
            get
            { return _expand; }
        }

        public static RoutedUICommand Collapse
        {
            get
            { return _collapse; }
        }

        public static RoutedUICommand CopyName
        {
            get
            { return _copyName; }
        }

    }
}