using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
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
            camPosition = target.transform.position;
            camPosition.z = -10f;
            this.transform.position = camPosition;
        }
    }
}
