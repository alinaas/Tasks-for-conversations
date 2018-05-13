namespace AppLogic.Garbage
{
    public class Exhaust
    {
        public bool IsSuccess { get; set; }
        public ExhaustComponent[] Components { get; set; }
    }

    public class ExhaustComponent
    {
        public string Formula { get; set; }

        public int Volume { get; set; }
    }
}
