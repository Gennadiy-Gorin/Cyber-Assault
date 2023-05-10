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
    private GameObject skillTreePanel;


    private SkillTreeManager tree;

    void Start()
    {
        
        tree = GetComponent<SkillTreeManager>();
        if (PlayerPrefs.GetInt("loadFromComplition", 0) != 0)
        {
            ShowSkillTree();

        }
        else
        {
            CharacterChooser.SetActive(false);
            skillTreePanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            if (PlayerPrefs.GetInt("Save", 0) != 0) continueButton.SetActive(true);
            else continueButton.SetActive(false);
        }
        
    }

    public void Continue() {

        string sceaneName = PlayerPrefs.GetString("CurrentLevel", "none");

        if (sceaneName != "none") {

            if (PlayerPrefs.GetInt("IsComplete", 0) != 0) {

                ShowSkillTree();
                return;
            
            }

            SceneManager.LoadScene(sceaneName, LoadSceneMode.Single);


        }


    }

    private void ShowSkillTree()
    {
        skillTreePanel.SetActive(true);
        CharacterChooser.SetActive(false);
        mainMenuPanel.SetActive(false);
        skillTreePanel.GetComponent<SkillTreeMenu>().Activate(tree);
        PlayerPrefs.SetInt("loadFromComplition", 0);
    }

    public void NewGame() {
        mainMenuPanel.SetActive(false);
        skillTreePanel.SetActive(false);
        CharacterChooser.SetActive(true);
        
    }

    public void StartGame(CharacterData character) {

        Debug.Log("Game Starts with character " + character.CharacterName);
        PlayerPrefs.SetString("Character", character.CharacterName);
        tree.ResetTree();
        PlayerPrefs.SetInt("loadFromComplition", 0);
        PlayerPrefs.SetInt("IsComplete", 0);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void BackToMenu() {
        CharacterChooser.SetActive(false);
        skillTreePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        if (PlayerPrefs.GetInt("Save", 0) != 0) continueButton.SetActive(true);
        else continueButton.SetActive(false);


    }
}
