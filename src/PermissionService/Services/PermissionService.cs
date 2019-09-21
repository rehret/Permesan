using System.Linq;
using System;
using System.Collections.Generic;
using PermissionService.Interfaces;
using PermissionService.Models;
using System.Threading.Tasks;

namespace PermissionService.Services
{
    public abstract class PermissionService<TUser, TPermission> : IPermissionService<TUser, TPermission> where TPermission : IEquatable<TPermission>
    {
        protected IDictionary<string, ProtectedContextMember<TPermission>> Protections { get; set; }

        public PermissionService(IDictionary<string, ProtectedContextMember<TPermission>> protections)
        {
            Protections = protections;
        }

        public PermissionService(IEnumerable<ProtectedContextMember<TPermission>> protections)
            : this(protections.ToDictionary((protection) => protection.PrivilegeName, (protection) => protection)) { }

        public PermissionService(IEnumerable<ProtectedContext<TPermission>> protectedContexts)
            : this(protectedContexts
                .SelectMany((context) => context.Actions ?? new List<ProtectedContextMember<TPermission>>())
                .Union(protectedContexts.SelectMany((context) => context.Properties ?? new List<ProtectedContextMember<TPermission>>()))) { }

        public async Task<bool> HasAccess<TContext>(TUser user, string privilegeName, TContext context) where TContext : new()
        {
            if (Protections.TryGetValue(privilegeName, out var privilege))
            {
                if (privilege.AlwaysAllowed)
                {
                    return true;
                }

                if (privilege.ContextRequired)
                {
                    if (context == null)
                    {
                        throw new ArgumentException($"Privilege \"{privilegeName}\" requires a context but the provided context is null");
                    }

                    return await HasAccessForContext(user, privilege, context);
                }
                else
                {
                    return MeetsRequirement(privilege.Requirement, await GetUserPermissions(user));
                }
            }
            else
            {
                throw new KeyNotFoundException($"Cannot find privilege with name \"{privilegeName}\"");
            }
        }

        protected virtual Task<IEnumerable<TPermission>> GetUserPermissions(TUser user)
        {
            throw new NotImplementedException();
        }

        protected virtual Task<bool> HasAccessForContext<TContext>(TUser user, ProtectedContextMember<TPermission> privilege, TContext context)
        {
            throw new NotImplementedException();
        }

        private bool MeetsRequirement(Requirement<TPermission> requirement, IEnumerable<TPermission> userPermissions)
        {
            var meetsRequirement = true;
            var meetsSubRequirements = true;

            if (requirement.Value != null)
            {
                meetsRequirement = userPermissions.Any((permission) => permission.Equals(requirement.Value));
            }

            if (requirement.SubRequirements != null && requirement.SubRequirements.Count() > 0)
            {
                if (requirement.Type == RequirementType.AllOf)
                {
                    meetsSubRequirements = requirement.SubRequirements.All((subRequirement) => MeetsRequirement(subRequirement, userPermissions));
                }
                else if (requirement.Type == RequirementType.OneOf)
                {
                    meetsSubRequirements = requirement.SubRequirements.Any((subRequirement) => MeetsRequirement(subRequirement, userPermissions));
                }
            }

            return meetsRequirement && meetsSubRequirements;
        }
    }
}
