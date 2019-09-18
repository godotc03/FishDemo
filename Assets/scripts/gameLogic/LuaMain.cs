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
    private const float GC_INTERVAL = 1;//1 second
    private float m_LastGcTime = 0;
    LuaEnv luaEnv = null;
    private Func<string, LuaTable> m_Require;

    public static LuaMain Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(CustomLoad);
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Require = luaEnv.Global.Get<Func<string, LuaTable>>("require");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - m_LastGcTime > GC_INTERVAL)
        {
            luaEnv.Tick();
            m_LastGcTime = Time.time;
        }
    }

    public void StartGame()
    {
        Debug.Log("Lua Start Game!");
        luaEnv.DoString("require 'lua.main'");

    }

    private void OnDestroy()
    {
        luaEnv.DoString("require 'lua.Dispose'");
        m_Require = null;
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

        //check hotfix direction (in PERSISTENT_DIR_PATH)
        string persistentPath = Constant.PERSISTENT_DIR_PATH + "/hotfix/" + longRelativePath.ToLower();

        if (File.Exists(persistentPath))
        {
            //Debug.Log("LoadLua from :" + persistentPath);
            int index = relativePath.LastIndexOf('/');
            string assetName = relativePath.Substring(index + 1);
            return LoadLuaBundle(persistentPath, assetName);
        }

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
        //Debug.Log("Load lua from resources:" + relativePath);
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

    public LuaTable Require(string luaPath)
    {
        try
        {
            //Debug.Log("Require: " + luaPath);
            return m_Require(luaPath);
        }
        catch (LuaException le)
        {
            Debug.LogError(le);
            return null;
        }
    }

}
