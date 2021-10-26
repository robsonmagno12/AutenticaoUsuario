using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using FilmesAPI.Data.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Services
{
    public class GerenteService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ReadGerenteDto AdicionaGerente(CreatedGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public ReadGerenteDto RecuperaGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(filme => filme.Id == id);
            if (gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

                return gerenteDto;
            }
            return null;
        }

        internal Result DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
            {
                return Result.Fail("Cinema não encontrado");
            }
            _context.Remove(gerente);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
