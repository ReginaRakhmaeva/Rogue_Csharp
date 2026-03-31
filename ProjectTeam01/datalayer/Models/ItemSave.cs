using ProjectTeam01.domain.Effects;
using ProjectTeam01.domain.Items;

namespace ProjectTeam01.datalayer.Models
{
    internal class ItemSave
    {
        public ItemType Type { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int GameLevel { get; set; }

        public FoodTypeEnum? FoodType { get; set; }
        public EffectTypeEnum? ElixirType { get; set; }
        public ScrollTypeEnum? ScrollType { get; set; }
        public int? Price { get; set; }
        public WeaponTypeEnum? WeaponType { get; set; }

    }

}
