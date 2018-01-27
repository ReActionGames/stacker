using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SnapToGrid : MonoBehaviour {

    public TetrisGrid tetrisGrid;

    [Button]
    public void Snap()
    {
        var grid = tetrisGrid.Grid;
        Vector3Int cellPosition = grid.LocalToCell(transform.localPosition);
        transform.localPosition = grid.GetCellCenterLocal(cellPosition);
    }
}
