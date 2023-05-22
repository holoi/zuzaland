using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class BuildProcessor
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            // For info.plist
            string plistPath = buildPath + "/Info.plist";
            PlistDocument plist = new();
            plist.ReadFromFile(plistPath);
            PlistElementDict rootDict = plist.root;

            rootDict.SetString("NSPhotoLibraryUsageDescription", "For saving recordings.");

            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}