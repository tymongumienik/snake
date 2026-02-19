namespace Snake.Application.Models;

public readonly record struct GameConfig
{
    public uint GridSize { get; }
    public string UserName { get; }

    public GameConfig(uint gridSize, string userName)
    {
        if (!IsValidGridSize(gridSize)) throw new ArgumentOutOfRangeException(nameof(gridSize));
        if (!IsValidUserName(userName)) throw new ArgumentException(nameof(userName));

        GridSize = gridSize;
        UserName = userName;
    }

    public static bool IsValidGridSize(uint gridSize)
    {
        return gridSize >= 4 && gridSize <= 32;
    }

    public static bool IsValidUserName(string userName)
    {
        return !string.IsNullOrWhiteSpace(userName) && userName.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)) && userName.Length >= 1 && userName.Length <= 32;
    }
}
