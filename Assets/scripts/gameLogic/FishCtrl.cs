using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class FishCtrl : MonoBehaviour
{
    [SerializeField]
    private float swimmingSpeed = 0.5f;
    private float turnRoundInterval = 2.0f;      //five seconds make a random turn round

    [SerializeField]
    private int worth_Gold = 10;
    [SerializeField]
    private int worth_Diamond = 0;

    public GameObject   goldPrefab;
    public GameObject   diamondPrefab;
    public int MaxHP = 5;

    private int HP = 0;

    private Animator anim;
    private SpriteRenderer spRenderer;
    private float fadeOutTime = 2.0f;
    private float resetTime = 10.0f;             //Reset to spawn pos after this time
    private float liveTime = 0f;
    private float turnRoundTime = 0f;
    private bool isDead = false;
    private bool isHit = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        liveTime += Time.deltaTime;

        if (liveTime >= resetTime)
        {
            Dead();
        }
        else if (liveTime >= resetTime - fadeOutTime || isDead)
        {
            spRenderer.color -= new Color(0,0,0,Time.deltaTime/fadeOutTime);
        }
        else if(!isHit)
        {
            Swimming();
        }
    }

    private void Swimming()
    {
        transform.Translate(transform.right * swimmingSpeed * Time.deltaTime, Space.World);
        if(turnRoundTime >= turnRoundInterval)
        {
            transform.Rotate(transform.forward * Random.Range(0, 45), Space.World);
            turnRoundTime = 0;
        }
        else
        {
            turnRoundTime += Time.deltaTime;
        }
    }


    private void PlayDying()
    {
        anim.SetBool("isHit", true);
        isHit = true;
    }

    public void Dead()
    {
        FishSpawner.Instance.AddDeadFish(gameObject);
        gameObject.SetActive(false);
        isDead = true;

    }

    public void Reset(Transform trans)
    {
        gameObject.SetActive(true);
        anim.SetBool("isHit", false);
        anim.Play("swimming");
        liveTime = 0;
        turnRoundTime = 0;
        isDead = false;
        isHit = false;
        spRenderer.color = new Color(1,1,1,1);
        transform.position = trans.position;
        transform.rotation = trans.rotation;
        HP = MaxHP;
    }

    public void TakeDamage(int value)
    {
        HP -= value;
        if(HP <= 0)
        {
            PlayDying();
            Invoke("Award", 0.5f);
        }
    }

    private void Award()
    {
        PlayerController.Instance.AddGold(worth_Gold);
        Instantiate(goldPrefab, transform.position, Quaternion.identity);

        if(worth_Diamond>0)
        {
            PlayerController.Instance.AddDiamond(worth_Diamond);
            Instantiate(diamondPrefab, transform.position+new Vector3(0.5f,0,0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "FishNet")
            isHit = true;
        Invoke("ReSwimming", 0.3f);
    }

    private void ReSwimming()
    {
        if(HP > 0)
            isHit = false;
    }
}
