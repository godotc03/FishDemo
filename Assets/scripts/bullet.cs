using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class bullet : MonoBehaviour
{
    private float moveSpeed = 3f;
    private Rigidbody2D rg;
    private void Awake()
    {
        Destroy(gameObject, 3);
        rg = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rg.velocity = transform.right * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
