using System;
using System.Collections.Generic;
using Calculator.command;

namespace Calculator
{
    public class Calculator: ICalculator
    {
        private command.identifier.ICommandIdentifier m_commandIdentifier;
        private List<MathLazyCommand> m_lazyCommands; // Commands to be evaluated at print

        private Dictionary<string, int> m_valueRegister;
        private List<string> m_lazyRegister; // Names on registers that are to be lazy evaluated

        public List<int> PrintValues { get; private set; } // Output from print

        public Calculator()
        {
            m_commandIdentifier = new command.identifier.CalcCommandIdentifier();
            m_lazyCommands = new List<MathLazyCommand>(); // Commands to be evaluated at prints

            m_valueRegister = new Dictionary<string, int>();
            m_lazyRegister = new List<string>(); // Commands to be evaluated at print

            PrintValues = new List<int>();
    }

        public void Command(string commandLine)
        {
            command.ICommand<CommandTypes> command = m_commandIdentifier.Identify(commandLine, 
                m_lazyRegister);
            DoAction( command );
        }

        private void DoAction( command.ICommand<CommandTypes> command )
        {
            switch ( command.CommandType )
            {
                case CommandTypes.MATH:
                    MathAction( (MathCommand)command );
                    break;

                case CommandTypes.MATH_LAZY:
                    MathLazyAction( (MathLazyCommand)command );
                    break;

                case CommandTypes.PRINT:
                    PrintAction( (PrintCommand)command );
                    break;

                default: // CommandTypes.UNKNOWN
                    UnknownAction( (UnknownCommand)command );
                    break;
            }
        }

        private void MathAction( MathCommand command )
        {
            string registerKey = command.MathOperator.LeftTerm;
            bool containsKey = m_valueRegister.ContainsKey(registerKey);
            int registerValue = containsKey ? m_valueRegister[registerKey] : 0; // If not in register, start with zero

            bool rightTermIsNum = MathCommandHelper.IsNumber(command.MathOperator.RightTerm);
            int value = rightTermIsNum ?
                int.Parse(command.MathOperator.RightTerm) : m_valueRegister[command.MathOperator.RightTerm];

            // Calculate new value & update register
            registerValue = command.MathOperator.Execute( registerValue, value );
            m_valueRegister[registerKey] = registerValue;
        }

        private void MathLazyAction( MathLazyCommand command )
        {
            m_lazyCommands.Add( command );

            // If not already therem add to the list registers to be evaluated
            if (!m_lazyRegister.Contains(command.MathOperator.LeftTerm)) {
                m_lazyRegister.Add( command.MathOperator.LeftTerm );
            }
        }

        private void UnknownAction(UnknownCommand command)
        {
            Console.WriteLine("Unknown action: " + command.Command);
        }

        private void PrintAction(PrintCommand command)
        {
            int result;

            // If true, no need to evaluate values
            if ( m_valueRegister.ContainsKey(command.Term) ) {
                result = m_valueRegister[ command.Term ];
            }
            else  { // We need to lazy evaluate
                EvaluteTerm( command.Term );
                result = m_valueRegister[ command.Term ];
            }

            PrintValues.Add(result);
            Console.WriteLine(result);
        }

        private void EvaluteTerm(string term)
        {
            // Find the all the commands using the term on the left-hand side
            List<MathLazyCommand> cmdsToEval = m_lazyCommands.FindAll(delegate (MathLazyCommand command) {
                return (command.MathOperator.LeftTerm == term);
            });

            // Find all dependent terms that need evaluation
            List<string> dependentTermsToEval = new List<string>();
            foreach (MathLazyCommand command in cmdsToEval) {
                // If true then the term is dependent and needs to be evaluilated
                if ( m_lazyRegister.Contains(command.MathOperator.RightTerm) ) {
                    dependentTermsToEval.Add(command.MathOperator.RightTerm);
                }
            }

            // Evaluate the right-hand side terms first
            foreach (string termToEval in dependentTermsToEval) {
                EvaluteTerm( termToEval );
            }

            // At this point all the dependecis are calculated
            // So we can now calculate the value that originally was asked for
            foreach (MathLazyCommand command in cmdsToEval)
            {
                int leftTermValue = m_valueRegister.ContainsKey(term) ?
                    m_valueRegister[term] : 0; // If nothing said before, we start with 0
                int rightTermValue = m_valueRegister[ command.MathOperator.RightTerm ];

                m_valueRegister[ term ] = command.MathOperator.Execute(leftTermValue, rightTermValue);
            }

            // Cleanup & remove
            m_lazyCommands.RemoveAll( command => command.MathOperator.LeftTerm == term );
            m_lazyRegister.RemoveAll( stringValue => stringValue == term );
        }
    }
}
