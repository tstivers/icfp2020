using System;

namespace app.Operations
{
    public interface IToken
    {
        public IToken Apply(IToken token)
        {
            throw new NotImplementedException();
        }

        public bool SkipLeft => false;
        public bool SkipRight => false;
    }
}