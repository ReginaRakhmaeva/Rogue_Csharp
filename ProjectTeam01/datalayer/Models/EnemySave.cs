using ProjectTeam01.domain.Characters;

namespace ProjectTeam01.datalayer.Models
{
    internal class EnemySave
    {
        public EnemyTypeEnum EnemyType { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int ActualHp { get; set; }
        public bool IsTriggered { get; set; }

        public bool? OgreCooldown { get; set; }
        public bool? IsInvisible { get; set; }
        public bool? EvadedFirstAttack { get; set; }
        public MimicsRepresentation? Representation { get; set; }
    }
}
