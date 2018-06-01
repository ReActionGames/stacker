using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacker
{
    public class SocialManager : MonoBehaviour
    {

        [SerializeField] private bool googlePlayOverride = false;

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.Android || googlePlayOverride)
            {
                PlayGamesPlatform.Activate();
                Social.localUser.Authenticate((bool success) => {
                    Debug.Log("Authenticating User: " + (success ? "successful" : "unsuccessful"));
                });
            }
        }

    }
}