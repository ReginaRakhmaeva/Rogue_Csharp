namespace ProjectTeam01.domain.Effects
{
    internal static class EffectConstants
    {
        // Длительность эффектов (в тиках)
        public const int SLEEP_DURATION = 1;           // Длительность эффекта сна
        public const int DEFAULT_EFFECT_DURATION = 5;   // Длительность остальных эффектов

        // Значения эффектов
        public const int BUFF_STRENGTH_VALUE = 5;       // Значение баффа силы
        public const int BUFF_AGILITY_VALUE = 5;        // Значение баффа ловкости
        public const int BUFF_MAX_HP_VALUE = 10;        // Значение баффа максимального здоровья
        public const int DEFAULT_EFFECT_VALUE = 0;       // Значение по умолчанию
    }
}
