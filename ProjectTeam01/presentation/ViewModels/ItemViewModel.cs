using ProjectTeam01.domain.Effects;
using ProjectTeam01.domain.generation;
using ProjectTeam01.domain.Items;

namespace ProjectTeam01.presentation.ViewModels;

/// Представление предмета для отображения на фронтенде
public class ItemViewModel
{
    public Position Position { get; set; }
    public ItemType Type { get; set; }
    public WeaponTypeEnum? WeaponType { get; set; }
    public int? StrengthBonus { get; set; }

    public int? HealthValue { get; set; } 

    public EffectTypeEnum? ElixirType { get; set; }

    public ScrollTypeEnum? ScrollType { get; set; }

    public int? Price { get; set; } 
}

