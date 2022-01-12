using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Services
{
    public class logoutService
    {
        private SignInManager<IdentityUser<int>> _signiManager;

        public logoutService(SignInManager<IdentityUser<int>> signiManager)
        {
            _signiManager = signiManager;
        }

        public Result DeslogarUsuario()  
        {
            var resultadoIdentity = _signiManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou!");
        }
    }
}
