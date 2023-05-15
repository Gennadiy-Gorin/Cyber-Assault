using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultDrone : Enemy
{


    // Update is called once per frame
    void LateUpdate()
    {
        HandleBehaviour();
    }

    protected override void HandleBehaviour()
    {
        if (target == null) { target = gameObject.transform;
            GetComponent<Animator>().SetBool("isWalking", false);
        }
        if (isDead) return;
        MoveToTarget();
        RotateToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 moveDirection = (target.position - transform.position).normalized;
        //transform.position += currentSpeed * Time.deltaTime * moveDirection;
        gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2((transform.position.x + moveDirection.x * Time.fixedDeltaTime * currentSpeed), transform.position.y + moveDirection.y * currentSpeed * Time.fixedDeltaTime));
    }

    private void RotateToTarget()
    {
        float angle = Vector3.SignedAngle(Vector3.up, (target.position - transform.position).normalized, Vector3.forward);
        Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);
        if (angle < 0) transform.localScale = new Vector3(-1, 1, 1);//gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else transform.localScale = new Vector3(1, 1, 1);
        //transform.rotation = rotationToTarget;
    }

    
}
