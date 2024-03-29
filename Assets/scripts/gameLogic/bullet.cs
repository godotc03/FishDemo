﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class bullet : MonoBehaviour
{
    private float moveSpeed = 3f;
    private Rigidbody2D rg;
    //private int damage = 5;

    public GameObject netPrefab;

    private void Awake()
    {
        Destroy(gameObject, 3f);
        rg = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rg.velocity = transform.up * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Fish")
        {
            FishCtrl ctrl = other.transform.GetComponent<FishCtrl>();
            if(ctrl != null)
            {
                //ctrl.TakeDamage(damage);
            }

            Instantiate(netPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
