namespace Lattia
{
    public abstract class CheckPropertyGateResult
    {
        public class Ok : CheckPropertyGateResult
        {
            public static Ok Instance { get; } = new Ok();
        }

        public class Nok : CheckPropertyGateResult
        {
            public class NoPermission : Nok
            {
            }

            public class InvalidRequest : Nok
            {
            }
        }
    }
}
