using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Dto.Account
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsBelong { get; set; }
    }
}
