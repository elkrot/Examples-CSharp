using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfTextEditor
{
    public static class WpfTextEditorCommands
    {
        public static RoutedUICommand ExitCommand;
        public static RoutedUICommand WordWrapCommand;

        static WpfTextEditorCommands()
        {
            InputGestureCollection exitInputs = new InputGestureCollection();
            exitInputs.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
            ExitCommand = new RoutedUICommand("Exit application", "ExitApplication",
                typeof(WpfTextEditorCommands), exitInputs);

            WordWrapCommand = new RoutedUICommand("Word wrap", "WordWrap",
                typeof(WpfTextEditorCommands));
        }
    }
}
