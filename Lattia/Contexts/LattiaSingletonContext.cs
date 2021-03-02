using System.Collections.Generic;

namespace Lattia.Contexts
{
    public class LattiaSingletonContext
    {
        public Dictionary<string, PropertyTypeNode> PropertyTypeNodes { get; set; } = new Dictionary<string, PropertyTypeNode>();

        public Dictionary<string, IEnumerable<string>> ModelTypeFullNameToPropertyPaths { get; set; } = new Dictionary<string, IEnumerable<string>>();
    }
}
