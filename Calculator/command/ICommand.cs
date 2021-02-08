namespace Calculator.command
{
    public enum CommandTypes
    {
        UNKNOWN,
        MATH,
        MATH_LAZY,
        PRINT
    }

    public interface ICommand<T>
        where T : System.Enum
    {
        string Command { get; }
        T CommandType { get; }
    }
}
