using System.Reflection;

namespace Lattia
{
    public class LattiaModelArgument
    {
        public LattiaModelArgument(object value, ParameterInfo parameterInfo)
        {
            Value = value;
            ParameterInfo = parameterInfo;
        }

        public object Value { get; }
        public ParameterInfo ParameterInfo { get; }
    }
}
