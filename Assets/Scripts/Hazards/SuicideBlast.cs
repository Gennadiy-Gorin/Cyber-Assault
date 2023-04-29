using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBlast : MonoBehaviour,Damaging
{
    private float blastRadius;
    private float blastDamage;
    private float delay;
    private float radiusIncrease;
    private void OnEnable()
    {
     //   gameObject.GetComponentInParent<Health>().TakeDamage(1000);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.localScale = new Vector3(0, 0, 1);
        StartCoroutine("BlastDelay");
        radiusIncrease = blastRadius / delay;
    }


    void Update()
    {
        if (gameObject.transform.localScale.x < blastRadius)
        {
            gameObject.transform.localScale += new Vector3(radiusIncrease, radiusIncrease, 1) * Time.deltaTime;
        }

    }

    public void SetParams(float radius, float damage, float delay) {
        blastRadius = radius;
        blastDamage = damage;
        this.delay = delay;
    }

    IEnumerator BlastDelay() {

        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<Animator>().SetBool("exploding",true);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Bonus";
        Invoke("DestroyEnemy", 0.2f);
       
        //Destroy(gameObject, 0.35f);

    }

   
    public float GetDamage()
    {
        return blastDamage;
    }

    private void DestroyEnemy() {

        //gameObject.GetComponentInParent<Health>().enabled = true;
        gameObject.GetComponentInParent<Health>().TakeDamage(1000);
       // Destroy(gameObject);
    }
}
