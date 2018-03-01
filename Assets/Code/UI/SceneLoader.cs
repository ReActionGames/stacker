using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HenderStudios.Extensions
{
    public class SceneLoader : MonoBehaviour
    {

        public static void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public static void LoadScene(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public static void LoadSceneAdditive(string scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        public static void LoadSceneAdditive(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        }

        public static void ReloadScene()
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }



        public void LoadSceneWrapper(string scene)
        {
            LoadScene(scene);
        }

        public void LoadSceneWrapper(int buildIndex)
        {
            LoadScene(buildIndex);
        }

        public void LoadSceneAdditiveWrapper(string scene)
        {
            LoadSceneAdditive(scene);
        }

        public void LoadSceneAdditiveWrapper(int buildIndex)
        {
            LoadSceneAdditive(buildIndex);
        }

        public void ReloadSceneWrapper()
        {
            ReloadScene();
        }
    }
}