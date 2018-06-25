using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class CameraResolutionAdjuster : MonoBehaviour
    {

        [SerializeField] private Vector2 preferredScreenSize;
        [SerializeField] private Anchor anchor;
        [SerializeField] private float padding;
        [SerializeField] private float adjustmentAccuracy;

        private enum Anchor
        {
            Center,
            Top,
            Bottom,
            Left,
            Right
        }

        private void Start()
        {
            AdjustCameraSize();
        }

        [Button(ButtonSizes.Medium)]
        public void AdjustCameraSize()
        {
            if (adjustmentAccuracy <= 0)
            {
                adjustmentAccuracy = 0.1f;
            }
            float height = Camera.main.orthographicSize * 2.0f;
            float width = height * Screen.width / Screen.height;

            float originalHeight = height;
            float originalWidth = width;

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

            SetPosition(height - originalHeight, width - originalWidth);
        }

        private void SetPosition(float heightDelta, float widthDelta)
        {
            float offset;
            switch (anchor)
            {
                case Anchor.Center:
                    // Do nothing;
                    break;
                case Anchor.Top:
                    offset = heightDelta / 2;
                    offset -= padding;
                    transform.position = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
                    break;
                case Anchor.Bottom:
                    offset = heightDelta / 2;
                    offset -= padding;
                    transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
                    break;
                case Anchor.Left:
                    offset = widthDelta / 2;
                    offset -= padding;
                    transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
                    break;
                case Anchor.Right:
                    offset = widthDelta / 2;
                    offset -= padding;
                    transform.position = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
                    break;
            }
        }
    }
}