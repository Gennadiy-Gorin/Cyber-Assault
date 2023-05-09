using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
    private Button SkillPage;

    [SerializeField]
    private Button LevelPage;


    [SerializeField]
    private GameObject maxlevel;

    private SkillTree.Skill skill;
    private int index;

    private int skillPoints;

 public void Activate(SkillTreeManager tree) {
        descriptionPanel.SetActive(false);
        skillTree = tree;
        skillPoints = PlayerPrefs.GetInt("Points", 0);
        money.text = ": " + skillPoints;
        SkillPage.Select();
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
        if (skillPoints < 1) return;
        if (!skillTree.UpgradeSkill(skill.name)) {
            Debug.LogWarning("Unable to upgrade");
            return;
        }
        ShowButton();
        SetStars(skill.level);

        skillPoints--;
        money.text = ": " + skillPoints;
        PlayerPrefs.SetInt("Points", skillPoints);
        showSkill(index);

    }

    private void ShowButton() {

        if (skill.level < 4)
        {
            button.SetActive(true);
            maxlevel.SetActive(false);
        }
        else
        {
            button.SetActive(false);
            maxlevel.SetActive(true);
        }

    }

}
