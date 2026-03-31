namespace ProjectTeam01.domain.Effects
{
    public enum EffectTypeEnum
    {
        BuffStrength,
        BuffAgility,
        BuffMaxHp,
        Sleep
    }

    internal class ActiveEffect
    {
        public ActiveEffect(EffectTypeEnum effectType)
        {
            Type = effectType;
            RemainingTicks = DurationVal(effectType);
            Value = ValuesByType(effectType);
        }

        public ActiveEffect(EffectTypeEnum effectType, int value, int durationTicks)
        {
            Type = effectType;
            Value = value;
            RemainingTicks = durationTicks;
        }

        public static ActiveEffect FromSave(EffectTypeEnum effectType, int remainingTicks, int value)
        {
            return new ActiveEffect(effectType, value, remainingTicks);
        }

        public EffectTypeEnum Type { get; }

        public int Value { get; }

        public int RemainingTicks { get; private set; }

        public static int DurationVal(EffectTypeEnum effect)
        {
            if (effect == EffectTypeEnum.Sleep)
                return EffectConstants.SLEEP_DURATION;
            else return EffectConstants.DEFAULT_EFFECT_DURATION;
        }

        public static int ValuesByType(EffectTypeEnum effect)
        {
            switch (effect)
            {
                case EffectTypeEnum.BuffStrength:
                    return EffectConstants.BUFF_STRENGTH_VALUE;
                case EffectTypeEnum.BuffAgility:
                    return EffectConstants.BUFF_AGILITY_VALUE;
                case EffectTypeEnum.BuffMaxHp:
                    return EffectConstants.BUFF_MAX_HP_VALUE;
                default:
                    return EffectConstants.DEFAULT_EFFECT_VALUE;
            }
        }
        public void Tick()
        {
            if (!IsEffectOver)
            {
                RemainingTicks -= 1;
                if (RemainingTicks <= 0)
                    IsEffectOver = true;
            }
        }
        public bool IsEffectOver { get; private set; }


    }
}
