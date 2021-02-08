namespace Calculator.command
{
    public class PrintCommand : ICommand<CommandTypes>
    {
        public string Command { get; private set; }
        public CommandTypes CommandType { get; private set; }
        
        public string Term { get; private set; }

        public PrintCommand(string command)
        {
            Command = command;
            CommandType = CommandTypes.PRINT;

            Term = command.Split(" ")[1];
        }
    }
}
