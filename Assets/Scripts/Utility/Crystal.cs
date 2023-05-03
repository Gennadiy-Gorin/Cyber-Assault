using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]
    private int value = 100;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collecter") {
            collision.GetComponentInParent<PlayerController>().ChangeMoney(value, false);
            Destroy(gameObject);
        }
    }
}
