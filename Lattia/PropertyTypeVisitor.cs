using System;

namespace Lattia
{
    public static class PropertyTypeVisitor
    {
        public static int MaxDepth = 500;

        public static void Traverse(Type type, Action<PropertyTypeNode> evaluate) => Traverse(type, evaluate, null, 0);

        private static void Traverse(Type type, Action<PropertyTypeNode> evaluate, PropertyTypeNode node, int depth)
        {
            depth++;

            if (depth > MaxDepth)
            {
                return;
            }

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (!Utils.IsLattiaProperty(property))
                {
                    continue;
                }

                var currentNode = new PropertyTypeNode(node, property);

                evaluate(currentNode);

                var innerType = property.GetGenericArgument();

                switch (property.GetGenericArgument().ResolveSerializablePropertyType())
                {
                    case SerializablePropertyType.Simple:
                        break;

                    case SerializablePropertyType.Object:
                        Traverse(innerType, evaluate, currentNode, depth);
                        break;

                    case SerializablePropertyType.Enumerable:
                        Traverse(innerType.GetEnumerableItemType(), evaluate, currentNode, depth);
                        break;

                    default: throw new NotSupportedException(innerType.FullName);
                }
            }

            depth--;
        }
    }
}
