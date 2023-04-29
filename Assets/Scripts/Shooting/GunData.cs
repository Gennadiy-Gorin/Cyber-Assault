using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunData", menuName = "Gun Data", order = 51)]
public class GunData : ScriptableObject
{
    [SerializeField]
    private string gunName;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int cost;
    [SerializeField]
    private int level;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private int bulletCapasity;
    [SerializeField]
    private int bulletFire;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float reloadSpeed;
    [SerializeField]
    private FireTypes fireType;
    [SerializeField]
    private float fireRate;
   

    public string GunName { get => gunName; }
    public string Description { get => description; }
    public int Cost { get => cost;  }

    public int Level { get => level; }
    public Sprite Icon { get => icon; }
    public float AttackDamage { get => attackDamage;  }
    public GameObject Bullet { get => bullet;  }
    public int BulletCapasity { get => bulletCapasity; }

    public int BulletFire { get => bulletFire; }
    public float ReloadSpeed { get => reloadSpeed; }
    public FireTypes FireType { get => fireType; }
    public float FireRate { get => fireRate;  }
    public float BulletSpeed { get => bulletSpeed; }
}
