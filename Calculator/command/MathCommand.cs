using Calculator.command.mathOperator;

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

            OperationTypes operationType = GetOperatorType();
            MathOperator = GetMathOperator( operationType );
        }

        private OperationTypes GetOperatorType()
        {
            return MathCommandHelper.GetOperatorType( Command );
        }

        private mathOperator.IOperator<OperationTypes> GetMathOperator(OperationTypes operationType)
        {
            return MathCommandHelper.GetMathOperator( Command, operationType );
        }
    }
}
