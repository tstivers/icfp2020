using System.Collections.Generic;

namespace app.Operations
{
    public class LateBoundToken : IToken
    {
        private string _id;
        private Dictionary<string, IToken> _variables;

        public LateBoundToken(string id)
        {
            _id = id;
        }

        public IToken Bind()
        {
            return _variables[_id];
        }
    }
}