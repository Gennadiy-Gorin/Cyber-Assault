using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRangeBonus : Bonus
{
    private CircleCollider2D playerCollider;
    private int radius;
    public override void Activate()
    {
        bonusLevel = 1;
        playerCollider = GetComponentInParent<CircleCollider2D>();
        radius = 1;
        maxlevel = 4;
    }

    public override void Deactivate()
    {

        playerCollider.radius = 1;
        Destroy(this.gameObject);
    }

    public override void Upgrade()
    {
        if (bonusLevel <= maxlevel) {
            bonusLevel++;
            radius += 1;
            playerCollider.radius = radius;
        
        }
    }

   
}
