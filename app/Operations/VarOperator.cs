using System.Collections.Generic;

namespace app.Operations
{
    public class VarOperator : IToken
    {
        public static Dictionary<string, VarOperator> Cache = new Dictionary<string, VarOperator>();

        private readonly string _id;

        public static VarOperator Acquire(string id)
        {
            if (Cache.TryGetValue(id, out var cached))
                return cached;

            var x = new VarOperator(id);
            Cache[id] = x;

            return x;
        }

        private VarOperator(string id)
        {
            _id = id;
        }

        public override string ToString()
        {
            return _id;
        }
    }
}