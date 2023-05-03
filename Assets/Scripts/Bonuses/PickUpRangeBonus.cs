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
        //playerCollider = GetComponentInParent<CircleCollider2D>();
        playerCollider = GetPlayerPickUp();
        radius = 1;
        playerCollider.radius = 1.5f;
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

    private CircleCollider2D GetPlayerPickUp() {

        Transform parent = transform.parent;
        CircleCollider2D circle=null;
        foreach (Transform transform in parent)
        {
            if (transform.CompareTag("Collecter"))
            {
                circle = transform.gameObject.GetComponent<CircleCollider2D>();
                break;
            }
        }
        return circle;
    }

   
}
