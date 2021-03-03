namespace Lattia.Webi
{
    public class MyNestedEntity
    {
        public int? MyInt { get; set; }

        public string Jotain { get; set; }

        public MyNestedEntity Nested { get; set; }
    }
}
