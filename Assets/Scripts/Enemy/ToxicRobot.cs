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
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {

        Vector3 pos = transform.position;
        pos.y = pos.y-0.5f ;

        yield return new WaitForSeconds(0.2f);
        GameObject puddle = Instantiate(toxicPuddle, pos, new Quaternion(0, 0, 0, 0));
        puddle.GetComponent<ToxicPuddle>().SetDamage(data.EnemyDamage + data.EnemyLevel * 2f);
    }
}
