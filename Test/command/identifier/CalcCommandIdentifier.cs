using NUnit.Framework;
using Calculator.command;
using Calculator.command.identifier;
using System.Collections.Generic;

namespace CalcCommandIdentifierTest
{
    class CalcCommandIdentifierTest
    {
        private ICommandIdentifier m_commandIdentifier;
        
        [SetUp]
        public void Setup()
        {
            m_commandIdentifier = new CalcCommandIdentifier();
        }

        [TestCase("print a", new[] {""}, CommandTypes.PRINT)]
        [TestCase("priNT a", new[] { "" }, CommandTypes.PRINT)]
        [TestCase("b multiply 10", new[] { "" }, CommandTypes.MATH)]
        [TestCase("b multiPLY 10", new[] { "" }, CommandTypes.MATH)]
        [TestCase("b substract c", new[] { "c" }, CommandTypes.MATH_LAZY)]
        [TestCase("B SUBSTRACT c", new[] { "c" }, CommandTypes.MATH_LAZY)]
        [TestCase("B monkey c", new[] { "" }, CommandTypes.UNKNOWN)]
        [TestCase("chicken elephant", new[] { "" }, CommandTypes.UNKNOWN)]
        [TestCase("chicken elephant elephant elephant", new[] { "" }, CommandTypes.UNKNOWN)]
        public void IdentifyTest(string commandline, string[] lazyRegister, CommandTypes expected )
        {
            List<string> lazyRegisterList = new List<string>(lazyRegister);

            ICommand<CommandTypes> command = m_commandIdentifier.Identify(commandline, lazyRegisterList);
            Assert.That(command.CommandType, Is.EqualTo(expected));
        }
    }
}
