using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterData playerData;
    [SerializeField]
    private GunData currentGun;
    private float maxHp;
    private float speed;
    private float damageResistance;
    private float gunDamageBonus;
    private float moneyBonus;
    [SerializeField]
    private float money;
    private float xpBonus;

    public static event Action onMoneyChange;
    public static event Action onPlayerDeath;

    private SkillTreeManager tree;


    public float GunDamageBonus { get => gunDamageBonus; set => gunDamageBonus = value; }
    public float MoneyBonus { get => moneyBonus; set => moneyBonus = value; }
    public float XpBonus { get => xpBonus; set => xpBonus = value; }

    public float GetDamageResistance() {
        return damageResistance;
    } 

   /* void Start()
    {
        SetCharacteristics();
        money = 0;
        xpBonus = 0;
    }*/

    public void SetData(CharacterData data) {
        playerData = data;
        SetCharacteristics();
        money = 0;
       // xpBonus = 0;

    }

    public void SetCharacteristics() {

        if (playerData == null) return;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = playerData.AnimationController;
        speed = playerData.Speed;
        GetComponent<Move>().Speed = speed;
        maxHp = GetMaxHp();
        GetComponent<Health>().maxHealth = maxHp;
        currentGun = playerData.DefaultGun;
        GetComponentInChildren<ShootingController>().NewGun(currentGun);
        GetComponentInChildren<ShootingController>().SetReloadBonus(0.1f * tree.GetSkillLevel("Twin mag"));
        damageResistance = SetDamageResistance();
        gunDamageBonus = SetGunDamageBonus();
        GetComponentInChildren<ShootingController>().SetDamageBuff(gunDamageBonus);
        xpBonus= GetXpBonus() ;
        SetBonusParams();
    }

    private float SetGunDamageBonus()
    {
        return playerData.GunDamageBonus + 0.1f * tree.GetSkillLevel("Armor-piercing bullets");
    }

    private float SetDamageResistance()
    {
        return playerData.DamageResist + 0.1f*tree.GetSkillLevel("Armor");
    }

    public float GetMoney() {
        return money;
    }

    public void ChangeMoney(int amount, bool isSpend) {
        if (isSpend) money -= amount;
        else money += (int)(amount * (1 + moneyBonus)+0.5f);
        if (onMoneyChange != null) onMoneyChange();

    }

    private float GetMaxHp() {

        return playerData.MaxHealth * (1f + 0.2f*tree.GetSkillLevel("Improved medkit"));

    }

    private float GetXpBonus() {

        return playerData.XpBonus + (0.1f*tree.GetSkillLevel("Database"));
    }
    /*private void OnDestroy()
    {
        if (onPlayerDeath != null) {

            onPlayerDeath();
        }
    }*/

    private void SetBonusParams() {

        GetComponent<BonusController>().MaxBonusNumber = 2 + tree.GetSkillLevel("More pockets");
        GetComponent<BonusController>().BuffDamage(playerData.AbilityDamageBonus+0.1f * tree.GetSkillLevel("Science degree"));

    }

    public void Death() {
        if (onPlayerDeath != null)
        {

            onPlayerDeath();
        }
    }

    public void SetSkillTree(SkillTreeManager skillTree) {

        tree = skillTree;
    }

}
