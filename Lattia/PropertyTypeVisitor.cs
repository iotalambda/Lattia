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

                var innerType = property.PropertyType.GetGenericArguments()[0];

                if (Utils.IsNonStringEnumerable(innerType))
                {
                    Traverse(innerType.GetGenericArguments()[0], evaluate, currentNode);
                }
                else if (Utils.IsSimpleType(innerType))
                {
                }
                else if (Utils.IsObjectType(innerType))
                {
                    Traverse(innerType, evaluate, currentNode);
                }
                else
                {
                    throw new NotImplementedException(innerType.FullName);
                }
            }
        }
    }
}
