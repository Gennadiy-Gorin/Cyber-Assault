using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour,Damaging
{

    [SerializeField]
    private float speed;


    [SerializeField]
    private GameObject blastPrefab;

    private float blastDamage;
    private float blastRadius;

    private void Start()
    {
        //blastDamage = 0f;
       // blastRadius = 0f;
    }

    private void Update()
    {
        this.transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnDestroy()
    {
        Debug.Log("Rocket damage= " + blastDamage);
       GameObject blast= Instantiate(blastPrefab, transform.position, new Quaternion(0,0,0,0));
        blast.GetComponent<Explosion>().Activate(blastDamage, blastRadius);
    }

    public float GetDamage()
    {
        return blastDamage;
    }

    public void SetParams(float damage, float radius) {

        blastDamage = damage;
        blastRadius = radius;

    }
}
