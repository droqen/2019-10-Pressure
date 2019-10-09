using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reaction_xxi : navdi3.xxi.BaseTilemapXXI
{
    private void Start()
    {
        InitializeManualTT();
    }

    public override int[] GetSolidTileIds()
    {
        return new int[] { };
    }
    public override int[] GetSpawnTileIds()
    {
        return new int[0];
    }
    public override void SpawnTileId(int TileId, Vector3Int TilePos)
    {
        throw new System.NotImplementedException();
    }
}
