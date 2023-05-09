using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPlane;

    [SerializeField]
    private GameObject weaponPage;

    [SerializeField]
    private GameObject bonusPage;

    [SerializeField]
    private GameObject weaponDescription;

    [SerializeField]
    private GameObject bonusDescription;
    [SerializeField]
    private GameObject descriptionFiller;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Text playerMoneyText;

    [SerializeField]
    private List<GunData> guns;

    [SerializeField]
    private List<GunData> weaponsToBuy;

    private GunData currentGun;

    [SerializeField]
    private List<Bonus> activeBonuses;

    [SerializeField]
    private List<Bonus> fillerBonuses;


    [SerializeField]
    private Bonus bb;

    [SerializeField]
    private Filler fillerClass;

    [SerializeField]
    private float playerMoney;


    private float discount=0f;


    public void Open() {


        shopPlane.SetActive(true);

        descriptionFiller.SetActive(true);

        startButton.Select();

        currentGun = GetComponent<GameManager>().PlayerInstance.GetComponentInChildren<ShootingController>().GunData;

        playerMoney = GetComponent<GameManager>().PlayerInstance.GetComponent<PlayerController>().GetMoney();

        playerMoneyText.text = playerMoney.ToString() + " crystals";

        if (ChooseWeapon())
        {
            weaponsToBuy.Add(currentGun);
            fillerClass.FillButtons(weaponsToBuy);

        }
        else Debug.LogError("Cannot add weapons to shop!");
        GetActiveBonuses();
        fillerClass.FillButtons(activeBonuses, fillerBonuses);

        ShowWeaponPage();

    }

    public void CloseShop() {

        shopPlane.SetActive(false);
        descriptionFiller.SetActive(true);
        activeBonuses.Clear();
        weaponsToBuy.Clear();
        GetComponent<GameManager>().NextWave();
       // GetComponent<GameManager>().StopTime(false);
    }

    public void ShowWeaponPage() {

        bonusDescription.SetActive(false);
        bonusPage.SetActive(false);  
        descriptionFiller.SetActive(true);
        weaponDescription.SetActive(true);
        weaponPage.SetActive(true);
            

    }

    public void changeDescrtiptionWeapon(int place) { ////place [0,3]

        if (weaponsToBuy.Count < 0 && place > weaponsToBuy.Count-1) return;

        if (place == weaponsToBuy.Count - 1) fillerClass.FillDescription(GetNextLevelGun());
        else fillerClass.FillDescription(weaponsToBuy[place]);
        descriptionFiller.SetActive(false);


    }

    public void changeDescrtiptionBonus(int place)
    {
        if (place < 0 || place > 8) return;

        if (place > 5)
        {
            place -= 5;
            fillerClass.FillDescription(fillerBonuses[place-1]);
        }else  fillerClass.FillDescription(activeBonuses[place]);

        descriptionFiller.SetActive(false);


    }


    public void ShowBonusPage()
    {

        bonusDescription.SetActive(true);
        bonusPage.SetActive(true);


       
        descriptionFiller.SetActive(true);
        weaponDescription.SetActive(false);
        weaponPage.SetActive(false);

    }

    private void GetActiveBonuses()
    {
        activeBonuses.Clear();
        BonusController bonusController = GetComponent<GameManager>().PlayerInstance.GetComponent<BonusController>();

        foreach (GameObject bonus in bonusController.activeBonuses)
        {

            activeBonuses.Add(bonus.GetComponent<Bonus>());

        }
    }

    private bool ChooseWeapon() {

        weaponsToBuy = new List<GunData>();

        for (int i = 0; i < 3; i++) {

            int place = Random.Range(0, guns.Count);
            if (guns[place].GunName != currentGun.GunName && !IsAlreadyInOption(guns[place]) && IsAllowedLevel(guns[place]))
            {

                weaponsToBuy.Add(guns[place]);
            }
            else i--;
        
        
        }
        if (weaponsToBuy.Count < 3) return false;

        return true;
    }

    private bool IsAlreadyInOption(GunData gunData) {

        foreach (GunData gun in weaponsToBuy) {

            if (gun.GunName == gunData.GunName ) return true;
        
        }

        return false;
    }

    private bool IsAllowedLevel(GunData gunData) {

        if (gunData.Level <= currentGun.Level + 1 && gunData.Level >= currentGun.Level - 1) return true;


        return false;
    }

    private GunData GetNextLevelGun() {

        if (currentGun.Level >= 4) return currentGun; ////// Исправить срочно

        foreach (GunData gun in guns) {

            if (string.Compare(gun.GunName, currentGun.GunName) == 0 && gun.Level == currentGun.Level + 1) return gun;
        
        }

        return null;

    }

    public void BuyWeapon(GunData gun) {

        GunData tempGun =gun;


        if (tempGun.Cost > playerMoney) return;

        int place = GetWeapon(gun); //[0,3]

        if (place < 0) {
            Debug.LogError("Chosen gun does not exist in list");
            return;
        }

       
        playerMoney -= (int)tempGun.Cost* (1f - discount);
        GetComponent<GameManager>().PlayerInstance.GetComponent<PlayerController>().ChangeMoney((int)(tempGun.Cost*(1f-discount)),true);

        playerMoneyText.text = playerMoney.ToString() + " crystals";

        if (place == 3)
        {
           // tempGun = GetNextLevelGun();
            weaponsToBuy.RemoveAt(place);
            weaponsToBuy.Add(tempGun);
            currentGun = tempGun;

        }
        else {
            //tempGun = weaponsToBuy[place];
            weaponsToBuy.RemoveAt(weaponsToBuy.Count - 1);
            weaponsToBuy[place] = currentGun;
            currentGun = tempGun;
            weaponsToBuy.Add(currentGun);
        }

        gameObject.GetComponent<GameManager>().PlayerInstance.GetComponentInChildren<ShootingController>().NewGun(currentGun);


        fillerClass.FillButtons(weaponsToBuy);
        ShowWeaponPage();
        //fillerClass.FillDescription(GetNextLevelGun());
        /*if (ChooseWeapon())
        {
            weaponsToBuy.Add(GetNextLevelGun());
            fillerClass.FillButtons(weaponsToBuy);

        }*/

    }

    private int GetWeapon(GunData gun)
    {
        for (int i=0;i<weaponsToBuy.Count;i++) { 
            if (gun.GunName == weaponsToBuy[i].GunName ) return i;

        }
        return -1;
    }

    public void SellBonus(Bonus bonus) {

        bb = bonus;
        playerMoney += bonus.cost*bonus.BonusLevel;
        GetComponent<GameManager>().PlayerInstance.GetComponent<PlayerController>().ChangeMoney(bonus.cost*bonus.BonusLevel, false);
        playerMoneyText.text = playerMoney.ToString() + " crystals";

        GetComponent<GameManager>().PlayerInstance.GetComponent<BonusController>().RemoveBonus(bonus);

        GetActiveBonuses();
        fillerClass.FillButtons(activeBonuses, fillerBonuses);
        ShowBonusPage();

    }

    public void BuyConsumable(Bonus bonus)
    {
        if (bonus.cost > playerMoney) return;

        playerMoney -= bonus.cost;
        GetComponent<GameManager>().PlayerInstance.GetComponent<PlayerController>().ChangeMoney(bonus.cost, true);
        playerMoneyText.text = playerMoney.ToString() + " crystals";

        GameObject newBonus = Instantiate(bonus.gameObject, GetComponent<GameManager>().PlayerInstance.transform);
        GetComponent<GameManager>().PlayerInstance.GetComponent<BonusController>().AddBonus(newBonus.GetComponent<Bonus>(),-1);

        ShowBonusPage();



    }

    public GunData GetCurrentGun() {

        return currentGun;
    }


    public bool isCurrentGun(GunData gun) {

        if (gun.GunName == currentGun.GunName && gun.Level == currentGun.Level) return true;
        return false;
    
    }


    public void Evaluate() {
        float dpsk;
        foreach (GunData gun in guns) {

             dpsk = (gun.AttackDamage * gun.BulletCapasity * gun.BulletFire) / (60f * gun.ReloadSpeed * gun.FireRate);
            Debug.Log("DPSK for " + gun.GunName + "( " + gun.Level + " )= " + dpsk);
        
        }
    
    }

    public float GetDiscount() {

        return discount;
    }

    public void SetDiscount(float amount)
    {

         discount=amount;
    }

}
