using System.Collections;
using System.Collections.Generic;
using Bantam;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class PortedTests
    {
        
        void Setup()
        {
            // Function call.
            _expectationses.AddExpectation("a()", "a()");
            _expectationses.AddExpectation("a(b)", "a(b)");
            _expectationses.AddExpectation("a(b, c)", "a(b, c)");
            //_expectationses.AddExpectation("a(b)(c)", "a(b)(c)");
            _expectationses.AddExpectation("a(b) + c(d)", "(a(b) + c(d))");
            _expectationses.AddExpectation("a(b ? c : d, e + f)", "a((b ? c : d), (e + f))");

            // Unary precedence.
            _expectationses.AddExpectation("~!-+a", "(~(!(-(+a))))");
            _expectationses.AddExpectation("a!!!", "(((a!)!)!)");

            // Unary and binary predecence.
            _expectationses.AddExpectation("-a * b", "((-a) * b)");
            _expectationses.AddExpectation("!a + b", "((!a) + b)");
            _expectationses.AddExpectation("~a ^ b", "((~a) ^ b)");
            _expectationses.AddExpectation("-a!", "(-(a!))");
            _expectationses.AddExpectation("!a!", "(!(a!))");

            // Binary precedence.
            _expectationses.AddExpectation("a = b + c * d ^ e - f / g", "(a = ((b + (c * (d ^ e))) - (f / g)))");

            // Binary associativity.
            _expectationses.AddExpectation("a = b = c", "(a = (b = c))");
            _expectationses.AddExpectation("a + b - c", "((a + b) - c)");
            _expectationses.AddExpectation("a * b / c", "((a * b) / c)");
            _expectationses.AddExpectation("a ^ b ^ c", "(a ^ (b ^ c))");

            // Conditional operator.
            _expectationses.AddExpectation("a ? b : c ? d : e", "(a ? b : (c ? d : e))");
            _expectationses.AddExpectation("a ? b ? c : d : e", "(a ? (b ? c : d) : e)");
            _expectationses.AddExpectation("a + b ? c * d : e / f", "((a + b) ? (c * d) : (e / f))");

            // Grouping.
            _expectationses.AddExpectation("a + (b + c) + d", "((a + (b + c)) + d)");
            _expectationses.AddExpectation("a ^ (b + c)", "(a ^ (b + c))");
            _expectationses.AddExpectation("(!a)!", "((!a)!)");
        }

        private readonly Expectations<string> _expectationses = new Expectations<string>();

        [TestMethod]
        public void TestMethod1()
        {
            Setup();
            foreach (var expectation in _expectationses)
            {
                var source = expectation.Source;
                var expected = expectation.Expected;
                var actual = Parse(source);
                Assert.AreEqual(expected, actual);
            }
    
        }

        private static string Parse(string source)
        {
            var lexer = new Lexer(source);
            Parser parser = new BantamParser(lexer, new ParserMap());

            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.ToString();
            return actual;
        }

    }

    internal class Expectations<T> : IEnumerable<Expectation<T>>
    {
        private readonly IList<Expectation<T>> _expectations= new List<Expectation<T>>();

        public void AddExpectation(T source, T expected)
        {
            _expectations.Add(new Expectation<T>(source, expected));
        }

        public IEnumerator<Expectation<T>> GetEnumerator()
        {
            return _expectations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class Expectation<T>
    {
        public Expectation(T source,T expected)
        {
            Source = source;
            Expected = expected;
        }

        public T Expected { get; private set; }

        public T Source { get; private set; }
    }
}
