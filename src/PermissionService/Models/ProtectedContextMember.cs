namespace PermissionService.Models
{
    public class ProtectedContextMember<T>
    {
        public string PrivilegeName { get; set; }

        public string Description { get; set; }

        public bool AlwaysAllowed { get; set; }

        public bool ContextRequired { get; set; }

        public Requirement<T> Requirement { get; set; }
    }
}
