using HenderStudios.Events;
using Sirenix.OdinInspector;
using Stacker.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stacker.Cells
{
    public class DyingCell : MonoBehaviour
    {
        [SerializeField] private DyingCellSettings settings;
        [Required]
        [SerializeField] private Image texture;

        private float jumpXDistance;
        private bool rotate;
        private SpriteRenderer sprite
        {
            get
            {
                return GetComponent<SpriteRenderer>();
            }
        }
        
        public void Play(Color color, Vector2 position, bool hasCoin)
        {
            transform.position = position;
            jumpXDistance = settings.JumpXDistance;
            sprite.color = color;
            SetTexture(FindObjectOfType<TetroGrid>());
            if (hasCoin)
                SpawnCoin();
            PlaySFX();
            PlayAnimation();
            rotate = true;
        }

        private void PlaySFX()
        {
            EventManager.TriggerEvent(EventNames.PlaySFX, new Message(settings.SoundFX));
        }

        private void SetTexture(TetroGrid grid)
        {
            Theme theme = grid.TetroSettings.Theme;
            texture.sprite = theme.Sprite;
            texture.color = theme.Color;
            texture.type = Image.Type.Simple;
            texture.preserveAspect = true;
        }

        private void SpawnCoin()
        {
            GameObject coin = settings.EarnedCoinPrefab.Spawn();
            coin.transform.position = transform.position;
            coin.transform.position += Vector3.up;
            Currency.AddCoins(1);
        }

        private void PlayAnimation()
        {
            //transform.position = Vector3.zero;
            StartCoroutine(Jump());
            StartCoroutine(Fade());
        }

        private void ToggleRotation()
        {
            rotate = !rotate;
        }

        private void ResetRotation()
        {
            rotate = false;
            transform.rotation = Quaternion.identity;
        }

        private void End()
        {
            ResetRotation();
            gameObject.Recycle();
        }

        private void Update()
        {
            if (rotate)
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * settings.RotationSpeed * jumpXDistance * settings.RotationDampener);
            }
        }

        private IEnumerator Jump()
        {
            float time = 0;
            float xPos = transform.position.x;
            float startXPos = transform.position.x;
            float endXPos = startXPos + jumpXDistance;
            float referenceYPos = transform.position.y;

            while (time < settings.JumpLength)
            {
                float perc = time / settings.JumpLength;
                xPos = Mathf.Lerp(startXPos, endXPos, perc);
                float offsetYPos = settings.JumpCurve.Evaluate(perc);
                Vector3 pos = new Vector3(xPos, referenceYPos + offsetYPos);
                transform.position = pos;
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(xPos, referenceYPos + settings.JumpCurve.Evaluate(1));
            End();
        }

        private IEnumerator Fade()
        {
            float time = 0;
            Color startColor = sprite.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

            while (time < settings.JumpLength)
            {
                float perc = time / settings.JumpLength;
                sprite.color = Color.Lerp(startColor, endColor, settings.FadeCurve.Evaluate(perc));
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}