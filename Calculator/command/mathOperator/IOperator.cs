namespace Calculator.command.mathOperator
{
    public enum OperationTypes
    {
        UNKNOWN,
        ADD,
        SUBSTRACT,
        MULTIPLY
    }

    public interface IOperator<T>
        where T : System.Enum
    {
        string LeftTerm { get; }
        string RightTerm { get; }
        T OperationType { get; }

        int Execute(int first, int second);
    }
}
