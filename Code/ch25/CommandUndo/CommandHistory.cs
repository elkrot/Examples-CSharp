using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandUndo
{
    class CommandHistory
    {
        private Stack<ICommand> _stack = new Stack<ICommand>();

        public bool CanUndo
        {
            get
            {
                return _stack.Count > 0;
            }
        }

        public string MostRecentCommandName
        {
            get
            {
                if (CanUndo)
                {
                    ICommand cmd = _stack.Peek();
                    return cmd.Name;
                }
                return string.Empty;
            }
        }

        public void PushCommand(ICommand command)
        {
            _stack.Push(command);
        }
        
        public ICommand PopCommand()
        {
            return _stack.Pop();
        }
    }
}
