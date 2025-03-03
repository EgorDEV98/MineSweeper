namespace MineSweeper.Application.Models.Dtos;

/// <summary>
/// ДТО создания новой игры
/// </summary>
public class CreateNewGameDto
{
    /// <summary>
    /// Ширина поля
    /// </summary>
    public required int Width { get; set; }
    
    /// <summary>
    /// Высота поля
    /// </summary>
    public required int Height { get; set; }
    
    /// <summary>
    /// Кол-во мин
    /// </summary>
    public required int MinesCount { get; set; }
}