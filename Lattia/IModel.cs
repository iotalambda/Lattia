using Lattia.Json;
using Newtonsoft.Json;

namespace Lattia
{
    [JsonConverter(typeof(ModelJsonConverter))]
    public interface IModel
    {
    }
}
