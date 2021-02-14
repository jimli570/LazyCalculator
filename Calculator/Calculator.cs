using System;
using System.Collections.Generic;
using Calculator.command;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Calculator : ICalculator
    {
        private command.identifier.ICommandIdentifier<CommandTypes> m_commandIdentifier;

        private Dictionary<string, int> m_register;
        private List<string> m_lazyRegisters; // Registers to be evaluated at print
        private List<ICommand<CommandTypes>> m_lazyCommands; // Commands to be evaluated at print

        public List<string> PrintLog { get; private set; } // Output from print

        public Calculator()
        {
            m_register = new Dictionary<string, int>();
            m_lazyRegisters = new List<string>(); // Commands to be evaluated at prints
            m_commandIdentifier = new command.identifier.CalcCommandIdentifier();

            m_lazyCommands = new List<ICommand<CommandTypes>>(); // Commands to be evaluated at prints

            PrintLog = new List<string>();
        }

        public void Command(string commandline)
        {
            commandline = TrimCommandLine(commandline);
            CommandTypes commandType = m_commandIdentifier.Identify(commandline);
            ICommand<CommandTypes> command = CommandHelper.CreateCommand(commandType, commandline);

            DoAction(command, commandType);
        }

        private void DoAction(command.ICommand<CommandTypes> command, CommandTypes commandType)
        {
            switch (command.CommandType)
            {
                case CommandTypes.MATH_ADD:
                    MathAddAction((MathAddCommand)command);
                    break;

                case CommandTypes.MATH_MULT:
                    MathMultAction((MathMultCommand)command);
                    break;

                case CommandTypes.MATH_SUB:
                    MathSubAction((MathSubCommand)command);
                    break;

                case CommandTypes.PRINT:
                    PrintAction((PrintCommand)command);
                    break;

                default: // CommandTypes.UNKNOWN
                    UnknownAction((UnknownCommand)command);
                    break;
            }
        }

        private void MathAddAction(MathAddCommand command)
        {
            bool isEvaluated = m_register.ContainsKey(command.RegisterName);
            bool needLazyEval = CommandHelper.NeedsLazyEval(command.Value, m_lazyRegisters, m_register);

            if (needLazyEval && !isEvaluated) {
                m_lazyCommands.Add(command);

                if (!m_lazyRegisters.Contains(command.RegisterName)) {
                    m_lazyRegisters.Add(command.RegisterName);
                }
            }
            else {
                // Update register with result
                m_register = command.Execute(m_register);
            }
        }

        private void MathMultAction(MathMultCommand command)
        {
            bool isEvaluated = m_register.ContainsKey(command.RegisterName);
            bool needLazyEval = CommandHelper.NeedsLazyEval(command.Value, m_lazyRegisters, m_register);

            if (needLazyEval && !isEvaluated) {
                m_lazyCommands.Add(command);

                if (!m_lazyRegisters.Contains(command.RegisterName)) {
                    m_lazyRegisters.Add(command.RegisterName);
                }
            }
            else {
                // Update register with result
                m_register = command.Execute(m_register);
            }
        }

        private void MathSubAction(MathSubCommand command)
        {
            bool isEvaluated = m_register.ContainsKey(command.RegisterName);
            bool needLazyEval = CommandHelper.NeedsLazyEval(command.Value, m_lazyRegisters, m_register);

            if (needLazyEval && !isEvaluated) {
                m_lazyCommands.Add(command);

                if (!m_lazyRegisters.Contains(command.RegisterName)) {
                    m_lazyRegisters.Add(command.RegisterName);
                }
            }
            else {
                // Update register with result
                m_register = command.Execute(m_register);
            }
        }

        private void PrintAction(PrintCommand command)
        {
            bool needToEval = !m_register.ContainsKey(command.RegisterName);

            // Evaluate if needed
            bool readyToPrint = needToEval ? EvaluteTerm(command.RegisterName, 0) : true;

            string printValue = readyToPrint ?
                m_register[command.RegisterName].ToString() :
                ("Failed to evaluate: " + command.RegisterName);

            Console.WriteLine(printValue);
            PrintLog.Add(printValue);
        }

        private void UnknownAction(UnknownCommand command)
        {
            Console.WriteLine("Unknown action: " + command.Command);
        }

        private bool EvaluteTerm(string term, int depth)
        {
            const int maxDepth = 50;
            if (depth > maxDepth) {
                return false;
            }

            // Find the all the commands using the term on the left-hand side that are flagged for lazy-eval
            List<ICommand<CommandTypes>> cmdsToEval = m_lazyCommands.FindAll(delegate (ICommand<CommandTypes> command)
            {
                return (command.RegisterName == term);
            });

            // Find all dependent terms that need evaluation
            List<string> dependentTermsToEval = new List<string>();
            foreach (ICommand<CommandTypes> command in cmdsToEval) {
                // If true then the term is dependent and needs to be evaluilated
                if (m_lazyRegisters.Contains(command.Value)) {
                    dependentTermsToEval.Add(command.Value);
                }
            }
            
            // Evaluate the right-hand side terms first
            foreach (string termToEval in dependentTermsToEval) {
                if(!EvaluteTerm(termToEval, ++depth)) {
                    return false;
                }
            }

            // At this point all the dependecis are calculated
            // So we can now calculate the value that originally was asked for
            foreach (ICommand<CommandTypes> command in cmdsToEval) {
                m_register = command.Execute(m_register);
            }

            // Cleanup & remove
            m_lazyCommands.RemoveAll(command => command.RegisterName == term);
            m_lazyRegisters.RemoveAll(stringValue => stringValue == term);

            return true;
        }

        private string TrimCommandLine(string commandline)
        {
            commandline = commandline.ToLower(); // We want it case insensitive
            commandline = Regex.Replace(commandline, @"\A\s+", ""); // Remove leading whitespaces
            commandline = Regex.Replace(commandline, @"\s+\Z", ""); // Remove trailing whitespaces
            commandline = Regex.Replace(commandline, @"\s+", " "); // Remove pontential extra spaces in between words/commands/values

            return commandline;
        }
    }
}
