
using UnityEditor;
using System.IO;

//TODO version file, diff filelist

public class AssetBundleBuilder : EditorWindow
{
    [MenuItem("HotUpdate/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        //TODO
        string dir = "AssetBundles";
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        //TODO different platform
        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);

    }
}
