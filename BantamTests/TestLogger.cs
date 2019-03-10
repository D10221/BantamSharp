/**
 * see: https://github.com/Microsoft/vstest/issues/799
 */
namespace BantamTests
{
    ///<summary>
    ///  Does it work on Windows?
    ///</summary>
    public static class TestLogger
    {
        public static void Log(string message)
        {
            using (var writer = new System.IO.StreamWriter(System.Console.OpenStandardOutput()))
                writer.WriteLine(message);
        }
    }
}