using System.Collections.Generic;

namespace PermissionService.Models
{
    public class Requirement<T>
    {
        public RequirementType Type { get; set; }

        public IEnumerable<Requirement<T>> SubRequirements { get; set; }

        public T Value { get; set; }
    }

    public enum RequirementType
    {
        AllOf,
        OneOf
    }
}
