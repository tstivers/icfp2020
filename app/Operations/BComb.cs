namespace app.Operations
{
    public class BComb : IApplyable
    {
        public IApplyable Value1 { get; }
        public IApplyable Value2 { get; }

        public BComb()
        { }

        private BComb(IToken value1)
        {
            Value1 = (IApplyable)value1;
        }

        private BComb(IToken value1, IToken value2)
        {
            Value1 = (IApplyable)value1;
            Value2 = (IApplyable)value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new BComb(arg);

            if (Value2 == null)
                return new BComb(Value1, arg);

            var arg1 = Value2.Apply(arg);
            return Value1.Apply(arg1);
        }
    }
}