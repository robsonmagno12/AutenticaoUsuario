using FilmesAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesApi.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        //tera qu e explicitar que são  Lazy para trazer dados do BD colocando a palavra vitual
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }
    }
}
