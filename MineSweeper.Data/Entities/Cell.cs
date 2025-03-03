namespace MineSweeper.Data.Entities;

/// <summary>
/// Информация о ячейке
/// </summary>
public class Cell
{
    /// <summary>
    /// Уникальный идентификатор ячейки
    /// </summary>
    public Guid Id { get; set; }
        
    /// <summary>
    /// Идентификатор связанной игры
    /// </summary>
    public Guid GameId { get; set; }
        
    /// <summary>
    /// Строка расположения ячейки (нумерация с 0)
    /// </summary>
    public int Row { get; set; }
        
    /// <summary>
    /// Колонка расположения ячейки (нумерация с 0)
    /// </summary>
    public int Column { get; set; }
        
    /// <summary>
    /// Признак наличия мины в ячейке
    /// </summary>
    public bool HasMine { get; set; }
        
    /// <summary>
    /// Количество мин в соседних ячейках
    /// </summary>
    public int AdjacentMines { get; set; }
        
    /// <summary>
    /// Признак того, что ячейка открыта
    /// </summary>
    public bool IsRevealed { get; set; }
        
    /// <summary>
    /// Связанная игра
    /// </summary>
    public Game Game { get; set; }
}