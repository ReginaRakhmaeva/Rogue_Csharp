using ProjectTeam01.domain.generation;
using ProjectTeam01.domain.Items;

namespace ProjectTeam01.domain.Session;
// отвечает за логику инвентаря
internal partial class GameSession
{
    public void PickupItem(Item item)
    {
        if (item == null) return;

        if (Player.HeroBackpack.Add(item))
        {
            _currentLevel.RemoveEntity(item);
        }
    }

    public bool UsePlayerItem(Item item)
    {
        if (item == null) return false;
        bool used = Player.HeroBackpack.UseItem(item, Player);
        if (!used) return false;

        switch (item)
        {
            case Food:
                _statistics.RecordFoodConsumed();
                break;
            case Elixir:
                _statistics.RecordElixirConsumed();
                break;
            case Scroll:
                _statistics.RecordScrollConsumed();
                break;
        }

        return true;
    }

    public bool EquipPlayerWeapon(Weapon weapon)
    {
        if (weapon == null) return false;

        if (!Player.HeroBackpack.AllItems.Contains(weapon))
            return false;

        var previousWeapon = Player.WeaponManager.EquippedWeapon;

        bool success = Player.HeroBackpack.EquipWeapon(weapon, Player);

        if (success && previousWeapon != null)
        {
            if (Player.HeroBackpack.AllItems.Contains(previousWeapon))
            {
                Player.HeroBackpack.DeleteItem(previousWeapon);
            }

            var dropPosition = FindFreeNeighborPosition(Player.Position);
            if (dropPosition != null)
            {
                previousWeapon.MoveTo(dropPosition.Value.X, dropPosition.Value.Y);
                _currentLevel.AddEntity(previousWeapon);
            }
            else
            {
                previousWeapon.MoveTo(Player.Position.X, Player.Position.Y);
                _currentLevel.AddEntity(previousWeapon);
            }
        }

        return success;
    }

    public bool UnequipPlayerWeapon()
    {
        return Player.WeaponManager.UnequipWeapon() != null;
    }

    public bool DropPlayerItem(Item item)
    {
        if (item == null) return false;

        if (!Player.HeroBackpack.AllItems.Contains(item))
            return false;

        Player.HeroBackpack.DeleteItem(item);

        var dropPosition = FindFreeNeighborPosition(Player.Position);
        if (dropPosition != null)
        {
            item.MoveTo(dropPosition.Value.X, dropPosition.Value.Y);
            _currentLevel.AddEntity(item);
            return true;
        }

        Player.HeroBackpack.Add(item);
        return false;
    }

    private Position? FindFreeNeighborPosition(Position center)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int newX = center.X + dx;
                int newY = center.Y + dy;

                if (_currentLevel.IsWalkable(newX, newY) && !_currentLevel.HasAnyEntityAt(newX, newY))
                {
                    return new Position(newX, newY);
                }
            }
        }

        return null;
    }

    public IReadOnlyList<Weapon> GetPlayerWeapons() => Player.HeroBackpack.AllItems.OfType<Weapon>().ToList().AsReadOnly();

    public IReadOnlyList<Food> GetPlayerFood() => Player.HeroBackpack.AllItems.OfType<Food>().ToList().AsReadOnly();

    public IReadOnlyList<Elixir> GetPlayerElixirs() => Player.HeroBackpack.AllItems.OfType<Elixir>().ToList().AsReadOnly();

    public IReadOnlyList<Scroll> GetPlayerScrolls() => Player.HeroBackpack.AllItems.OfType<Scroll>().ToList().AsReadOnly();
    public int GetTotalGold()
    {
        var treasure = Player.HeroBackpack.AllItems.OfType<Treasure>().ToList();
        return treasure.Count > 0 ? treasure[0].Price : 0;
    }
}


