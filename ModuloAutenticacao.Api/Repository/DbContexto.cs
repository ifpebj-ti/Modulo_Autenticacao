using Microsoft.EntityFrameworkCore;
using ModuloAutenticacao.Api.Domain;


namespace ModuloAutenticacao.Api.Repository;

    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options):base(options) 
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Habilitar o Lazy Loading
            optionsBuilder.UseLazyLoadingProxies();
        }


        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Enderecos> Endereco { get; set; }

    }

