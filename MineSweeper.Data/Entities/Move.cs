namespace MineSweeper.Data.Entities;

/// <summary>
/// Ход
/// </summary>
public class Move
{
    /// <summary>
    /// Уникальный идентификатор хода
    /// </summary>
    public Guid Id { get; set; }
        
    /// <summary>
    /// Идентификатор связанной игры
    /// </summary>
    public Guid GameId { get; set; }
        
    /// <summary>
    /// Строка хода
    /// </summary>
    public int Row { get; set; }
        
    /// <summary>
    /// Колонка хода
    /// </summary>
    public int Column { get; set; }
        
    /// <summary>
    /// Дата и время выполнения хода
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
        
    /// <summary>
    /// Связанная игра
    /// </summary>
    public Game Game { get; set; }
}