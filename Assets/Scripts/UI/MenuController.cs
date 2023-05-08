using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject CharacterChooser;

    [SerializeField]
    private GameObject continueButton;

    void Start()
    {
        CharacterChooser.SetActive(false);
        mainMenuPanel.SetActive(true);
        if (PlayerPrefs.GetInt("Save", 0) != 0) continueButton.SetActive(true);
        else continueButton.SetActive(false);
    }

    public void Continue() {

        string sceaneName = PlayerPrefs.GetString("CurrentLevel", "none");

        if (sceaneName != "none") {

            SceneManager.LoadScene(sceaneName, LoadSceneMode.Single);


        }


    }


    public void NewGame() {
        mainMenuPanel.SetActive(false);

        CharacterChooser.SetActive(true);
        
    }

    public void StartGame(CharacterData character) {

        Debug.Log("Game Starts with character " + character.CharacterName);
        PlayerPrefs.SetString("Character", character.CharacterName);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
