using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBonus : Bonus
{
    private PlayerController player;
    private float defaultBonus;
    private float bonusXP;
    public override void Activate()
    {
        bonusLevel = 1;
        player = gameObject.GetComponentInParent<PlayerController>();
        defaultBonus = player.XpBonus;
        bonusXP = 0.25f;
        player.XpBonus = bonusXP;
        maxlevel = 4;
    }

    public override void Deactivate()
    {
        player.XpBonus = defaultBonus;
        Destroy(gameObject);
    }

    public override void Upgrade()
    {
        if (bonusLevel <= maxlevel)
        {
            bonusLevel++;
            bonusXP += 0.25f;
            player.XpBonus = bonusXP;

        }
    }
}
