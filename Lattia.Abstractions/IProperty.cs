namespace Lattia.Abstractions
{
    public interface IProperty<T>
    {
        bool HasValue { get; set; }

        T Value { get; set; }
    }
}
