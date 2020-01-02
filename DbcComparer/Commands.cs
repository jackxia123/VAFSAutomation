using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbcComparer
{
    public class treeViewCommands
    {
        private readonly static RoutedUICommand _expand;
        private readonly static RoutedUICommand _collapse;
        private readonly static RoutedUICommand _copyName;
        private readonly static RoutedUICommand _findMatch;

        private readonly static RoutedUICommand _expandB;
        private readonly static RoutedUICommand _collapseB;
        private readonly static RoutedUICommand _copyNameB;
        private readonly static RoutedUICommand _findMatchB;

        static treeViewCommands()
        {
            InputGestureCollection inputsExpand = new InputGestureCollection();
            inputsExpand.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            _expand = new RoutedUICommand(
            "Expand", "Expand", typeof(treeViewCommands), inputsExpand);

            InputGestureCollection inputsExpandB = new InputGestureCollection();
            inputsExpandB.Add(new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"));
            _expandB = new RoutedUICommand(
            "Expand", "Expand", typeof(treeViewCommands), inputsExpandB);

            InputGestureCollection inputsCollapse = new InputGestureCollection();
            inputsCollapse.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Ctrl+X"));
            _collapse = new RoutedUICommand(
            "Collapse", "Collapse", typeof(treeViewCommands), inputsCollapse);

            InputGestureCollection inputsCollapseB = new InputGestureCollection();
            inputsCollapseB.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Ctrl+X"));
            _collapseB = new RoutedUICommand(
            "Collapse", "Collapse", typeof(treeViewCommands), inputsCollapseB);

            InputGestureCollection inputsCopyName = new InputGestureCollection();
            inputsCopyName.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Ctrl+C"));
            _copyName = new RoutedUICommand(
            "CopyName", "CopyName", typeof(treeViewCommands), inputsCopyName);

            InputGestureCollection inputsCopyNameB = new InputGestureCollection();
            inputsCopyNameB.Add(new KeyGesture(Key.C, ModifierKeys.Control, "Ctrl+C"));
            _copyNameB = new RoutedUICommand(
            "CopyName", "CopyName", typeof(treeViewCommands), inputsCopyNameB);

            InputGestureCollection inputsFindMatch = new InputGestureCollection();
            inputsFindMatch.Add(new KeyGesture(Key.F, ModifierKeys.Control, "Ctrl+F"));
            _findMatch = new RoutedUICommand(
            "FindMatch", "FindMatch", typeof(treeViewCommands), inputsFindMatch);

            InputGestureCollection inputsFindMatchB = new InputGestureCollection();
            inputsFindMatchB.Add(new KeyGesture(Key.F, ModifierKeys.Control, "Ctrl+F"));
            _findMatchB = new RoutedUICommand(
            "FindMatch", "FindMatch", typeof(treeViewCommands), inputsFindMatchB);

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

        public static RoutedUICommand FindMatch
        {
            get
            { return _findMatch; }
        }

        public static RoutedUICommand ExpandB
        {
            get
            { return _expandB; }
        }

        public static RoutedUICommand CollapseB
        {
            get
            { return _collapseB; }
        }

        public static RoutedUICommand CopyNameB
        {
            get
            { return _copyNameB; }
        }

        public static RoutedUICommand FindMatchB
        {
            get
            { return _findMatchB; }
        }

    }
}
