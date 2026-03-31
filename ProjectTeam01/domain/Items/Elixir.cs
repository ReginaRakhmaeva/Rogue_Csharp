using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.Effects;
using ProjectTeam01.domain.Items.Interfaces;

namespace ProjectTeam01.domain.Items
{
    internal class Elixir : Item, IUsableItem
    {
        public Elixir(EffectTypeEnum elixirType, int posX, int posY) : base(ItemType.Elixir, posX, posY)
        {
            ElixirType = elixirType;
        }

        void IUsableItem.Use(Hero hero)
        {
            if (!hero.HeroBackpack.AllItems.Contains(this))
                return;

            var random = Random.Shared;

            int value = CalculateValue(hero, random);
            int durationSeconds = random.Next(ItemConstants.ELIXIR_MIN_DURATION_SECONDS, ItemConstants.ELIXIR_MAX_DURATION_SECONDS + 1);
            int durationTicks = durationSeconds;

            ActiveEffect effect = new ActiveEffect(ElixirType, value, durationTicks);
            hero.ActiveEffectManager.AddActiveEffect(effect);
        }

        private int CalculateValue(Hero hero, Random random)
        {
            int maxIncrease = ElixirType switch
            {
                EffectTypeEnum.BuffAgility => hero.BaseAgility * ItemConstants.ELIXIR_MAX_PERCENT_AGILITY / 100,
                EffectTypeEnum.BuffStrength => hero.BaseStrength * ItemConstants.ELIXIR_MAX_PERCENT_STRENGTH / 100,
                EffectTypeEnum.BuffMaxHp => hero.MaxHp * ItemConstants.ELIXIR_MAX_PERCENT_MAX_HP / 100,
                _ => 1
            };

            if (ElixirType == EffectTypeEnum.BuffMaxHp)
            {
                int minIncrease = Math.Max(ItemConstants.ELIXIR_MIN_INCREASE_FOR_MAX_HP, maxIncrease / 3);
                return random.Next(minIncrease, maxIncrease + 1);
            }
            return random.Next(1, Math.Max(2, maxIncrease + 1));
        }

        public EffectTypeEnum ElixirType { get; }
    }
}
