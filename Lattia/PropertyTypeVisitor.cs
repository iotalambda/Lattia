using System;

namespace Lattia
{
    public static class PropertyTypeVisitor
    {
        public static void Traverse(Type type, Action<PropertyTypeNode> evaluate) => Traverse(type, evaluate, null);

        private static void Traverse(Type type, Action<PropertyTypeNode> evaluate, PropertyTypeNode node)
        {
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
                        Traverse(innerType, evaluate, currentNode);
                        break;

                    case SerializablePropertyType.Enumerable:
                        Traverse(innerType.GetEnumerableItemType(), evaluate, currentNode);
                        break;

                    default: throw new NotSupportedException(innerType.FullName);
                }
            }
        }
    }
}
