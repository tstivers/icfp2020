namespace app.Operations
{
    public class CComb : IApplyable
    {
        public IToken Value1 { get; }
        public IToken Value2 { get; }

        public CComb()
        { }

        private CComb(IToken value1)
        {
            Value1 = value1;
        }

        private CComb(IToken value1, IToken value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new CComb(arg);

            if (Value2 == null)
                return new CComb(Value1, arg);

            var arg1 = ((IApplyable)Value1.Resolve()).Apply(arg.Resolve()).Resolve() as IApplyable;
            return arg1.Apply(Value2.Resolve()).Resolve();
        }

        public override string ToString()
        {
            return $"c [{Value1?.Resolve()}] [{Value2?.Resolve()}]".TrimEnd();
        }
    }
}