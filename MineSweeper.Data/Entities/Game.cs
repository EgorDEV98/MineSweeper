namespace MineSweeper.Data.Entities;

/// <summary>
/// Игра
/// </summary>
public class Game
{
    /// <summary>
    /// Уникальный идентификатор игры
    /// </summary>
    public Guid Id { get; set; }
        
    /// <summary>
    /// Ширина игрового поля
    /// </summary>
    public int Width { get; set; }
        
    /// <summary>
    /// Высота игрового поля
    /// </summary>
    public int Height { get; set; }
        
    /// <summary>
    /// Количество мин на поле
    /// </summary>
    public int MineCount { get; set; }
        
    /// <summary>
    /// Признак завершенности игры
    /// </summary>
    public bool IsCompleted { get; set; }
        
    /// <summary>
    /// Дата и время создания игры
    /// </summary>
    public DateTimeOffset CreatedDate { get; set; }
        
    /// <summary>
    /// Дата и время завершения игры (если завершена)
    /// </summary>
    public DateTimeOffset? EndDate { get; set; }
        
    /// <summary>
    /// Дата и время последнего обновления игры
    /// </summary>
    public DateTimeOffset LastUpdate { get; set; }
        
    /// <summary>
    /// Список ячеек игрового поля
    /// </summary>
    public List<Cell> Cells { get; set; } = new();
        
    /// <summary>
    /// Список ходов, сделанных в игре
    /// </summary>
    public List<Move> Moves { get; set; } = new();
}