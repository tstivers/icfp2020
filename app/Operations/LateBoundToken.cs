using System.Collections.Generic;

namespace app.Operations
{
    public class LateBoundToken : IToken
    {
        public static Dictionary<string, LateBoundToken> Cache = new Dictionary<string, LateBoundToken>();

        public readonly string Id;

        public static LateBoundToken Acquire(string id)
        {
            if (Cache.TryGetValue(id, out var cached))
                return cached;

            var x = new LateBoundToken(id);
            Cache[id] = x;

            return x;
        }

        private LateBoundToken(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id;
        }
    }
}