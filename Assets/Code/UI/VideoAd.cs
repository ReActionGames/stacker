using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

namespace Stacker
{
    public class VideoAd : MonoBehaviour
    {
        [SerializeField] private int rewardAmount;
        [OnValueChanged("CacheDelays")]
        [SerializeField] private float increaseDelay;
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
            delay = new WaitForSeconds(increaseDelay);
            startDelay = new WaitForSeconds(delayUntilStartIncrease);
        }

        [Button]
        public void ShowDefaultAd()
        {
            if (Application.isPlaying == false)
                return;
#if UNITY_ADS
            if (!Advertisement.IsReady())
            {
                Debug.Log("Ads not ready for default placement");
                return;
            }

            Advertisement.Show();
#endif
        }

        [Button]
        public void ShowRewardedAd()
        {
            if (Application.isPlaying == false)
                return;

            const string RewardedPlacementId = "rewardedVideo";

#if UNITY_ADS
            if (!Advertisement.IsReady(RewardedPlacementId))
            {
                Debug.Log(string.Format("Ads not ready for placement '{0}'", RewardedPlacementId));
                return;
            }

            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(RewardedPlacementId, options);
#endif
        }

#if UNITY_ADS
        private void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("The ad was successfully shown.");
                    RewardPlayer();
                    break;
                case ShowResult.Skipped:
                    Debug.Log("The ad was skipped before reaching the end.");
                    break;
                case ShowResult.Failed:
                    Debug.LogError("The ad failed to be shown.");
                    break;
            }
        }

#endif

        private void RewardPlayer()
        {
            StartCoroutine(AddCoins());
        }

        private IEnumerator AddCoins()
        {
            int amountGiven = 0;

            yield return startDelay;

            while (amountGiven < rewardAmount)
            {
                Currency.AddCoins(1);
                amountGiven++;
                yield return delay;
            }
        }
    }
}