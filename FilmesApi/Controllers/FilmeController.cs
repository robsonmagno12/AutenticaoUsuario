using FilmesApi.Services;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }


        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readDto = _filmeService.AdcionaFilme(filmeDto);

            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = readDto.Id }, readDto);
        }
        //determinando faixaEtaria de Idade para filmes
        //caso for pesquisar o valor int? ClassificacaoEtaria = null para puxa todos filme ao coloca valor ele puxa
        //https://localhost:44380/filme?classificacaoEtaria=12 trazer filme com maior de 12 anos que pode assistir
        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? ClassificacaoEtaria = null)
        {
           List<ReadFilmeDto> readDto =  _filmeService.RecuperaFilmes(ClassificacaoEtaria);
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            ReadFilmeDto readDto =  _filmeService.RecuperaFilmesPorId(id);
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result resultado = _filmeService.AtualizarFilme(id, filmeDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Result resultado = _filmeService.DeletaFilme(id);
            if (resultado.IsFailed) return NotFound();

            return NoContent();
        }

    }
}