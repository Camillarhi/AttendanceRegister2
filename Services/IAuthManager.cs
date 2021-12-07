using AttendanceRegister2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginModel login);
        Task<string> CreateToken();
    }
}
