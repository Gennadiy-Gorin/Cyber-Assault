using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHelper : MonoBehaviour
{
    public GameObject[] bonuses;
    public BonusController controller;
    private int length;
    private int t = 0;
    bool active = false;

    void Start()
    {
        length = bonuses.Length;
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!active)
                ChangeBonus();

        }
        else if (Input.GetKeyDown(KeyCode.U)) {

            controller.activeBonuses[0].GetComponent<Bonus>().Upgrade();
        
        }

    }

    private void ChangeBonus()
    {

        if (t == length - 1) t = 0;
         else t++;
        GameObject bonus=Instantiate(bonuses[t], controller.gameObject.transform);

        controller.AddBonus(bonus.GetComponent<Bonus>(),-1);

    }
}
