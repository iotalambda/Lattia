using System.Collections.Generic;

namespace Lattia.Pipelines
{
    public class InitializePropertyTypeNodePipeline : IInitializePropertyTypeNodePipeline
    {
        private readonly IEnumerable<IInitializePropertyTypeNode> items;

        public InitializePropertyTypeNodePipeline(IEnumerable<IInitializePropertyTypeNode> items)
        {
            this.items = items;
        }

        public void InitializePropertyTypeNode(PropertyTypeNode propertyType)
        {
            foreach (var item in items)
            {
                item.Initialize(propertyType);
            }
        }
    }
}
