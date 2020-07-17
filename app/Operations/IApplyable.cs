using app.NewFolder;
using System;

namespace app.Operations
{
    public interface IApplyable : IToken
    {
        public IToken Apply(IToken value)
        {
            throw new NotImplementedException();
        }
    }
}