using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService;


        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }
        [HttpPost]
        public IActionResult AdicionaGerente(CreatedGerenteDto Dto)
        {
            ReadGerenteDto readDto = _gerenteService.AdicionaGerente(Dto);     
            return CreatedAtAction(nameof(RecuperaGerentePorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id) 
        {
            ReadGerenteDto readDto = _gerenteService.RecuperaGerentePorId(id);
           
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
            Result resultado = _gerenteService.DeletaGerente(id);
            if (resultado.IsFailed) return NotFound();
           
            return NoContent();
        }
    }
}
