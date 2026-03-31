using ProjectTeam01.datalayer.Models;
using ProjectTeam01.domain.Effects;

namespace ProjectTeam01.datalayer.Mappers
{
    internal class ActiveEffectMapper
    {
        public static ActiveEffectSave ToSave(ActiveEffect effect)
        {
            return new ActiveEffectSave
            {
                Type = effect.Type,
                RemainingTicks = effect.RemainingTicks,
                Value = effect.Value
            };
        }

        public static ActiveEffect FromSave(ActiveEffectSave save)
        {
            return new ActiveEffect(save.Type, save.Value, save.RemainingTicks);
        }
    }
}
