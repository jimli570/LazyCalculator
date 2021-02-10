using System.Collections.Generic;

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

        // We might have to look up values in the register
        int Execute(Dictionary<string, int> valueRegister);
    }
}
