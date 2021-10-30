using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]

    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;


        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
            
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
        {
            //500 erro interno
            //Todo chamar o service
            Result resultado = _cadastroService.CadastroUsuario(createDto);
            if (resultado.IsFailed) return StatusCode(500);
           
            return Ok();
        }
    }
}
