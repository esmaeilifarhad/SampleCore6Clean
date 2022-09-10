using BusinesLayer.IRepository.Identity;
using Domains.Domains.IdentityDomains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Identity
{
    public class RoleRepository : IRoleRepository
    {
        public UserManager<UserApplication> _userManager { get; }
        public RoleManager<RoleApplication> _roleManager { get; }
        public RoleRepository(RoleManager<RoleApplication> roleManager, UserManager<UserApplication> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        public async Task<IEnumerable<RoleApplication>> GetAllRoles()
        {

            var res = await _roleManager.Roles.ToListAsync();
            return res;
        }
        public async Task<Domains.Domains.IdentityDomains.RoleApplication> FindRoleById(string roleId)
        {
            var roleApplication = await _roleManager.FindByIdAsync(roleId);
            return roleApplication;
        }
        public async Task<IdentityResult> CreateRole(RoleApplication roleApplication)
        {

            var result = await _roleManager.CreateAsync(roleApplication);
            return result;
        }
        public async Task<IdentityResult> CreateUserRole(UserApplication userApplication)
        {
            IEnumerable<string> roles = new List<string>();
            var result = await _userManager.AddToRolesAsync(userApplication, roles);
            return result;
        }



        public async Task<IdentityResult> CreateUserRole(string userId, IEnumerable<string> rolesString)
        {
            var userApplication = await _userManager.FindByIdAsync(userId);

            var roles2 = await _userManager.GetRolesAsync(userApplication);
            await _userManager.RemoveFromRolesAsync(userApplication, roles2.ToArray());

            //var result1=await _userManager.RemoveFromRolesAsync(userApplication, rolesString);
            IEnumerable<string> roles = new List<string>();
            var result = await _userManager.AddToRolesAsync(userApplication, rolesString);
            return result;
        }
    }
}
