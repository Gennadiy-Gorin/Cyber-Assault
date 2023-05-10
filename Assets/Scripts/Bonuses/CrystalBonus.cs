using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBonus : Bonus
{
    private PlayerController player;

    public override void Activate()
    {
        player = GetComponentInParent<PlayerController>();
        bonusLevel = -1;
        player.ChangeMoney(500, false);
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
