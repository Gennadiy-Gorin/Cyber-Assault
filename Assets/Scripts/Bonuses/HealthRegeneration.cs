using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegeneration : Bonus
{
    private float regenPersentAmount;
    private float regenRate;
    private Health playerHealth;
    private float healthTimer = 0;

    public override void Activate()
    {
        bonusLevel = 1;
        maxlevel = 4;
        playerHealth = gameObject.GetComponentInParent<Health>();
        regenPersentAmount = 1f;
        regenRate = 5f;
        
    }

    public override void Deactivate()
    {
        Destroy(this.gameObject);
    }

    public override void Upgrade()
    {
        if (bonusLevel <= maxlevel)
        {
            bonusLevel++;
            regenPersentAmount += 0.75f;
            regenRate -= 1f;
        }
    }

   
    void Update()
    {
        if (playerHealth != null) {
            healthTimer += Time.deltaTime;
            if (healthTimer >= regenRate) {
                healthTimer = 0;
                playerHealth.RecieveHealing(regenPersentAmount,true);
            
            }
        
        }

    }
}
