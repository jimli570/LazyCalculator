using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.command.identifier
{
    public class CalcCommandIdentifier : ICommandIdentifier
    {
        private const int PRINT_COMMAND_LEN = 2;
        private const int MATH_COMMAND_LEN = 3;

        private readonly string PRINT_COMMAND = "print";
        private readonly List<string> VALID_MATH_COMMANDS = new List<string>
            { "add", "substract", "multiply" };

        public ICommand<CommandTypes> Identify(string commandline, List<string> lazyRegister)
        {
            commandline = commandline.ToLower(); // We want it case insensitive
            command.CommandTypes commandType = IdentifyCommandType(commandline, lazyRegister);

            command.ICommand<CommandTypes> command;
            switch (commandType)
            {
                case CommandTypes.PRINT:
                    command = new command.PrintCommand( commandline );
                    break;

                case CommandTypes.MATH:
                    command = new command.MathCommand( commandline );
                    break;

                case CommandTypes.MATH_LAZY:
                    command = new command.MathLazyCommand( commandline );
                    break;

                default: // CommandTypes.UNKNOWN
                    command = new command.UnknownCommand( commandline );
                    break;
            }

            return command;
        }
        
        private CommandTypes IdentifyCommandType(string commandline, List<string> lazyRegister)
        {
            CommandTypes commandType = PotentiallyValidCommand(commandline) ?
                    GetCommandType(commandline, lazyRegister) : CommandTypes.UNKNOWN;

            return commandType;
        }

        private bool PotentiallyValidCommand(string commandline)
        {
            bool isAlphanumerical = IsAlphaNumerical(commandline);
            bool correctLength = IsLengthCorrect(commandline);

            return (isAlphanumerical && correctLength);
        }

        private bool IsAlphaNumerical(string commandline)
        {
            return Regex.IsMatch(commandline.Replace(" ", ""), "^[a-zA-Z0-9]*$");
        }

        private bool IsLengthCorrect(string commandline)
        {
            string[] commands = commandline.Split(" ");
            int numOfCommands = commands.Length;

            return ((numOfCommands == PRINT_COMMAND_LEN) ||
                   (numOfCommands == MATH_COMMAND_LEN));
        }

        private CommandTypes GetCommandType(string commandline, List<string> lazyRegister)
        {
            string[] commands = commandline.Split(" ");
            int numOfCommands = commands.Length;

            CommandTypes commandType = CommandTypes.UNKNOWN; // Unknown until we know otherwise

            // Print command. Ex: "print a" or "print 10"
            if ((commands.Length == PRINT_COMMAND_LEN) && (commands[0] == PRINT_COMMAND)) {
                commandType = CommandTypes.PRINT;
            }
            /* Math command. Ex: "a add b" or "a multiply 10"
             * Note: Numerical values are not accepted as register values
             */
            else if ( commands.Length == MATH_COMMAND_LEN &&
                       VALID_MATH_COMMANDS.Contains(commands[1]) &&
                       !MathCommandHelper.IsNumber(commands[0]))
            {
                bool needLazyEvaluation = NeedsLazyEvaluation(commands[0], commands[2], lazyRegister);

                commandType = needLazyEvaluation ?
                    CommandTypes.MATH_LAZY : CommandTypes.MATH;
            }
            else
            {
                commandType = CommandTypes.UNKNOWN;
            }

            return commandType;
        }

        private bool NeedsLazyEvaluation(string leftTerm, string rightTerm, List<string> lazyRegister)
        {
            return (!lazyRegister.Contains(rightTerm) && !MathCommandHelper.IsNumber(rightTerm)) ||
                    lazyRegister.Contains(rightTerm);

        }
    }
}
