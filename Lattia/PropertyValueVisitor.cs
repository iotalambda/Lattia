using Lattia.Utils;
using System;
using System.Collections;

namespace Lattia
{
    public static class PropertyValueVisitor
    {
        public static void Traverse(object model, Action<PropertyValueNode> evaluate) => Traverse(model, evaluate, null);

        private static void Traverse(object model, Action<PropertyValueNode> evaluate, PropertyValueNode node)
        {
            if (model == default)
            {
                return;
            }

            var type = model.GetType();

            var propertyInfos = type.GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                if (!Helpers.IsLattiaProperty(propertyInfo))
                {
                    continue;
                }

                var property = propertyInfo.GetValue(model) as Property;

                if (!property.HasValue)
                {
                    continue;
                }

                var currentNode = new PropertyValueNode(node, property, propertyInfo);

                evaluate(currentNode);

                if (!property.HasValue)
                {
                    continue;
                }

                var innerType = propertyInfo.GetGenericArgument();

                switch (innerType.ResolveSerializablePropertyType())
                {
                    case SerializablePropertyType.Simple:
                        break;

                    case SerializablePropertyType.Object:
                        Traverse(property.ObjValue, evaluate, currentNode);
                        break;

                    case SerializablePropertyType.Enumerable:
                        foreach (var item in property.ObjValue as IEnumerable)
                        {
                            Traverse(item, evaluate, currentNode);
                        }
                        break;

                    default: throw new NotSupportedException(innerType.FullName);
                }
            }
        }
    }
}
