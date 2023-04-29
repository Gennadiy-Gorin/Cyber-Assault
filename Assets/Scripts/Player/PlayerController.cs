using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterData playerData;
    [SerializeField]
    private GunData currentGun;
    private float maxHp;
    private float speed;
    private float damageResistance;
    private float gunDamageBonus;
    private float moneyBonus;
    [SerializeField]
    private int money;
    private float xpBonus;

    public float GunDamageBonus { get => gunDamageBonus; set => gunDamageBonus = value; }
    public float MoneyBonus { get => moneyBonus; set => moneyBonus = value; }
    public float XpBonus { get => xpBonus; set => xpBonus = value; }

    public float GetDamageResistance() {
        return damageResistance;
    } 

    void Start()
    {
        SetCharacteristics();
        money = 0;
        xpBonus = 0;
    }

    

    public void SetCharacteristics() {

        if (playerData == null) return;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = playerData.AnimationController;
        speed = playerData.Speed;
        GetComponent<Move>().Speed = speed;
        maxHp = playerData.MaxHealth;
        GetComponent<Health>().maxHealth = maxHp;
        currentGun = playerData.DefaultGun;
        GetComponentInChildren<ShootingController>().NewGun(currentGun);
        damageResistance = playerData.DamageResist;
        gunDamageBonus = playerData.GunDamageBonus;
        GetComponentInChildren<ShootingController>().SetDamageBuff(gunDamageBonus);
        xpBonus = playerData.XpBonus;
    }

    public int GetMoney() {
        return money;
    }

    public void ChangeMoney(int amount, bool isSpend) {
        if (isSpend) money -= amount;
        else money += (int)(amount * (1 + moneyBonus)+0.5f);

    }


}
