using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField]
    private  List<LevelData> levels;

    private LevelData currentLevel;

    [SerializeField]
    private LevelMenu levelMenu;

    private static int currentLevelReward = 1;


   public void SelectLevels() {
        int index = CurrentLevel(PlayerPrefs.GetString("CurrentLevel", "none"));
        if (index < 0) {
            Debug.LogError("No level saved");
            return;
        }
        currentLevel = levels[index];
        currentLevel.IsComplete = true;
        currentLevelReward = currentLevel.Reward;
        levelMenu.SetLevels(FindSameDifficulty(), FindHigherDifficulty());
    }



    public void ResetLevels() {

        foreach (LevelData level in levels) {

            level.IsComplete = false;
        }
    
    }



    private LevelData FindHigherDifficulty() {
        if (currentLevel.Difficulty == Difficulty.Hard) {
            return FindSameDifficulty();
        
        }


        foreach (LevelData level in levels)
        {

            if (level.IsComplete == false && level.Difficulty == currentLevel.Difficulty + 1) return level ;
        }

        return null;

    }

    private LevelData FindSameDifficulty()
    {

        foreach (LevelData level in levels)
        {

            if (level.IsComplete == false && level.Difficulty == currentLevel.Difficulty) return level;
        }



        return null;
    }

    private int CurrentLevel(string name)
    {
        if (name == "none") return -1;

        for (int i = 0; i < levels.Count; i++) {

            if (levels[i].LevelName == name) return i;
        
        }


        return -1;

    }

   

}
