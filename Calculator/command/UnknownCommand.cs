namespace Calculator.command
{
    public class UnknownCommand : ICommand<CommandTypes>
    {
        public string Command { get; private set; }
        public CommandTypes CommandType { get; private set; }

        public UnknownCommand(string command)
        {
            Command = command;
            CommandType = CommandTypes.UNKNOWN;
        }
    }
}
