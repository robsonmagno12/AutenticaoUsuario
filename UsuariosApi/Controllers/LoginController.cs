using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _LoginService;

        public LoginController(LoginService loginService)
        {
            _LoginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest request) 
        {
            //request ao logar e não tiver cadastrado sera não autorizado.
           Result resultado = _LoginService.LogaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
}
