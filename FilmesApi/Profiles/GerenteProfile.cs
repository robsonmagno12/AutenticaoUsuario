using AutoMapper;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using System.Linq;

namespace FilmesApi.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreatedGerenteDto, Gerente>();
            //está mapeando gerente para readGerenteDDto e para o mebro de cinamas para genrente
            //está defininfo seguinte opções para mapear MapFrom  do gerente retorna do gerente.Cinemas
            // selecione apenas Id, Nome, Endereço , enderecoID para não aparecer os dado do gerente ao puxar postman
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, opts => opts
                .MapFrom(gerente => gerente.Cinemas.Select
                (c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId })));
        }
    }
}
