using MineSweeper.Application.Interfaces;
using MineSweeper.Application.Models.Dtos;
using MineSweeper.Application.Models.Responces;
using MineSweeper.Data.Entities;

/// <summary>
/// Сервис для управления игрой "Сапёр" без использования базы данных
/// </summary>
public class MineSweeperService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private const int MaxWidth = 30;
    private const int MaxHeight = 30;
    private static readonly Dictionary<Guid, Game> _games = new();

    public MineSweeperService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    /// <summary>
    /// Создание новой игры
    /// </summary>
    public async Task<NewGameResponse> CreateNewGameAsync(CreateNewGameDto dto, CancellationToken ct)
    {
        ValidateField(dto.Height, dto.Width, dto.MinesCount);
        var currentTime = _dateTimeProvider.GetCurrentDateTime();

        var game = new Game
        {
            Id = Guid.NewGuid(),
            Width = dto.Width,
            Height = dto.Height,
            MineCount = dto.MinesCount,
            CreatedDate = currentTime,
            LastUpdate = currentTime,
            IsCompleted = false,
            Cells = GenerateGameField(dto.Width, dto.Height, dto.MinesCount)
        };

        _games[game.Id] = game;

        return new NewGameResponse()
        {
            GameId = game.Id,
            Width = game.Width,
            Height = game.Height,
            MinesCount = game.MineCount,
            Fields = ConvertFieldToResponse(game.Cells, game.Width, game.Height),
            IsCompleted = game.IsCompleted,
        };
    }

    /// <summary>
    /// Проверка корректности параметров игры
    /// </summary>
    /// <param name="height">Высота поля</param>
    /// <param name="width">Ширина поля</param>
    /// <param name="minesCount">Количество мин</param>
    private void ValidateField(int height, int width, int minesCount)
    {
        if (width < 1 || width > MaxWidth)
            throw new ArgumentException($"Width must be between 1 and {MaxWidth}.");
        if (height < 1 || height > MaxHeight)
            throw new ArgumentException($"Height must be between 1 and {MaxHeight}.");
        if (minesCount < 1 || minesCount >= height * width)
            throw new ArgumentException("Mines count must be at least 1 and less than total cells count.");
    }

    /// <summary>
    /// Обработка хода игрока
    /// </summary>
    public async Task<TurnResponse> TurnFieldAsync(TurnFieldDto dto, CancellationToken ct)
    {
        if (!_games.TryGetValue(dto.Id, out var game))
            throw new ArgumentException("Game not found");

        if (game.IsCompleted)
            throw new InvalidOperationException("Game is already completed");

        var cell = game.Cells.FirstOrDefault(c => c.Row == dto.Row && c.Column == dto.Col);
        if (cell == null)
            throw new ArgumentException("Invalid move");
        if (cell.IsRevealed)
            throw new ArgumentException("Cell is already revealed");

        cell.IsRevealed = true;
        if (cell.HasMine)
        {
            game.IsCompleted = true;
            RevealAllMines(game);
        }
        else if (cell.AdjacentMines == 0)
        {
            RevealEmptyCells(game, dto.Row, dto.Col);
        }

        game.LastUpdate = _dateTimeProvider.GetCurrentDateTime();

        return new TurnResponse()
        {
            GameId = game.Id,
            Width = game.Width,
            Height = game.Height,
            MinesCount = game.MineCount,
            Fields = ConvertFieldToResponse(game.Cells, game.Width, game.Height),
            IsCompleted = game.IsCompleted,
        };
    }

    /// <summary>
    /// Генерация игрового поля с минами
    /// </summary>
    private List<Cell> GenerateGameField(int width, int height, int minesCount)
    {
        var cells = new List<Cell>();
        var mines = new bool[height, width];
        var rand = new Random();
        int placedMines = 0;

        while (placedMines < minesCount)
        {
            int row = rand.Next(height);
            int col = rand.Next(width);
            if (!mines[row, col])
            {
                mines[row, col] = true;
                placedMines++;
            }
        }

        for (int r = 0; r < height; r++)
        {
            for (int c = 0; c < width; c++)
            {
                cells.Add(new Cell
                {
                    Id = Guid.NewGuid(),
                    Row = r,
                    Column = c,
                    HasMine = mines[r, c],
                    AdjacentMines = CountAdjacentMines(mines, r, c),
                    IsRevealed = false
                });
            }
        }
        return cells;
    }
    
    /// <summary>
    /// Подсчет количества мин вокруг ячейки
    /// </summary>
    private int CountAdjacentMines(bool[,] mines, int row, int col)
    {
        int count = 0;
        int height = mines.GetLength(0);
        int width = mines.GetLength(1);

        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                if (dr == 0 && dc == 0) continue; // Пропускаем саму ячейку
                int nr = row + dr, nc = col + dc;
                if (nr >= 0 && nr < height && nc >= 0 && nc < width && mines[nr, nc])
                {
                    count++;
                }
            }
        }
        return count;
    }
    
    /// <summary>
    /// Конвертация игрового поля в удобочитаемый формат (двумерный массив строк)
    /// </summary>
    private List<List<string>> ConvertFieldToResponse(List<Cell> cells, int width, int height)
    {
        var field = new List<List<string>>(height);
        for (int r = 0; r < height; r++)
        {
            var row = new List<string>(width);
            for (int c = 0; c < width; c++)
            {
                row.Add(" ");
            }
            field.Add(row);
        }

        foreach (var cell in cells)
        {
            field[cell.Row][cell.Column] = cell.IsRevealed
                ? (cell.HasMine ? "X" : cell.AdjacentMines.ToString())
                : " ";
        }

        return field;
    }

    
    /// <summary>
    /// Раскрытие всех мин после проигрыша
    /// </summary>
    private void RevealAllMines(Game game)
    {
        foreach (var cell in game.Cells.Where(c => c.HasMine))
        {
            cell.IsRevealed = true;
        }
    }

    /// <summary>
    /// Раскрытие пустых ячеек при открытии ячейки без мин
    /// </summary>
    private void RevealEmptyCells(Game game, int row, int col)
    {
        var queue = new Queue<Cell>();
        var visited = new HashSet<Cell>();
        var startCell = game.Cells.First(c => c.Row == row && c.Column == col);
        queue.Enqueue(startCell);
        visited.Add(startCell);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            current.IsRevealed = true;

            if (current.AdjacentMines == 0)
            {
                foreach (var neighbor in GetNeighbors(game, current.Row, current.Column))
                {
                    if (!visited.Contains(neighbor) && !neighbor.HasMine)
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Получение соседних ячеек
    /// </summary>
    private IEnumerable<Cell> GetNeighbors(Game game, int row, int col)
    {
        var directions = new (int, int)[]
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1)
        };

        foreach (var (dr, dc) in directions)
        {
            var neighbor = game.Cells.FirstOrDefault(c => c.Row == row + dr && c.Column == col + dc);
            if (neighbor != null)
                yield return neighbor;
        }
    }
}