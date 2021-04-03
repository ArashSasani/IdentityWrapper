using CMS.Core.Interfaces;
using CMS.Core.Model;
using CMS.Data.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private AuthContext _context;

        private ApplicationUserManager _userManager;

        public AuthRepository()
        {
            _context = new AuthContext();
            _userManager = new ApplicationUserManager(new UserStore<User>(_context));
        }

        #region Roles
        public IQueryable<IdentityRole> GetRoles(string searchTerm = "", string sortItem = "", string sortOrder = "")
        {
            IQueryable<IdentityRole> roles;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = _context.Roles
                    .Where(q => q.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
            }
            else
            {
                roles = _context.Roles;
            }
            switch (sortItem.ToLower())
            {
                case "rolename":
                    if (sortOrder == "desc")
                        roles = roles.OrderByDescending(r => r.Name);
                    else
                        roles = roles.OrderBy(r => r.Name);
                    break;
                default:
                    roles = roles.OrderBy(r => r.Name);
                    break;
            }
            return roles;
        }

        public IdentityRole GetRoleById(string id)
        {
            return _context.Roles.SingleOrDefault(r => r.Id == id);
        }

        public IdentityRole GetRoleByName(string name)
        {
            return _context.Roles.SingleOrDefault(r => r.Name == name);
        }

        public bool RoleExists(string rolename)
        {
            return _context.Roles.Any(q => q.Name == rolename);
        }
        public bool RoleExists(string rolename, string excludeRoleId)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Name == rolename);
            if (role != null)
            {
                return role.Id == excludeRoleId ? false : true;
            }
            return false;
        }

        public void AddRole(string rolename)
        {
            var roleExists = _context.Roles.Any(r => r.Name == rolename);
            if (!roleExists)
            {
                _context.Roles.Add(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = rolename
                });
                _context.SaveChanges();
            }
        }

        public void UpdateRole(string roleId, string rolename)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);
            if (role != null)
            {
                var roleExists = _context.Roles.Any(r => r.Id != roleId && r.Name == rolename);
                if (!roleExists)
                {
                    role.Name = rolename;

                    _context.Entry(role).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
        }

        public void RemoveRole(string roleId)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }
        #endregion

        #region UsersInRoles
        public List<IdentityUserRole> GetUsersRoles()
        {
            return _context.Set<IdentityUserRole>().ToList();
        }

        public IdentityUserRole GetUserRole(string userId, string roleId)
        {
            return _context.Set<IdentityUserRole>()
                .SingleOrDefault(q => q.UserId == userId && q.RoleId == roleId);
        }

        public IList<string> GetRolesForUser(string userId)
        {
            var roleNames = _userManager.GetRoles(userId);
            return roleNames;
        }

        public async Task<IList<string>> GetRolesForUserAsync(string userId)
        {
            var roleNames = await _userManager.GetRolesAsync(userId);
            return roleNames;
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string rolename)
        {
            return await _userManager.IsInRoleAsync(userId, rolename);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string rolename)
        {
            return await _userManager.AddToRoleAsync(userId, rolename);
        }

        public async Task<IdentityResult> AddUserToRolesAsync(string userId, params string[] roleNames)
        {
            return await _userManager.AddToRolesAsync(userId, roleNames);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string rolename)
        {
            return await _userManager.RemoveFromRoleAsync(userId, rolename);
        }

        public async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, params string[] roleNames)
        {
            return await _userManager.RemoveFromRolesAsync(userId, roleNames);
        }
        #endregion

        #region Users
        public IQueryable<User> GetUsers(string searchTerm = "", string sortItem = ""
            , string sortOrder = "")
        {
            IQueryable<User> users;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = _context.Users
                    .Where(q => q.UserName.ToLower().Contains(searchTerm.Trim().ToLower()));
            }
            else
            {
                users = _context.Users;
            }
            switch (sortItem.ToLower())
            {
                case "username":
                    if (sortOrder == "desc")
                        users = users.OrderByDescending(r => r.UserName);
                    else
                        users = users.OrderBy(r => r.UserName);
                    break;
                default:
                    users = users.OrderBy(r => r.UserName);
                    break;
            }
            return users;
        }

        public User FindUserByUsername(string username)
        {
            return _userManager.FindByName(username);
        }

        public async Task<User> FindUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User> FindUserAsync(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public async Task<User> FindUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public PasswordVerificationResult UserPassIsCorrect(string hashedPassword
            , string providedPassword)
        {
            return _userManager.PasswordHasher
                .VerifyHashedPassword(hashedPassword, providedPassword);
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null ? true : false;
        }

        public async Task<bool> UserExists(string username, string excludeUserId)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return user.Id == excludeUserId ? false : true;
            }
            return false;
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(User user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user, string password)
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(password);
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }
        #endregion

        #region UserClaims
        public async Task<IdentityResult> AddClaimForUserAsync(string userId, Claim claim)
        {
            return await _userManager.AddClaimAsync(userId, claim);
        }

        public async Task<IdentityResult> RemoveClaimFromUserAsync(string userId, Claim claim)
        {
            return await _userManager.RemoveClaimAsync(userId, claim);
        }
        #endregion

        #region UsersInfos
        public async Task<IdentityResult> UpdateUserInfoAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
        #endregion

        #region RolesAccessPaths
        public IQueryable<RoleAccessPath> GetAccessPathsByRoleId(string roleId)
        {
            return _context.RolesAccessPaths.Where(ra => ra.RoleId == roleId)
                .Include("AccessPath");
        }

        public void AddRoleAccessPaths(string roleId, List<Guid> accessPathIds)
        {
            foreach (var accessPathId in accessPathIds)
            {
                _context.RolesAccessPaths.Add(new RoleAccessPath
                {
                    RoleId = roleId,
                    AccessPathId = accessPathId
                });
            }
            _context.SaveChanges();
        }

        public void AddRoleAccessPath(string roleId, Guid accessPathId)
        {
            _context.RolesAccessPaths.Add(new RoleAccessPath
            {
                RoleId = roleId,
                AccessPathId = accessPathId
            });
            _context.SaveChanges();
        }
        public void RemoveRoleAccessPath(string roleId, Guid accessPathId)
        {
            var roleAccess = _context.RolesAccessPaths
                .SingleOrDefault(ra => ra.RoleId == roleId && ra.AccessPathId == accessPathId);
            if (roleAccess != null)
            {
                _context.RolesAccessPaths.Remove(roleAccess);
                _context.SaveChanges();
            }
        }

        public void RemoveAllRoleAccessPaths(string roleId)
        {
            var roleAccesses = _context.RolesAccessPaths.Where(ra => ra.RoleId == roleId).AsEnumerable();
            _context.RolesAccessPaths.RemoveRange(roleAccesses);
            _context.SaveChanges();
        }
        #endregion

        #region AccessPaths
        public IQueryable<AccessPath> GetAccessPaths()
        {
            return _context.AccessPaths.Include("AccessPathCategory");
        }

        public IQueryable<AccessPath> GetAccessPaths(Guid parentId)
        {
            return _context.AccessPaths.Where(ap => ap.ParentId == parentId);
        }

        public AccessPath GetAccessPath(Guid id)
        {
            return _context.AccessPaths.SingleOrDefault(q => q.Id == id);
        }

        public AccessPath GetAccessPath(string path)
        {
            return _context.AccessPaths.FirstOrDefault(q => q.Path.ToLower() == path.ToLower());
        }

        public void AddAccessPath(string title, string path)
        {
            _context.AccessPaths.Add(new AccessPath
            {
                Title = title,
                Path = path.ToLower()
            });
            _context.SaveChanges();
        }

        public void UpdateAccessPath(Guid id, string title, string path)
        {
            var accessPath = _context.AccessPaths.SingleOrDefault(ap => ap.Id == id);
            if (accessPath != null)
            {
                accessPath.Title = title;
                accessPath.Path = path.ToLower();
            }
            _context.Entry(accessPath).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveAccessPath(Guid id)
        {
            var accessPath = _context.AccessPaths.SingleOrDefault(ap => ap.Id == id);
            if (accessPath != null)
            {
                _context.AccessPaths.Remove(accessPath);
                _context.SaveChanges();
            }
        }
        #endregion

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _userManager.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}