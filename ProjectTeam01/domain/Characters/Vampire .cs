namespace ProjectTeam01.domain.Characters
{
    internal class Vampire : Enemy
    {
        public Vampire(int posX, int posY) : base(EnemyTypeEnum.Vampire, posX, posY)
        {
            ActualHp = CharacterConstants.VAMPIRE_BASE_HP;
            BaseAgility = CharacterConstants.VAMPIRE_BASE_AGILITY;
            BaseStrength = CharacterConstants.VAMPIRE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.VAMPIRE_HOSTILITY_LEVEL;
        }

        public Vampire(int posX, int posY, int actualHp) : base(EnemyTypeEnum.Vampire, posX, posY)// конструктор для воостановления врага из сохранения
        {
            ActualHp = actualHp;
            BaseAgility = CharacterConstants.VAMPIRE_BASE_AGILITY;
            BaseStrength = CharacterConstants.VAMPIRE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.VAMPIRE_HOSTILITY_LEVEL;
        }

        public bool EvadedFirstAttack { get; set; } = false;
    }
}
