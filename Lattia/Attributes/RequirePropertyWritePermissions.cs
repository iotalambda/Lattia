using System;
namespace Lattia.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class RequirePropertyWritePermissions : Attribute
    {
    }
}
