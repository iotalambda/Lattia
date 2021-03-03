using Lattia.Webi;
using System.Collections.Generic;

namespace Lattia
{
    public class MyEntity
    {
        public string MyString { get; set; }

        public MyNestedEntity MyNested { get; set; }

        public int[] MyArr { get; set; }

        public List<MyNestedEntity> MyNesteds { get; set; }

        public int? MyOtherInt { get; set; }
    }
}
