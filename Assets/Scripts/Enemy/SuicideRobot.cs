using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideRobot : AssaultDrone
{
    private float blastRadius;
    [SerializeField]
    private float blastDelay;
    private GameObject blast;
    private bool isSelfDestoying=false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isSelfDestoying)
        {
            isSelfDestoying = true;
            target = null;
            GetComponent<Rigidbody2D>().isKinematic = true;
            foreach (BoxCollider2D coliders in GetComponents<BoxCollider2D>())
            {
                coliders.enabled = false;
            }
            StartExplosion();

        }
    }
    private void StartExplosion()
    {
        blastRadius = 1.5f + data.EnemyLevel * 0.2f;
        //gameObject.GetComponent<Health>().enabled = false;
        gameObject.GetComponentInChildren<SuicideBlast>().SetParams(blastRadius,data.EnemyDamage,blastDelay);
        gameObject.GetComponentInChildren<SuicideBlast>().enabled = true;

    }

    public override void DoBeforeDestroy()
    {
        if (!isSelfDestoying) base.DoBeforeDestroy();
    }
}
