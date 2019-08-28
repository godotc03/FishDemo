using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class FishCtrl : MonoBehaviour
{
    [SerializeField]
    private float swimmingSpeed = 0.5f;
    private float turnRoundInterval = 2.0f;      //five seconds make a random turn round

    private Animator anim;
    private SpriteRenderer spRenderer;
    private float fadeOutTime = 2.0f;
    private float resetTime = 8.0f;             //Reset to spawn pos after this time
    private float liveTime = 0f;
    private float turnRoundTime = 0f;
    private bool isDead = false;
    private bool isHit = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
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

    // For Test Animation
    private void OnMouseDown()
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
    }
}
