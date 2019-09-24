using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

///<summary>
/// check version, check diff file list
/// download new files 
///</summary>


//TODO Update itself
//TODO Generate version file and diff filelist auto

public class HotUpdater : MonoBehaviour
{


    //TestCode
    private string CDN_Version = "1.1.2";
    private string Cur_Version = "1.1.1";
    private string[] fileList = { "lua/main.lua.ab", "lua/fixfishctrl.lua.ab", "Version.json" };
    //end of TestCode
    private string CDN_URL = "";
    public bool UpdateFinished { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
#if UNITY_EDITOR_WIN
        CDN_URL = "file:///" + Application.dataPath + "/../FakeServer/";
#elif UNITY_EDITOR_OSX
        CDN_URL = "file://" + Application.dataPath + "/../FakeServer/";
#endif
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DoUpdate()   //TODO 
    {
        //TODO download version file and check version

        //TODO generate download file list.

        //TODO download files and replace older files

        //TODO show download percent in UI.

        string scriptsSavePath = Path.Combine(Application.persistentDataPath, "hotfix");

        foreach (string file in fileList)
        {
            string fileSavePath = Path.Combine(scriptsSavePath, file);
            if (!Directory.Exists(Path.GetDirectoryName(fileSavePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileSavePath));
            }
            UnityWebRequest request = new UnityWebRequest(CDN_URL + file);
            DownloadHandlerFile dh = new DownloadHandlerFile(fileSavePath);
            dh.removeFileOnAbort = true;
            request.method = UnityWebRequest.kHttpVerbGET;
            request.downloadHandler = dh;

            yield return request.SendWebRequest();
           
            if (request.error != null)
            {
                Debug.Log("Download error:" + request.error);
            }
        }
        //yield return new WaitForSeconds(3);
        UpdateFinished = true;
    }
}
