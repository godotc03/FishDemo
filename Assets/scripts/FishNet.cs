using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class FishNet : MonoBehaviour
{
    public int damage = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Fish")
        {
            FishCtrl ctrl = other.transform.GetComponent<FishCtrl>();
            if (ctrl != null)
            {
                ctrl.TakeDamage(5);
            }
        }
    }

    public void OnFinish()
    {
        Destroy(gameObject);
    }
}
