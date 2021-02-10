using NUnit.Framework;
using System.Collections.Generic;
using Calculator.fileReader;

namespace CalculatorTest
{
    class CalculatorTest
    {
        [TestCase( new[] { "A add 2", "A add 3", "print A", "B add 5", "B substract 2", "print B", "A add 1", "print A" }, new[] { 5, 3, 6 })]
        [TestCase( new[] { "a add 10", "b add a", "b add 1", "print b" }, new[] { 1 })]
        [TestCase( new[] { "result add revenue", "result substract costs", "revenue add 200",
            "costs add salaries", "salaries add 20", "salaries multiply 5", "costs add 10", "print result" }, new[] { 90 })]
        public void CalculatorOutputTest(string[] input, int[] expected)
        {
            Calculator.Calculator calculator = new Calculator.Calculator();

            List<string> inputList = new List<string>(input);
            List<int> expectedList = new List<int>(expected);

            // Send each line of potential commands to calculator
            foreach (string commandline in inputList) {
                calculator.Command(commandline);
            }

            Assert.That(calculator.PrintLog, Is.EqualTo(expectedList));
        }
    }
}
