using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;


    [SerializeField]
    private Transform level;
    

    private int currentWave;
    private float timeSinceLastSpawn;
    private int enemiesSpawned;
    private bool isWaveComplete;
    private int currentSpawnAmount;
    private float currentSpawnInterval;
    [SerializeField]
    private float cooldownBeforeWave;

    public int EnemiesSpawned { get => enemiesSpawned; }

   

    [System.Serializable]
    private class Wave
    {
        [SerializeField] public int enemyCount;// количество врагов на волне необходимых для прохождения
        [SerializeField] public float maxSpawnInterval;
        [SerializeField] public int minSpawnAmount;// количество врагов при спавне
        [SerializeField] public int increaseSpawnSpeed;
        [SerializeField] public EnemyType[] enemyTypes;
    }
    [System.Serializable]
    private class EnemyType {
        [SerializeField] public EnemyController enemyClass;
        [SerializeField] public int enemyLevel;
        [SerializeField] public int chanceToSpawn;
    }
    public int EnemyCount { get => waves[currentWave].enemyCount; }
    public bool IsWaveComplete { get => isWaveComplete; set => isWaveComplete = value; }

    private void Start()
    {
        enemiesSpawned = 0;
        currentWave = 0;
        timeSinceLastSpawn = cooldownBeforeWave;
        isWaveComplete = false;
        currentSpawnAmount = waves[currentWave].minSpawnAmount;
        currentSpawnInterval= waves[currentWave].maxSpawnInterval;
    }

    private void Update()
    {
        if (!isWaveComplete)//waves[currentWave].enemyCount > enemiesSpawned)
        {
            //Debug.Log("Passed firts if");
            if (Time.timeSinceLevelLoad > timeSinceLastSpawn + currentSpawnInterval)
            {
                // Determine spawn location

              //  Debug.Log("Spawning " + waves[currentWave].spawnAmount + " enemies. After " + timeSinceLastSpawn + " seconds");

                Spawn(currentSpawnAmount);


            }
        }
    }

    public void NextWave() {
        enemiesSpawned = 0;
        currentWave++;
        timeSinceLastSpawn += cooldownBeforeWave;
        isWaveComplete = false;
        currentSpawnAmount = waves[currentWave].minSpawnAmount;
        currentSpawnInterval = waves[currentWave].maxSpawnInterval;
    }

    private void Spawn(int spawnCount)
    {

        for (int i=0; i<spawnCount;i++) {
            Vector3 position = GetSpawnPosition();
           EnemyType enemyObject = GetEnemy();
            if (!SpawnEnemy(position, enemyObject.enemyClass.gameObject, enemyObject.enemyLevel)) {
                Debug.LogWarning("Failed to spawn enemy!!!!");
                return;
            }
           

        }

      timeSinceLastSpawn = Time.timeSinceLevelLoad;
      enemiesSpawned += spawnCount;
        IncreaseDifficulty();

    }

    private void IncreaseDifficulty() {
        if (enemiesSpawned < waves[currentWave].increaseSpawnSpeed)
            return;

        enemiesSpawned = 0;
        currentSpawnInterval -= 1f;
        if (currentSpawnInterval < 1f) {

            currentSpawnInterval = waves[currentWave].maxSpawnInterval;
            currentSpawnAmount++;
        }

    }

    private bool SpawnEnemy(Vector3 position, GameObject enemyObject,int enemyLevel)
    {
        GameObject enemyGameObject = Instantiate(enemyObject, position, enemyObject.transform.rotation, null);
        if (enemyGameObject == null) return false;

        enemyGameObject.GetComponent<EnemyController>().SetData(enemyGameObject);
        enemyGameObject.GetComponent<EnemyController>().Activate();
       if(! enemyGameObject.GetComponent<EnemyController>().ChangeEnemyLevel(enemyLevel)) return false;
        return true;
    }

    private EnemyType GetEnemy()
    {
        int number = UnityEngine.Random.Range(1, 100);
        int tempChance=0;
        for (int i = 0; i < waves[currentWave].enemyTypes.Length; i++) {
            tempChance += waves[currentWave].enemyTypes[i].chanceToSpawn;
            if (number < tempChance) return waves[currentWave].enemyTypes[i];
        }
        return waves[currentWave].enemyTypes[0];
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPoint=new Vector3(0,0,-1);

        float rangeX = level.localScale.x/2f;
        float rangeY = level.localScale.y/2f;

        Vector3 currentPosition = transform.position;
        //while(spawnPoint==null)
        // Debug.Log("Scale= "+ rangeX);

        while (spawnPoint.z < 0) {
            float spawnPositionX = currentPosition.x + UnityEngine.Random.Range( 5f, 10f)*(UnityEngine.Random.Range(0, 2) * 2 - 1);
            float spawnPositionY = currentPosition.y + UnityEngine.Random.Range( 10f, 15f)*(UnityEngine.Random.Range(0, 2) * 2 - 1);
            if (Mathf.Abs(spawnPositionX) < rangeX && Mathf.Abs(spawnPositionY) < rangeY)
                spawnPoint = new Vector3(spawnPositionX, spawnPositionY);
       // Debug.Log("spawn at position: [" + spawnPositionX + "; " + spawnPositionY + "]");
       }
        return spawnPoint;
    }
}
