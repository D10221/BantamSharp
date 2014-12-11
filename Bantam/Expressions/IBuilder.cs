namespace Bantam.Expressions
{
    public interface IBuilder
    {
        IBuilder Append(string s);
        IBuilder Append(char c);
    }
}