using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField]
    private  SkillTree tree;

    public SkillTree Tree { get => tree; }

    public  void ResetTree() {

        PlayerPrefs.SetString("Skills", "0 0 0 0 0 0 2 0");
        LoadSkillTree();

        foreach (SkillTree.Skill skill in tree.skills) {

            
            skill.level = 0;
            if (skill.name == "More pockets") skill.level = 2;
        }
    
    }


    public bool UpgradeSkill(string skillName) {

        

        foreach (SkillTree.Skill skill in tree.skills)
        {

            if (string.Compare(skill.name, skillName)==0)
            {
                if (skill.level >= 4) return false;
                 skill.level++;
                SaveSkillTree();
                return true;

            }

        }
        Debug.LogWarning("No skill with such name");
        return false;
    }


    public int GetSkillLevel(string skillName) {

        foreach (SkillTree.Skill skill in tree.skills)
        {

            if (skill.name == skillName) {
                return skill.level;
            
            }


        }


        Debug.LogWarning("No skill with such name");
        return 0;
    }

    private void SaveSkillTree() {
        string temp="";
        foreach (SkillTree.Skill skill in tree.skills)
        {
            temp += skill.level.ToString()+" ";

        }

        temp=temp.Trim();
        PlayerPrefs.SetString("Skills", temp);

    }

    public void LoadSkillTree() {
        string skills = PlayerPrefs.GetString("Skills", "none"); // "0 0 0 0 0 0 2 0"
        if (skills == "none")
        {
            Debug.Log("Cannot find tree. Setting default values");
            ResetTree();
            return;

        }

       var mass=skills.Split(' ').Select(Int32.Parse).ToArray();


        for (int i = 0; i < mass.Length; i++) {

            tree.skills[i].level = mass[i];
        
        
        }


    }

}
