using System.Collections.Generic;

namespace Calculator.command.mathOperator
{
    public class UnknownOperator : IOperator<OperationTypes>
    {
        public string LeftTerm { get; private set; }
        public string RightTerm { get; private set; }
        public OperationTypes OperationType { get; private set; }

        public UnknownOperator(string command)
        {
            LeftTerm = "UNKNOWN";
            RightTerm = "UNKNOWN";
            OperationType = OperationTypes.UNKNOWN;
        }

        public int Execute(Dictionary<string, int> valueRegister)
        {
            return 0; // Just to comply with interface
        }
    }
}
