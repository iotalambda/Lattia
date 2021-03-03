using System.Collections.Generic;

namespace Lattia
{
    public interface IReducer<T>
    {
        T Reduce(IEnumerable<T> source);
    }
}
