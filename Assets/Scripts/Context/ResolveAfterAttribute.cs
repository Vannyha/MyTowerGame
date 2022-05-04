using System;

namespace Context
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResolveAfterAttribute : Attribute
    {
        
        public Type[] Dependencies { get; }

        public ResolveAfterAttribute(params Type[] dependencies)
        {
            this.Dependencies = dependencies;
        }
    }
}