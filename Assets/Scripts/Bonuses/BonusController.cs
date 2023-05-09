using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    public List<GameObject> activeBonuses;

    [SerializeField]
    private int maxBonusNumber; 

    private float damageBuff;

    public int MaxBonusNumber { get => maxBonusNumber; set => maxBonusNumber = value; }

    void Start()
    {
        activeBonuses = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuffDamage(float buffPersentage) {
        damageBuff = buffPersentage;

       /* foreach (GameObject bonus in activeBonuses) {
            if (bonus.GetComponent<Bonus>().type == BonusType.Attack)
            {
             Buffable bonusBuff= (Buffable) bonus.GetComponent(typeof(Buffable));
                bonusBuff.Buff(buffPersentage);
            }
        }*/
    }

    public void AddBonus(Bonus newBonus,int place)
    {
        /* if (activeBonuses.Count != 0)
         {
             activeBonuses[0].GetComponent<Bonus>().Deactivate();
             activeBonuses.Insert(0, newBonus.gameObject);
         }
         else activeBonuses.Add(newBonus.gameObject);
         newBonus.Activate();
         if (newBonus.type == BonusType.Attack && damageBuff != 0) {
             Buffable bonusBuff = (Buffable)newBonus.GetComponent(typeof(Buffable));
             bonusBuff.Buff(damageBuff);
         }*/
        /* int position = isActivatedBonus(newBonus);
         if (position>=0) {
             activeBonuses[position].GetComponent<Bonus>().Upgrade();
             return;
          }*/
        if (newBonus.type == BonusType.Filler) {
            newBonus.Activate();
            StartCoroutine(DeactivateBonusOverTime(newBonus, 1f));
            return;
        }


        if (place >= 0) {
            activeBonuses[place].GetComponent<Bonus>().Deactivate();
            activeBonuses.Insert(place, newBonus.gameObject);
        }else activeBonuses.Add(newBonus.gameObject);

        newBonus.Activate();
        if (newBonus.type == BonusType.Attack && damageBuff != 0)
        {
            Buffable bonusBuff = (Buffable)newBonus.GetComponent(typeof(Buffable));
            bonusBuff.Buff(damageBuff);
        }

    }

    public void RemoveBonus(Bonus bonusToRemove)
    {

        /* foreach (GameObject bonus in activeBonuses) {

             if (bonus.GetComponent<Bonus>().bonusName == bonusToRemove.name) {


                 activeBonuses.Remove(bonus);
                 bonus.GetComponent<Bonus>().Deactivate();
             }

         }
        */

        Debug.Log("Remove function is called");
        

        for (int i = 0; i < activeBonuses.Count; i++) {

            Bonus temp = activeBonuses[i].GetComponent<Bonus>();
            if (temp.bonusName == bonusToRemove.bonusName) {

                Debug.Log("Found correspinding bonus");
                activeBonuses.RemoveAt(i);
                temp.Deactivate();

            }
        

        }
    
    
    
    }

    IEnumerator DeactivateBonusOverTime(Bonus bonus, float time) {

        yield return new WaitForSeconds(time);
        bonus.Deactivate();
    
    }

    private int isActivatedBonus(Bonus bonus) {

        for (int i= 0; i <activeBonuses.Count;i++)
        {
            if (activeBonuses[i].GetComponent<Bonus>().bonusName == bonus.bonusName) return i;

        }
        return-1;

    }

}
