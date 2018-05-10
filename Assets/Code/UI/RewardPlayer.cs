using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Stacker
{
    public class RewardPlayer : MonoBehaviour
    {
        [SerializeField] private AnimationCurve increaseSpeedCurve;
        [SerializeField] private float increaseTime;
        [OnValueChanged("CacheDelays")]
        [SerializeField] private float delayUntilStartIncrease;

        private WaitForSeconds delay;
        private WaitForSeconds startDelay;

        private void Start()
        {
            CacheDelays();
        }

        private void CacheDelays()
        {
            startDelay = new WaitForSeconds(delayUntilStartIncrease);
        }

        public void Reward(Product product)
        {
            Debug.Log("Reward code for (" + product.definition.id + ")");
            ResolveReward(product);
        }

        private void ResolveReward(Product product)
        {
            switch (product.definition.id)
            {
                case "com.henderstudios.stacker.coins.200":
                    RewardWithCoins(200);
                    break;
                case "com.henderstudios.stacker.coins.500":
                    RewardWithCoins(500);
                    break;
                case "com.henderstudios.stacker.coins.900":
                    RewardWithCoins(900);
                    break;
                default:
                    Debug.LogWarning("Could not resolve reward for product: " + product.definition.id);
                    break;
            }
        }

        public void RewardWithCoins(int amount)
        {
            Debug.Log("Rewarding player with {" + amount + "} coins...");
            StartCoroutine(AddCoins(amount));
        }

        private IEnumerator AddCoins(int amount)
        {
            int amountGiven = 0;

            yield return startDelay;

            float timer = 0;

            while (timer < increaseTime)
            {
                timer += Time.deltaTime;
                float percent = timer / increaseTime;
                int totalAmountGiven = (int)(increaseSpeedCurve.Evaluate(percent) * amount);
                int amountDelta = totalAmountGiven - amountGiven;

                Currency.AddCoins(amountDelta);
                amountGiven += amountDelta;

                yield return null;
            }
        }
    }
}