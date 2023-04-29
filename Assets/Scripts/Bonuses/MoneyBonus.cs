using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBonus : Bonus
{
    private PlayerController player;
    private float bonus;
    public override void Activate()
    {
        bonusLevel = 1;
        player = gameObject.GetComponentInParent<PlayerController>();
        bonus = 0.25f;
        maxlevel = 4;
        player.MoneyBonus = bonus;
    }

    public override void Deactivate()
    {
        player.MoneyBonus = 0;
        Destroy(this.gameObject);
    }

    public override void Upgrade()
    {
        if (bonusLevel <= maxlevel) {
            bonusLevel++;
            bonus += 0.25f;
            player.MoneyBonus = bonus;

        }
    }
}
