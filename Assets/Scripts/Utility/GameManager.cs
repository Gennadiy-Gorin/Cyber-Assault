using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    private GameObject playerInstance;

    [SerializeField]
    private CharacterData playerData;


    [SerializeField]
    private CharacterData[] characters;

    [SerializeField]
    private FloatingJoystick joystic;


    [SerializeField]
    private GameObject projectileHolder;
    // [SerializeField]
    //private int playerMaxLevel;

    //  private CameraMove camera;
    /*[SerializeField]
    private int currentPlayerLevel;
    [SerializeField]
    private float currentXP;*/

    private Spawner spawner;

    [SerializeField]
    private WaveSpawner waveSpawner;

    private int enemydeaths;

    public GameObject PlayerInstance { get => playerInstance; }

    public static event Action onWaveComplete;

    [SerializeField]
    private GameObject stageComplete;

    [SerializeField]
    private GameObject nextWave;

    [SerializeField]
    private  Text money;

    [SerializeField]
    private Text stageText;

    [SerializeField]
    private GameObject tapScreen;

    [SerializeField]
    private Text reward;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private GameObject looseScreen;

    [SerializeField]
    private GameObject pauseScreen;

    [SerializeField]
    private Text stageProgressionText;

    public static bool isComplete=false;

    /* [SerializeField]
private float requiredXP;*/
    private SkillTreeManager skillManager;

    void Start()
    {
        isComplete = false;
        //currentPlayerLevel = 1;
        //currentXP = 0;
        //requiredXP = SolveRequiredXP();
        skillManager = GetComponent<SkillTreeManager>();
        winScreen.SetActive(false);
        looseScreen.SetActive(false);
        pauseScreen.SetActive(false);
        StopTime(true);
        playerData = GetCharacter(PlayerPrefs.GetString("Character", null)); 
        if (playerPrefab != null && playerData != null) {

            playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);
            Camera.main.GetComponent<CameraMove>().SetTarget(playerInstance);
            playerInstance.GetComponent<PlayerController>().SetSkillTree(skillManager);
            playerInstance.GetComponent<PlayerController>().SetData(playerData);
            playerInstance.GetComponent<Move>().joystic = joystic;
            GetComponent<LevelSystem>().enabled = true;
            enemydeaths = 0;
            Enemy.onDeathEvent += onEnemyDeath;
            PlayerController.onMoneyChange += UpdateMoneyUI;
            PlayerController.onPlayerDeath += GameOver;
            money.text = "0";
            stageText.text = "Stage 1";
            stageProgressionText.text = "0/" + waveSpawner.EnemyCount;
            PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetInt("Save", 1);
            SetShopDiscount();
        }
        tapScreen.SetActive(true);
       
        LevelSystem.levelUp += NewLevel;
        
      //  Enemy.onDeathEvent += GainXp;
    }

    private void SetShopDiscount()
    {

        int level = skillManager.GetSkillLevel("Discount");
        GetComponent<Shop>().SetDiscount(0.1f * level);


    }

    public void NewLevel() {
        if (isComplete) return;
        StopTime(true);
        GetComponent<BonusChooser>().StartBonusChoosing();
    }
    /*  public void GainXp(float xp) {

          currentXP += xp * (1f + playerInstance.GetComponent<PlayerController>().XpBonus);

      }
      private void Update()
      {
          if (currentXP >= requiredXP) {
              LevelUp();
          }

      }

      private void LevelUp() {
          currentPlayerLevel++;
          currentXP = Mathf.RoundToInt(currentXP - requiredXP);
          requiredXP = SolveRequiredXP();
      }

      private int SolveRequiredXP() {

          int solveForRequiredXp = 0;
          for (int levelCycle = 1; levelCycle <= currentPlayerLevel; levelCycle++) {

              solveForRequiredXp += (int)Mathf.Floor(levelCycle + 300f * Mathf.Pow(2f, levelCycle / 7f));
          }

          return solveForRequiredXp/4;
      }
    */

    public void StartGame() {
        tapScreen.SetActive(false);
        StopTime(false);

    }

    public void NextWave() {
        //corutine next stage
        nextWave.GetComponent<Text>().text = "Wave " + (waveSpawner.CurrentWave+2);
        nextWave.SetActive(true);
        StartCoroutine(NextWaveText());
        StopTime(false);
       

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) StageComplete();
        if (Input.GetKeyDown(KeyCode.E)) GetComponent<Shop>().Evaluate();
        if (Input.GetKeyDown(KeyCode.Escape)) Pause(!pauseScreen.activeSelf);
    }
    public void onEnemyDeath() {

        //Debug.Log("enemy died");
        if (!isComplete)
        {
            enemydeaths++;
            if (stageProgressionText == null)
            {

                Debug.LogWarning("Text missing");
                return;
            }
            stageProgressionText.text = enemydeaths + "/" + waveSpawner.EnemyCount;
        }
        if (waveSpawner!=null&&enemydeaths >= waveSpawner.EnemyCount) {
            //StopTime(true);
            StageComplete();
        }
    
    }

    private void StageComplete()
    {
        StopTime(true);
        playerInstance.GetComponent<Health>().enabled = false;
        enemydeaths = 0;
        waveSpawner.IsWaveComplete = true;
        if (waveSpawner.IsLastWave()) {
            LevelComplete();
            return;
        }
        stageComplete.SetActive(true);
        StartCoroutine(StageComplition());
        //Courutine stage complete
       
        if (onWaveComplete != null)
        {
            onWaveComplete();
        }
        foreach (Transform child in projectileHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
       
    }

    private void LevelComplete()
    {
        isComplete = true;
        winScreen.SetActive(true);
        reward.text = PlayerPrefs.GetInt("Reward", 1).ToString();
        StopTime(true);
        //реализовать вылезание панельки
        
        PlayerPrefs.SetInt("IsComplete", 1);
        
    }

    public void ToMenu(bool isLevelComplete) {
        isComplete = true;
        if (isLevelComplete)
        {
            int points = PlayerPrefs.GetInt("Points", 0);
            PlayerPrefs.SetInt("Points", points+ PlayerPrefs.GetInt("Reward", 1));
            PlayerPrefs.SetInt("loadFromComplition", 1);
           
        }
        else {
            PlayerPrefs.SetInt("loadFromComplition", 0);
            PlayerPrefs.SetInt("IsComplete", 0);

        }
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    IEnumerator StageComplition() {

        yield return new WaitForSecondsRealtime(3f);//new WaitForSeconds(3f);
        stageComplete.SetActive(false);
        GetComponent<Shop>().Open();
        stageText.text = "Stage "+(waveSpawner.CurrentWave+2);

    }

    IEnumerator NextWaveText()
    {

        yield return new WaitForSecondsRealtime(3f);//new WaitForSeconds(3f);
        nextWave.SetActive(false);
        waveSpawner.NextWave();
        stageProgressionText.text = "0/" + waveSpawner.EnemyCount;
    }


    public void StopTime(bool isStopping) {
        if (isStopping) Time.timeScale = 0;
        else Time.timeScale = 1;



    }

    public void UpdateMoneyUI() {
        if (money == null) return;
        float amount = playerInstance.GetComponent<PlayerController>().GetMoney();
        money.text =((int)(amount+0.5f)).ToString() ;

    }

    public void GameOver() {
        isComplete = true;
        looseScreen.SetActive(true);
        StopTime(true);
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.SetInt("Save",0);
        

    }


    private CharacterData GetCharacter(string name) {
        foreach (CharacterData character in characters) {

            if (string.Compare(character.CharacterName, name) == 0) return character;
        
        }

        return null;
    }

    private void OnDestroy()
    {
        LevelSystem.levelUp -= NewLevel;
        Enemy.onDeathEvent -= onEnemyDeath;
        PlayerController.onMoneyChange -= UpdateMoneyUI;
        PlayerController.onPlayerDeath -= GameOver;
    }

    public void Pause(bool paused)
    {
        if (paused == true && Time.timeScale == 0f) return;
        pauseScreen.SetActive(paused);
        StopTime(paused);
    }

    private void OnApplicationFocus(bool pause)
    {
        if(pause==false) Pause(!pause);
    }

    /*public void Unpause()
    {
        pauseScreen.SetActive(false);
        StopTime(false);
    }*/

}
