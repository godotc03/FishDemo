using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class fish : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // For Test Animation
    private void OnMouseDown()
    {
        anim.SetBool("isHit", true);
        
    }

    public void Reset()
    {
        anim.SetBool("isHit", false);
        anim.Play("swimming");
    }
}
