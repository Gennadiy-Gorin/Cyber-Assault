using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Need too add effects on hit and death

    public float currentHealth = 10f;
    public float maxHealth = 10f;
    private float basicHealth;
    private float timeInvinceble;
    private bool isInvinceble;

    private void Start()
    {
        basicHealth = maxHealth;
        currentHealth = maxHealth;
        isInvinceble = false;
        timeInvinceble = 0.3f;
    }

    public void TakeDamage(float damageAmount)
    {
        if (!isInvinceble)
        {
          //  Debug.Log(damageAmount);
            if (gameObject.GetComponent<PlayerController>() != null)
            {
              currentHealth-=damageAmount*(1f-GetComponent<PlayerController>().GetDamageResistance());
            }
            else currentHealth -= damageAmount;
            
            isInvinceble = true;
            StartCoroutine("Invinceble");
            CheckDeath();
        }
    }

    public void RecieveHealing(float healAmount, bool isPersentage)
    {
        if (isPersentage) {
            currentHealth += maxHealth / 100f * healAmount;
        }
        else { 
            currentHealth += healAmount; 
        }

         if (currentHealth > maxHealth) currentHealth = maxHealth;
    }


    private void CheckDeath() {

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die() {

        if (gameObject.GetComponent<Enemy>() != null)
        {
            gameObject.GetComponent<Enemy>().DoBeforeDestroy();
        }
        Destroy(this.gameObject);
    }

    public void ChangeMaxHp(float increasePersantage) {

        if (increasePersantage <= 0) maxHealth = basicHealth;
        else maxHealth+= basicHealth * 0.01f * increasePersantage;
    
    }

    IEnumerator Invinceble() {

        yield return new WaitForSeconds(timeInvinceble);
        isInvinceble = false;
    
    }
}
