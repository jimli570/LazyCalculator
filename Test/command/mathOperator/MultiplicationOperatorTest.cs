using NUnit.Framework;
using Calculator.command.mathOperator;

namespace MultiplicationOperatorTest
{
    [TestFixture]
    class MultiplicationOperatorTest
    {
        [TestCase("a", "b", "a")]
        [TestCase("a10", "b", "a10")]
        public void MultiplicationOperatorLeftTermTest(string leftTerm, string rightTerm, string expected)
        {
            MultiplicationOperator mathOperator = new MultiplicationOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.LeftTerm, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "b")]
        [TestCase("a", "b10", "b10")]
        public void MultiplicationOperatorRightTermTest(string leftTerm, string rightTerm, string expected)
        {
            MultiplicationOperator mathOperator = new MultiplicationOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.RightTerm, Is.EqualTo(expected));
        }

        [TestCase("a", "b", OperationTypes.MULTIPLY)]
        [TestCase("a", "b", OperationTypes.MULTIPLY)]
        public void MultiplicationOperatorOperationTypeTest(string leftTerm, string rightTerm, OperationTypes expected)
        {
            MultiplicationOperator mathOperator = new MultiplicationOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.OperationType, Is.EqualTo(expected));
        }

        [TestCase(1, 2, 2)]
        [TestCase(10, 12, 120)]
        public void MultiplicationOperatorOperationExecuteTest(int leftTerm, int rightTerm, int expected)
        {
            MultiplicationOperator mathOperator = new MultiplicationOperator("", "");

            Assert.That(mathOperator.Execute(leftTerm, rightTerm), Is.EqualTo(expected));
        }


    }
}
