using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{

    private LevelData sameDifficulty;
    private LevelData harderDifficulty;

    [SerializeField]
    private GameObject description;


    [SerializeField]
    private Text levelName;

    [SerializeField]
    private Text descriptionText;

    [SerializeField]
    private Text reward;

    [SerializeField]
    private Text difficulty;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Image sameDifLevel;

    [SerializeField]
    private Image harderDifLevel;


    public void SetLevels(LevelData same, LevelData harder) {

        sameDifficulty = same;
        harderDifficulty = harder;

        if (sameDifficulty == null && harderDifficulty == null) {

            Debug.LogWarning("No more levels left");
          
        
        }

        if (sameDifficulty == null) {
            
            
          sameDifLevel.transform.parent.gameObject.SetActive(false); }
        else sameDifLevel.sprite = sameDifficulty.Image;

        if (harderDifficulty == null) harderDifLevel.transform.parent.gameObject.SetActive(false);
        else harderDifLevel.sprite =  harderDifficulty.Image;



    }


    public void Activate() {

        description.SetActive(false);

    
    }


    public void ShowDescription(int index) {

        description.SetActive(true);
        LevelData levelToShow;

        if (index == 1) levelToShow = sameDifficulty;
        else levelToShow = harderDifficulty;

        levelName.text = levelToShow.LevelName;
        descriptionText.text = levelToShow.LevelDescription;

        image.sprite = levelToShow.Image;

        reward.text =   levelToShow.Reward.ToString();

        difficulty.text = "Difficulty: " + levelToShow.Difficulty.ToString();

                


    
    }


    public void StartLevel() {

        if (levelName.text != null) {

            PlayerPrefs.SetInt("Reward", int.Parse(reward.text));
            SceneManager.LoadScene(levelName.text, LoadSceneMode.Single);


        }
    
    
    }
    
    


}
