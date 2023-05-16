using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField]
    private  SkillTree tree;

    public SkillTree Tree { get => tree; }

    public  void ResetTree() {

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

}
