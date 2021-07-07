using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.GameCatalog.Models;

namespace DIO.GameCatalog.Repositories.Interfaces
{
    public interface IGamesRepository : IDisposable
    {
        Task<List<Game>> Obter(int pagina, int quantidade);
        Task<Game> Obter(Guid id);
        Task<List<Game>> Obter(string nome, string produtora);
        Task Inserir(Game game);
        Task Atualizar(Game game);
        Task Remover(Guid id);
    }
}
