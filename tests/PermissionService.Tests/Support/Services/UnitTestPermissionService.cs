using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PermissionService.Models;
using PermissionService.Services;
using PermissionService.Tests.Support.Models;

namespace PermissionService.Tests.Support.Services
{
    public class UnitTestPermissionService : PermissionService<User, Permission>
    {
        public UnitTestPermissionService(IEnumerable<ProtectedContext<Permission>> protectedContexts) : base(protectedContexts) { }

        protected override Task<IEnumerable<IEquatable<Permission>>> GetUserPermissions(User user)
        {
            return Task.FromResult(TestConstants.UserPermissions
                .Where((permission) => permission.UserId == user.Id)
                .Select((permission) => (IEquatable<Permission>)permission));
        }

        protected override async Task<bool> HasAccessForContext<TContext>(User user, ProtectedContextMember<Permission> privilege, TContext context)
        {
            if (context is Employee employeeContext)
            {
                return await HasAccessForEmployeeContext(user, privilege, employeeContext);
            }

            return false;
        }

        private async Task<bool> HasAccessForEmployeeContext(User user, ProtectedContextMember<Permission> privilege, Employee context)
        {
            switch (privilege.PrivilegeName)
            {
                case "Employee_Action_View":
                    var userPermissions = await GetUserPermissions(user);
                    var requiredPermission = new Permission
                    {
                        Value = context.Location
                    };

                    return userPermissions.Any((permission) => permission.Equals(requiredPermission));

                default:
                    return false;
            }
        }
    }
}
