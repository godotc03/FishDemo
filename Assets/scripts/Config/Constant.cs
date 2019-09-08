using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public const string VERSION = "1.1.0";
    public const string BUNDLE_DIR =
#if UNITY_IOS
                "iOS";
#elif UNITY_ANDROID
                "Android";
#elif UNITY_WEBGL
                "WebGL";
#else
                "Other";
#endif

    public const string VERSION_NAME = "Version.json";
    public const string FILE_LIST_NAME = "FileList.json";

    public static string PERSISTENT_DIR_PATH
    {
        get
        {
            return Application.persistentDataPath + "/" + BUNDLE_DIR;
        }
    }
    public static string STREAMING_DIR_PATH
    {
        get
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            return Application.dataPath + "!assets/" + BUNDLE_DIR;
#else
            return Application.streamingAssetsPath + "/" + BUNDLE_DIR;
#endif
        }
    }

}
