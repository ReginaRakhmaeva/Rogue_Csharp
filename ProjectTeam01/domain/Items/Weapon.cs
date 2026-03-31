namespace ProjectTeam01.domain.Items
{
    public enum WeaponTypeEnum
    {
        Axe,
        Dagger,
        Sword,
        Bow
    }
    internal class Weapon : Item
    {
        public Weapon(WeaponTypeEnum weaponType, int posX, int posY) : base(ItemType.Weapon, posX, posY)
        {
            WeaponType = weaponType;
        }

        public WeaponTypeEnum WeaponType { get; }

        public int StrengthBonus
        {
            get
            {
                return WeaponType switch
                {
                    WeaponTypeEnum.Axe => ItemConstants.WEAPON_AXE_BONUS,
                    WeaponTypeEnum.Dagger => ItemConstants.WEAPON_DAGGER_BONUS,
                    WeaponTypeEnum.Sword => ItemConstants.WEAPON_SWORD_BONUS,
                    WeaponTypeEnum.Bow => ItemConstants.WEAPON_BOW_BONUS,
                    _ => 0
                };
            }
        }
    }
}
