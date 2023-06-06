using ConsoleAppEntity.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntity
{
    public class Contexto : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Email> Emails { get; set; }

        public Contexto()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["EntityAtos"];

            string retorno = "";

            if(settings != null)
            {
                retorno = settings.ConnectionString;
            }

            optionsBuilder.UseSqlServer(retorno);

            optionsBuilder.UseLazyLoadingProxies();// se eu tenho um atributo de uma outra classe. Ele tras atributos que tem conexão entre eles.

            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasOne(e => e.pessoa)
                .WithMany(e => e.emails)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
