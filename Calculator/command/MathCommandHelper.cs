using Calculator.command.mathOperator;

namespace Calculator.command
{
    class MathCommandHelper
    {
        public static bool IsNumber(string potentialNumber)
        {
            return int.TryParse(potentialNumber, out int n);
        }

        public static OperationTypes GetOperatorType( string command )
        {
            // At this point we know there is 3 commands stored in 'm_command'
            string operatorCommand = command.Split(" ")[1];
            OperationTypes operationType;

            switch (operatorCommand)
            {
                case "add":
                    operationType = OperationTypes.ADD;
                    break;

                case "substract":
                    operationType = OperationTypes.SUBSTRACT;
                    break;

                case "multiply":
                    operationType = OperationTypes.MULTIPLY;
                    break;

                default: // unknown operator
                    operationType = OperationTypes.UNKNOWN;
                    break;
            }

            return operationType;
        }

        public static mathOperator.IOperator<OperationTypes> GetMathOperator(string command, OperationTypes operationType)
        {
            /* At this point we know there is 3 commands stored in 'm_command'
             * Where first & last are the values to use. And second idicates which math-operator to use
            */
            string[] commandValues = command.Split(" ");

            mathOperator.IOperator<OperationTypes> mathOperator;

            switch (operationType)
            {
                case OperationTypes.ADD:
                    mathOperator = new mathOperator.AdditionOperator( commandValues[0], commandValues[2] );
                    break;

                case OperationTypes.SUBSTRACT:
                    mathOperator = new mathOperator.SubstractOperator( commandValues[0], commandValues[2] );
                    break;

                case OperationTypes.MULTIPLY:
                    mathOperator = new mathOperator.MultiplicationOperator( commandValues[0], commandValues[2] );
                    break;

                default: // Unknown operator
                    mathOperator = new mathOperator.UnknownOperator( command );
                    break;
            }

            return mathOperator;
        }
    }
}
