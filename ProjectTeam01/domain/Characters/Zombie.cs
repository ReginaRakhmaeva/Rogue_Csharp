namespace ProjectTeam01.domain.Characters
{
    internal class Zombie : Enemy
    {
        public Zombie(int posX, int posY) : base(EnemyTypeEnum.Zombie, posX, posY)
        {
            ActualHp = CharacterConstants.ZOMBIE_BASE_HP;
            BaseAgility = CharacterConstants.ZOMBIE_BASE_AGILITY;
            BaseStrength = CharacterConstants.ZOMBIE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.ZOMBIE_HOSTILITY_LEVEL;
        }
        public Zombie(int posX, int posY, int actualHp) : base(EnemyTypeEnum.Zombie, posX, posY)// конструктор для воостановления врага из сохранения
        {
            ActualHp = actualHp;
            BaseAgility = CharacterConstants.ZOMBIE_BASE_AGILITY;
            BaseStrength = CharacterConstants.ZOMBIE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.ZOMBIE_HOSTILITY_LEVEL;
        }
    }
}
