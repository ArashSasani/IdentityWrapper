using CMS.Core.Model;
using CMS.Service.Dtos.AccessPath;
using CMS.Service.Dtos.AccessPathCategory;
using CMS.Service.Dtos.Role;
using CMS.Service.Dtos.RoleAccessPath;
using CMS.Service.Dtos.User;
using CMS.Service.Dtos.UserInfo;
using CMS.Service.Dtos.UserInRole;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Service.Interfaces
{
    public interface IAuthService
    {
        #region Roles
        List<RoleDto> GetRoles(string searchTerm = "", string sortItem = "", string sortOrder = "");
        RoleDto GetRoleById(string id);
        RoleDto GetRoleByName(string name);
        bool RoleExists(string rolename);
        bool RoleExists(string rolename, string excludeRoleId);
        void AddRole(string rolename);
        void UpdateRole(string roleId, string rolename);
        void RemoveRole(string roleId);
        #endregion

        #region UsersInRoles
        Task<UserInRoleDto> GetUserRoleAsync(string userId, string roleId);
        Task<UserInRolesDto> GetRolesForUserAsync(string userId);
        Task<UserInRolesDto> GetRolesForUserByUsernameAsync(string username);
        Task<bool> IsUserInRoleAsync(string userId, string rolename);
        Task<IdentityResult> AddUserToRoleAsync(string userId, string roleId);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleId);
        #endregion

        #region Users
        List<UserDto> GetUsers(string searchTerm = "", string sortItem = "", string sortOrder = "");
        List<UserDto> GetUsersExcept(string exceptionUsername, string searchTerm = ""
            , string sortItem = "", string sortOrder = "");
        UserDto FindUserByUsername(string username);
        Task<UserDto> FindUserByUsernameAsync(string username);
        Task<UserDto> FindUserAsync(string username, string password);
        Task<UserDto> FindUserAsync(string id);
        bool UserPassIsCorrect(string hashedPassword, string providedPassword);
        Task<bool> UserExistsAsync(string username);
        Task<bool> UserExistsAsync(string username, string excludeUserId);
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerDto);
        /// <summary>
        /// create a user with no password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateUserAsync(User user);
        Task<IdentityResult> UpdateUserAsync(RegisterUserDto userModel);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> DeleteUserByUsernameAsync(string username);
        #endregion

        #region UserClaims
        Task<IdentityResult> AddClaimForUserAsync(string userId, Claim claim);
        Task<IdentityResult> RemoveClaimFromUserAsync(string userId, Claim claim);
        #endregion

        #region UsersInfos
        Task<UserInfoDto> GetUserInfoAsync(string userId);
        Task<UserInfoDto> GetUserInfoByUsernameAsync(string username);
        Task<IdentityResult> UpdateUserInfoAsync(UpdateUserInfoDto userInfoDto);
        #endregion

        #region RolesAccessPaths
        RoleAccessPathsDto GetAccessPathsForRole(string roleId);
        List<Guid> GetAccessPathIdsForRole(string roleId);
        bool DoesRoleHaveAccessPath(string roleId, Guid accessPathId);
        void AddRoleAccessPaths(string roleId, List<Guid> accessPathIds);
        void AddRoleAccessPath(string roleId, Guid accessPathId);
        void RemoveRoleAccessPath(string roleId, Guid accessPathId);
        void RemoveAllRoleAccessPaths(string roleId);
        void UpdateRoleAccessPaths(RoleAccessPathsDto roleAccessPaths);
        #endregion

        #region AccessPath
        List<AccessPathCategoryDto> GetAccessPaths();
        List<AccessPathCategoryDto> GetAccessPaths(Guid parentId);
        AccessPathDto GetAccessPath(Guid id);
        AccessPathDto GetAccessPath(string path);
        void AddAccessPath(string title, string path);
        void UpdateAccessPath(Guid id, string title, string path);
        void RemoveAccessPath(Guid id);
        #endregion
    }
}
