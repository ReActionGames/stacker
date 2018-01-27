using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SnapToGrid : MonoBehaviour {

    public TetrisGrid tetrisGrid;
    [BoxGroup("Snapping")]
    public Transform objectToSnap;

    [Button(ButtonSizes.Large), BoxGroup("Snapping")]
    public void Snap()
    {
        if (tetrisGrid == null || objectToSnap == null)
            return;
        var grid = tetrisGrid.Grid;
        Vector3Int cellPosition = grid.LocalToCell(objectToSnap.localPosition);
        objectToSnap.localPosition = grid.GetCellCenterLocal(cellPosition);
    }

    public void Snap(Transform _transform)
    {
        if (tetrisGrid == null)
            return;
        var grid = tetrisGrid.Grid;
        Vector3Int cellPosition = grid.LocalToCell(_transform.localPosition);
        _transform.localPosition = grid.GetCellCenterLocal(cellPosition);
    }
}
