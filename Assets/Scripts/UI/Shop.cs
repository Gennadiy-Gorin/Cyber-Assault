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
    private List<GunData> guns;

    [SerializeField]
    private List<GunData> weaponsToBuy;

    private GunData currentGun;

    [SerializeField]
    private List<Bonus> activeBonuses;

    [SerializeField]
    private Button startButton;


    [SerializeField]
    private GameObject weaponDescription;

    [SerializeField]
    private GameObject bonusDescription;
    [SerializeField]
    private GameObject descriptionFiller;


    public void Open() {



        shopPlane.SetActive(true);
        descriptionFiller.SetActive(true);
        startButton.Select();


    }

    private void CloseShop() {

        shopPlane.SetActive(false);
        GetComponent<GameManager>().StopTime(false);
    }

    public void ShowWeaponPage() {

        bonusDescription.SetActive(false);
        bonusPage.SetActive(false);

        descriptionFiller.SetActive(true);
        weaponDescription.SetActive(true);
        weaponPage.SetActive(true);




    }

}
