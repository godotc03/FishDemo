
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
#if UNITY_EDITOR_WIN
        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
#elif UNITY_EDITOR_OSX
        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
#endif
    }

    //TODO
    [MenuItem("HotUpdate/Lua2Txt")]
    static void ChangeLuaToTxt()
    {

    }

    [MenuItem("HotUpdate/Generate FileList")]
    static void GenerateDLCFileList()
    {

    }

    [MenuItem("HotUpdate/Generate Version File")]
    static void GenerateDLCVersionFile()
    {

    }

}
