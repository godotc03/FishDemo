using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public int GunLevel { get; private set; }
    public int GoldCount { get; private set; }
    public int DiamondCount { get; private set; }

    public Transform[] bullets;

    private bool canAttack = true;  //For CD

    [SerializeField]
    private float CDTime = 1f;
    private float refillTime = 0f;
    [SerializeField]
    private Transform firePos;

    //For gun level change
    [SerializeField]
    private float GunChangeCD = 3f;
    public int MaxGunLevel = 3;
    private bool IsGunChangeable = true;
    private float gunLevelChangeCD = 3f;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        GunLevel = 0;
        GoldCount = 1000;
        DiamondCount = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        refillTime = 0f;
        GunController.instance.RefreshGunLevel(GunLevel, MaxGunLevel);
        GunController.instance.RefreshAward(GoldCount, DiamondCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {
            refillTime += Time.deltaTime;
            if (refillTime >= CDTime)
            {
                canAttack = true;
                refillTime = 0;
            }
        }
        RotateGun();
        if (canAttack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
            }
        }

        if(!IsGunChangeable)
        {
            gunLevelChangeCD -= Time.deltaTime;
            if(gunLevelChangeCD <= 0)
            {
                gunLevelChangeCD = GunChangeCD;
                IsGunChangeable = true;
            }
        }
    }
    private void RotateGun()
    {
        Vector3 msPos = Input.mousePosition;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(msPos);

        Vector3 gunPos = transform.position;
        Vector3 targetDir = targetPos - gunPos;
        float rotateAngle = Vector2.Angle(targetDir, Vector3.up);

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
        Instantiate(bullets[GunLevel], firePos.position, transform.rotation);
    }

    public void AddGold(int v)
    {
        GoldCount += v;
        GunController.instance.RefreshAward(GoldCount, DiamondCount);
    }

    public void AddDiamond(int v)
    {
        DiamondCount += v;
        GunController.instance.RefreshAward(GoldCount, DiamondCount);
    }

    public void ChangeGunLevel(int delt)
    {
        GunLevel += delt;
        IsGunChangeable = false;
        if(GunLevel < 0){
            GunLevel = 0;
        }else if(GunLevel > MaxGunLevel)
        {
            GunLevel = MaxGunLevel;
        }

        GunController.instance.RefreshGunLevel(GunLevel, MaxGunLevel);

    }
}
