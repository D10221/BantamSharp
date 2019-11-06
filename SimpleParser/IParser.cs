using System.Collections.Generic;

namespace SimpleParser
{
    public interface IParser<TTokenType>
    {
        ISimpleExpression<TTokenType> Parse(int precedence = 0);        
    }
}