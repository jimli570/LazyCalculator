using NUnit.Framework;
using Calculator.command.mathOperator;
using System.Collections.Generic;

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

        [TestCase("a", 2, "0", 2)]
        [TestCase("b", 10, "2", 12)]
        public void AdditionOperatorOperationExecuteTest(string registerName, int regValue, string rightTerm, int expected)
        {
            AdditionOperator mathOperator = new AdditionOperator(registerName, rightTerm);

            Assert.That(mathOperator.Execute( new Dictionary<string, int>() { {registerName, regValue } } ), Is.EqualTo(expected));
        }
    }
}
