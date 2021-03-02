using System.Text;

namespace Lattia
{
    public abstract class PropertyNode<TPropertyNode> where TPropertyNode : PropertyNode<TPropertyNode>
    {
        public PropertyNode(TPropertyNode parent, string name)
        {
            Parent = parent;

            Name = name;
        }

        public TPropertyNode Parent { get; }

        public string Name { get; }


        private string path;

        public string Path
        {
            get
            {
                if (path == default)
                {
                    var stringBuilder = new StringBuilder();

                    EvaluatePath(stringBuilder);
                }

                return path;
            }
            set
            {
                path = value;
            }
        }

        internal void EvaluatePath(StringBuilder stringBuilder)
        {
            if (Parent != default)
            {
                Parent.EvaluatePath(stringBuilder);
            }

            stringBuilder.Append(".");

            stringBuilder.Append(Name); // TODO: Json representation

            path = stringBuilder.ToString();
        }
    }
}
