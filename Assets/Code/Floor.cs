using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    
    [SerializeField]
    private int size;

    private TetrisGrid grid;

    private void Start()
    {
        grid = FindObjectOfType<TetrisGrid>();
        for (int x = -size; x < size; x++)
        {
            Vector2 pos = new Vector2(transform.position.x + x, transform.position.y);
            grid.SetCellFull(pos);
        }
    }
}
