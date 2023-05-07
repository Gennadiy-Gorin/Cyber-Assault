using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    private GameObject playerInstance;

    [SerializeField]
    private CharacterData playerData;

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
    private Text stageProgressionText;

    /* [SerializeField]
private float requiredXP;*/

    void Start()
    {
       
        //currentPlayerLevel = 1;
        //currentXP = 0;
        //requiredXP = SolveRequiredXP();
        if (playerPrefab != null && playerData != null) {

            playerInstance = Instantiate(playerPrefab, transform.position, transform.rotation);
            Camera.main.GetComponent<CameraMove>().SetTarget(playerInstance);
            playerInstance.GetComponent<PlayerController>().SetCharacteristics();
            playerInstance.GetComponent<Move>().joystic = joystic;
            GetComponent<LevelSystem>().enabled = true;
            enemydeaths = 0;
            Enemy.onDeathEvent += onEnemyDeath;
            PlayerController.onMoneyChange += UpdateMoneyUI;
            money.text = "0";
            stageText.text = "Stage 1";
            stageProgressionText.text = "0/" + waveSpawner.EnemyCount;
        }
        LevelSystem.levelUp += NewLevel;
        
      //  Enemy.onDeathEvent += GainXp;
    }

    public void NewLevel() {
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
    }
    public void onEnemyDeath() {

        //Debug.Log("enemy died");
        enemydeaths++;
        stageProgressionText.text = enemydeaths+"/" + waveSpawner.EnemyCount;
        if (waveSpawner!=null&&enemydeaths >= waveSpawner.EnemyCount) {
            //StopTime(true);
            StageComplete();
        }
    
    }

    private void StageComplete()
    {
        StopTime(true);
        stageComplete.SetActive(true);
        playerInstance.GetComponent<Health>().enabled = false;
        enemydeaths = 0;
        waveSpawner.IsWaveComplete = true;
        if (waveSpawner.IsLastWave()) {
            //LevelComplete();
            return;
        }
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

    IEnumerator StageComplition() {

        yield return new WaitForSecondsRealtime(3f);//new WaitForSeconds(3f);
        stageComplete.SetActive(false);
        GetComponent<Shop>().Open();
        stageText.text = "Stage "+waveSpawner.CurrentWave+2;

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

}
