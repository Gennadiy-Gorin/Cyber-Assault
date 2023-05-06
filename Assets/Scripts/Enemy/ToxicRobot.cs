using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicRobot : AssaultDrone
{
    [SerializeField]
    private GameObject toxicPuddle;

    public override void DoBeforeDestroy()
    {
        base.DoBeforeDestroy();
        GameObject puddle=Instantiate(toxicPuddle, transform.position,new Quaternion(0,0,0,0));
        puddle.GetComponent<ToxicPuddle>().SetDamage(data.EnemyDamage+data.EnemyLevel*2f);
    }
}
