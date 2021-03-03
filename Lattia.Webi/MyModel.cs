using Lattia.Attributes;
using Lattia.Webi;
using System.Collections.Generic;

namespace Lattia
{
    public class MyModel
    {
        [ReadOnly]
        public Property<string> MyString { get; set; } = Property<string>.Default();

        public Property<int> MyInt { get; set; } = Property<int>.Default();

        public Property<int?> MyOtherInt { get; set; } = Property<int?>.Default();

        public Property<MyNestedModel> MyNested { get; set; } = Property<MyNestedModel>.Default();

        public Property<int[]> MyArr { get; set; } = Property<int[]>.Default();

        public Property<List<MyNestedModel>> MyNesteds { get; set; } = Property<List<MyNestedModel>>.Default();

        public Property<ICollection<MyNestedModel>> MyNesteds2 { get; set; } = Property<ICollection<MyNestedModel>>.Default();
    }
}
