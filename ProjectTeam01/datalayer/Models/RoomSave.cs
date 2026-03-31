namespace ProjectTeam01.datalayer.Models;

internal class RoomSave
{
    public int Sector { get; set; }
    public PositionSave TopLeft { get; set; } = new();
    public PositionSave BottomRight { get; set; } = new();
    public List<PositionSave> Doors { get; set; } = new();
    public bool IsStartRoom { get; set; }
    public bool IsEndRoom { get; set; }
}

