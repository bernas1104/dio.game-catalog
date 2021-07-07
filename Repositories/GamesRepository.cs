using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DIO.GameCatalog.Context;
using DIO.GameCatalog.Models;
using DIO.GameCatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DIO.GameCatalog.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly GameDbContext _dbContext;
        private bool _disposed = false;

        public GamesRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Game>> Obter(int pagina, int quantidade)
        {
            return await _dbContext.Games.FromSqlInterpolated(
                $"SELECT * FROM games ORDER BY id OFFSET {((pagina - 1) * quantidade)} ROWS FETCH NEXT {quantidade} ROWS ONLY"
            ).ToListAsync();
        }

        public async Task<Game> Obter(Guid id)
        {
            return await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Game>> Obter(string nome, string produtora)
        {
            return await _dbContext.Games.Where(
                    x => (x.Nome == nome) && (x.Produtora == produtora)
                )
                .ToListAsync();
        }

        public async Task Inserir(Game game)
        {
            await _dbContext.Games.AddAsync(game);
            await SaveChanges();
        }

        public async Task Atualizar(Game game)
        {
            _dbContext.Games.Update(game);
            await SaveChanges();
        }

        public async Task Remover(Guid id)
        {
            var game = await _dbContext.Games.FirstAsync(x => x.Id == id);

            _dbContext.Games.Remove(game);

            await SaveChanges();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext?.Dispose();
            }

            _disposed = true;
        }

        private async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    }
}
