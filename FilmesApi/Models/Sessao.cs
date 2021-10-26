using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Models
{
    public class Sessao
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }
        public virtual Cinema Cinema { get; set; }
        public virtual  Filme Filme { get; set; }
        // indentificar a sessao que ela pssuir  de filme e cinema
        public  int CinemaId { get; set; }
        public int FilmeId { get; set; }
        public DateTime HorarioCerramento { get; set; }
    }
}
