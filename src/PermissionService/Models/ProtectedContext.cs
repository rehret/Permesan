using System.Collections.Generic;

namespace PermissionService.Models
{
    public class ProtectedContext<T>
    {
        public string Name { get; set; }

        public IEnumerable<ProtectedContextMember<T>> Actions { get; set; }

        public IEnumerable<ProtectedContextMember<T>> Properties { get; set; }
    }
}
