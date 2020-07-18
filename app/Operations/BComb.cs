namespace app.Operations
{
    public class BComb : IApplyable
    {
        public IToken Value1 { get; }
        public IToken Value2 { get; }

        public BComb()
        { }

        private BComb(IToken value1)
        {
            Value1 = (IApplyable)value1;
        }

        private BComb(IToken value1, IToken value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new BComb(arg);

            if (Value2 == null)
                return new BComb(Value1, arg);

            var arg1 = ((IApplyable)Value2.Resolve()).Apply(arg.Resolve()).Resolve();
            return ((IApplyable)Value1.Resolve()).Apply(arg1.Resolve()).Resolve();
        }

        public override string ToString()
        {
            return $"b [{Value1?.Resolve()}] [{Value2?.Resolve()}]".TrimEnd();
        }
    }
}