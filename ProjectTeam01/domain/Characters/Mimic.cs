namespace ProjectTeam01.domain.Characters
{
    public enum MimicsRepresentation
    {
        Food,
        Elixir,
        Scroll,
        Weapon,
        Mimic
    }

    internal class Mimic : Enemy
    {
        protected static readonly Random random = new();
        public MimicsRepresentation Representation { get; set; }

        public Mimic(int posX, int posY) : base(EnemyTypeEnum.Mimic, posX, posY)
        {
            ActualHp = CharacterConstants.MIMIC_BASE_HP;
            BaseAgility = CharacterConstants.MIMIC_BASE_AGILITY;
            BaseStrength = CharacterConstants.MIMIC_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.MIMIC_HOSTILITY_LEVEL;
            Representation = RandomRepresentation();
        }

        public Mimic(int posX, int posY, int actualHp) : base(EnemyTypeEnum.Mimic, posX, posY)
        {
            ActualHp = actualHp;
            BaseAgility = CharacterConstants.MIMIC_BASE_AGILITY;
            BaseStrength = CharacterConstants.MIMIC_BASE_STRENGTH;
            HostilityLevel = CharacterConstants.MIMIC_HOSTILITY_LEVEL;
            Representation = RandomRepresentation();
        }

        private MimicsRepresentation RandomRepresentation()
        {
            MimicsRepresentation mimics = (MimicsRepresentation)random.Next(CharacterConstants.MIMIC_REPRESENTATION_MIN, CharacterConstants.MIMIC_REPRESENTATION_MAX);
            return mimics;
        }
    }
}
