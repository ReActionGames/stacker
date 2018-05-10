using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HenderStudios.Extensions
{
    public static class TransformExtensions
    {
        public static Transform Clear(this Transform transform)
        {
            if (Application.isPlaying)
            {
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    GameObject.DestroyImmediate(child.gameObject);
                }
            }
            return transform;
        }
    }
}