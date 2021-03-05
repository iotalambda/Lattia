namespace Lattia
{
    public abstract class CheckPropertyGateResult
    {
        public class Success : CheckPropertyGateResult
        {
            public static Success Instance { get; } = new Success();
        }

        public class Error : CheckPropertyGateResult
        {
        }
    }
}
