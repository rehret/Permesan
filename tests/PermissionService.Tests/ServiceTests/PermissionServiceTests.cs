using Xunit;
using PermissionService.Tests.Support;
using PermissionService.Tests.Support.Models;
using PermissionService.Tests.Support.Services;

namespace PermissionService.Tests
{
    public class PermissionServiceTests
    {
        [Fact]
        public async void UserWithProperLocationPermissionsShouldBeAbleToViewEmployee()
        {
            // Arrange
            var user = new User
            {
                Id = "johndoe"
            };

            var employee = new Employee
            {
                Name = "Foo Bar",
                Location = "Headquarters"
            };

            var permissionService = new UnitTestPermissionService(TestConstants.ProtectedContexts);

            // Act
            var result = await permissionService.HasAccess(user, "Employee_Action_View", employee);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void UserWithoutProperLocationPermissionShouldNotBeAbleToViewEmployee()
        {
            // Arrange
            var user = new User
            {
                Id = "johndoe"
            };

            var employee = new Employee
            {
                Name = "Foo Bar",
                Location = "Downtown"
            };

            var permissionService = new UnitTestPermissionService(TestConstants.ProtectedContexts);

            // Act
            var result = await permissionService.HasAccess(user, "Employee_Action_View", employee);

            // Assert
            Assert.False(result);
        }
    }
}
