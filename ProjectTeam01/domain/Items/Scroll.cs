using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.Items.Interfaces;

namespace ProjectTeam01.domain.Items
{
    public enum ScrollTypeEnum
    {
        Agility,
        Strength,
        MaxHp
    }

    internal class Scroll(ScrollTypeEnum scrollType, int posX, int posY) : Item(ItemType.Scroll, posX, posY), IUsableItem
    {
        public ScrollTypeEnum ScrollType { get; } = scrollType;

        void IUsableItem.Use(Hero hero)
        {
            switch (ScrollType)
            {
                case ScrollTypeEnum.Agility:
                    hero.ChangeBaseAgility(ItemConstants.SCROLL_AGILITY_BONUS);
                    break;
                case ScrollTypeEnum.Strength:
                    hero.ChangeBaseStrength(ItemConstants.SCROLL_STRENGTH_BONUS);
                    break;
                case ScrollTypeEnum.MaxHp:
                    hero.ChangeBaseMaxHp(ItemConstants.SCROLL_MAX_HP_BONUS);
                    hero.Heal(ItemConstants.SCROLL_HEAL_AMOUNT);
                    break;
                default:
                    break;
            }
        }


    }

}
