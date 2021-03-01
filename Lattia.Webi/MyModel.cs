using Lattia.Attributes;
using Lattia.Webi;

namespace Lattia
{
    public class MyModel
    {
        [ReadOnly]
        public Property<string> MyString { get; set; } = Property<string>.Default();

        public Property<int> MyInt { get; set; } = Property<int>.Default();

        public Property<MyNestedModel> MyNested { get; set; } = Property<MyNestedModel>.Default();
    }
}
