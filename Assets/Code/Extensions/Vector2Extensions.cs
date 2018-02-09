using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions {

    /// <summary>
    /// Returns the Vector2Int representation of the vector.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2Int ToVector2Int(this Vector2 v)
    {
        return new Vector2Int((int)v.x, (int)v.y);
    }

}
