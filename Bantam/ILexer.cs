namespace Bantam
{
    public interface ILexer
    {
        IToken Next();
        string InputText { get; }
    }
}