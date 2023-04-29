using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Bonus
{

    private float speedScale;
    private Move playerMovement;
    private float basicSpeed;

    


    public override void Upgrade()
    {
        bonusLevel++;
        speedScale += 0.2f;
        playerMovement.Speed = basicSpeed*speedScale;
    }

    public override void Activate()
    {
        bonusLevel = 1;
        speedScale = 1.2f;
        playerMovement = this.GetComponentInParent<Move>();
        basicSpeed = playerMovement.Speed;
        playerMovement.Speed =playerMovement.Speed* speedScale;


        Debug.Log("sped scale " + speedScale);
        Debug.Log("Speed" + playerMovement.Speed);

    }

    public override void Deactivate()
    {
        playerMovement.Speed = basicSpeed;
        Destroy(this.gameObject);
    }

}
