using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MineSweeper.WebApi.Models;

/// <summary>
/// Модель хода
/// </summary>
public class TurnFieldRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Required]
    [JsonPropertyName("game_id")]
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Колонока
    /// </summary>
    [Required]
    [JsonPropertyName("col")]
    public required int Col { get; set; }
    
    /// <summary>
    /// Строка
    /// </summary>
    [Required]
    [JsonPropertyName("row")]
    public required int Row { get; set; }
}