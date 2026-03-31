namespace ProjectTeam01.domain.Characters
{
    internal class Ghost : Enemy
    {
        public Ghost(int posX, int posY) : base(EnemyTypeEnum.Ghost, posX, posY)
        {
            ActualHp = CharacterConstants.GHOST_BASE_HP;
            BaseAgility = CharacterConstants.GHOST_BASE_AGILITY;
            BaseStrength = CharacterConstants.GHOST_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.GHOST_HOSTILITY_LEVEL;
        }

        public Ghost(int posX, int posY, int actualHp) : base(EnemyTypeEnum.Ghost, posX, posY)
        {
            ActualHp = actualHp;
            BaseAgility = CharacterConstants.GHOST_BASE_AGILITY;
            BaseStrength = CharacterConstants.GHOST_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.GHOST_HOSTILITY_LEVEL;
        }

        public bool IsInvisible { get; set; } = true;

    }
}
