namespace ProjectTeam01.domain.Combat;

internal static class BattleConstants
{
    // Шанс попадания
    public const int INITIAL_HIT_CHANCE = 70;        // Базовый шанс попадания (%)
    public const int STANDARD_AGILITY = 50;          // Стандартная ловкость
    public const double AGILITY_FACTOR = 0.3;       // Множитель ловкости

    // Лут
    public const double LOOT_AGILITY_FACTOR = 0.2;   // Множитель ловкости для лута
    public const double LOOT_HP_FACTOR = 0.5;        // Множитель здоровья для лута
    public const double LOOT_STRENGTH_FACTOR = 0.5;  // Множитель силы для лута
    public const int LOOT_RANDOM_MAX = 20;           // Максимальное случайное значение для лута

    // Вампир
    public const int MAX_HP_PART = 10;               // Делитель для урона вампира (max_hp / 10)
}

