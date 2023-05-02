using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHP : Bonus
{
    private Health playerHealth;
    [SerializeField]
    private int healPercentage;

    public override void Activate()
    {
        bonusLevel = -1;
        playerHealth = gameObject.GetComponentInParent<Health>();
        if (playerHealth != null) {
            playerHealth.RecieveHealing(healPercentage, true);
        
        }
    }

    public override void Deactivate()
    {
        Destroy(this.gameObject);
    }

    public override void Upgrade()
    {
        Debug.LogWarning("WTF. You can't upgrade it");
    }

}
