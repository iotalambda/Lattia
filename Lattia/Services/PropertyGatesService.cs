using System.Collections.Generic;

namespace Lattia.Services
{
    public class PropertyGatesService : IPropertyGatesService
    {
        private readonly LattiaSingletonContext context;

        public PropertyGatesService(LattiaSingletonContext context)
        {
            this.context = context;
        }

        public IEnumerable<CheckPropertyGateResult.Error> CheckForErrors(IEnumerable<object> models, params ICheckPropertyGate[] gates)
        {
            var errorResults = new List<CheckPropertyGateResult.Error>();

            foreach (var model in models)
            {
                PropertyValueVisitor.Traverse(model, propertyValue =>
                {
                    var propertyType = context.PropertyTypeNodes[propertyValue.Path];
                    foreach (var gate in gates)
                    {
                        var checkResult = gate.Check(propertyType, propertyValue);
                        if (checkResult is CheckPropertyGateResult.Error errorResult)
                        {
                            errorResults.Add(errorResult);
                        }
                    }
                });
            }

            return errorResults;
        }
    }
}