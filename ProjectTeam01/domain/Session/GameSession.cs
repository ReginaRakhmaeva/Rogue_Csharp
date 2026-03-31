using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.generation;

namespace ProjectTeam01.domain.Session;

/// Управления игровым процессом
internal partial class GameSession
{
    private Level _currentLevel;
    private Hero _player;
    private readonly Random _random;
    private readonly LevelGenerator _levelGenerator;
    private readonly EntityGenerator _entityGenerator;
    private LevelMapQuery _mapQuery;
    private readonly GameStatistics _statistics;
    private bool _gameCompleted;
    private readonly FogOfWar _fogOfWar;

    public Level CurrentLevel => _currentLevel;
    public Hero Player => _player;
    public int CurrentLevelNumber => _currentLevel.LevelNumber;
    public GameStatistics Statistics => _statistics;
    public bool IsGameCompleted => _gameCompleted;
    public FogOfWar FogOfWar => _fogOfWar;

    private readonly Balance _balanceCalc;

    private float _difficultyFactor = 1.0f;
    public float DifficultyFactor
    { 
        get { return _difficultyFactor;}    
    }

    /// Конструктор для создания НОВОЙ игры.
    public GameSession(Level level, Hero player, int levelNumber = 1)
    {
        _currentLevel = level ?? throw new ArgumentNullException(nameof(level));
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _currentLevel.LevelNumber = levelNumber;
        _random = new Random();
        _levelGenerator = new LevelGenerator();
        _entityGenerator = new EntityGenerator();
        _mapQuery = new LevelMapQuery(_currentLevel);
        _statistics = new GameStatistics(levelNumber);
        _fogOfWar = new FogOfWar();

        if (!_currentLevel.Entities.Contains(_player))
        {
            _currentLevel.AddEntity(_player);
        }
        
        InitializeFogOfWar();
        _balanceCalc = new Balance();
        _difficultyFactor = 1.0f;
    }

    /// Конструктор для ЗАГРУЗКИ игры из сохранения.
    public GameSession(Level level, Hero player, int levelNumber, GameStatistics statistics)
    {
        _currentLevel = level ?? throw new ArgumentNullException(nameof(level));
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _statistics = statistics ?? throw new ArgumentNullException(nameof(statistics));
        _currentLevel.LevelNumber = levelNumber;
        _random = new Random();
        _levelGenerator = new LevelGenerator();
        _entityGenerator = new EntityGenerator();
        _mapQuery = new LevelMapQuery(_currentLevel);
        _fogOfWar = new FogOfWar();

        if (!_currentLevel.Entities.Contains(_player))
        {
            _currentLevel.AddEntity(_player);
        }
        _balanceCalc = new Balance();
        _difficultyFactor = _balanceCalc.Calculate(statistics);
    }

    internal void RecalculateDifficulty()
    {
        _difficultyFactor = _balanceCalc.Calculate(_statistics);
    }
    
    private void InitializeFogOfWar()
    {
        var startRoom = _currentLevel.FindRoomAt(_currentLevel.StartPosition.X, _currentLevel.StartPosition.Y);
        if (startRoom != null)
        {
            _fogOfWar.EnterRoom(startRoom, _currentLevel.StartPosition);
        }
    }
    
    internal void RestoreFogOfWar(System.Collections.Generic.HashSet<int> visitedRooms, System.Collections.Generic.HashSet<string> visitedCorridorSegments)
    {
        _fogOfWar.RestoreVisitedRooms(visitedRooms);
        _fogOfWar.RestoreVisitedCorridorSegments(visitedCorridorSegments);
        
        RestoreCorridorsBetweenVisitedRooms(visitedRooms);
        
        var currentRoom = _currentLevel.FindRoomAt(_player.Position.X, _player.Position.Y);
        if (currentRoom != null)
        {
            _fogOfWar.EnterRoom(currentRoom, _player.Position);
        }
        else
        {
            var currentCorridor = _currentLevel.FindCorridorAt(_player.Position.X, _player.Position.Y);
            if (currentCorridor != null)
            {
                int? corridorIndex = FindCorridorIndex(currentCorridor);
                if (corridorIndex.HasValue)
                {
                    _fogOfWar.EnterCorridor(currentCorridor, _player.Position, corridorIndex.Value);
                }
            }
        }
    }
    
    private void RestoreCorridorsBetweenVisitedRooms(System.Collections.Generic.HashSet<int> visitedRooms)
    {
        for (int corridorIndex = 0; corridorIndex < _currentLevel.Corridors.Count; corridorIndex++)
        {
            var corridor = _currentLevel.Corridors[corridorIndex];
            if (corridor.Points == null || corridor.Points.Count < 2)
                continue;
            
            var connectedRooms = new System.Collections.Generic.HashSet<int>();
            
            var firstPoint = corridor.Points[0];
            var lastPoint = corridor.Points[corridor.Points.Count - 1];
            
            foreach (var levelRoom in _currentLevel.Rooms)
            {
                if (levelRoom.Doors != null)
                {
                    foreach (var door in levelRoom.Doors)
                    {
                        if ((door.X != 0 || door.Y != 0))
                        {
                            if ((door.X == firstPoint.X && door.Y == firstPoint.Y) ||
                                (door.X == lastPoint.X && door.Y == lastPoint.Y))
                            {
                                connectedRooms.Add(levelRoom.Sector);
                                break;
                            }
                        }
                    }
                }
            }
            
            if (connectedRooms.Count >= 2 && connectedRooms.All(sector => visitedRooms.Contains(sector)))
            {
                for (int segmentIndex = 0; segmentIndex < corridor.Points.Count - 1; segmentIndex++)
                {
                    string segmentKey = $"{corridorIndex}_{segmentIndex}";
                    _fogOfWar.AddVisitedCorridorSegment(segmentKey);
                }
            }
        }
    }
    
    private int? FindCorridorIndex(Corridor corridor)
    {
        for (int i = 0; i < _currentLevel.Corridors.Count; i++)
        {
            if (_currentLevel.Corridors[i] == corridor)
            {
                return i;
            }
        }
        return null;
    }
}
