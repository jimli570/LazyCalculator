using Calculator.command.mathOperator;
using Calculator.command.identifier;

namespace Calculator.command
{
    public class MathCommand : ICommand<CommandTypes>
    {
        public string Command { get; private set; }
        public CommandTypes CommandType { get; private set; }

        public mathOperator.IOperator<OperationTypes> MathOperator { get; }

        public MathCommand(string command)
        {
            Command = command;
            CommandType = CommandTypes.MATH;

            string operatorCommand = command.Split(" ")[1];

            OperationTypes operationType =  CalcCommandIdentifier.VALID_MATH_OPERATORS[operatorCommand];
            MathOperator = MathCommandHelper.CreateMathOperator(Command, operationType);
        }
    }
}
