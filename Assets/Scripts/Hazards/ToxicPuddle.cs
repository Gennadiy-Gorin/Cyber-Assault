using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPuddle : MonoBehaviour,Damaging
{
    [SerializeField]
    private float damage;
    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float damageAmount) {
        damage = damageAmount;
    }

    void Start()
    {
       // damage = 0; 
    }

}
