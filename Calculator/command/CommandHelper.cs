using Calculator.command;
using System.Collections.Generic;

namespace Calculator.command
{
    class CommandHelper
    {

        public static ICommand<CommandTypes> CreateCommand(CommandTypes commandType, string commandline)
        {
            ICommand<CommandTypes> command;

            switch (commandType)
            {
                case CommandTypes.MATH_ADD:
                    command = new MathAddCommand(commandline);
                    break;

                case CommandTypes.MATH_MULT:
                    command = new MathMultCommand(commandline);
                    break;

                case CommandTypes.MATH_SUB:
                    command = new MathSubCommand(commandline);
                    break;

                case CommandTypes.PRINT:
                    command = new PrintCommand(commandline);
                    break;

                default: // CommandTypes.UNKNOWN
                    command = new UnknownCommand(commandline);
                    break;
            }

            return command;
        }

        public static bool IsNumber(string potentialNumber)
        {
            return int.TryParse(potentialNumber, out int n);
        }

        public static bool NeedsLazyEval(string registerName, List<string> lazyRegister, Dictionary<string, int> register)
        {
            //return ((lazyRegister.Contains(registerName) && !IsNumber(registerName)) ||
            //    register.ContainsKey(registerName));
            return ((!lazyRegister.Contains(registerName) && !IsNumber(registerName)) ||
                    !register.ContainsKey(registerName));
        }
    }
}
