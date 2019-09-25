using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

[Hotfix]
public class FishSpawner : MonoBehaviour
{
    public static FishSpawner Instance { get; private set;}
    public GameObject[] fishPrefabs;
    public Transform[] fishSpawnPos;

    [SerializeField]
    private float autoSpawnInterval = 3;
    [SerializeField]
    private int spawnCount = 2; //spawn count per time


    private float spawnCDTime = 0;
    private List<GameObject> deadFish;

    private void Awake()
    {
        Instance = this;
        deadFish = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnCDTime += Time.deltaTime;
        if(spawnCDTime >= autoSpawnInterval)
        {
            CreateRandomFish(spawnCount);
            spawnCDTime = 0;
        }
    }

    public bool CreateRandomFish(int count)
    {
        if (fishPrefabs.Length < 1 || fishSpawnPos.Length < 1) return false;

        for (int i = 0; i < count; ++i)
        {
            GameObject prefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
            Transform trans = fishSpawnPos[Random.Range(0, fishSpawnPos.Length)];
            if(deadFish.Count > 0)
            {
                GameObject go = deadFish[0];
                deadFish.RemoveAt(0);
                FishCtrl f = go.GetComponent<FishCtrl>();
                f.Reset(trans);
            }
            else{
                Instantiate(prefab, trans.position, trans.rotation, transform);
            }
        }
        return true;
    }

    public void AddDeadFish(GameObject go)
    {
        deadFish.Add(go);
    }
}
