using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Tooltip("Whether or not to apply damage when triggers collide")]
    public bool dealDamageOnTriggerEnter = false;
    [Tooltip("Whether or not to apply damage when triggers stay, for damage over time")]
    public bool dealDamageOnTriggerStay = false;
    [Tooltip("Whether or not to destroy the attached game object after dealing damage")]
    public bool destroyAfterDamage = true;

    public bool damagePlayer = true;
    public bool damageEnemy = false;
    public bool singleHit = true;
    private bool isDealingDamage = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealDamageOnTriggerEnter&&!collision.isTrigger)
        {
            if (isDealingDamage && collision.gameObject==null) return;
            isDealingDamage = true;
            DealDamage(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dealDamageOnTriggerStay && !collision.isTrigger)
        {
            if (isDealingDamage && collision.gameObject == null) return;
            DealDamage(collision.gameObject);
        }
    }

    private void DealDamage(GameObject collisionGameObject)
    {
      
        Health collidedHealth = collisionGameObject.GetComponent<Health>();

        if (collidedHealth != null)
        {
            Damaging dam = (Damaging)this.GetComponent(typeof(Damaging));
            
            if (collisionGameObject.tag == "Player" && damagePlayer == true)
            {
               
                //Debug.Log("Colided tag: " + collisionGameObject.tag);
                collidedHealth.TakeDamage(dam.GetDamage());

            }
            else if (collisionGameObject.tag == "Enemy" && damageEnemy == true)
            {
               
                collidedHealth.TakeDamage(dam.GetDamage());
                //Debug.Log("DealingDamage:" + dam.GetDamage());
            } else return;
                       
            if (destroyAfterDamage)
            {
                if (gameObject.GetComponent<Enemy>() != null)
                {
                    //gameObject.GetComponent<Enemy>().DoBeforeDestroy();
                }
                Destroy(this.gameObject);
            }
          

        }
        if (!singleHit) isDealingDamage = false;
    }

}
