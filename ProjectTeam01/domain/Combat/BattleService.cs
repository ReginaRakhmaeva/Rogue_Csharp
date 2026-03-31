namespace ProjectTeam01.domain.Combat;

internal class BattleService
{
    public static bool HitSuccess(int attackerBaseAgility, int targetBaseAgility)
    {
        double hitChance = BattleConstants.INITIAL_HIT_CHANCE 
            + (attackerBaseAgility - targetBaseAgility - BattleConstants.STANDARD_AGILITY) 
            * BattleConstants.AGILITY_FACTOR;

        int hitChanceInt = (int)Math.Round(hitChance);
        if (hitChanceInt < 0) hitChanceInt = 0;
        if (hitChanceInt > 100) hitChanceInt = 100;

        return Random.Shared.Next(0, 100) < hitChanceInt;
    }
}

