using System.ComponentModel.DataAnnotations;

namespace MineSweeper.WebApi.Models;

/// <summary>
/// Модель создания новой игры
/// </summary>
public class CreateNewGameRequest
{
    /// <summary>
    /// Ширина поля
    /// </summary>
    [Required]
    public required int Width { get; set; }
    
    /// <summary>
    /// Высота поля
    /// </summary>
    [Required]
    public required int Height { get; set; }
    
    /// <summary>
    /// Кол-во мин
    /// </summary>
    [Required]
    public required int Mines_Count { get; set; }
}