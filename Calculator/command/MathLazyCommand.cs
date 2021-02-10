using Calculator.command.mathOperator;
using Calculator.command.identifier;

namespace Calculator.command
{
    public class MathLazyCommand : ICommand<CommandTypes>
    {
        public string Command { get; private set; }
        public CommandTypes CommandType { get; private set; }

        public mathOperator.IOperator<OperationTypes> MathOperator { get; }

        public MathLazyCommand(string command)
        {
            Command = command;
            CommandType = CommandTypes.MATH_LAZY;

            string operatorCommand = command.Split(" ")[1];

            OperationTypes operationType = CalcCommandIdentifier.VALID_MATH_OPERATORS[ operatorCommand ];
            MathOperator = MathCommandHelper.CreateMathOperator(Command, operationType);
        }
    }
}
