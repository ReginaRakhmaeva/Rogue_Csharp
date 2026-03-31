using ProjectTeam01.domain.Characters;
using ProjectTeam01.domain.generation;

namespace ProjectTeam01.presentation.ViewModels;

/// Представление врага для отображения на фронтенде
public class EnemyViewModel
{
    public Position Position { get; set; }
    public EnemyTypeEnum EnemyType { get; set; }
    public bool IsDead { get; set; }
    public bool IsTriggered { get; set; }

    public MimicsRepresentation? MimicRepresentation { get; set; }

    public bool? IsInvisible { get; set; }
}

