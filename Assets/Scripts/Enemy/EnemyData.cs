using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data", order = 52)]
public class EnemyData: ScriptableObject
{
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private int enemyLevel;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private Sprite enemySprite;
    [SerializeField]
    private float enemyDamage;
    [SerializeField]
    private int enemyHealth;
    [SerializeField]
    private int xpCost;

    public string EnemyName { get => enemyName; }
    public int EnemyLevel { get => enemyLevel; }
    public float EnemySpeed { get => enemySpeed;  }
    public Sprite EnemySprite { get => enemySprite;  }
    public float EnemyDamage { get => enemyDamage;}
    public int XpCost { get => xpCost; }
    public int EnemyHealth { get => enemyHealth; }
}
