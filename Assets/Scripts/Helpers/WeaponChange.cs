using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{

    public ShootingController playerGun;
    public GunData gun;
    //private int count;
    //private int t = 0;

    void Start()
    {
        //count = gun.Length;
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
        //if (t == count - 1) t = 0;
       // else t++;

        playerGun.NewGun( gun);
    }
}
