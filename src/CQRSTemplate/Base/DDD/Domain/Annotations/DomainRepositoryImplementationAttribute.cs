using System;

namespace Base.DDD.Domain.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DomainRepositoryImplementationAttribute : Attribute
    {
    }
}