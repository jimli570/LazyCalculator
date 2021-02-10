using System.Collections.Generic;
using System.Text.RegularExpressions;
using Calculator.command.mathOperator;

namespace Calculator.command.identifier
{
    public class CalcCommandIdentifier : ICommandIdentifier
    {
        public readonly static Dictionary<CommandTypes, int> COMMAND_LEN = new Dictionary<CommandTypes, int> {
            { CommandTypes.PRINT, 2 },
            { CommandTypes.MATH, 3 },
            { CommandTypes.MATH_LAZY, 3 }
        };

        public readonly static Dictionary<CommandTypes, string> VALID_COMMANDS = new Dictionary<CommandTypes, string> {
            { CommandTypes.PRINT, "print" }
        };

        //public readonly static Dictionary<OperationTypes, string> VALID_MATH_OPERATORS = new Dictionary<OperationTypes, string> {
        //    { OperationTypes.ADD, "add" },
        //    { OperationTypes.MULTIPLY, "substract" },
        //    { OperationTypes.SUBSTRACT, "multiply" }
        //};

        public readonly static Dictionary<string, OperationTypes> VALID_MATH_OPERATORS = new Dictionary<string, OperationTypes> {
            {"add", OperationTypes.ADD },
            { "substract" , OperationTypes.SUBSTRACT},
            { "multiply", OperationTypes.MULTIPLY  }
        };

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
            CommandTypes commandType = ValidateFormat(commandline) ?
                    GetCommandType(commandline, lazyRegister) : CommandTypes.UNKNOWN;

            return commandType;
        }

        private bool ValidateFormat(string commandline)
        {
            bool isAlphaNumerical = Regex.IsMatch(commandline.Replace(" ", ""), "^[a-zA-Z0-9]*$");

            return isAlphaNumerical && ValidateLength(commandline);
        }

        private bool ValidateLength(string commandline)
        {
            int numOfCommands = commandline.Split(" ").Length;

            return COMMAND_LEN.ContainsValue(numOfCommands);
        }

        private CommandTypes GetCommandType(string commandline, List<string> lazyRegister)
        {
            string[] commands = commandline.Split(" ");
            int numOfCommands = commands.Length;

            CommandTypes commandType = CommandTypes.UNKNOWN; // Unknown until we know otherwise

            // Print command. Ex: "print a" or "print 10"
            if ( COMMAND_LEN[CommandTypes.PRINT].Equals(commands.Length) &&
                VALID_COMMANDS[CommandTypes.PRINT].Equals(commands[0]) ) {
                commandType = CommandTypes.PRINT;
            }
            /* Math command. Ex: "a add b" or "a multiply 10"
             * Note: Numerical values are not accepted as register values
             */
            else if ( COMMAND_LEN[CommandTypes.MATH].Equals(commands.Length) &&
                      VALID_MATH_OPERATORS.ContainsKey(commands[1]) &&
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
