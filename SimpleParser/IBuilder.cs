using System.Collections.Generic;

namespace SimpleParser
{
    public interface IBuilder<TCHAR>
    {
        IBuilder<TCHAR> Append(IEnumerable<TCHAR> cs);
        IBuilder<TCHAR> Append(TCHAR c);
        string Build();
    }
}