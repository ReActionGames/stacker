using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PlayerPrefsClearer {
    
    [MenuItem("Tools/Clear Player Prefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs Cleared.");
    }
}
