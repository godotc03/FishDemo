using System;
using System.Text;
using UnityEngine;
using XLua;


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
        //TODO check hotfix direction
        //Check assetbundle
        //
        TextAsset asset = Resources.Load<TextAsset>(relativePath);
        return asset? asset:null;
    }
}
