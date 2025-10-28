using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Grid : MonoBehaviour
{
    private int _Row = 0;
    private int _Column = 0;
    private float _TileSize = 0;
    private FB_Tile[,] _GridArray;

    public void Initialize(int Row, int Column, float TileSize)
    {
        _Row = Row;
        _Column = Column;
        _TileSize = TileSize;
        _GridArray = new FB_Tile[_Row, _Column];
    }

    public void GenetateTerrain()
    {
        for (int i = 0; i < _Row; i++)
        {
            for (int j = 0; j < _Column; j++)
            {
                Vector3 Position = new Vector3(i, j) * _TileSize;
                GameObject Tile = Instantiate(FB_PrefabManager.GetPrefab(FB_PrefabType.SquareTile), Position, Quaternion.identity);
                Tile.transform.SetParent(this.transform);
                _GridArray[i, j] = Tile.GetComponent<FB_Tile>();
            }
        }
    }
}
