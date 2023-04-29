using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{

    public ShootingController playerGun;
    public GunData[] guns;
    private int count;
    private int t = 0;

    void Start()
    {
        count = guns.Length;
        //playerGun.GunData = guns[t];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {

            ChangeGun();
        
        }

    }
    private void ChangeGun()
    {
        if (t == count - 1) t = 0;
        else t++;

        playerGun.NewGun( guns[t]);
    }
}
