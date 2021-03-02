using Lattia.Contexts;
using Lattia.Setups;
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

        public void InitializePropertyTypeNode(InitializePropertyTypeNodeContext context)
        {
            foreach (var item in items)
            {
                item.InitializePropertyTypeNode(context);
            }
        }
    }
}
