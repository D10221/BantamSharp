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
        public static PrefixParselet PrefixOperatorParselet(int precedence)
        {
            return (parse, Lexer, token) =>
            {
                // To handle right-associative operators like "^", we allow a slightly
                // lower precedence when parsing the right-hand side. This will let a
                // parselet with the same precedence appear on the right, which will then
                // take *this* parselet's result as its left-hand argument.
                var right = parse(precedence);
                return new PrefixExpression(token, right);
            };
        }
        /// <summary>
        /// Generic infix parselet for an unary arithmetic operator. Parses postfix
        /// unary "?" expressions.
        /// </summary>
        public static (int, InfixParselet) PostfixOperatorParselet(int precedence)
        {
            ISimpleExpression Parse(
            Parse parse,
            IList<Token> lexer,
            Token token, ISimpleExpression left)
            {
                return new PostfixExpression(token, left);
            }
            return (precedence, Parse);

        }
        public static PrefixParselet LiteralParselet(TokenType tokenType)
        {
            ISimpleExpression Parse(Parse parse, IList<Token> lexer, Token token)
            {
                return new LiteralExpression(token);
            }
            return Parse;
        }
        public static (int, InfixParselet) AssignParselet()
        {

            int precedence = (int)uParserTests.Precedence.ASSIGNMENT;

            ISimpleExpression Left;
            ISimpleExpression Right;

            ISimpleExpression Parse(
                    Parse parse,
                    IList<Token> lexer,
                    Token token,
                    ISimpleExpression left)
            {
                //Why -1
                Right = parse(precedence - 1);
                Left = left;
                if (left as NameExpression == null)
                    throw new ParseException($"Expected {nameof(NameExpression)} but found {left}.");

                return new AssignExpression(left, Right);
            }

            return (precedence, Parse);
        }
        /// <summary>
        /// Generic infix parselet for a binary arithmetic operator. The only
        /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
        /// associativity, so we can use a single parselet class for all of those.
        /// </summary>
        public static (int, InfixParselet) BinaryOperatorParselet(int precedence, bool isRight)
        {
            ISimpleExpression Parse(
                Parse parse,
                IList<Token> lexer,
                Token token, ISimpleExpression left)
            {
                // To handle right-associative operators like "^", we allow a slightly
                // lower precedence when parsing the right-hand side. This will let a
                // parselet with the same precedence appear on the right, which will then
                // take *this* parselet's result as its left-hand argument.
                var right = parse(precedence - (isRight ? 1 : 0));
                return new BinaryOperatorExpression(token, left, right);
            }
            return (precedence, Parse);
        }
        /// <summary>
        ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
        /// </summary>
        public static (int, InfixParselet) ConditionalParselet()
        {
            int Precedence = (int)uParserTests.Precedence.CONDITIONAL;

            ISimpleExpression Parse(
                        Parse parse,
                        IList<Token> lexer,
                        Token token,
                        ISimpleExpression left)
            {
                var thenArm = parse(0);
                var next = lexer.Consume();
                if (next.TokenType != TokenType.COLON)
                    throw new ParseException("Expected COLON");
                //WHy  precedence -1 
                var elseArm = parse(Precedence - 1);
                return new ConditionalExpression(left, thenArm, elseArm);
            }
            return (Precedence, Parse);
        }
        /// <summary>
        ///     Parselet to parse a function call like "a(b, c, d)".
        /// </summary>
        public static (int, InfixParselet) CallParselet()
        {
            int precedence = (int)uParserTests.Precedence.CALL;

            var right = TokenType.PARENT_RIGHT;
            ISimpleExpression Parse(
            Parse parse,
            IList<Token> lexer,
            Token token,
            ISimpleExpression left)
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
            }
            return (precedence, Parse);

        }
        /// <summary>
        /// Parses token used to group an expression, like "a * (b + c)".
        /// </summary>
        public static PrefixParselet GroupParselet(TokenType right)
        {
            ISimpleExpression Parse(
                Parse parse,
                IList<Token> lexer,
                Token token
                )
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
            }
            return Parse;
        }
    }
}
