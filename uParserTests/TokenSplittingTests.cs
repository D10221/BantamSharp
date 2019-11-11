using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TokenSplitting;

namespace TokenSplittingTests
{
    using static Assert;

    [TestClass]
    public class TokenSplittingTests
    {
        TokenSplitter spliter => new TokenSplitter(
            Concat(
                grouping,
                delimiters,
                logical,
                binary,
                quotes,
                comments
            ).ToArray()
        );

        IList<TokenSource> split(string x) => spliter.Split(x);

        [TestMethod]
        public void TokenSplitterTest()
        {
            var values = Values(split(@"
1
2
3
            "));
            AreEqual("123", Joined(values));
        }
        [TestMethod]
        public void AcceptsEmptyString()
        {
            AreEqual("", Joined(Values(split(""))));
        }
        ///<summary>
        /// Contradicts NoDelimiterPriority choose one
        ///</summary>
        [TestMethod]
        public void DelimiterPriority()
        {
            // may not be a good idea parsing instead of splitting
            // can be soved using the parse use 'names' as values in a group expression
            // but spaces have to be preserved
            AreEqual("[A B]", Joined(Values(split("[A B]"))));
        }
        ///<summary>
        /// Contradicts DelimiterPriority choose one
        ///</summary>
        [TestMethod]
        public void NoDelimiterPriority()
        {
            // may not be a good idea parsing instead of splitting
            // can be soved using the parse use 'names' as values in a group expression
            // but spaces have to be preserved
            AreEqual("[,A,B,]", Joined(Values(split("[A B]")), ","));
        }
        [TestMethod]
        public void CompositePostFix()
        {
            // use '+=' as  1 token or let the parser figure out +=            
            AreEqual("i,+,=,1",
                Joined(Values(split("i+=1")), ","));
        }
        [TestMethod]
        public void BinaryVsLogical()
        {
            // & vs && ...and || vd |
            AreEqual("a,|,1,%%,b,&,2",
                Joined(Values(split(
                    "a | 1 %% b & 2"
                    )), ","));
        }
        [TestMethod]
        public void Comments()
        {
            AreEqual("--,a,/*,?,*/,//,b,<!-,x,->",
                Joined(Values(split(
                    "--a /*?*/ \r //b <!-x->"
                    )), ","));
        }
        IEnumerable<string> Values(IList<TokenSource> input)
        {
            return input.Select(x => x.Value);
        }
        string Joined(IEnumerable<string> input, string separator = "")
        {
            return input.Aggregate((a, b) => a + separator + b);
        }
        IList<T> Concat<T>(IList<T> a, params IList<T>[] param)
        {
            IList<T> output = new List<T>();
            foreach (var i in param)
            {
                foreach (var x in i)
                {
                    output.Add(x);
                }
            }
            return output;
        }
        //string[] EOL = new[] { "\n\r", "\n", "\r" };
        //Regex Space = new Regex(@"\s+");

        string[] grouping = {
                "(",")",
                "[", "]",
                "<",">",
                "{","}"
        };
        // special characters        
        string[] delimiters = {
                  ",",";",":",
                  "=",
                  "+", "-",
                  "*","/",
                  "~", "%",
                  "!",
                  "?",":",
                  "#","@","$",
                  "."
            };
        string[] logical = {
                  "&&", "||"
        };
        string[] binary = {
                "^","&","|",
            };
        string[] quotes = {
            "\"","'","`",
        };
        // too many choose 1 style of comments
        // they are compose of primitive symbols anyway
        // parse or split ? 
        // prefixes
        string[] comments = {
            "--",
            "/*","*/",
            "<!-", "->",
            "//"
        };
    }
}