using ProjectTeam01.datalayer.Mappers;
using ProjectTeam01.datalayer.Models;
using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.generation;
using ProjectTeam01.domain.Items;
using ProjectTeam01.domain.Session;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectTeam01.datalayer
{
    internal class GameDataService
    {
        public static GameSave CreateSave(Hero hero, List<Enemy> enemies, List<Item> items, Level level, GameStatistics statistics, FogOfWar fogOfWar)
        {
            var save = new GameSave
            {
                Hero = HeroMapper.ToSave(hero),
                Enemies = enemies.Select(e => EnemyMapper.ToSave(e)).ToList(),
                Items = items.Select(i => ItemMapper.ToSave(i)).ToList(),
                Level = LevelMapper.ToSave(level),
                Statistics = GameStatisticsMapper.ToSave(statistics),
                GameLevel = level.LevelNumber,
                VisitedRooms = fogOfWar.GetVisitedRooms().ToList(),
                VisitedCorridorSegments = fogOfWar.GetVisitedCorridorSegments().ToList()
            };
            return save;
        }

        public static void SaveToFile(GameSave save, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string jsonString = JsonSerializer.Serialize(save, options);
            File.WriteAllText(filePath, jsonString);
        }

        private static void LoadFromFile(string filePath, out GameSave save)
        {
            string jsonString = File.ReadAllText(filePath);
            save = JsonSerializer.Deserialize<GameSave>(jsonString) ?? throw new Exception("Failed to deserialize game save.");
        }

        internal static void LoadSave(string filePath, out Hero hero, out List<Enemy> enemies, out List<Item> items, out Level level, out GameStatistics statistics)
        {
            LoadFromFile(filePath, out GameSave gameSave);
            hero = HeroMapper.FromSave(gameSave.Hero);
            enemies = gameSave.Enemies.Select(es => EnemyMapper.FromSave(es)).ToList();
            items = gameSave.Items.Select(isave => ItemMapper.FromSave(isave)).ToList();
            level = LevelMapper.FromSave(gameSave.Level);
            statistics = GameStatisticsMapper.FromSave(gameSave.Statistics);
        }

        public static GameSession LoadGame(string filePath)
        {
            Hero hero;
            List<Enemy> enemies;
            List<Item> items;
            Level level;
            GameStatistics statistics;

            LoadSave(filePath, out hero, out enemies, out items, out level, out statistics);

            level.AddEntity(hero);
            foreach (var enemy in enemies)
            {
                level.AddEntity(enemy);
            }
            foreach (var item in items)
            {
                level.AddEntity(item);
            }

            var session = new GameSession(level, hero, level.LevelNumber, statistics);

            LoadFromFile(filePath, out GameSave gameSave);
            var visitedRooms = new System.Collections.Generic.HashSet<int>();
            if (gameSave.VisitedRooms != null && gameSave.VisitedRooms.Count > 0)
            {
                visitedRooms = new System.Collections.Generic.HashSet<int>(gameSave.VisitedRooms);
            }

            var visitedCorridorSegments = new System.Collections.Generic.HashSet<string>();
            if (gameSave.VisitedCorridorSegments != null && gameSave.VisitedCorridorSegments.Count > 0)
            {
                visitedCorridorSegments = new System.Collections.Generic.HashSet<string>(gameSave.VisitedCorridorSegments);
            }

            session.RestoreFogOfWar(visitedRooms, visitedCorridorSegments);

            return session;
        }

        public static void AddAttemptToScoreboard(GameStatistics statistics, string scoreboardPath)
        {
            ScoreboardSave scoreboard;
            if (File.Exists(scoreboardPath))
            {
                string jsonString = File.ReadAllText(scoreboardPath);
                scoreboard = JsonSerializer.Deserialize<ScoreboardSave>(jsonString) ?? new ScoreboardSave();
            }
            else
            {
                scoreboard = new ScoreboardSave();
            }

            var attemptSave = GameStatisticsMapper.ToSave(statistics);
            scoreboard.SessionStats.Add(attemptSave);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string updatedJson = JsonSerializer.Serialize(scoreboard, options);
            File.WriteAllText(scoreboardPath, updatedJson);
        }

        public static ScoreboardSave LoadScoreboard(string scoreboardPath)
        {
            if (!File.Exists(scoreboardPath))
            {
                return new ScoreboardSave();
            }

            string jsonString = File.ReadAllText(scoreboardPath);
            return JsonSerializer.Deserialize<ScoreboardSave>(jsonString) ?? new ScoreboardSave();
        }

        public static List<GameStatisticsSave> GetTopAttempts(string scoreboardPath, int count = 0)
        {
            var scoreboard = LoadScoreboard(scoreboardPath);

            var sorted = scoreboard.SessionStats
                .OrderByDescending(s => s.TreasuresCollected)
                .ToList();

            if (count > 0 && count < sorted.Count)
            {
                return sorted.Take(count).ToList();
            }

            return sorted;
        }
    }
}
