using System.Collections.Generic;
using PermissionService.Models;
using PermissionService.Tests.Support.Models;

namespace PermissionService.Tests.Support
{
    public static class TestConstants
    {
        public static IEnumerable<ProtectedContext<Permission>> ProtectedContexts = new List<ProtectedContext<Permission>>
        {
            new ProtectedContext<Permission>
            {
                Name = "Employee",
                Actions = new List<ProtectedContextMember<Permission>>
                {
                    new ProtectedContextMember<Permission>
                    {
                        PrivilegeName = "Employee_Action_View",
                        Description = "can view employee",
                        AlwaysAllowed = false,
                        ContextRequired = true,
                        Requirement = new Requirement<Permission>
                        {
                            Value = new Permission
                            {
                                Value = "CanViewEmployees"
                            }
                        }
                    },
                    new ProtectedContextMember<Permission>
                    {
                        PrivilegeName = "Employee_Action_Delete",
                        Description = "can delete employee",
                        AlwaysAllowed = false,
                        ContextRequired = false,
                        Requirement = new Requirement<Permission>
                        {
                            Value = new Permission
                            {
                                Value = "CanDeleteEmployees"
                            }
                        }
                    }
                }
            }
        };

        public static IEnumerable<Permission> UserPermissions = new List<Permission>
        {
            new Permission
            {
                UserId = "johndoe",
                Value = "CanViewEmployees"
            },
            new Permission
            {
                UserId = "johndoe",
                Value = "Headquarters"
            },
            new Permission
            {
                UserId = "janedoe",
                Value = "CanDeleteEmployees"
            }
        };
    }
}
