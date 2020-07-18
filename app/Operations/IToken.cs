using System;

namespace app.Operations
{
    public interface IToken
    {
        public IToken Resolve()
        {
            return this;
        }

        public IToken Apply(IToken token)
        {
            throw new NotImplementedException();
        }

        public IToken Reduce()
        {
            Console.WriteLine(this);
            return this;
        }

        public bool SkipLeft => false;
        public bool SkipRight => false;
    }
}