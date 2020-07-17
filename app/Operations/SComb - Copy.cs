﻿using app.NewFolder;

namespace app.Operations
{
    public class CComb : IApplyable
    {
        public IApplyable Value1 { get; }
        public IApplyable Value2 { get; }

        public CComb()
        { }

        private CComb(IToken value1)
        {
            Value1 = (IApplyable)value1;
        }

        private CComb(IToken value1, IToken value2)
        {
            Value1 = (IApplyable)value1;
            Value2 = (IApplyable)value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new CComb(arg);

            if (Value2 == null)
                return new CComb(Value1, arg);

            var arg1 = Value1.Apply(arg);
            var arg2 = Value2.Apply(arg);

            return (arg1 as IApplyable).Apply(arg2);
        }
    }
}