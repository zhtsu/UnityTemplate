using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FB_TileData;

//[CreateAssetMenu(fileName = "NewUnitData", menuName = "FB Data/UnitData")]
public class FB_UnitData : FB_Data
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Dictionary<string, FAnimation> AnimationTable { get; private set; } = new Dictionary<string, FAnimation>();

    public string BelongingModId { get; private set; }

    public void Initialize(string ModId)
    {
        BelongingModId = ModId;
        AnimationTable.Clear();
    }
}
