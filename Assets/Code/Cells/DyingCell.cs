using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker.Cells
{
    public class DyingCell : MonoBehaviour
    {
        private enum Direction
        {
            Left = -1,
            Right = 1
        }

        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float jumpLength;
        [SerializeField] private float maxJumpXDistance;
        [ReadOnly]
        [SerializeField] private float jumpXDistance;
        //[SerializeField] private Direction jumpDirection;
        [SerializeField] private float rotationSpeed;

        private bool rotate;

        [Button]
        public void Both()
        {
            jumpXDistance = Random.Range(-maxJumpXDistance, maxJumpXDistance);
            PlayAnimation();
            rotate = true;
        }

        [Button]
        public void PlayAnimation()
        {
            //transform.position = Vector3.zero;
            StartCoroutine(Jump());
        }

        [Button]
        public void ToggleRotation()
        {
            rotate = !rotate;
        }

        private void ResetRotation()
        {
            rotate = false;
            transform.rotation = Quaternion.identity;
        }

        private void Update()
        {
            if (rotate)
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed * /*(int)jumpDirection*/ jumpXDistance);
            }
        }

        private IEnumerator Jump()
        {
            float time = 0;
            float xPos = transform.position.x;
            float startXPos = transform.position.x;
            float endXPos = startXPos + jumpXDistance;
            float referenceYPos = transform.position.y;

            while (time < jumpLength)
            {
                float perc = time / jumpLength;
                //xPos += Time.deltaTime * (int)jumpDirection;
                xPos = Mathf.Lerp(startXPos, endXPos, perc);
                float offsetYPos = jumpCurve.Evaluate(perc);
                Vector3 pos = new Vector3(xPos, referenceYPos + offsetYPos);
                transform.position = pos;
                //Vector3 pos = Vector3.ler
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(xPos, referenceYPos + jumpCurve.Evaluate(1));
            ResetRotation();
        }
    }
}