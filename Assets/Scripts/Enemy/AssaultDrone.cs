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
        if (target == null) target = gameObject.transform;
        MoveToTarget();
        RotateToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 moveDirection = (target.position - transform.position).normalized;
        transform.position += currentSpeed * Time.deltaTime * moveDirection;
    }

    private void RotateToTarget()
    {
        float angle = Vector3.SignedAngle(Vector3.up, (target.position - transform.position).normalized, Vector3.forward);
        Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);

        //transform.rotation = rotationToTarget;
    }

    
}
