using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.IRepository.Identity
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Domains.Domains.IdentityDomains.RoleApplication>> GetAllRoles();
        Task<Domains.Domains.IdentityDomains.RoleApplication> FindRoleById(string roleId);
        Task<IdentityResult> CreateRole(Domains.Domains.IdentityDomains.RoleApplication roleApplication);
        Task<IdentityResult> CreateUserRole(string userId, IEnumerable<string> roles);
    }
}
