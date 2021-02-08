using NUnit.Framework;
using Calculator.command;
using Calculator.command.mathOperator;

namespace PrintCommandTest
{
    class PrintCommandTest
    {
        [TestCase("print a", "print a")]
        [TestCase("print abc", "print abc")]
        [TestCase("print X", "print X")]
        public void PrintCommandCommandTest(string commandline, string expected)
        {
            PrintCommand command = new PrintCommand(commandline);

            Assert.That(command.Command, Is.EqualTo(expected));
        }

        [TestCase("print a", CommandTypes.PRINT)]
        [TestCase("print abc", CommandTypes.PRINT)]
        [TestCase("print x", CommandTypes.PRINT)]
        public void PrintCommandCommandTypeTest(string commandline, CommandTypes expected)
        {
            PrintCommand command = new PrintCommand(commandline);

            Assert.That(command.CommandType, Is.EqualTo(expected));
        }

        [TestCase("print a", "a")]
        [TestCase("print abc", "abc")]
        [TestCase("print x", "x")]
        public void PrintCommandTermTest(string commandline, string expected)
        {
            PrintCommand command = new PrintCommand(commandline);

            Assert.That(command.Term, Is.EqualTo(expected));
        }
    }
}
