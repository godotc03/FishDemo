using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///lancher Updater and start game.
///</summary>

public class Launcher : MonoBehaviour
{
    //TODO Embedded Browser in game to show the news and update information

    public static Launcher Instance { get; private set; }
    public HotUpdater Updater { get; private set; }
    public LuaMain LuaMain { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Updater = gameObject.AddComponent<HotUpdater>();
        this.LuaMain = gameObject.AddComponent<LuaMain>();
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        StartCoroutine(Updater.DoUpdate());
        yield return new WaitUntil(() => Updater.UpdateFinished);
        LuaMain.StartGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
