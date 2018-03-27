//Copyright (C) Joshua Hendershot
// Version Incrementor Script for Unity by Francesco Forno (Fornetto Games)
// Inspired by http://forum.unity3d.com/threads/automatic-version-increment-script.144917/
using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class VersionIncrementor
{
#region Public Methods

    public static void OnPostprocessBuild (BuildTarget target, string pathToBuiltProject)
    {
        Debug.Log ("Build v" + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")");
        IncreaseBuild ();
    }

#endregion Public Methods

#region Private Methods

    private static void IncrementVersion (int majorIncr, int minorIncr, int buildIncr)
    {
        string[] lines = PlayerSettings.bundleVersion.Split ('.');

        lines = AddDefaultValuesIfEmpty(lines);

        int MajorVersion = int.Parse (lines[0]) + majorIncr;
        int MinorVersion = int.Parse (lines[1]) + minorIncr;
        int Build = int.Parse (lines[2]) + buildIncr;

        if (majorIncr > 0)
        {
            MinorVersion = 0;
        }
        if (majorIncr > 0 || minorIncr > 0)
        {
            Build = 0;
        }

        string buildStr = MajorVersion.ToString("0") + "." +
            MinorVersion.ToString("0") + "." +
            Build.ToString("0");
        int buildNum = MajorVersion * 10000 + MinorVersion * 1000 + Build;
        PlayerSettings.bundleVersion = buildStr;
        PlayerSettings.Android.bundleVersionCode = buildNum;
        PlayerSettings.iOS.buildNumber = buildNum.ToString();
    }

    private static string[] AddDefaultValuesIfEmpty(string[] lines)
    {
        if (lines.Length >= 3)
            return lines;

        string[] temp = new string[3];

        for (int i = 0; i < temp.Length; i++)
        {
            if (i >= lines.Length)
            {
                temp[i] = "0";
                continue;
            }
            temp[i] = lines[i];
        }

        return temp;
    }

    [MenuItem ("Build/Increase Minor Version", priority = 2)]
    private static void IncreaseMinor ()
    {
        IncrementVersion (0, 1, 0);
    }

    [MenuItem ("Build/Increase Major Version", priority = 3)]
    private static void IncreaseMajor ()
    {
        IncrementVersion (1, 0, 0);
    }

    private static void IncreaseBuild ()
    {
        IncrementVersion (0, 0, 1);
    }

#endregion Private Methods
}