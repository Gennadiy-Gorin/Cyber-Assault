using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData[] enemyLevels;
    private Enemy currentEnemy;
    [SerializeField]
    private GameObject crystalDrop;

    private void Start()
    {
        currentEnemy = gameObject.GetComponent<Enemy>();
        //ChangeEnemyLevel(1);
    }


    public bool ChangeEnemyLevel(int level) {
        if (currentEnemy == null|| level<=0)
        {//|| level > enemyLevels.Length) {
            return false;
        }
        else if (level > enemyLevels.Length) {
            Debug.LogWarning("Current level is to big for this entity. Setting max level instead");
            currentEnemy.ChangeData(enemyLevels[enemyLevels.Length-1]);
            return true;
        }
        currentEnemy.ChangeData(enemyLevels[level - 1]);
        return true;
      
    }

    public void SetData(GameObject enemy) {

        if(enemy.GetComponent<Enemy>()==null)Debug.LogWarning("Enemy component is null");
        currentEnemy=enemy.GetComponent<Enemy>();
        currentEnemy.CrystalDrop = crystalDrop;
    }

    public void Activate() {
        if (currentEnemy != null) currentEnemy.Activate();
    }
  
}
