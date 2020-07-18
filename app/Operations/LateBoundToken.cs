using System.Collections.Generic;

namespace app.Operations
{
    public class LateBoundToken : IToken
    {
        private string _id;
        private Dictionary<string, IToken> _variables;

        public LateBoundToken(string id, Dictionary<string, IToken> variables)
        {
            _id = id;
            _variables = variables;
        }

        public IToken Resolve()
        {
            return _variables[_id];
        }
    }
}