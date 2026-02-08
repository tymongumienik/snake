using Snake.Application.Core;
using Snake.Application.Models;
using System.Globalization;

namespace Snake.Tests;

public class GameInstanceTests
{
    [Fact]
    public void Render_ReturnsCorrectCellCount()
    {
        var game = new GameInstance(new GameConfig(8, "Player1"), new Random(42));
        var rendered = game.Render();

        // count graphemes instead of string length (otherwise emojis mess p)
        var renderedWithoutNewlines = rendered.Replace("\r", "").Replace("\n", "");

        Assert.Equal(8 * 8, new StringInfo(renderedWithoutNewlines).LengthInTextElements);
    }

    [Fact]
    public void Constructor_InitializesRemainingCellsAsEmpty()
    {
        var game = new GameInstance(new GameConfig(8, "Player1"), new Random(42));
        var rendered = game.Render();

        var emptyCount = 0;
        var charEnum = StringInfo.GetTextElementEnumerator(rendered);

        while (charEnum.MoveNext())
        {
            string grapheme = charEnum.GetTextElement();
            if (grapheme == GameCell.Empty.ToSymbol())
            {
                ++emptyCount;
            }
        }

        // one is a player, one is food
        Assert.Equal(8 * 8 - 2, emptyCount);
    }

    [Fact]
    public void Id_IsUnique()
    {
        var game1 = new GameInstance(new GameConfig(8, "Player1"), new Random(42));
        var game2 = new GameInstance(new GameConfig(8, "Player2"), new Random(42));

        Assert.NotEqual(game1.Id, game2.Id);
    }

    [Fact]
    public void Score_StartsAtZero()
    {
        var game = new GameInstance(new GameConfig(8, "Player1"), new Random(42));
        Assert.Equal(0, game.Score);
    }

    [Fact]
    public void Render_OutputHasCorrectRowCount()
    {
        var game = new GameInstance(new GameConfig(8, "Player1"), new Random(42));
        var renderedWithoutNewlines = new string([.. game.Render().Where(c => c != '\r' && c != '\n')]);

        var length = new StringInfo(renderedWithoutNewlines).LengthInTextElements;

        Assert.Equal(8 * 8, length);
    }
}
