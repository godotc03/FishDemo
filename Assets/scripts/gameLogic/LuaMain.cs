using System;
using System.Text;
using UnityEngine;
using XLua;
using System.IO;


/// <summary>
/// Lua main.
/// Entry point of lua
/// </summary>

public class LuaMain : MonoBehaviour
{
    LuaEnv luaEnv = null;
    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(CustomLoad);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Debug.Log("Lua Start Game!");
        luaEnv.DoString("require 'lua.Main'");

    }

    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

    private byte[] CustomLoad(ref string fileName)
    {
        string extension = ".lua";
        fileName = fileName.EndsWith(extension,StringComparison.CurrentCultureIgnoreCase) ? fileName.Substring(0, fileName.Length - extension.Length) : fileName;
        string relativePath = fileName.Replace(".", "/") + extension;
        TextAsset textAsset = LoadLua(relativePath);
        if (textAsset)
        {
            return Encoding.UTF8.GetBytes(textAsset.text);
        }
        return null;
    }

    //TODO load from asset bundle and hot fixed directory
    public static TextAsset LoadLua(string relativePath)
    {
        string longRelativePath = relativePath + "." + Constant.ASSET_BUNDLE_VARIANT;

        //TODO check hotfix direction (PERSISTENT_DIR_PATH)
        //Debug.Log("Persistent Dir:"+Constant.PERSISTENT_DIR_PATH);

        //Check assetbundle //TODO using different path along with platforms
        string streamingPath = Constant.STREAMING_DIR_PATH + "/" + longRelativePath.ToLower();
        if (File.Exists(streamingPath)) //TODO FileManager for different platform.
        {
            //Debug.Log("Load from assetbundle:" + relativePath);
            int index = relativePath.LastIndexOf('/');
            string assetName = relativePath.Substring(index + 1);
            return LoadLuaBundle(streamingPath, assetName);
        }
        //
        TextAsset asset = Resources.Load<TextAsset>(relativePath);
        return asset? asset:null;
    }

    public static TextAsset LoadLuaBundle(string path, string assetName)
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle)
        {
            TextAsset asset = assetBundle.LoadAsset<TextAsset>(assetName);
            assetBundle.Unload(false);
            return asset;
        }
        return null;
    }
}
