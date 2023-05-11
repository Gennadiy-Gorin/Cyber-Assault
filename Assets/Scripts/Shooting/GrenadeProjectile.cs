using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : Bullet
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private GameObject blastPrefab;

    private void OnDestroy()
    {
        if (GameManager.isComplete) return;
        Debug.Log("grenade damage= " + GetDamage());
        GameObject blast = Instantiate(blastPrefab, transform.position, new Quaternion(0, 0, 0, 0));
        blast.GetComponent<Explosion>().Activate(GetDamage(), radius);
    }
}
