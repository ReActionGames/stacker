//Copyright (C) Joshua Hendershot
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;

[InitializeOnLoad]
public static class AutomateBuildSettings
{
    #region Private Fields

    private static string keystoreName = "D:/Desktop/Unity Projects/Keystores/upload_key.keystore";
    private static string keystorePass = "kZ27Lf";
    private static string keyaliasName = "upload";
    private static string keyaliasPass = "kZ27Lf";

    private static string apkPath = "D:\\Google Drive\\Game APKs\\Stacker.apk";
    private static string iOSPath = "D:\\Desktop\\iOS Builds\\Stacker";

    private static string applicationIdendtifier = "com.Default.Identifier";

    #endregion Private Fields

    #region Public Methods

    [PostProcessBuild(1)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
    {
        VersionIncrementor.OnPostprocessBuild(target, pathToBuildProject);
    }

    #endregion Public Methods

    #region Private Methods

    [MenuItem("Build/Prepare For Build", priority = 0)]
    private static void PrepareBuild()
    {
        string companyName = Regex.Replace(PlayerSettings.companyName, @"\s", string.Empty);
        string productName = Regex.Replace(PlayerSettings.productName, @"\s", string.Empty);
        applicationIdendtifier = "com." + companyName.ToLower() +
            "." + productName.ToLower();
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, applicationIdendtifier);
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, applicationIdendtifier);
        PlayerSettings.Android.keystoreName = keystoreName;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPass;
    }

    [MenuItem("Build/Build Android #%&B", priority = 1)]
    private static void BuildAndroid()
    {
        PrepareBuild();
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetScenes(),
            locationPathName = apkPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    [MenuItem("Build/Build iOS", priority = 2)]
    private static void BuildIOS()
    {
        PrepareBuild();
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetScenes(),
            locationPathName = iOSPath,
            target = BuildTarget.iOS,
            options = BuildOptions.None
        };
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    private static string[] GetScenes()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        int length = scenes.Length;
        string[] paths = new string[length];
        for (int i = 0; i < length; i++)
        {
            paths[i] = scenes[i].path;
        }
        return paths;
    }

    #endregion Private Methods
}