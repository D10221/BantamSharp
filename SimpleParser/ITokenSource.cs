namespace SimpleParser
{
    public interface ITokenSource
    {
        string Value { get; }
        int Line { get;  }
        int Column { get; }
    }
}