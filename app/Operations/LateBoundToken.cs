using System.Collections.Generic;

namespace app.Operations
{
    public class LateBoundToken : IToken
    {
        public string Id;
        public Dictionary<string, IToken> Variables;

        public LateBoundToken(string id, Dictionary<string, IToken> variables)
        {
            Id = id;
            Variables = variables;
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}