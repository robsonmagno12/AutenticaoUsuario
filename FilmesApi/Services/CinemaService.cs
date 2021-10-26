using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Services
{
    public class CinemaService
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public  ReadCinemaDto AdicionarCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public  List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            List<Cinema> cinemas = _context.Cinemas.ToList();
            if (cinemas == null)
            {
                //se a lista de cinema estiver vazia retornar NotFound
                // não temos acesso a NoCOntent e OK retorno para usuario no Services
                return null;
            }
            if (!String.IsNullOrEmpty(nomeDoFilme))
            {
                //efetuar uma consultar se não for vaziar
                //query está recebendo um cinema da lista do cinema, dado um condição where
                // (cinema.Sessaos.Any(sessao => 
                //  sessao.Filme.Titulo == nomeDoFilme) )que o cinema tenha uma sessão quais quer  e seja ingual 
                // o nome do filme que está passando no cinema
                IEnumerable<Cinema> query = from cinema in cinemas
                                            where cinema.Sessaos.Any(sessao =>
                                            sessao.Filme.Titulo == nomeDoFilme)
                                            select cinema;
                //List<Cinema> cinemas = _context.Cinemas.ToList(); sera substituido por  cinemas = query.ToList();
                cinemas = query.ToList();
            }
            //_mapper.Map quero mapear para a lista de cinema
            return  _mapper.Map<List<ReadCinemaDto>>(cinemas);

           
        }
      
        public ReadCinemaDto RecuperaCinemasPorId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return cinemaDto;
            }
            return null;
        }

        public Result AtualizarCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return Result.Ok();
        }


        public Result DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }
            _context.Remove(cinema);
            _context.SaveChanges(); 
            return Result.Ok();
        }
    }
}
