using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] enemyTypes;

    public EnemyController enemyController;

    public float spawnRangeX = 6.0f;
    public float spawnRangeY = 10.0f;
    private float radiusSpawn = 5f;
    public float spawnRate;
    public int spawnCount;
    int enemyLvl;


    private float lastSpawnTime = Mathf.NegativeInfinity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnTimer();
    }

    private void CheckSpawnTimer()
    {
        // If it is time for an enemy to be spawned
        if (Time.timeSinceLevelLoad > lastSpawnTime + spawnRate)
        {
            // Determine spawn location

            Spawn(spawnCount);
           
        }
    }


    private void Spawn(int Count) {

        for (int i = 1; i <= Count; i++) {
            Vector3 postion = GetSpawnLocation();
            GameObject enemyPrefab = GetEnemyType();
            SpawnEnemy(postion, enemyPrefab);


        }

    
    }


    private Vector3 GetSpawnLocation() {
        float x = Random.Range(0 - spawnRangeX, spawnRangeX);
        if (x < 0) x -= radiusSpawn;
        else x += radiusSpawn;
        float y = Random.Range(0 - spawnRangeY, spawnRangeY);
        if (y < 0) y -= radiusSpawn;
        else y+= radiusSpawn;
        // Return the coordinates as a vector
        return new Vector3(transform.position.x + x, transform.position.y + y, 0);
        

    }


    private void SpawnEnemy(Vector3 spawnLocation, GameObject enemy) {


        GameObject enemyGameObject = Instantiate(enemy, spawnLocation, enemy.transform.rotation, null);
        
        enemyGameObject.GetComponent<EnemyController>().SetData(enemyGameObject);
        enemyGameObject.GetComponent<EnemyController>().Activate();

        bool answer=enemyGameObject.GetComponent<EnemyController>().ChangeEnemyLevel(Random.Range(1, 4));

       
        // Incremment the spawn count
        lastSpawnTime = Time.timeSinceLevelLoad;

    }

    private GameObject GetEnemyType() {

        int index = Random.Range(0, enemyTypes.Length);

        return enemyTypes[index];


    }

}
