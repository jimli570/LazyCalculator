using System.Collections.Generic;

namespace Calculator.command.mathOperator
{
    public class AdditionOperator: IOperator<OperationTypes>
    {
        public string LeftTerm { get; private set; }
        public string RightTerm { get; private set; }
        public OperationTypes OperationType { get; private set; }

        public AdditionOperator(string first, string second)
        {
            LeftTerm = first;
            RightTerm = second;
            OperationType = OperationTypes.ADD;
        }

        public int Execute(Dictionary<string, int> valueRegister)
        {
            int lTerm = valueRegister.ContainsKey(LeftTerm) ?
                valueRegister[LeftTerm] : 0;

            int rTerm = valueRegister.ContainsKey(RightTerm) ?
                valueRegister[RightTerm] : int.Parse(RightTerm);

            return (lTerm + rTerm);
        }
    }
}
