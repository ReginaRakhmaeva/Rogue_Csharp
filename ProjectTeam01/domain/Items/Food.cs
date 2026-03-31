using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.Items.Interfaces;

namespace ProjectTeam01.domain.Items
{
    public enum FoodTypeEnum
    {
        Apple,
        Bread,
        Meat,
        Feast
    }

    internal class Food : Item, IUsableItem
    {
        public FoodTypeEnum FoodType { get; }

        public Food(FoodTypeEnum foodType, int posX, int posY) : base(ItemType.Food, posX, posY)
        {
            FoodType = foodType;
        }

        public int HealthValue
        {
            get
            {
                return FoodType switch
                {
                    FoodTypeEnum.Apple => ItemConstants.FOOD_APPLE_HEALTH,
                    FoodTypeEnum.Bread => ItemConstants.FOOD_BREAD_HEALTH,
                    FoodTypeEnum.Meat => ItemConstants.FOOD_MEAT_HEALTH,
                    FoodTypeEnum.Feast => ItemConstants.FOOD_FEAST_HEALTH,
                    _ => ItemConstants.FOOD_APPLE_HEALTH
                };
            }
        }

        void IUsableItem.Use(Hero hero)
        {
            int healAmount = HealthValue;
            int missingHp = hero.MaxHp - hero.ActualHp;
            if (healAmount > missingHp)
                healAmount = missingHp;

            hero.Heal(healAmount);
        }
    }
}
