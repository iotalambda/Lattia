﻿using System;

namespace Lattia
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class WriteOnlyAttribute : Attribute
    {
    }
}
