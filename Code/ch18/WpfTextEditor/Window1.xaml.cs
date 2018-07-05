using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTextEditor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //setup handlers for our custom commands
            CommandBinding cmdBindingExit = new CommandBinding(WpfTextEditorCommands.ExitCommand);
            cmdBindingExit.Executed += new ExecutedRoutedEventHandler(cmdBindingExit_Executed);
            cmdBindingExit.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingExit_CanExecute);
            
            CommandBinding cmdBindingWordWrap = new CommandBinding(WpfTextEditorCommands.WordWrapCommand);
            cmdBindingWordWrap.Executed += new ExecutedRoutedEventHandler(cmdBindingWordWrap_Executed);
            
            this.CommandBindings.Add(cmdBindingExit);
            this.CommandBindings.Add(cmdBindingWordWrap);

        }

        void cmdBindingExit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = textBox.Text.Length == 0;
        }

        void cmdBindingWordWrap_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textBox.TextWrapping = ((textBox.TextWrapping == TextWrapping.NoWrap) ? TextWrapping.Wrap : TextWrapping.NoWrap);
        }

        void cmdBindingExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
