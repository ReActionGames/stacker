using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsX {

    /// <summary>
    /// Sets a boolean in PlayerPrefs.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static void SetBool(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value ? 1 : 0);
    }

    /// <summary>
    /// Returns a boolean in PlayerPrefs.
    /// </summary>
    /// <param name="name"></param>
    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    /// <summary>
    /// Returns a boolean in PlayerPrefs.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    public static bool GetBool(string name, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return GetBool(name);
        }
        return defaultValue;
    }
}
