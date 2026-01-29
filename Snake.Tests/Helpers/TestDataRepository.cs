using Snake.Application.Models;
using Snake.Application.Repositories;

namespace Snake.Tests.Helpers
{
    public class TestDataRepository : IDataRepository
    {
        public List<GameResult> SavedResults { get; } = [];

        public void SaveGameResult(GameResult result)
        {
            SavedResults.Add(result);
        }
    }
}
