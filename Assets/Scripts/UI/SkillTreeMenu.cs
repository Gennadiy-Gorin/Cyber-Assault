using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SkillTreeMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject descriptionPanel;

    private SkillTreeManager skillTree;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text skillName;
    [SerializeField]
    private Text descriprion;

    [SerializeField]
    private Text money;

    [SerializeField]
    private Image[] stars;

    [SerializeField]
    private GameObject button;

  


    [SerializeField]
    private GameObject maxlevel;

    [SerializeField]
    private Text costText;

  

    private SkillTree.Skill skill;
    private int index;

    private int skillPoints;

 public void Activate(SkillTreeManager tree) {
        descriptionPanel.SetActive(false);
        skillTree = tree;
        skillPoints = PlayerPrefs.GetInt("Points", 0);
        money.text = ": " + skillPoints;

        
    }



    public void showSkill(int number) {

        index = number;
        if (number >= skillTree.Tree.skills.Count) return;
        skill = skillTree.Tree.skills[number];
        ResetStars();
        descriprion.text = skill.description;

        skillName.text = skill.name;
        icon.sprite = skill.icon;

        ShowButton();
      
       SetStars(skill.level);

        descriptionPanel.SetActive(true);
    }

    private void SetStars(int level) {
        


        for (int i = 0; i < level; i++) {

            stars[i].color = Color.white;

        }
    
    }

    private void ResetStars()
    {

        foreach (Image star in stars) {
            star.color = Color.black;
           
        }
    }

    public void Upgrade() {
        int cost = CalculateCost(skill.level);
        if (skillPoints < cost) return;
        if (!skillTree.UpgradeSkill(skill.name)) {
            Debug.LogWarning("Unable to upgrade");
            return;
        }
        ShowButton();
        SetStars(skill.level);

        skillPoints-=cost;
        money.text = ": " + skillPoints;
        PlayerPrefs.SetInt("Points", skillPoints);
        showSkill(index);

    }

    private void ShowButton() {

        if (skill.level < 4)
        {
            costText.text=CalculateCost(skill.level).ToString();
            button.SetActive(true);
            maxlevel.SetActive(false);
        }
        else
        {
            button.SetActive(false);
            maxlevel.SetActive(true);
        }

    }

    private int CalculateCost(int level)
    {
        int cache1 = 0;
        int cache2 = 1;

        int cache3=cache1;
        if (level == 1) return cache2 + 1;
        for (int i = 1; i < level; i++)
        {
            cache3 = cache1 + cache2;

            cache1 = cache2;
            cache2 = cache3;
        
        }
        return cache3+1;
    }
}
