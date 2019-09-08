using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// check version, check diff file list
/// download new files 
///</summary>


//TODO Update itself
//TODO Generate version file and diff filelist auto

public class HotUpdater : MonoBehaviour
{
    public bool UpdateFinished { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DoUpdate()   //TODO 
    {

        yield return new WaitForSeconds(3);
        UpdateFinished = true;
    }
}
