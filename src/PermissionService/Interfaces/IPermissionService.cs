using System;
using System.Threading.Tasks;

namespace PermissionService.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TPermission"></typeparam>
    public interface IPermissionService<TUser, TPermission> where TPermission : IEquatable<TPermission>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="privilege"></param>
        /// <param name="context"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        Task<bool> HasAccess<TContext>(TUser user, string privilegeName, TContext context) where TContext : new();
    }
}
