using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
    private Vector3 offcet = new Vector3(0f, 0f, -10f);
  //  private float time = 0.1f;
    private Vector3 velocity = Vector3.zero;
    Vector3 camPosition;
    void Start()
    {
       // target = GameObject.FindGameObjectWithTag("player");
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }
    
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position + offcet;
            // camPosition = target.transform.position;
            // camPosition.z = -10f;
            // this.transform.position = camPosition;
            transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref velocity, Time.fixedDeltaTime);
        }
    }
}
