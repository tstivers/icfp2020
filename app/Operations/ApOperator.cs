namespace app.Operations
{
    public class ApOperator : IToken
    {
        public IToken f { get; set; }
        public IToken x { get; set; }

        public ApOperator(IToken ff, IToken fx)
        {
            f = ff;
            x = fx;
        }

        public ApOperator()
        { }

        public override string ToString()
        {
            return $"ap [{f}] [{x}]";
        }
    }
}