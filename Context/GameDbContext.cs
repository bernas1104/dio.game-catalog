using DIO.GameCatalog.Mappings;
using DIO.GameCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace DIO.GameCatalog.Context
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new GameMapping());
        }
    }
}
