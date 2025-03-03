using System.Text.Json.Serialization;

namespace MineSweeper.Application.Models.Responces;

/// <summary>
/// Ответ на запрос хода
/// </summary>
public class TurnResponse
{
    /// <summary>
    /// Уникальный идентификатор игры
    /// </summary>
    [JsonPropertyName("game_id")]
    public Guid GameId { get; set; }
        
    /// <summary>
    /// Ширина игрового поля
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
        
    /// <summary>
    /// Высота игрового поля
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
        
    /// <summary>
    /// Количество мин на поле
    /// </summary>
    [JsonPropertyName("mines_count")]
    public int MinesCount { get; set; }
        
    /// <summary>
    /// Игровое поле в виде двумерного массива
    /// </summary>
    [JsonPropertyName("field")]
    public List<List<string>> Fields { get; set; }
        
    /// <summary>
    /// Флаг завершения игры
    /// </summary>
    [JsonPropertyName("completed")]
    public bool IsCompleted { get; set; }
}