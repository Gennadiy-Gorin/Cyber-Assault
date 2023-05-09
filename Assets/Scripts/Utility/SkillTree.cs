using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skilltree", menuName = "Skill Tree", order = 54)]
public class SkillTree : ScriptableObject
{
    [System.Serializable]
    public  class Skill {
       public  string name;
        public string description;
        public Sprite icon;
        public  int level;
    
    }

    [SerializeField]
    public  List<Skill> skills;

    
}
