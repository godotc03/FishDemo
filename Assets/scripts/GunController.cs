﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public static GunController instance;
    public Sprite[] GunImages;


    //TODO make animation and remove this code
    public Transform idlePos;
    public Transform attackPos;
    public Transform gunImage;

    //[HideInInspector]
    //public float rotateSpeed = 5f;
    private Image img;

    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
        img = gunImage.GetComponent<Image>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RotateGun();
        img.sprite = GunImages[PlayerController.Instance.GunLevel - 1];
    }

    private void RotateGun()
    {
        Vector3 msPos = Input.mousePosition;
        Vector3 targetPos = msPos;
        Vector3 gunPos = transform.position;
        Vector3 targetDir = targetPos - gunPos;
        float rotateAngle = Vector2.Angle(targetDir, Vector3.up);
        Debug.Log("targetPos:" + targetPos.ToString());

        if (targetPos.x > gunPos.x)
        {
            rotateAngle = -rotateAngle;
        }

        //clamp
        if (rotateAngle < -75)
        {
            rotateAngle = -75;
        }
        else if (rotateAngle > 75)
        {
            rotateAngle = 75;
        }

        transform.eulerAngles = new Vector3(0, 0, rotateAngle);
    }

    private void Attack()
    {
        transform.position = Vector3.Lerp(transform.position, attackPos.position, 0.5f);
        Invoke("ResetPos", 0.4f);

    }

    private void ResetPos()
    {
        transform.position = Vector3.Lerp(transform.position, idlePos.position, 0.2f);
    }

}
