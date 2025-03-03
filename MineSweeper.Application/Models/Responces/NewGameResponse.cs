using System.Text.Json.Serialization;

namespace MineSweeper.Application.Models.Responces;

/// <summary>
/// Ответ создания новой игры
/// </summary>
public class NewGameResponse
{
    /// <summary>
    /// Идентификатор игры
    /// </summary>
    [JsonPropertyName("game_id")]
    public Guid GameId { get; set; }
    
    /// <summary>
    /// Ширина поля
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    /// <summary>
    /// Высота поля
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
    
    /// <summary>
    /// Кол-во мин
    /// </summary>
    [JsonPropertyName("mines_count")]
    public int MinesCount { get; set; }
    
    /// <summary>
    /// Признак окончания игры
    /// </summary>
    [JsonPropertyName("completed")]
    public bool IsCompleted { get; set; }

    [JsonPropertyName("field")]
    public List<List<string>> Fields { get; set; }
    
}