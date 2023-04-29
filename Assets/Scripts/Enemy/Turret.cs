using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public Transform gun;
    private float speed;
    protected override void HandleBehaviour()
    {
        if (target == null) target = gameObject.transform;
        RotateGunToTarget();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) target = gameObject.transform;
        speed=45;
    }

    // Update is called once per frame
    void Update()
    {
        HandleBehaviour();
    }

    private void RotateGunToTarget()
    {
        float angle = Vector3.SignedAngle(Vector3.up, (target.position - transform.position).normalized, Vector3.forward);
       // Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);
       // if (angle < 0 && angle > -180) gun.gameObject.GetComponent<SpriteRenderer>().flipX = false;
       // else if (angle >= 0 && angle < 180) gun.gameObject.GetComponent<SpriteRenderer>().flipX = true;
      //  gun.localRotation = rotationToTarget;
       // float deltaRotationZ = angle - gun.localRotation.eulerAngles.z;
        Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);
        gun.localRotation = Quaternion.RotateTowards(gun.localRotation,rotationToTarget,speed*Time.deltaTime);
    }

    
}
