using AutoMapper;
using BusinesLayer.Dto.Account;
using BusinesLayer.IRepository.Identity;
using Domains.Domains.IdentityDomains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Identity
{
    public class IdentityService : IIdentityServices
    {
        //private readonly IConfiguration _config;
        private readonly UserManager<UserApplication> _userManager;
        private readonly RoleManager<RoleApplication> _roleManager;
        private readonly SignInManager<UserApplication> _signInManager;

        private readonly IMapper _mapper;
        //private readonly ApplicationDbContext _context;

        public IdentityService(
            //ApplicationDbContext context,
            //IConfiguration config,
            UserManager<UserApplication> userManager,
            RoleManager<RoleApplication> roleManager,
            SignInManager<UserApplication> signInManager,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            //_config = config;
            //_context = context;
        }

        public async Task<string> Login(UserLoginDto userLoginDto)
        {
            try
            {


                var userToLogin = await _userManager.FindByNameAsync(userLoginDto.Username);
                if (userToLogin == null)
                {
                    throw new ArgumentException("خطا نام کاربری و یا پسورد اشتباه است");
                }

                var ff = await _signInManager.PasswordSignInAsync(userToLogin.UserName, userLoginDto.Password, true, false);


                // await _signInManager.SignInAsync(userToLogin, true);



                //این بخش اضافه شده برای اینکه برای اکشن های ساده بدون آی پی آی ها بتوانیم نقش به کاربر دسترسی بدهیم

                var claims = new List<Claim>();


                claims.Add(new Claim("FullName", userToLogin.FirstName + " " + userToLogin.LastName));
                claims.Add(new Claim("Test", "Test"));
                var roles = await _userManager.GetRolesAsync(userToLogin);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                };
                claims.Add(new Claim(ClaimTypes.Role, "Dictionary"));
                //----<<--------------------




                //if (ff.Succeeded)
                //{

                return "";

                //}
                //else
                //{
                //    throw new ArgumentException("خطا نام کاربری و یا پسورد اشتباه است");
                //}

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<string> Register(UserRegisterDto userRegisterDto, bool IsAdministrator)
        {



            if (await _userManager.FindByNameAsync(userRegisterDto.Username) != null) throw new ArgumentException();

            var userToCreate = _mapper.Map<UserApplication>(userRegisterDto);

            userToCreate.UserName = userRegisterDto.PhoneNumber;

            var resault = await _userManager.CreateAsync(userToCreate, userRegisterDto.Password);

            if (resault.Succeeded)
            {
                //if (IsAdministrator)
                //{
                //    var result = await _userManager.AddToRoleAsync(userToCreate, "Admin");
                //    if (!resault.Succeeded) throw new Exception(resault.Errors.ToString());
                //}
                //else
                //{
                var result = await _userManager.AddToRoleAsync(userToCreate, "USER");
                if (!resault.Succeeded) throw new Exception(resault.Errors.ToString());
                //}

                var userToLogin = await _userManager.FindByNameAsync(userRegisterDto.Username);
                var res = await _signInManager.CheckPasswordSignInAsync(userToLogin, userRegisterDto.Password, false);

                if (res.Succeeded)
                {
                    return "";
                }

                throw new ArgumentException();
            }
            throw new Exception(resault.Errors.ToString());
        }

        public async Task DeleteUser(string id)
        {
            var oldUser = await _userManager.FindByIdAsync(id);
            if (oldUser == null) throw new ArgumentException("کاربری برای حذف پیدا نشد");



            var resault = await _userManager.DeleteAsync(oldUser);
            if (resault.Succeeded)
            {

            }
            else
            {
                throw new ArgumentException("در حذف خطایی رخ داده است");
            }

        }

        public async Task<UserDetail> GetUserDetailById(long Id)
        {
            return _mapper.Map<UserDetail>(await _userManager.FindByIdAsync(Id.ToString()));
        }


        public async Task<List<UserDetail>> GetUsers()
        {
            var users = await _userManager.Users.Include(u => u.UserRoleApplications).ThenInclude(ur => ur.RoleApplication).Select(q => new UserDetail
            {
                Id = q.Id,
                FirstName = q.FirstName,
                LastName = q.LastName,
                UserName = q.UserName
            }).ToListAsync();
            // var res=  _mapper.Map<UserDetail>(users);
            return users;
        }

        public async Task<UserEditDto> GetUserById(int id)
        {
            var olduser = await _userManager.Users.SingleOrDefaultAsync(q => q.Id == id);
            if (olduser == null)
            {
                throw new ArgumentException("مقداری یافت نشد");
            }

            UserEditDto userEditDto = new UserEditDto()
            {
                FirstName = olduser.FirstName,
                LastName = olduser.LastName

            };
            return userEditDto;
        }

        public async Task<List<UserRoleDto>> GetRoles(int id)
        {
            List<UserRoleDto> lstUserRole = new List<UserRoleDto>();
            var olduser = await _userManager.Users.SingleOrDefaultAsync(q => q.Id == id);
            var userInRoles = await _userManager.GetRolesAsync(olduser);
            var roles = await _roleManager.Roles.ToListAsync();
            for (int i = 0; i < roles.Count; i++)
            {
                UserRoleDto userRoleDto = new UserRoleDto();
                //var isExist = userInRoles.SingleOrDefault(q => q.Contains(roles[i].ToString()));
                var isExist = userInRoles.SingleOrDefault(q => q == roles[i].ToString());

                if (isExist == null)
                {
                    userRoleDto.IsBelong = false;
                    userRoleDto.RoleId = roles[i].Id;
                    userRoleDto.RoleName = roles[i].Name;
                }
                else
                {
                    userRoleDto.IsBelong = true;
                    userRoleDto.RoleId = roles[i].Id;
                    userRoleDto.RoleName = roles[i].Name;
                }
                lstUserRole.Add(userRoleDto);
            }
            return lstUserRole;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<RoleApplication>> GetAllRoles()
        {
            var res = await _roleManager.Roles.ToListAsync();
            return res;
        }
        //public async Task<IdentityResult> DeleteUser(string userId)
        //{
        //    var userApplication = await _userManager.FindByIdAsync(userId);
        //    var res = await _userManager.DeleteAsync(userApplication);
        //    return res;
        //}
    }
}
