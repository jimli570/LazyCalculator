using System.Collections.Generic;

namespace Calculator.command.identifier
{
    public interface ICommandIdentifier
    {
        ICommand<CommandTypes> Identify(string commandline, List<string> lazyRegister);
    }
}
