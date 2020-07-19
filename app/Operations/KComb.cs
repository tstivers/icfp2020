using System.Collections.Generic;

namespace app.Operations
{
    public class KComb : IToken
    {
        private static KComb _empty = new KComb();
        public static Dictionary<IToken, KComb> Cache = new Dictionary<IToken, KComb>();

        public static KComb Acquire()
        {
            return _empty;
        }

        private static KComb Acquire(IToken arg1)
        {
            if (Cache.TryGetValue(arg1, out var cached))
                return cached;

            var x = new KComb(arg1);
            Cache[arg1] = x;

            return x;
        }

        private IToken x0 { get; set; }

        private KComb()
        {
        }

        public bool SkipRight => x0 != null;

        private KComb(IToken value)
        {
            x0 = value;
        }

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return Acquire(arg);

            return x0;
        }

        public override string ToString()
        {
            return $"t [{x0}]";
        }
    }
}