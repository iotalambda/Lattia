using System.Collections.Generic;

namespace Lattia.Services
{
    public interface ICheckPropertyGatesService
    {
        IEnumerable<CheckPropertyGateResult.Nok> CheckPropertyWriteGates(IEnumerable<object> models);
    }
}
