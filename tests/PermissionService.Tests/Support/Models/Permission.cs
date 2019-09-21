using System;

namespace PermissionService.Tests.Support.Models
{
    public class Permission : IEquatable<Permission>
    {
        public string UserId { get; set; }

        public string Value { get; set; }

        public bool Equals(Permission other)
        {
            return Value.Equals(other.Value);
        }
    }
}
