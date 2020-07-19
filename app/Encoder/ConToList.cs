using app.Operations;
using System.Text;

namespace app.Encoder
{
    public static class ConToList
    {
        public enum ConsState
        {
            none,
            inlist
        }

        public static StringBuilder ToReadableList(this IToken token, StringBuilder sb = null, ConsState state = ConsState.none)
        {
            if (sb == null)
                sb = new StringBuilder();

            if (token is ConsOperator cons)
            {
                if (state == ConsState.none)
                {
                    state = ConsState.inlist;
                    sb.Append("(");
                    cons.x1.ToReadableList(sb, state);
                }
            }

            return sb;
        }
    }
}