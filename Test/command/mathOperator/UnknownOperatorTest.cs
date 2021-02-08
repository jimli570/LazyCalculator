using NUnit.Framework;
using Calculator.command.mathOperator;

namespace UnknownOperatorTest
{
    [TestFixture]
    class UnknownOperatorTest
    {
        [TestCase("whaterver_text", "UNKNOWN")]
        [TestCase("abdraKadabra", "UNKNOWN")]
        public void UnknownOperatorLeftTermTest(string term, string expected)
        {
            UnknownOperator mathOperator = new UnknownOperator(term);

            Assert.That(mathOperator.LeftTerm, Is.EqualTo(expected));
        }

        [TestCase("whaterver_text", "UNKNOWN")]
        [TestCase("abdraKadabra", "UNKNOWN")]
        public void UnknownOperatorRightTermTest(string term, string expected)
        {
            UnknownOperator mathOperator = new UnknownOperator(term);

            Assert.That(mathOperator.RightTerm, Is.EqualTo(expected));
        }

        [TestCase("whaterver_text", OperationTypes.UNKNOWN)]
        [TestCase("whaterver_text23", OperationTypes.UNKNOWN)]
        public void UnknownOperatorOperationTypeTest(string term, OperationTypes expected)
        {
            UnknownOperator mathOperator = new UnknownOperator(term);

            Assert.That(mathOperator.OperationType, Is.EqualTo(expected));
        }

        [TestCase(10, 10, 0)]
        [TestCase(24, 12, 0)]
        public void SubstractOperatorOperationExecuteTest(int leftTerm, int rightTerm, int expected)
        {
            UnknownOperator mathOperator = new UnknownOperator("");

            Assert.That(mathOperator.Execute(leftTerm, rightTerm), Is.EqualTo(expected));
        }
    }
}
