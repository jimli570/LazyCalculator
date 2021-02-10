using NUnit.Framework;
using Calculator.command.mathOperator;
using System.Collections.Generic;

namespace SubstractOperatorTest
{
    [TestFixture]
    class SubstractOperatorTest
    {
        [TestCase("a", "b", "a")]
        [TestCase("a10", "b", "a10")]
        public void SubstractOperatorLeftTermTest(string leftTerm, string rightTerm, string expected)
        {
            SubstractOperator mathOperator = new SubstractOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.LeftTerm, Is.EqualTo(expected));
        }

        [TestCase("a", "b", "b")]
        [TestCase("a", "b10", "b10")]
        public void SubstractOperatorRightTermTest(string leftTerm, string rightTerm, string expected)
        {
            SubstractOperator mathOperator = new SubstractOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.RightTerm, Is.EqualTo(expected));
        }

        [TestCase("a", "b", OperationTypes.SUBSTRACT)]
        [TestCase("a", "b", OperationTypes.SUBSTRACT)]
        public void SubstractOperatorOperationTypeTest(string leftTerm, string rightTerm, OperationTypes expected)
        {
            SubstractOperator mathOperator = new SubstractOperator(leftTerm, rightTerm);

            Assert.That(mathOperator.OperationType, Is.EqualTo(expected));
        }

        [TestCase("a", 2, "10", -8)]
        [TestCase("b", 12, "-2", 14)]
        public void SubstractOperatorOperationExecuteTest(string registerName, int regValue, string rightTerm, int expected)
        {
            SubstractOperator mathOperator = new SubstractOperator(registerName, rightTerm);

            Assert.That(mathOperator.Execute(new Dictionary<string, int>() { { registerName, regValue } }), Is.EqualTo(expected));
        }
    }
}
