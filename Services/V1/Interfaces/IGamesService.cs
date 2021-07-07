using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.GameCatalog.ViewModels;

namespace DIO.GameCatalog.Services.V1.Interfaces
{
    public interface IGamesService : IDisposable
    {
        Task<List<GameViewModel>> Obter(int pagina, int quantidade);
        Task<GameViewModel> Obter(Guid id);
        Task<GameViewModel> Inserir(GameViewModel game);
        Task Atualizar(Guid id, GameViewModel game);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
