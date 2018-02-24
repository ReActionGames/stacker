using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolutionAdjuster : MonoBehaviour {
    
    [SerializeField] private Vector2 preferredScreenSize;
    [SerializeField] private float adjustmentAccuracy;

    [Button]
    public void AdjustCameraSize()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;

        // Increase size if too big
        while (height < preferredScreenSize.y || width < preferredScreenSize.x)
        {
            Camera.main.orthographicSize += adjustmentAccuracy;

            height = Camera.main.orthographicSize * 2.0f;
            width = height * Screen.width / Screen.height;
        }

        // Decrease size if too small
        while (height > preferredScreenSize.y && width > preferredScreenSize.x)
        {
            Camera.main.orthographicSize -= adjustmentAccuracy;

            height = Camera.main.orthographicSize * 2.0f;
            width = height * Screen.width / Screen.height;
        }
    }
}
