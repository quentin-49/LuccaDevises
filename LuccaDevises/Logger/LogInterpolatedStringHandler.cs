using System.Runtime.CompilerServices;
using System.Text;

namespace LuccaDevises.Logger
{
    [InterpolatedStringHandler]
    public ref struct LogInterpolatedStringHandler
    {
        // Storage for the built-up string
        readonly StringBuilder builder;

        public LogInterpolatedStringHandler(int literalLength, int formattedCount)
        {
            builder = new StringBuilder(literalLength);
        }

        public void AppendLiteral(string s)
        {
            builder.Append(s);
        }

        public void AppendFormatted<T>(T t)
        {
            builder.Append(t?.ToString());
        }

        internal string GetFormattedText() => builder.ToString();
    }
}
