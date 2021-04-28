using CMS.Core.Interfaces;
using CMS.Core.Model;
using CMS.Service.Dtos.AccessPath;
using CMS.Service.Dtos.AccessPathCategory;
using CMS.Service.Dtos.Role;
using CMS.Service.Dtos.RoleAccessPath;
using CMS.Service.Dtos.User;
using CMS.Service.Dtos.UserInfo;
using CMS.Service.Dtos.UserInRole;
using CMS.Service.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication.Infrastructure.Interfaces;

namespace CMS.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _identityRepository;
        private readonly IImageService _imageService;

        public AuthService(IAuthRepository identityRepository, IImageService imageService)
        {
            _identityRepository = identityRepository;
            _imageService = imageService;
        }

        #region Roles
        public List<RoleDto> GetRoles(string searchTerm = "", string sortItem = ""
            , string sortOrder = "")
        {
            var roles = _identityRepository.GetRoles(searchTerm, sortItem, sortOrder);

            var dto = new List<RoleDto>();
            foreach (var role in roles)
            {
                dto.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            return dto;
        }

        public RoleDto GetRoleById(string id)
        {
            var role = _identityRepository.GetRoleById(id);
            if (role == null)
            {
                return null;
            }
            var dto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
            return dto;
        }

        public RoleDto GetRoleByName(string name)
        {
            var role = _identityRepository.GetRoleByName(name);
            if (role == null)
            {
                return null;
            }
            var dto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
            return dto;
        }

        public bool RoleExists(string rolename)
        {
            return _identityRepository.RoleExists(rolename);
        }
        public bool RoleExists(string rolename, string excludeRoleId)
        {
            return _identityRepository.RoleExists(rolename, excludeRoleId);
        }

        public void AddRole(string rolename)
        {
            _identityRepository.AddRole(rolename);
        }

        public void UpdateRole(string roleId, string rolename)
        {
            _identityRepository.UpdateRole(roleId, rolename);
        }

        public void RemoveRole(string roleId)
        {
            _identityRepository.RemoveRole(roleId);
        }
        #endregion

        #region UsersInRoles
        public async Task<UserInRoleDto> GetUserRoleAsync(string userId, string roleId)
        {
            var role = _identityRepository.GetRoleById(roleId);
            var user = await _identityRepository.FindUserAsync(userId);
            if (user == null || role == null)
            {
                return null;
            }
            var dto = new UserInRoleDto
            {
                UserId = user.Id,
                Username = user.UserName,
                RoleId = role.Id,
                Rolename = role.Name
            };
            return dto;
        }

        public async Task<UserInRolesDto> GetRolesForUserAsync(string userId)
        {
            var user = await _identityRepository.FindUserAsync(userId);
            if (user == null)
            {
                return null;
            }
            var dto = new UserInRolesDto
            {
                UserId = user.Id,
                Username = user.UserName
            };
            var roleNames = await _identityRepository.GetRolesForUserAsync(userId);
            foreach (var roleName in roleNames)
            {
                var role = _identityRepository.GetRoleByName(roleName);
                dto.Roles.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = roleName
                });
            }
            return dto;
        }

        public async Task<UserInRolesDto> GetRolesForUserByUsernameAsync(string username)
        {
            var user = await _identityRepository.FindUserByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }
            var dto = new UserInRolesDto
            {
                UserId = user.Id,
                Username = user.UserName
            };
            var roleNames = await _identityRepository.GetRolesForUserAsync(user.Id);
            foreach (var roleName in roleNames)
            {
                var role = _identityRepository.GetRoleByName(roleName);
                dto.Roles.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = roleName
                });
            }
            return dto;
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string rolename)
        {
            return await _identityRepository.IsUserInRoleAsync(userId, rolename);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleId)
        {
            var role = _identityRepository.GetRoleById(roleId);
            var user = await _identityRepository.FindUserAsync(userId);

            if (user == null || role == null)
            {
                return IdentityResult.Failed("Role or user is not available");
            }

            var isUserInRole = await IsUserInRoleAsync(user.Id, role.Name);
            if (!isUserInRole)
            {
                return await _identityRepository.AddUserToRoleAsync(userId, role.Name);
            }
            else
            {
                return IdentityResult.Failed("The role is already registered for the user");
            }
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleId)
        {
            var role = _identityRepository.GetRoleById(roleId);
            var user = await _identityRepository.FindUserAsync(userId);

            if (user == null || role == null)
            {
                return IdentityResult.Failed("Role or user is not available");
            }

            return await _identityRepository.RemoveUserFromRoleAsync(userId, role.Name);
        }
        #endregion

        #region Users
        public List<UserDto> GetUsers(string searchTerm = "", string sortItem = ""
            , string sortOrder = "")
        {
            var users = _identityRepository.GetUsers(searchTerm, sortItem, sortOrder);
            var dto = new List<UserDto>();
            foreach (var user in users)
            {
                dto.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    UserType = "",
                    FullName = user.Name + " " + user.LastName
                });
            }
            return dto;
        }

        public List<UserDto> GetUsersExcept(string exceptionUsername, string searchTerm = ""
            , string sortItem = "", string sortOrder = "")
        {
            if (string.IsNullOrEmpty(exceptionUsername))
            {
                return GetUsers(searchTerm, sortItem, sortOrder);
            }

            var users = _identityRepository.GetUsers(searchTerm, sortItem, sortOrder)
                .Where(q => q.UserName.ToLower() != exceptionUsername.ToLower());
            var dto = new List<UserDto>();
            foreach (var user in users)
            {
                dto.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    UserType = "",
                    FullName = user.Name + " " + user.LastName
                });
            }
            return dto;
        }

        public UserDto FindUserByUsername(string username)
        {
            var user = _identityRepository.FindUserByUsername(username);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                };
            }
            return null;
        }

        public async Task<UserDto> FindUserByUsernameAsync(string username)
        {
            var user = await _identityRepository.FindUserByUsernameAsync(username);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                };
            }
            return null;
        }

        public async Task<UserDto> FindUserAsync(string username, string password)
        {
            var user = await _identityRepository.FindUserAsync(username, password);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                };
            }
            return null;
        }

        public async Task<UserDto> FindUserAsync(string id)
        {
            var user = await _identityRepository.FindUserAsync(id);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                };
            }
            return null;
        }

        public bool UserPassIsCorrect(string hashedPassword, string providedPassword)
        {
            var result = _identityRepository.UserPassIsCorrect(hashedPassword, providedPassword);
            switch (result)
            {
                case PasswordVerificationResult.Failed:
                case PasswordVerificationResult.SuccessRehashNeeded:
                    return false;
                case PasswordVerificationResult.Success:
                    return true;
            }
            return false;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _identityRepository.UserExists(username);
        }

        public async Task<bool> UserExistsAsync(string username, string excludeUserId)
        {
            return await _identityRepository.UserExists(username, excludeUserId);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto userModel)
        {
            var user = new User
            {
                UserName = userModel.UserName
            };
            if (userModel.UserInfo != null)
            {
                user.Name = userModel.UserInfo.Name;
                user.LastName = userModel.UserInfo.LastName;
            }
            return await _identityRepository.RegisterUserAsync(user, userModel.Password);
        }

        public async Task<IdentityResult> CreateUserAsync(User user)
        {
            return await _identityRepository.CreateUserAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(RegisterUserDto userModel)
        {
            var user = await _identityRepository.FindUserAsync(userModel.UserId);
            if (user != null)
            {
                user.UserName = userModel.UserName;

                return await _identityRepository.UpdateUserAsync(user, userModel.Password);
            }
            return IdentityResult.Failed("User is not available");
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _identityRepository.FindUserAsync(userId);
            if (user != null)
            {
                return await _identityRepository.DeleteUserAsync(user);
            }
            return IdentityResult.Failed("User is not available");
        }

        public async Task<IdentityResult> DeleteUserByUsernameAsync(string username)
        {
            var user = await _identityRepository.FindUserByUsernameAsync(username);
            if (user != null)
            {
                return await _identityRepository.DeleteUserAsync(user);
            }
            return IdentityResult.Failed("User is not available");
        }
        #endregion

        #region UserClaims
        public async Task<IdentityResult> AddClaimForUserAsync(string userId, Claim claim)
        {
            return await _identityRepository.AddClaimForUserAsync(userId, claim);
        }

        public async Task<IdentityResult> RemoveClaimFromUserAsync(string userId, Claim claim)
        {
            return await _identityRepository.RemoveClaimFromUserAsync(userId, claim);
        }
        #endregion

        #region UsersInfos
        public async Task<UserInfoDto> GetUserInfoAsync(string userId)
        {
            var user = await _identityRepository.FindUserAsync(userId);
            if (user == null)
            {
                return null;
            }
            var dto = new UserInfoDto
            {
                UserId = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                NationalCode = user.NationalCode,
                BirthDate = user.BirthDate.HasValue
                    ? user.BirthDate.Value.ToShortDateString() : "",
                Image = (user.Image != null && user.Image.Length > 0)
                    ? _imageService.EncodeToBase64(user.Image)
                    : WebApplication.Infrastructure.AppSettings.DEFAULT_USER_IMAGE_DATA,
                Details = user.Details
            };
            return dto;
        }

        public async Task<UserInfoDto> GetUserInfoByUsernameAsync(string username)
        {
            var user = await _identityRepository.FindUserByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }
            var dto = new UserInfoDto
            {
                UserId = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                NationalCode = user.NationalCode,
                BirthDate = user.BirthDate.HasValue
                    ? user.BirthDate.Value.ToShortDateString() : "",
                Image = (user.Image != null && user.Image.Length > 0)
                    ? _imageService.EncodeToBase64(user.Image)
                    : WebApplication.Infrastructure.AppSettings.DEFAULT_USER_IMAGE_DATA,
                Details = user.Details
            };
            return dto;
        }

        public async Task<IdentityResult> UpdateUserInfoAsync(UpdateUserInfoDto userInfoDto)
        {
            var user = await _identityRepository.FindUserAsync(userInfoDto.UserId);
            if (user != null)
            {
                user.Name = userInfoDto.Name;
                user.LastName = userInfoDto.LastName;
                user.NationalCode = userInfoDto.NationalCode;
                user.BirthDate = userInfoDto.BirthDate;
                user.Image = userInfoDto.ImageBytes;
                user.Details = userInfoDto.Details;
                user.Image = (userInfoDto.Image != null && userInfoDto.Image.Length > 0)
                    ? _imageService.DecodeFromBase64(userInfoDto.Image) : null;

                return await _identityRepository.UpdateUserInfoAsync(user);
            }
            return IdentityResult.Failed("User is not available");
        }
        #endregion

        #region RolesAccessPaths
        public RoleAccessPathsDto GetAccessPathsForRole(string roleId)
        {
            var result = new RoleAccessPathsDto();

            var role = _identityRepository.GetRoleById(roleId);
            if (role != null)
            {
                result.RoleId = role.Id;
                result.Rolename = role.Name;

                var roleAccessPaths = _identityRepository.GetAccessPathsByRoleId(roleId).ToList();
                foreach (var roleAccessPath in roleAccessPaths)
                {
                    result.AccessPaths.Add(new AccessPathDto
                    {
                        ParentId = roleAccessPath.AccessPath.ParentId,
                        Id = roleAccessPath.AccessPathId,
                        Title = roleAccessPath.AccessPath.Title,
                        Priority = roleAccessPath.AccessPath.Priority,
                        Path = roleAccessPath.AccessPath.Path
                    });
                }
            }

            return result;
        }

        public List<Guid> GetAccessPathIdsForRole(string roleId)
        {
            var ids = new List<Guid>();

            var roleAccessPaths = _identityRepository.GetAccessPathsByRoleId(roleId).ToList();
            foreach (var roleAccessPath in roleAccessPaths)
            {
                ids.Add(roleAccessPath.AccessPathId);
            }

            return ids;
        }

        public bool DoesRoleHaveAccessPath(string roleId, Guid accessPathId)
        {
            var role = _identityRepository.GetRoleById(roleId);
            if (role != null)
            {
                return _identityRepository.GetAccessPathsByRoleId(roleId).AsEnumerable()
                    .Any(q => q.AccessPathId == accessPathId);
            }
            return false;
        }

        public void AddRoleAccessPaths(string roleId, List<Guid> accessPathIds)
        {
            _identityRepository.AddRoleAccessPaths(roleId, accessPathIds);
        }

        public void AddRoleAccessPath(string roleId, Guid accessPathId)
        {
            _identityRepository.AddRoleAccessPath(roleId, accessPathId);
        }
        public void RemoveRoleAccessPath(string roleId, Guid accessPathId)
        {
            _identityRepository.RemoveRoleAccessPath(roleId, accessPathId);
        }

        public void RemoveAllRoleAccessPaths(string roleId)
        {
            _identityRepository.RemoveAllRoleAccessPaths(roleId);
        }

        public void UpdateRoleAccessPaths(RoleAccessPathsDto roleAccessPaths)
        {
            string roleId = roleAccessPaths.RoleId;

            using (var scope = new TransactionScope())
            {
                var accessPathIds = roleAccessPaths.AccessPaths.Select(x => x.Id).ToList();
                //remove all
                RemoveAllRoleAccessPaths(roleId);
                //add new ones
                AddRoleAccessPaths(roleId, accessPathIds);

                scope.Complete();
            }
        }

        #endregion

        #region AccessPaths
        public List<AccessPathCategoryDto> GetAccessPaths()
        {
            var accessPaths = _identityRepository.GetAccessPaths();

            return accessPaths.GroupBy(item => item.AccessPathCategory)
                .Select(grp => new AccessPathCategoryDto
                {
                    ParentId = grp.Key.Id,
                    ParentTitle = grp.Key.Title,
                    AccessPaths = grp.Select(x => new AccessPathDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Path = x.Path,
                        Priority = x.Priority
                    }).OrderBy(ap => ap.Priority).ToList()
                }).OrderBy(o => o.ParentTitle).ToList();
        }

        public List<AccessPathCategoryDto> GetAccessPaths(Guid parentId)
        {
            var accessPaths = _identityRepository.GetAccessPaths(parentId);

            return accessPaths.GroupBy(item => item.AccessPathCategory)
                .Select(grp => new AccessPathCategoryDto
                {
                    ParentId = grp.Key.Id,
                    ParentTitle = grp.Key.Title,
                    AccessPaths = grp.Select(x => new AccessPathDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Path = x.Path,
                        Priority = x.Priority
                    }).OrderBy(ap => ap.Priority).ToList()
                }).ToList();
        }

        public AccessPathDto GetAccessPath(Guid id)
        {
            var accessPath = _identityRepository.GetAccessPath(id);
            return new AccessPathDto
            {
                Id = accessPath.Id,
                Title = accessPath.Title,
                Priority = accessPath.Priority,
                Path = accessPath.Path
            };
        }

        public AccessPathDto GetAccessPath(string path)
        {
            var accessPath = _identityRepository.GetAccessPath(path);
            return new AccessPathDto
            {
                Id = accessPath.Id,
                Title = accessPath.Title,
                Priority = accessPath.Priority,
                Path = accessPath.Path
            };
        }

        public void AddAccessPath(string title, string path)
        {
            _identityRepository.AddAccessPath(title, path);
        }

        public void UpdateAccessPath(Guid id, string title, string path)
        {
            _identityRepository.UpdateAccessPath(id, title, path);
        }

        public void RemoveAccessPath(Guid id)
        {
            _identityRepository.RemoveAccessPath(id);
        }

        #endregion
    }
}
