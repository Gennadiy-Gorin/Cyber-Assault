using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusButtonFill : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Text bonusName;

    [SerializeField]
    private Text description;

    [SerializeField]
    private Image[] stars;


    public void FillButton(Bonus bonus) {

        if (bonus != null) {
            for (int i = 0; i < stars.Length; i++)
            {
               stars[i].gameObject.GetComponent<Animator>().enabled=false;
                stars[i].color = Color.black;

            }
            image.sprite = bonus.icon;
            bonusName.text = bonus.bonusName;
          
            description.text = bonus.description;
            foreach (Image star in stars) {
                if (bonus.type ==BonusType.Filler)
                {
                    star.enabled = false;
                  
                }else star.enabled = true;


            } 
            

            for (int i = 0; i < bonus.BonusLevel; i++) {
               
                stars[i].color = Color.white;          
            }
            if(bonus.BonusLevel<4) stars[bonus.BonusLevel].gameObject.GetComponent<Animator>().enabled = true;

        }
    
    }

}
