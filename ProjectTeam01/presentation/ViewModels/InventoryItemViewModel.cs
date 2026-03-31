using ProjectTeam01.domain.Effects;
using ProjectTeam01.domain.Items;

namespace ProjectTeam01.presentation.ViewModels;

/// Представление предмета из инвентаря игрока для отображения на фронтенде
public class InventoryItemViewModel
{
    public ItemType Type { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public WeaponTypeEnum? WeaponType { get; set; }
    public int? StrengthBonus { get; set; }

    public int? HealthValue { get; set; } 

    public EffectTypeEnum? ElixirType { get; set; }

    public ScrollTypeEnum? ScrollType { get; set; }

    public int? Price { get; set; }
}

