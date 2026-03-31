namespace ProjectTeam01.domain.generation;

/// Константы для генерации уровней
public static class GenerationConstants
{
    // Сетка комнат
    public const int ROOMS_PER_SIDE = 3;
    public const int MAX_ROOMS_NUMBER = ROOMS_PER_SIDE * ROOMS_PER_SIDE;
    public const int MAX_CORRIDORS_NUMBER = 12;

    // Размеры карты
    public const int MAP_HEIGHT = 30;
    public const int MAP_WIDTH = 90;

    // Размеры регионов (каждая секция 3x3)
    public const int SECTOR_HEIGHT = MAP_HEIGHT / ROOMS_PER_SIDE;
    public const int SECTOR_WIDTH = MAP_WIDTH / ROOMS_PER_SIDE;

    // Размеры комнат
    public const int MIN_ROOM_WIDTH = 6;
    public const int MAX_ROOM_WIDTH = SECTOR_WIDTH - ROOM_BORDER_OFFSET;
    public const int MIN_ROOM_HEIGHT = 5;
    public const int MAX_ROOM_HEIGHT = SECTOR_HEIGHT - ROOM_BORDER_OFFSET;
    public const int ROOM_BORDER_OFFSET = 2;  // Отступ от границ сектора для комнат
}
