namespace ProjectTeam01.domain.Characters
{
    internal class Ogre : Enemy
    {
        public Ogre(int posX, int posY) : base(EnemyTypeEnum.Ogre, posX, posY)
        {
            ActualHp = CharacterConstants.OGRE_BASE_HP;
            BaseAgility = CharacterConstants.OGRE_BASE_AGILITY;
            BaseStrength = CharacterConstants.OGRE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.OGRE_HOSTILITY_LEVEL;
        }

        public Ogre(int posX, int posY, int actualHp) : base(EnemyTypeEnum.Ogre, posX, posY)
        {
            ActualHp = actualHp;
            BaseAgility = CharacterConstants.OGRE_BASE_AGILITY;
            BaseStrength = CharacterConstants.OGRE_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.OGRE_HOSTILITY_LEVEL;
        }

        public bool OgreCooldown { get; set; } = false;
    }
}
