using System;
using System.Collections.Generic;

namespace Lattia.Services
{
    public interface IPropertyGatesService
    {
        IEnumerable<CheckPropertyGateResult.Error> CheckForErrors(IEnumerable<object> models, params ICheckPropertyGate[] gates);
    }
}
