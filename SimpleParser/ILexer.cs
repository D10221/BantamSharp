namespace SimpleParser
{
    public interface ILexer
    {
        IToken Next();
        string InputText { get; }
    }
}