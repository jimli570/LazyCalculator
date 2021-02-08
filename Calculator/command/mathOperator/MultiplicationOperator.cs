namespace Calculator.command.mathOperator
{
    public class MultiplicationOperator : IOperator<OperationTypes>
    {
        public string LeftTerm { get; private set; }
        public string RightTerm { get; private set; }
        public OperationTypes OperationType { get; private set; }

        public MultiplicationOperator(string first, string second)
        {
            LeftTerm = first;
            RightTerm = second;
            OperationType = OperationTypes.MULTIPLY;
        }

        public int Execute(int first, int second)
        {
            return (first * second);
        }
    }
}
