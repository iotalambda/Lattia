using System.Collections.Generic;
using System.Linq;

namespace Lattia
{
    public class LattiaContext
    {
        public Dictionary<string, PropertyTypeNode> PropertyTypeNodes { get; set; } = new Dictionary<string, PropertyTypeNode>();

        public Dictionary<string, IEnumerable<string>> ModelTypeFullNameToPropertyPaths { get; set; } = new Dictionary<string, IEnumerable<string>>();
    }
}
