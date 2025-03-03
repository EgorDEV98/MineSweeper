using Microsoft.AspNetCore.Mvc;
using MineSweeper.Application.Models.Dtos;
using MineSweeper.Application.Models.Responces;

using MineSweeper.WebApi.Models;

namespace MineSweeper.WebApi.Controllers;

[ApiController]
public class MineSweeperController : ControllerBase
{
    private readonly MineSweeperService _mineSweeperService;

    public MineSweeperController(MineSweeperService mineSweeperService)
    {
        _mineSweeperService = mineSweeperService;
    }

    /// <summary>
    /// Создать новую игру
    /// </summary>
    /// <param name="request">Модель создания новой игры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("new")]
    public async Task<NewGameResponse> CreateNewGameAsync([FromBody] CreateNewGameRequest request, CancellationToken ct)
        => await _mineSweeperService.CreateNewGameAsync(new CreateNewGameDto
        {
            Width = request.Width,
            Height = request.Height,
            MinesCount = request.Mines_Count
        }, ct);


    /// <summary>
    /// Сделать ход
    /// </summary>
    /// <param name="request">Модель хода</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("turn")]
    public async Task<TurnResponse> TurnFieldAsync([FromBody] TurnFieldRequest request, CancellationToken ct)
        => await _mineSweeperService.TurnFieldAsync(new TurnFieldDto
        {
            Id = request.Id,
            Col = request.Col,
            Row = request.Row,
        }, ct);

}