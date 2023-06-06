using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : Bonus
{
    private float healthIncrease;
    private Health playerHealth;


    public override void Activate()
    {
        bonusLevel = 1;
        maxlevel = 4;
        healthIncrease = 30f;
        playerHealth = gameObject.GetComponentInParent<Health>();
        if (playerHealth == null) {
            Debug.LogWarning("IncreaseHealth: cant get player health");
            Deactivate();
        }
        ApplyBonus(healthIncrease);

    }

    public override void Deactivate()
    {
        ApplyBonus(-30f*bonusLevel);
        Destroy(this.gameObject);
    }

    public override void Upgrade()
    {
        if (bonusLevel <= maxlevel)
        {
            bonusLevel++;
            healthIncrease += 30f;
            ApplyBonus(healthIncrease);
        }

    }

    private void ApplyBonus(float amount) {

        if (playerHealth != null) {

            playerHealth.ChangeMaxHp(amount);
        
        }
    
    }
    
}
