using NUnit.Framework;
using Calculator.command;
using Calculator.command.mathOperator;

namespace MathLazyCommandTest
{
    class MathLazyCommandTest
    {
        [TestCase("b add c", "b add c")]
        [TestCase("b substract b", "b substract b")]
        [TestCase("b multiply b", "b multiply b")]
        public void MathLazyCommandCommandTest(string commandline, string expected)
        {
            MathLazyCommand command = new MathLazyCommand(commandline);

            Assert.That(command.Command, Is.EqualTo(expected));
        }

        [TestCase("b add c", CommandTypes.MATH_LAZY)]
        [TestCase("b substract b", CommandTypes.MATH_LAZY)]
        [TestCase("b multiply b", CommandTypes.MATH_LAZY)]
        public void MathLazyCommandCommandTypeTest(string commandline, CommandTypes expected)
        {
            MathLazyCommand command = new MathLazyCommand(commandline);

            Assert.That(command.CommandType, Is.EqualTo(expected));
        }

        [TestCase("b add c", OperationTypes.ADD)]
        [TestCase("b substract b", OperationTypes.SUBSTRACT)]
        [TestCase("b multiply b", OperationTypes.MULTIPLY)]
        public void MathLazyCommandGetOperatorTypeTest(string commandline, OperationTypes expected)
        {
            MathLazyCommand command = new MathLazyCommand(commandline);

            Assert.That(command.MathOperator.OperationType, Is.EqualTo(expected));
        }
    }
}
