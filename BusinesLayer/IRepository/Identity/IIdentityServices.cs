using BusinesLayer.Dto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.IRepository.Identity
{
    public interface IIdentityServices
    {
        Task<string> Login(UserLoginDto userLoginDto);

        Task<string> Register(UserRegisterDto userRegisterDto, bool IsAdministrator);

        Task<UserDetail> GetUserDetailById(long Id);

        Task DeleteUser(string id);
        Task<List<UserDetail>> GetUsers();
        Task<UserEditDto> GetUserById(int id);
        Task<List<UserRoleDto>> GetRoles(int id);
        Task Logout();
        Task<IEnumerable<Domains.Domains.IdentityDomains.RoleApplication>> GetAllRoles();
    }
}
