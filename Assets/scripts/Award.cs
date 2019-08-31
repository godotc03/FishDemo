using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour
{
    private Vector3 targetPos;
    [SerializeField]
    private float FlyTime = 2.8f;
    private float stayTime = 0.8f;
    private float curFlyTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = PlayerController.Instance.transform.position;
        Destroy(gameObject, FlyTime + stayTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(stayTime > 0)
        {
            stayTime -= Time.deltaTime;
        }
        else
        {
            MoveToPlayer();
        }
    }

    private void  MoveToPlayer()
    {
        curFlyTime += Time.deltaTime; 
        transform.position = Vector3.Lerp(transform.position, targetPos, curFlyTime/FlyTime);
    }
}
