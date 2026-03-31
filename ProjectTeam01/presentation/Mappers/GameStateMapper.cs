using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.generation;
using ProjectTeam01.domain.Items;
using ProjectTeam01.domain.Session;
using ProjectTeam01.presentation.ViewModels;

namespace ProjectTeam01.presentation.Mappers;

internal static class GameStateMapper
{
    public static GameStateViewModel ToViewModel(GameState gameState)
    {
        return new GameStateViewModel
        {
            Player = ToPlayerViewModel(gameState),
            Enemies = gameState.Enemies.Select(ToEnemyViewModel).ToList(),
            Items = gameState.Items.Select(ToItemViewModel).ToList(),
            Level = ToLevelViewModel(gameState.LevelGeometry, gameState.FogOfWar),
            CurrentLevelNumber = gameState.CurrentLevelNumber,

            InventoryWeapons = gameState.PlayerWeapons.Select(ToInventoryItemViewModel).ToList(),
            InventoryFood = gameState.PlayerFood.Select(ToInventoryItemViewModel).ToList(),
            InventoryElixirs = gameState.PlayerElixirs.Select(ToInventoryItemViewModel).ToList(),
            InventoryScrolls = gameState.PlayerScrolls.Select(ToInventoryItemViewModel).ToList(),
            TotalGold = gameState.TotalGold
        };
    }

    private static PlayerViewModel ToPlayerViewModel(GameState gameState)
    {
        return new PlayerViewModel
        {
            Position = gameState.PlayerPosition,        
            Health = gameState.PlayerHealth,            
            MaxHealth = gameState.PlayerMaxHealth,     
            Agility = gameState.PlayerAgility,          
            Strength = gameState.PlayerStrength,        
            IsSleep = gameState.PlayerIsSleep          
        };
    }

    private static EnemyViewModel ToEnemyViewModel(Enemy enemy)
    {
        var viewModel = new EnemyViewModel
        {
            Position = enemy.Position,     
            EnemyType = enemy.EnemyType,        
            IsDead = enemy.IsDead,             
            IsTriggered = enemy.IsTriggered      
        };

        if (enemy is Mimic mimic)
        {
            viewModel.MimicRepresentation = mimic.Representation;
        }

        if (enemy is Ghost ghost)
        {
            viewModel.IsInvisible = ghost.IsInvisible;
        }

        return viewModel;
    }

    private static ItemViewModel ToItemViewModel(Item item)
    {
        var viewModel = new ItemViewModel
        {
            Position = item.Position,    
            Type = item.Type           
        };

        switch (item)
        {
            case Weapon weapon:
                viewModel.WeaponType = weapon.WeaponType;           
                viewModel.StrengthBonus = weapon.StrengthBonus;
                break;

            case Food food:
                viewModel.HealthValue = food.HealthValue;
                break;    

            case Elixir elixir:
                viewModel.ElixirType = elixir.ElixirType;      
                break;

            case Scroll scroll:
                viewModel.ScrollType = scroll.ScrollType;
                break;

        }

        return viewModel;
    }


    private static InventoryItemViewModel ToInventoryItemViewModel(Item item)
    {
        var viewModel = new InventoryItemViewModel
        {
            Type = item.Type  
        };

        switch (item)
        {
            case Weapon weapon:
                viewModel.WeaponType = weapon.WeaponType;           
                viewModel.StrengthBonus = weapon.StrengthBonus;
                viewModel.DisplayName =
                $"Weapon: {weapon.WeaponType} +{weapon.StrengthBonus} Str";      
                break;

            case Food food:
                viewModel.HealthValue = food.HealthValue;
                viewModel.DisplayName = $"Food: + {food.HealthValue} HP";
                break;

            case Elixir elixir:
                viewModel.ElixirType = elixir.ElixirType; 
                viewModel.DisplayName = $"Elixir: {elixir.ElixirType}";
                break;

            case Scroll scroll:
                viewModel.ScrollType = scroll.ScrollType;
                viewModel.DisplayName = $"Scroll: {scroll.ScrollType}"; 
                break;

            case Treasure treasure:
                viewModel.Price = treasure.Price; 
                break;
        }

        return viewModel;
    }

    private static LevelViewModel ToLevelViewModel(Level level, FogOfWar fogOfWar)
    {
        return new LevelViewModel
        {
            Rooms = level.Rooms.Select(room => ToRoomViewModel(room, fogOfWar)).ToList(),
            Corridors = level.Corridors.Select((corridor, index) => ToCorridorViewModel(corridor, fogOfWar, index)).ToList(),
            ExitPosition = level.ExitPosition,                             
            StartPosition = level.StartPosition,                             
            LevelNumber = level.LevelNumber                               
        };
    }

    private static RoomViewModel ToRoomViewModel(Room room, FogOfWar fogOfWar)
    {
        int sector = room.Sector;
        bool isVisited = fogOfWar.IsRoomVisited(sector);
        bool isCurrentlyInRoom = fogOfWar.IsPlayerInRoom(sector);

        Dictionary<Position, bool> visibilityMap = new();
        if (isCurrentlyInRoom)
        {
            for (int y = room.TopLeft.Y; y <= room.BottomRight.Y; y++)
            {
                for (int x = room.TopLeft.X; x <= room.BottomRight.X; x++)
                {
                    var pos = new Position(x, y);
                    visibilityMap[pos] = fogOfWar.IsPositionVisible(x, y);
                }
            }
        }

        return new RoomViewModel
        {
            TopLeft = room.TopLeft,              
            BottomRight = room.BottomRight,    
            Doors = room.Doors.Where(d => d.X != 0 || d.Y != 0).ToArray(),
            IsStartRoom = room.IsStartRoom,  
            IsEndRoom = room.IsEndRoom,        
            Sector = sector,                    
            IsVisited = isVisited,            
            IsCurrentlyInRoom = isCurrentlyInRoom, 
            VisibilityMap = visibilityMap      
        };
    }

    private static CorridorViewModel ToCorridorViewModel(Corridor corridor, FogOfWar fogOfWar, int corridorIndex)
    {
        Dictionary<int, bool> segmentVisibility = new();

        if (corridor.Points != null && corridor.Points.Count >= 2)
        {
            for (int i = 0; i < corridor.Points.Count - 1; i++)
            {
                bool isVisible = fogOfWar.IsCorridorSegmentVisible(corridorIndex, i);
                segmentVisibility[i] = isVisible;
            }
        }

        return new CorridorViewModel
        {
            Type = corridor.Type,                    
            Cells = corridor.Cells?.ToList() ?? new List<Position>(), 
            Points = corridor.Points?.ToList() ?? new List<Position>(), 
            SegmentVisibility = segmentVisibility,  
            CellToSegments = corridor.CellToSegments ?? new Dictionary<Position, HashSet<int>>() 
        };
    }
}

