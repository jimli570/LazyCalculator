using NUnit.Framework;
using Calculator.command.mathOperator;

namespace AdditionOperatorTest
{
    [TestFixture]
    class AdditionOperatorTest
    {
        [TestCase("a", "b", "a")]
        [TestCase("a10", "b", "a10")]
        public void AdditionOperatorLeftTermTest(string leftTerm, string rightTerm, string expected)
        {
            AdditionOperator mathOperator = new AdditionOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.LeftTerm, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "b")]
        [TestCase("a", "b10", "b10")]
        public void AdditionOperatorRightTermTest(string leftTerm, string rightTerm, string expected)
        {
            AdditionOperator mathOperator = new AdditionOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.RightTerm, Is.EqualTo(expected));
        }

        [TestCase("a", "b", OperationTypes.ADD)]
        [TestCase("a", "b", OperationTypes.ADD)]
        public void AdditionOperatorOperationTypeTest(string leftTerm, string rightTerm, OperationTypes expected)
        {
            AdditionOperator mathOperator = new AdditionOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.OperationType, Is.EqualTo(expected));
        }

        [TestCase(1, 2, 3)]
        [TestCase(10, 12, 22)]
        public void AdditionOperatorOperationExecuteTest(int leftTerm, int rightTerm, int expected)
        {
            AdditionOperator mathOperator = new AdditionOperator("", "");

            Assert.That(mathOperator.Execute(leftTerm, rightTerm), Is.EqualTo(expected));
        }


    }
}
