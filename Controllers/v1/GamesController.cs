using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DIO.GameCatalog.Exceptions;
using DIO.GameCatalog.Services.V1.Interfaces;
using DIO.GameCatalog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DIO.GameCatalog.Controllers.V1
{
    [ApiController]
    [Route("api/v1/{controller}")]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gameService;

        public GamesController(IGamesService gamesService)
        {
            _gameService = gamesService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada.
        /// </summary>
        /// <remarks> Não é possível retornar jogos sem paginação.</remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo é igual a 1.</param>
        /// <param name="quantidade">Indica a quantidade de jogos por página. Mímimo é 1 e máximo é 50.</param>
        /// <response code="200">Retorna a lista de jogos.</response>
        /// <response code="204">Caso não haja nenhum jogo.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<GameViewModel>>> Obter(
            [FromQuery, Range(1, int.MaxValue)] int pagina = 1,
            [FromQuery, Range(1, 50)] int quantidade = 5
        )
        {
            return Ok(await _gameService.Obter(pagina, quantidade));
        }

        /// <summary>
        /// Buscar pelo jogo com o GUID especificado.
        /// </summary>
        /// <param name="id">GUID do jogoo jogo especificado.</param>
        /// <response code="200">Retorna o jogo especificado.</response>
        /// <response code="204">Caso o jogo especificado não exista.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<GameViewModel>> ObterPorId(
            [FromRoute] Guid id
        )
        {
            return Ok(await _gameService.Obter(id));
        }

        /// <summary>
        /// Insere um novo jogo no banco de dados.
        /// </summary>
        /// <param name="game">Informações do jogo sendo inserido.</param>
        /// <response code="200">Retorna o jogo inserido.</response>
        /// <response code="422">Caso o jogo já esteja inserido.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<GameViewModel>> InserirJogo(
            [FromBody] GameViewModel game
        )
        {
            try
            {
                return Ok(await _gameService.Inserir(game));
            }
            catch(RegisteredGameException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza todas as informações do jogo especificado.
        /// </summary>
        /// <param name="id">GUID do jogo sendo atualizado.</param>
        /// <param name="game">Informações atualizadas do jogo.</param>
        /// <response code="204">Jogo atualizado</response>
        /// <response code="404">Jogo especificado não existe.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> AtualizarJogo(
            [FromRoute] Guid id,
            [FromRoute] GameViewModel game
        )
        {
            try
            {
                await _gameService.Atualizar(id, game);
                return NoContent();
            }
            catch(NotRegisteredGameException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o preço do jogo especificado.
        /// </summary>
        /// <param name="id">GUID do jogo sendo atualizado.</param>
        /// <param name="preco">Preço atualizado do jogo.</param>
        /// <response code="204">Preço do jogo atualizado</response>
        /// <response code="404">Jogo especificado não existe.</response>
        [HttpPatch("{id:guid}/preco/{preco:double}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> AtualizarJogo(
            [FromRoute] Guid id,
            [FromRoute] double preco
        )
        {
            try
            {
                await _gameService.Atualizar(id, preco);
                return NoContent();
            }
            catch(NotRegisteredGameException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deleta o jogo especificado.
        /// </summary>
        /// <param name="id">GUID do jogo sendo deletado.</param>
        /// <response code="204">Jogo deletado</response>
        /// <response code="404">Jogo especificado não existe.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeletarJogo([FromRoute] Guid id)
        {
            try
            {
                await _gameService.Remover(id);
                return NoContent();
            }
            catch(NotRegisteredGameException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
