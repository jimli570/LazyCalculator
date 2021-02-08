using NUnit.Framework;
using Calculator.command;
using Calculator.command.mathOperator;

namespace UnknownCommandTest
{
    class UnknownCommandTest
    {
        [TestCase("b addd c", "b addd c")]
        [TestCase("b as substract b", "b as substract b")]
        [TestCase("b multiply saf b", "b multiply saf b")]
        public void UnknownCommandCommandTest(string commandline, string expected)
        {
            UnknownCommand command = new UnknownCommand(commandline);

            Assert.That(command.Command, Is.EqualTo(expected));
        }

        [TestCase("b addd c", CommandTypes.UNKNOWN)]
        [TestCase("b as substract b", CommandTypes.UNKNOWN)]
        [TestCase("b multiply saf b", CommandTypes.UNKNOWN)]
        public void UnknownCommandCommandTypeTest(string commandline, CommandTypes expected)
        {
            UnknownCommand command = new UnknownCommand(commandline);

            Assert.That(command.CommandType, Is.EqualTo(expected));
        }
    }
}
