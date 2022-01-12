using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _singInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> singInManager, TokenService tokenService)
        {
            _singInManager = singInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _singInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            //procurando o usuario após fazer o login
            if (resultadoIdentity.Result.Succeeded)
            {
                //pegando ID
                var identityUser = _singInManager
                    .UserManager
                    .Users
                    //fazendo comparação de letras maiuscula e menuscula para que compare e não de erro ao logar.
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper());
               Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);//200
            }
            return Result.Fail("Login não cadastrado");
        }
    }
}
