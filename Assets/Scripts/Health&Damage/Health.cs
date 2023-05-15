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

    [SerializeField]
    private GameObject healthBar;

    private float xPosition;

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
            ChangeUI();
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
        ChangeUI();
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
            return;
        }
        else gameObject.GetComponent<PlayerController>().Death();
       // Destroy(gameObject);
       
    }

    public void ChangeMaxHp(float increasePersantage) {

        if (increasePersantage <= 0) maxHealth = basicHealth;
        else maxHealth+= basicHealth * 0.01f * increasePersantage;
        ChangeUI();


    }

    private void ChangeUI() {
        if (healthBar == null) return;


        float percentage = 1-(currentHealth / maxHealth);//[0,1]


       // float baseX = healthBar.transform.parent.position.x;
        Vector3 baseTransform = healthBar.transform.parent.position;
        // Debug.Log(baseX);

        healthBar.transform.position = new Vector3(baseTransform.x-percentage, healthBar.transform.position.y, baseTransform.z);
    
    }
    IEnumerator Invinceble() {

        yield return new WaitForSeconds(timeInvinceble);
        isInvinceble = false;
    
    }
   
}
