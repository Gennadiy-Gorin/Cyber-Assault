using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Filler : MonoBehaviour
{

    [SerializeField]
    private List<Image> weaponsToBuy;

    [SerializeField]
    private List<Image> bonusesToSell;

    [SerializeField]
    private List<Image> fillerBonuses;

    [SerializeField]
    private Image currentWeapon;

    [SerializeField]
    private Button buyWeapon;

    [SerializeField]
    private Button sellBonus;

    [SerializeField]
    private Button buyBonus;

    [SerializeField]
    private WeaponDescription weaponDescription;

    [SerializeField]
    private BonusDescription bonusDescription;

    [SerializeField]
    private GameObject descriptionFiller;

    private GunData descriptionGun;

    [SerializeField]
    private Shop shop;

    public  void FillButtons(List<Bonus> bonuses,List<Bonus> fillerBonuses) {
        ResetButtons(bonusesToSell);
        ResetButtons(this.fillerBonuses);
        descriptionFiller.SetActive(true);
        for (int i = 0; i < bonuses.Count; i++) {

            bonusesToSell[i].gameObject.transform.parent.gameObject.SetActive(true);
            bonusesToSell[i].sprite = bonuses[i].icon;


        }
        for (int i = 0; i < fillerBonuses.Count; i++)
        {

            this.fillerBonuses[i].transform.parent.gameObject.SetActive(true);
            this.fillerBonuses[i].sprite = fillerBonuses[i].icon;
        }
    }

    public void FillButtons(List<GunData> gunDatas)
    {
        ResetButtons(weaponsToBuy);
        descriptionFiller.SetActive(true);
        for (int i = 0; i <3; i++)
        {

            weaponsToBuy[i].transform.parent.gameObject.SetActive(true);
            weaponsToBuy[i].sprite = gunDatas[i].Icon;

        }

        currentWeapon.sprite = gunDatas[gunDatas.Count - 1].Icon;
        descriptionGun = null;

    }

    public  void FillDescription(Bonus bonus) {


        buyBonus.onClick.RemoveAllListeners();
        sellBonus.onClick.RemoveAllListeners();
        weaponDescription.gameObject.SetActive(false);
        descriptionFiller.SetActive(false);
        if (bonus.type == BonusType.Filler) bonusDescription.bonusCost.text = "Cost: " + bonus.cost;
       else bonusDescription.bonusCost.text="Cost: "+bonus.cost*bonus.BonusLevel;
        bonusDescription.bonusDescription.text=bonus.description;
        bonusDescription.bonusIcon.sprite=bonus.icon;

        bonusDescription.bonusLevel.text="Level: "+bonus.BonusLevel;
        bonusDescription.bonusName.text=bonus.bonusName;
        bonusDescription.bonusType.text="Type: "+ bonus.type.ToString();

        bonusDescription.gameObject.SetActive(true);
        if (bonus.type == BonusType.Filler)
        {
            buyBonus.onClick.AddListener(delegate { BonusBuy(bonus); });
            sellBonus.gameObject.SetActive(false);
            buyBonus.gameObject.SetActive(true);
        }
        else {
            sellBonus.onClick.AddListener(delegate { BonusSell(bonus); });
            sellBonus.gameObject.SetActive(true);
            buyBonus.gameObject.SetActive(false);

        }

    }


    public void BonusBuy(Bonus bonus) {
        shop.BuyConsumable(bonus);

    }

    public void BonusSell(Bonus bonus)
    {
        shop.SellBonus(bonus);

    }


    public  void FillDescription(GunData gunData)
    {
        bonusDescription.gameObject.SetActive(false);
        descriptionFiller.SetActive(false);

      //  weaponDescription.weaponCapasity.text="Ammo capasity: "+gunData.BulletCapasity;
        weaponDescription.weaponDamage.text="Damage: "+ gunData.AttackDamage;
        weaponDescription.weaponFireRate.text="Fire rate: "+ (1f/gunData.FireRate).ToString("0.00");
        weaponDescription.weaponIcon.sprite=gunData.Icon;
        weaponDescription.weaponLevel.text="Level: "+gunData.Level;
       // weaponDescription.weaponReload.text= "Reload speed: "+ gunData.ReloadSpeed;
        weaponDescription.weaponType.text="Fire: "+ gunData.FireType.ToString();
        weaponDescription.gunName.text= gunData.GunName;

        weaponDescription.gameObject.SetActive(true);
        descriptionGun = gunData;
       // if (gunData.Level >= 4|| gunData.Level==2 && gunData.Icon==currentWeapon.sprite&& gunData.Level == shop.GetCurrentGun().Level)     /////  Исправить 
        if(shop.isCurrentGun(gunData))
        {

            buyWeapon.interactable = false;
            buyWeapon.GetComponentInChildren<Text>().text = "Max Level";
        }
        else
        {
            buyWeapon.interactable = true;
            buyWeapon.GetComponentInChildren<Text>().text = gunData.Cost*(1f-shop.GetDiscount()) + " c.";

            buyWeapon.onClick.RemoveAllListeners();
            buyWeapon.onClick.AddListener(delegate { WeaponBuy(gunData); });
            
        }
    }

    public void WeaponBuy(GunData gun) {
        if (gun == null) Debug.LogError("Gun null again");
        shop.BuyWeapon(gun);
    
    }

    private void ResetButtons(List<Image> buttons) {
        foreach (Image button in buttons) {
            button.transform.parent.gameObject.SetActive(false);
           // button.gameObject.SetActive(false);
        
        }
    
    }

}
