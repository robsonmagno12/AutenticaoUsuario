using FilmesApi.Models;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        //explicita na hora da executação, queremos fazer modificação
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //para minha entidade endereço  dizer que possuir entidade cime com . HasOne e
            //ele possuir o cinema que possuir endereço  WithOne
            //referencia que cinema tem o enderecoID nela e que é chave estrangeira
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Endereco)
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);
            //O gerente terá muitos ou nenhum cinema utiliza WithMany
            //OnDelete utilizando para não fazer delete em cascata, pois ao apagar gerente apaga filme e cinema
            //definir o tipo de deletação para Rrstrict .OnDelete(DeleteBehavior.Restrict)
            // também pode fazer com HasForeignKey(cinema => cinema.GerenteId.IsRequired(false) para que a chave do gerente seja null e criar o cinema
            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId);
            // .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sessao>()
             .HasOne(sessao => sessao.Filme)
             .WithMany(filme => filme.Sessaos)
             .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<Sessao>()
             .HasOne(sessao => sessao.Cinema)
             .WithMany(cinema => cinema.Sessaos)
             .HasForeignKey(sessao => sessao.CinemaId);

        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

    }
}