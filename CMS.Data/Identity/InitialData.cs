using CMS.Core.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Data.Identity
{
    public static class InitialData
    {
        public static List<User> GetAdminUsers()
        {
            var users = new List<User>
            {
                //admin pass: abc123@ -> change later
                new User
                {
                    Id = "e7f18795-000c-4afe-995f-b71ec95fee30",
                    UserName = "Admin",
                    Name = "Admin",
                    LastName = "",
                    PasswordHash = "AF924q+3egq51yXasR/R3JYrcJAD7VzH48vt7ZzSyRf/5X+rC5TGvjoS4dUq6wNv4w==",
                    SecurityStamp = "3656ca8d-bcf0-4e6d-8d49-81e7da37fd6d"
                },
                new User
                {
                    Id = "061f64c8-1eba-44a8-ac70-04e8c129ee81",
                    UserName = "1",
                    Name = "User 1",
                    LastName = "",
                    PasswordHash = "AF924q+3egq51yXasR/R3JYrcJAD7VzH48vt7ZzSyRf/5X+rC5TGvjoS4dUq6wNv4w==",
                    SecurityStamp = "3656ca8d-bcf0-4e6d-8d49-81e7da37fd6d",
                }
            };
            return users;
        }

        public static List<IdentityRole> GetRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "f0ef553e-63bf-465d-b5f2-8abbe9768692",
                    Name = "Admin"
                },
                new IdentityRole
                {
                    Id = "08ae5d44-fb25-4ec7-b4e2-2aa62a8dfefc",
                    Name = "Operator"
                }
            };
        }

        public static List<IdentityUserRole> GetUsersInRoles()
        {
            var usersInRoles = new List<IdentityUserRole>();

            var adminUsers = GetAdminUsers();
            var adminRole = GetRoles().SingleOrDefault(r => r.Name == "Admin");
            if (adminRole != null)
            {
                foreach (var adminUser in adminUsers)
                {
                    usersInRoles.Add(new IdentityUserRole
                    {
                        RoleId = adminRole.Id,
                        UserId = adminUser.Id
                    });
                }
            }

            return usersInRoles;
        }

        public static List<AccessPathCategory> GetAccessPathCategories()
        {
            return new List<AccessPathCategory>
            {
                new AccessPathCategory
                {
                    Id = Guid.Parse("ec00bcf7-dd13-4952-86dd-3617bcbee784"),
                    Title = "Home"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("b9e05fac-2879-4edb-92c4-02b53e1ae805"),
                    Title = "Role access paths management"
                },
                new AccessPathCategory
                {
                    Id=Guid.Parse("58e90c18-2c83-4194-89f8-97b9ea3026c3"),
                    Title="Roles management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("2e98edf9-6c14-442b-a788-320dbf7ecdf5"),
                    Title = "User info management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("e549df38-c585-479e-8ea8-81a3a11adb24"),
                    Title="User management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("01705ce9-f696-4390-b092-870240b9a0fc"),
                    Title="Users and role management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "Time access restrictions management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "Restricted IPs management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("c9bffb79-9fe1-4884-9904-4fe24520933a"),
                    Title = "User logs management"
                },
                new AccessPathCategory
                {
                    Id = Guid.Parse("c672c807-1f62-4c2f-8e61-a195d48039dc"),
                    Title = "User profile management"
                },
            };
        }

        public static List<AccessPath> GetAccessPaths()
        {
            //cms
            var restrictedAccessTimesControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "لیست محدودیت های زمانی",
                    Path = "GET api/cms/restricted/access/times",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "جزییات اطلاعات محدودیت زمانی",
                    Path = "GET api/cms/restricted/access/times/{id}",
                    Priority = 1
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "ثبت اطلاعات محدودیت زمانی",
                    Path = "POST api/cms/restricted/access/times/create",
                    Priority = 2
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "ویرایش اطلاعات محدودیت زمانی",
                    Path = "PUT api/cms/restricted/access/times/update",
                    Priority = 3
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "حذف موقت اطلاعات محدودیت زمانی",
                    Path = "DELETE api/cms/restricted/access/times/soft/{id}",
                    Priority = 4
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "حذف دایم اطلاعات محدودیت زمانی",
                    Path = "DELETE api/cms/restricted/access/times/permanent/{id}",
                    Priority = 5
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("367f75d6-d126-4130-8903-fb99147ae7dc"),
                    Title = "حذف انتخابی محدودیت های زمانی",
                    Path = "DELETE api/cms/restricted/access/times",
                    Priority = 6
                }
            };
            var restrictedIPsControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "لیست آی پی ها",
                    Path = "GET api/cms/restricted/ips",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "جزییات اطلاعات آی پی",
                    Path = "GET api/cms/restricted/ips/{id}",
                    Priority = 1
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "ثبت اطلاعات آی پی",
                    Path = "POST api/cms/restricted/ips/create",
                    Priority = 2
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "ویرایش اطلاعات آی پی",
                    Path = "PUT api/cms/restricted/ips/update",
                    Priority = 3
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "حذف موقت اطلاعات آی پی",
                    Path = "DELETE api/cms/restricted/ips/soft/{id}",
                    Priority = 4
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "حذف دایم اطلاعات آی پی",
                    Path = "DELETE api/cms/restricted/ips/permanent/{id}",
                    Priority = 5
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("0df91819-eb69-420c-ae9a-463b6d1a0692"),
                    Title = "حذف انتخابی آی پی ها",
                    Path = "DELETE api/cms/restricted/ips",
                    Priority = 6
                }
            };
            var roleAccessPathsControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("b9e05fac-2879-4edb-92c4-02b53e1ae805"),
                    Title = "جزییات دسترسی های نقش مورد نظر",
                    Path = "GET api/cms/role/access/paths/{id}",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("b9e05fac-2879-4edb-92c4-02b53e1ae805"),
                    Title = "تغییر دسترسی های نقش مورد نظر",
                    Path = "PUT api/cms/role/access/paths/update",
                    Priority = 1
                }
            };
            var rolesControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("58e90c18-2c83-4194-89f8-97b9ea3026c3"),
                    Title = "لیست نقش های کاربر سامانه",
                    Path = "GET api/cms/roles",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("58e90c18-2c83-4194-89f8-97b9ea3026c3"),
                    Title = "جزییات اطلاعات نقش کاربر سامانه",
                    Path = "GET api/cms/roles/{id}",
                    Priority = 1
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("58e90c18-2c83-4194-89f8-97b9ea3026c3"),
                    Title = "ثبت نقش کاربر سامانه",
                    Path = "POST api/cms/roles/create",
                    Priority = 2
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("58e90c18-2c83-4194-89f8-97b9ea3026c3"),
                    Title = "ویرایش اطلاعات نقش کاربر سامانه",
                    Path = "PUT api/cms/roles/update",
                    Priority = 3
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("58e90c18-2c83-4194-89f8-97b9ea3026c3"),
                    Title = "حذف نقش کاربر سامانه",
                    Path = "DELETE api/cms/roles/{id}",
                    Priority = 4
                }
            };
            var userInfoControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId=Guid.Parse("2e98edf9-6c14-442b-a788-320dbf7ecdf5"),
                    Title = "جزییات اطلاعات شخصی کاربر سامانه",
                    Path = "GET api/cms/user/info/{id}",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId=Guid.Parse("2e98edf9-6c14-442b-a788-320dbf7ecdf5"),
                    Title = "ویرایش اطلاعات شخصی کاربر سامانه",
                    Path = "PUT api/cms/user/info/update",
                    Priority = 1
                }
            };
            var usersInRolesControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("01705ce9-f696-4390-b092-870240b9a0fc"),
                    Title = "اطلاعات نقش های کاربر مورد نظر",
                    Path = "GET api/cms/user/in/roles/{userId}",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("01705ce9-f696-4390-b092-870240b9a0fc"),
                    Title = "جزییات اطلاعات نقش کاربر مورد نظر",
                    Path = "GET api/cms/user/in/roles/{userId}/{roleId}",
                    Priority = 1
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("01705ce9-f696-4390-b092-870240b9a0fc"),
                    Title = "ثبت نقش برای کاربر",
                    Path = "POST api/cms/user/in/roles/create",
                    Priority = 2
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("01705ce9-f696-4390-b092-870240b9a0fc"),
                    Title = "حذف نقش از کاربر",
                    Path = "DELETE api/cms/user/in/roles/{userId}/{roleId}",
                    Priority = 3
                }
            };
            var userLogsControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId=Guid.Parse("c9bffb79-9fe1-4884-9904-4fe24520933a"),
                    Title = "لیست تراکنش ها",
                    Path = "GET api/cms/user/logs/{userId}",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId=Guid.Parse("c9bffb79-9fe1-4884-9904-4fe24520933a"),
                    Title = "جزییات اطلاعات تراکنش",
                    Path = "GET api/cms/user/logs/{id}",
                    Priority = 1
                }
            };
            var usersControllerPaths = new List<AccessPath>
            {
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("e549df38-c585-479e-8ea8-81a3a11adb24"),
                    Title = "لیست کاربران سامانه",
                    Path = "GET api/cms/users",
                    Priority = 0
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("e549df38-c585-479e-8ea8-81a3a11adb24"),
                    Title = "جزییات اطلاعات کاربر سامانه",
                    Path = "GET api/cms/users/{id}",
                    Priority = 1
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("e549df38-c585-479e-8ea8-81a3a11adb24"),
                    Title = "ثبت کاربر سامانه",
                    Path = "POST api/cms/users/create",
                    Priority = 2
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("e549df38-c585-479e-8ea8-81a3a11adb24"),
                    Title = "ویرایش اطلاعات کاربر سامانه",
                    Path = "PUT api/cms/users/update",
                    Priority = 3
                },
                new AccessPath
                {
                    Id = Guid.NewGuid(),
                    ParentId = Guid.Parse("e549df38-c585-479e-8ea8-81a3a11adb24"),
                    Title = "حذف کاربر سامانه",
                    Path = "DELETE api/cms/users/{id}",
                    Priority = 4
                }
            };

            var accessPathList = new List<AccessPath>();
            //cms
            accessPathList.AddRange(restrictedAccessTimesControllerPaths);
            accessPathList.AddRange(restrictedIPsControllerPaths);
            accessPathList.AddRange(roleAccessPathsControllerPaths);
            accessPathList.AddRange(rolesControllerPaths);
            accessPathList.AddRange(userInfoControllerPaths);
            accessPathList.AddRange(usersInRolesControllerPaths);
            accessPathList.AddRange(userLogsControllerPaths);
            accessPathList.AddRange(usersControllerPaths);

            return accessPathList;
        }

        public static List<RoleAccessPath> GetRolesAccessPaths(List<AccessPath> usedAccessPaths)
        {
            var roleAccessList = new List<RoleAccessPath>();

            var roles = GetRoles();
            var adminRole = roles.SingleOrDefault(r => r.Name == "Admin");
            if (adminRole != null)
            {
                foreach (var accessPath in usedAccessPaths)
                {
                    roleAccessList.Add(new RoleAccessPath
                    {
                        RoleId = adminRole.Id,
                        AccessPathId = accessPath.Id
                    });
                }
            }

            return roleAccessList;
        }
    }
}