using Snake.Application.Core;
using Snake.Application.Models;
using Snake.Tests.Helpers;

namespace Snake.Tests;

public class ManagerTests
{
    [Fact]
    public void EndGame_SetsGameActiveToFalse()
    {
        var manager = new GameManager(new TestDataRepository());
        var game = manager.StartNewGame(new GameConfig(gridSize: 16, userName: "Test"));

        manager.EndGame(game.Id);

        Assert.False(game.Active);
    }

    [Fact]
    public void EndGame_RemovesGameFromManager()
    {
        var manager = new GameManager(new TestDataRepository());
        var game = manager.StartNewGame(new GameConfig(gridSize: 16, userName: "Test"));

        manager.EndGame(game.Id);

        Assert.Null(manager.GetGame(game.Id));
    }
}
