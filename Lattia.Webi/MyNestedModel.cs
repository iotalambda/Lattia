using System;

namespace Lattia.Webi
{
    public class MyNestedModel
    {

        //private Property<int?> Valu = Property<int?>.Default();

        //public Property<int?> MyInt { get { return Valu; } set { Valu = value; } }

        public Property<int?> MyInt { get; set; } = Property<int?>.Default();

        public Property<string> Jotain { get; set; } = Property<string>.Default();
    }
}
