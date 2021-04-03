using CMS.Core.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Core.Interfaces
{
    public interface IAuthRepository
    {
        #region Roles
        IQueryable<IdentityRole> GetRoles(string searchTerm = "", string sortItem = "", string sortOrder = "");
        IdentityRole GetRoleById(string id);
        IdentityRole GetRoleByName(string name);
        bool RoleExists(string rolename);
        bool RoleExists(string rolename, string excludeRoleId);
        void AddRole(string rolename);
        void UpdateRole(string roleId, string rolename);
        void RemoveRole(string roleId);
        #endregion

        #region UsersInRoles
        List<IdentityUserRole> GetUsersRoles();
        IdentityUserRole GetUserRole(string userId, string roleId);
        IList<string> GetRolesForUser(string userId);
        Task<IList<string>> GetRolesForUserAsync(string userId);
        Task<bool> IsUserInRoleAsync(string userId, string rolename);
        Task<IdentityResult> AddUserToRoleAsync(string userId, string rolename);
        Task<IdentityResult> AddUserToRolesAsync(string userId, params string[] roleNames);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string rolename);
        Task<IdentityResult> RemoveUserFromRolesAsync(string userId, params string[] roleNames);
        #endregion

        #region Users
        IQueryable<User> GetUsers(string searchTerm = "", string sortItem = "", string sortOrder = "");
        User FindUserByUsername(string username);
        Task<User> FindUserByUsernameAsync(string username);
        Task<User> FindUserAsync(string username, string password);
        Task<User> FindUserAsync(string id);
        PasswordVerificationResult UserPassIsCorrect(string hashedPassword
            , string providedPassword);
        Task<bool> UserExists(string username);
        Task<bool> UserExists(string username, string excludeUserId);
        Task<IdentityResult> RegisterUserAsync(User user, string password);
        Task<IdentityResult> CreateUserAsync(User user);
        Task<IdentityResult> UpdateUserAsync(User user, string password);
        Task<IdentityResult> DeleteUserAsync(User user);
        #endregion

        #region UserClaims
        Task<IdentityResult> AddClaimForUserAsync(string userId, Claim claim);
        Task<IdentityResult> RemoveClaimFromUserAsync(string userId, Claim claim);
        #endregion

        #region UsersInfos
        Task<IdentityResult> UpdateUserInfoAsync(User user);
        #endregion

        #region RolesAccessPaths
        IQueryable<RoleAccessPath> GetAccessPathsByRoleId(string roleId);
        void AddRoleAccessPaths(string roleId, List<Guid> accessPathIds);
        void AddRoleAccessPath(string roleId, Guid accessPathId);
        void RemoveRoleAccessPath(string roleId, Guid accessPathId);
        void RemoveAllRoleAccessPaths(string roleId);
        #endregion

        #region AccessPath
        IQueryable<AccessPath> GetAccessPaths();
        IQueryable<AccessPath> GetAccessPaths(Guid parentId);
        AccessPath GetAccessPath(Guid id);
        AccessPath GetAccessPath(string path);
        void AddAccessPath(string title, string path);
        void UpdateAccessPath(Guid id, string title, string path);
        void RemoveAccessPath(Guid id);
        #endregion
    }
}
