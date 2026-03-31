namespace ProjectTeam01.domain.generation;

/// Типы коридоров
public enum CorridorType
{
    Horizontal, 
    Vertical 
}

/// Представляет коридор, соединяющий две комнаты
public class Corridor
{
    /// Тип коридора
    public CorridorType Type { get; set; }

    /// Ключевые точки коридора (для логики генерации)
    public List<Position> Points { get; set; }

    /// Все клетки коридора (для отрисовки и движения)
    public List<Position> Cells { get; set; }
    
    /// Маппинг клеток к сегментам (кешируется при генерации)
    public Dictionary<Position, HashSet<int>> CellToSegments { get; set; }

    public Corridor()
    {
        Points = new List<Position>();
        Cells = new List<Position>();
        CellToSegments = new Dictionary<Position, HashSet<int>>();
    }
}

