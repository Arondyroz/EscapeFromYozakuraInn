using UnityEngine;

public class Cell
{
    public Vector3Int Position { get; private set; }
    public FloorType FloorType { get; private set; }

    public bool IsWalkable =>
        FloorType == FloorType.Walkable
        || FloorType == FloorType.Start
        || FloorType == FloorType.End
        || FloorType == FloorType.Checkpoint;

    public Cell(Vector3Int position, FloorType cellType)
    {
        Position = position;
        FloorType = cellType;
    }

    public void SetFloorType(FloorType floorType)
    {
        FloorType = floorType;
    }
}
