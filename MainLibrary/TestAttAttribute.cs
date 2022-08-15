using System;

namespace MainLibrary
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TestAttAttribute : Attribute
    {
        internal string Name { get; }

        public TestAttAttribute(string name)
        {
            Name = name;
        }
    }
}