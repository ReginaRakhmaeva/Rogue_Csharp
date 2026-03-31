namespace ProjectTeam01.domain.Characters
{
    internal class Snake : Enemy
    {
        public const int SleepChancePercent = CharacterConstants.SNAKE_SLEEP_CHANCE_PERCENT;

        public Snake(int posX, int posY) : base(EnemyTypeEnum.Snake, posX, posY)
        {
            ActualHp = CharacterConstants.SNAKE_BASE_HP;
            BaseAgility = CharacterConstants.SNAKE_BASE_AGILITY;
            BaseStrength = CharacterConstants.SNAKE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.SNAKE_HOSTILITY_LEVEL;
        }

        public Snake(int posX, int posY, int actualHp) : base(EnemyTypeEnum.Snake, posX, posY)
        {
            ActualHp = actualHp;
            BaseAgility = CharacterConstants.SNAKE_BASE_AGILITY;
            BaseStrength = CharacterConstants.SNAKE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.SNAKE_HOSTILITY_LEVEL;
        }

    }
}
