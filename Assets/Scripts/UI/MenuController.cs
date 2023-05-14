using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject CharacterChooser;

    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private GameObject afterLevelPanel;


    private SkillTreeManager tree;

    void Start()
    {
        
        
        tree = GetComponent<SkillTreeManager>();
        if (PlayerPrefs.GetInt("loadFromComplition", 0) != 0)
        {
            GetComponent<LevelController>().SelectLevels();
            ShowSkillTree();

        }
        else
        {
            CharacterChooser.SetActive(false);
            afterLevelPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            if (PlayerPrefs.GetInt("Save", 0) != 0) continueButton.SetActive(true);
            else continueButton.SetActive(false);
        }
        
    }

    public void Continue() {

        string sceaneName = PlayerPrefs.GetString("CurrentLevel", "none");

        if (sceaneName != "none") {

            if (PlayerPrefs.GetInt("IsComplete", 0) != 0) {
                GetComponent<LevelController>().SelectLevels();
                ShowSkillTree();
                return;
            
            }

            SceneManager.LoadScene(sceaneName, LoadSceneMode.Single);


        }


    }

    private void ShowSkillTree()
    {
        afterLevelPanel.SetActive(true);
        CharacterChooser.SetActive(false);
        mainMenuPanel.SetActive(false);
        afterLevelPanel.GetComponent<AfterLevelMenu>().SetTree(tree);
        afterLevelPanel.GetComponent<AfterLevelMenu>().ShowSkillTree();
        PlayerPrefs.SetInt("loadFromComplition", 0);
    }

    public void NewGame() {
        mainMenuPanel.SetActive(false);
        afterLevelPanel.SetActive(false);
        CharacterChooser.SetActive(true);
        
    }

    public void StartGame(CharacterData character) {

        Debug.Log("Game Starts with character " + character.CharacterName);
        PlayerPrefs.SetString("Character", character.CharacterName);
        PlayerPrefs.SetInt("Points", 0);
        tree.ResetTree();
        GetComponent<LevelController>().ResetLevels();
        PlayerPrefs.SetInt("loadFromComplition", 0);
        PlayerPrefs.SetInt("IsComplete", 0);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void BackToMenu() {
        CharacterChooser.SetActive(false);
        afterLevelPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        if (PlayerPrefs.GetInt("Save", 0) != 0) continueButton.SetActive(true);
        else continueButton.SetActive(false);


    }
}
