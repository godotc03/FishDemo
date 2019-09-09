using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public static string CDN_URL = "http://127.0.0.1:80/cdn";


    public const bool UNITY_EDITOR =
#if UNITY_EDITOR
                true;
#else
                false;
#endif


    public const RuntimePlatform PLATFORM =
#if UNITY_STANDALONE_WIN
                RuntimePlatform.WindowsPlayer;
#elif UNITY_STANDALONE_OSX
                RuntimePlatform.OSXPlayer;
#elif UNITY_ANDROID
                RuntimePlatform.Android;
#elif UNITY_IOS
                RuntimePlatform.IPhonePlayer;
#elif UNITY_WEBGL
                RuntimePlatform.WebGLPlayer;
#else
            0;
#endif

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
    public const string ASSET_BUNDLE_VARIANT = "ab";

    public static string PERSISTENT_DIR_PATH
    {
        get
        {
            return Application.persistentDataPath;
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
