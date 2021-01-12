using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zagrean_Robert_project.Models;

namespace Zagrean_Robert_project.Data
{
    public class SuplimenteContext : DbContext
    {
        public SuplimenteContext(DbContextOptions<SuplimenteContext> options) :
            base(options)
        {
        }
        public DbSet<Client> Clienti { get; set; }
        public DbSet<Comenzi> Comenzis { get; set; }
        public DbSet<Suplimente> Suplimentes { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<PublishedSuplements> PublishedSuplements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Comenzi>().ToTable("Comanda");
            modelBuilder.Entity<Suplimente>().ToTable("Supliment");
            modelBuilder.Entity<Publisher>().ToTable("Distribuitor");
            modelBuilder.Entity<PublishedSuplements>().ToTable("Suplimente distribuite");
            modelBuilder.Entity<PublishedSuplements>()
            .HasKey(c => new { c.SuplementeID, c.PublisherID });//configureaza cheia primara compusa
        }
    }
}
