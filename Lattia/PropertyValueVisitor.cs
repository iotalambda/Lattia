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
                if (!Utils.IsLattiaProperty(propertyInfo))
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

                var innerType = propertyInfo.PropertyType.GetGenericArguments()[0];

                if (Utils.IsNonStringEnumerable(innerType))
                {
                    foreach (var item in property.ObjValue as IEnumerable)
                    {
                        Traverse(item, evaluate, currentNode);
                    }
                }
                else if (Utils.IsSimpleType(innerType))
                {
                }
                else if (Utils.IsObjectType(innerType))
                {
                    Traverse(property.ObjValue, evaluate, currentNode);
                }
                else
                {
                    throw new NotImplementedException(innerType.FullName);
                }
            }
        }
    }
}
