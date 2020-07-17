using System;

namespace app.NewFolder
{
    public interface IToken
    {
        public IToken Eval(IToken arg1)
        {
            throw new InvalidOperationException();
        }

        public IToken Eval(IToken arg1, IToken arg2)
        {
            throw new InvalidOperationException();
        }

        public IToken Eval(IToken arg1, IToken arg2, IToken arg3)
        {
            throw new InvalidOperationException();
        }
    }
}