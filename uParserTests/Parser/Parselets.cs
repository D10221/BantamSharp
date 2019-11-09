using System;
using System.Collections.Generic;
using System.Linq;

namespace uParserTests
{
    public delegate ISimpleExpression InfixParselet(Parse parse, IList<Token> lexer, Token token, ISimpleExpression left);
    public delegate ISimpleExpression PrefixParselet(Parse parse, IList<Token> lexer, Token token);

    public static class Parselets
    {
        /// <summary>
        /// Simple parselet for a named variable like "abc"
        /// </summary>
        public static PrefixParselet NameParselet() => (parse, lexer, token) => new NameExpression(token);
        /// <summary>
        ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
        ///     unary "-", "+", "~", and "!" expressions.
        /// </summary>
        public static PrefixParselet PrefixOperatorParselet(int precedence) =>
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.                
            (parse, Lexer, token) => new PrefixExpression(token, right: parse(precedence));
        /// <summary>
        /// TODO
        /// </summary>
        public static PrefixParselet LiteralParselet(TokenType tokenType) =>
            (Parse, Lexer, token) =>
                new LiteralExpression(token);
        /// <summary>
        /// Parses token used to group an expression, like "a * (b + c)".
        /// </summary>
        public static PrefixParselet GroupParselet(TokenType right) =>
            (parse, lexer, token) =>
             {
                 var els = new List<ISimpleExpression>();
                 Token next; // default is EOF
                 while (!lexer.TryPeek(right, out next) && next != default)
                 {
                     els.Add(parse(0));
                 }
                 if (next?.TokenType == right)
                 {
                     lexer.Consume(next);
                 }
                 else
                 {
                     throw new ParseException(
                         $"Expected {right} but found {next?.ToString() ?? "default"}");
                 }
                 if (!els.Any())
                 {
                     throw new ParseException($"{nameof(GroupExpression)} Can't be empty!");
                 }
                 return new GroupExpression(token, els.ToArray());
             };
        /// <summary>
        /// Generic infix parselet for an unary arithmetic operator. Parses postfix
        /// unary "?" expressions.
        /// </summary>
        public static (int, InfixParselet) PostfixOperatorParselet(int precedence)
        {
            InfixParselet parselet = (parse, lexer, token, left) => new PostfixExpression(token, left);
            return (precedence, parselet);
        }
        /// <summary>
        /// TODO
        /// </summary>
        public static (int, InfixParselet) AssignParselet()
        {
            int precedence = (int)uParserTests.Precedence.ASSIGNMENT;
            InfixParselet parselet = (parse, lexer, token, left) =>
            {
                var right = parse(precedence - 1);//Why -1
                if (left as NameExpression == null)
                    throw new ParseException($"Expected {nameof(NameExpression)} but found {left}.");

                return new AssignExpression(left, right);
            };
            return (precedence, parselet);
        }
        /// <summary>
        /// Generic infix parselet for a binary arithmetic operator. The only
        /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
        /// associativity, so we can use a single parselet class for all of those.
        /// </summary>
        public static (int, InfixParselet) BinaryOperatorParselet(int precedence, bool isRight)
        {
            InfixParselet parselet = (parse, lexer, token, left) =>
            {
                // To handle right-associative operators like "^", we allow a slightly
                // lower precedence when parsing the right-hand side. This will let a
                // parselet with the same precedence appear on the right, which will then
                // take *this* parselet's result as its left-hand argument.
                var right = parse(precedence - (isRight ? 1 : 0));
                return new BinaryOperatorExpression(token, left, right);
            };
            return (precedence, parselet);
        }
        /// <summary>
        ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
        /// </summary>
        public static (int, InfixParselet) ConditionalParselet()
        {
            int precedence = (int) Precedence.CONDITIONAL;
            InfixParselet parselet = (parse, lexer, token, left) =>
            {
                var thenArm = parse(0);
                var next = lexer.Consume();
                if (next.TokenType != TokenType.COLON)
                    throw new ParseException("Expected COLON");
                //WHy  precedence -1 
                var elseArm = parse(precedence - 1);
                return new ConditionalExpression(left, thenArm, elseArm);
            };
            return (precedence, parselet);
        }
        /// <summary>
        ///     Parselet to parse a function call like "a(b, c, d)".
        /// </summary>
        public static (int, InfixParselet) CallParselet()
        {
            int precedence = (int) Precedence.CALL;
            var right = TokenType.PARENT_RIGHT;
            InfixParselet parselet = (parse, lexer, token, left) =>
            {
                var args = new List<ISimpleExpression>();
                Token next = default;
                while (!lexer.ConsumeIf(right, out next) && next != default)
                {
                    if (lexer.TryPeek(out next) && next != default && next.TokenType == TokenType.COMMA)
                    {
                        lexer.Consume();
                    }
                    else
                    {
                        args.Add(parse(0));
                    }
                }
                if (next?.TokenType != right)
                {
                    throw new ParseException($"Expected {right} but found {next?.ToString() ?? "Nothing"}");
                }
                return new FunctionCallExpression(left, args);
            };
            return (precedence, parselet);
        }
    }
}
