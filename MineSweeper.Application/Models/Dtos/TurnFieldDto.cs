namespace MineSweeper.Application.Models.Dtos;

/// <summary>
/// DTO хода
/// </summary>
public class TurnFieldDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Колонока
    /// </summary>
    public required int Col { get; set; }
    
    /// <summary>
    /// Строка
    /// </summary>
    public required int Row { get; set; }
}