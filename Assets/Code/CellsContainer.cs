using Sirenix.OdinInspector;
using UnityEngine;

public class CellsContainer : SerializedMonoBehaviour
{
    [TableMatrix]
    public int[,] Cells = new int[4, 4];
}

