using NUnit.Framework;
using Calculator.command;
using Calculator.command.mathOperator;

namespace MathCommandTest
{
    class MathCommandTest
    {
        [TestCase("b add c", "b add c")]
        [TestCase("b substract b", "b substract b")]
        [TestCase("b multiply b", "b multiply b")]
        public void MathCommandCommandTest(string commandline, string expected)
        {
            MathCommand command = new MathCommand(commandline);

            Assert.That(command.Command, Is.EqualTo(expected));
        }

        [TestCase("b add c", CommandTypes.MATH)]
        [TestCase("b substract b", CommandTypes.MATH)]
        [TestCase("b multiply b", CommandTypes.MATH)]
        public void MathCommandCommandTypeTest(string commandline, CommandTypes expected)
        {
            MathCommand command = new MathCommand(commandline);

            Assert.That(command.CommandType, Is.EqualTo(expected));
        }

        [TestCase("b add c", OperationTypes.ADD)]
        [TestCase("b substract b", OperationTypes.SUBSTRACT)]
        [TestCase("b multiply b", OperationTypes.MULTIPLY)]
        public void MathCommandGetOperatorTypeTest(string commandline, OperationTypes expected)
        {
            MathCommand command = new MathCommand(commandline);

            Assert.That(command.MathOperator.OperationType, Is.EqualTo(expected));
        }
    }
}
