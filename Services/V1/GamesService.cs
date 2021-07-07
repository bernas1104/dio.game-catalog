using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DIO.GameCatalog.Exceptions;
using DIO.GameCatalog.Models;
using DIO.GameCatalog.Repositories.Interfaces;
using DIO.GameCatalog.Services.V1.Interfaces;
using DIO.GameCatalog.ViewModels;

namespace DIO.GameCatalog.Services.V1
{
    public class GamesService : IGamesService
    {
        private readonly IGamesRepository _gamesRepository;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public GamesService(IGamesRepository gamesRepository, IMapper mapper)
        {
            _gamesRepository = gamesRepository;
            _mapper = mapper;
        }

        public async Task<List<GameViewModel>> Obter(int pagina, int quantidade)
        {
            var games = await _gamesRepository.Obter(pagina, quantidade);
            return _mapper.Map<List<GameViewModel>>(games);
        }

        public async Task<GameViewModel> Obter(Guid id)
        {
            return _mapper.Map<GameViewModel>(await _gamesRepository.Obter(id));
        }

        public async Task<GameViewModel> Inserir(GameViewModel game)
        {
            var gameExiste = await _gamesRepository.Obter(
                game.Nome,
                game.Produtora
            );

            if (gameExiste.Count > 0)
            {
                throw new RegisteredGameException();
            }

            var novoGame = _mapper.Map<Game>(game);
            novoGame.Id = Guid.NewGuid();

            await _gamesRepository.Inserir(novoGame);

            return _mapper.Map<GameViewModel>(novoGame);
        }

        public async Task Atualizar(Guid id, GameViewModel game)
        {
            var gameExiste = await BuscarGame(id);

            var gameAtualizado = _mapper.Map<Game>(game);
            gameAtualizado.Id = id;

            await _gamesRepository.Atualizar(gameAtualizado);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var gameExiste = await BuscarGame(id);

            gameExiste.Preco = preco;

            await _gamesRepository.Atualizar(gameExiste);
        }

        public async Task Remover(Guid id)
        {
            var gameExiste = await BuscarGame(id);

            await _gamesRepository.Remover(id);
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
                _gamesRepository?.Dispose();
            }

            _disposed = true;
        }

        private async Task<Game> BuscarGame(Guid id)
        {
            var game = await _gamesRepository.Obter(id);

            if (game == null)
            {
                throw new NotRegisteredGameException();
            }

            return game;
        }
    }
}
