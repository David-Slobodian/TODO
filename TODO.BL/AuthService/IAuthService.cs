using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.BL.Authentication;
using TODO.BL.Models;

namespace TODO.BL.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(LoginModel login);
        Task<Response> Registration(RegistrationModel model);
    }
}
