using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, Damaging
{
    [SerializeField]
    private float damage;
    private float speed;

    public float Speed { get => speed; set => speed = value; }

    void Start()
    {
        speed=10f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position +=transform.up * Time.deltaTime * Speed;
    }

    public Bullet (float damage) {
        this.damage = damage;
    }

    public float GetDamage()
    {

        return damage;
       // throw new System.NotImplementedException();
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
    
}
