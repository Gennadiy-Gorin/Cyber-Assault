using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusChooser : MonoBehaviour
{

    private BonusController bonusController;

    //список ативных в данный момент бонусов
    private List<GameObject> activeBonuses;

    //всевозможные бонусы на уровне 
    [SerializeField]
    private List<GameObject> allBonuses;

    //текущий пул бонусов
    [SerializeField]
    private List<GameObject> bonusPool;

    //кнопки для заполнения
    [SerializeField]
    private GameObject[] buttons;

    //спсок возможных бонусов-филлеров
    [SerializeField]
    private List<GameObject> fillerBonuses;

    //экран с бонусами
    [SerializeField]
    private GameObject plane;

    //всего на выбор даеться 3 бонуса
    private int[] bonusIndexes=new int[3];

    //непосредственно отображаемые бонусы
    private List<Bonus> bonusOptions=new List<Bonus>();

    //количество активных бонусов
    private int bonusCount;

    private int bonusLevel;
    //максимально доступное количество ативных бонусов
    private int maxBonusCount;

    private int fillersInOptions;

  

    private void Start()
    {
        bonusPool = new List<GameObject>(allBonuses);

    }

    public void StartBonusChoosing() {
               
        bonusOptions.Clear();

        bonusController = GetComponent<GameManager>().PlayerInstance.GetComponent<BonusController>();

        fillersInOptions = 0;

        if (bonusController == null) return;

        activeBonuses = bonusController.activeBonuses;

        bonusCount = activeBonuses.Count;

        maxBonusCount = bonusController.MaxBonusNumber;

        if (activeBonuses.Count >= maxBonusCount)
        {
            for (int i = 0; i < 3; i++)
            {
                if (CanAddFiller()) bonusOptions.Add(ChooseFillerBonus());

                else bonusOptions.Add(GetOptionsFromActive());

            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                bonusOptions.Add(ChooseBonus());

            }

            //  ChooseBonus();
           
        }
        FillButtons();
        plane.SetActive(true);
    
    }

    private Bonus ChooseFillerBonus()
    {
        fillersInOptions++;
        Debug.Log("Fillers amount = " + fillerBonuses.Count);

        int index = Random.Range(0, fillerBonuses.Count);

        return fillerBonuses[index].GetComponent<Bonus>();
    }

    private bool CanAddFiller()
    {
        int availibleBonusesCount=maxBonusCount;
        foreach (GameObject bonus in activeBonuses) {

            if (bonus.GetComponent<Bonus>().BonusLevel >= bonus.GetComponent<Bonus>().Maxlevel) availibleBonusesCount--;
        
        }
        if (availibleBonusesCount < 3 - fillersInOptions) return true;
        return false;
    }

    private Bonus ChooseBonus() {
        Bonus tempBonus=null;
        Bonus sameActiveBonus=null;
        do
        {
            do
            {
                tempBonus = GetRandomBonus(bonusPool);
            } while (isAlreadyInOptions(tempBonus));

            sameActiveBonus = isAlreadyActive(tempBonus);

            if (sameActiveBonus == null) break;

            Debug.Log("Same bonus alert");
        } while (isMaxLevel(sameActiveBonus));

        if (sameActiveBonus != null) Debug.Log("Bonus level=" + sameActiveBonus.BonusLevel);
        if (sameActiveBonus != null) return sameActiveBonus;
        return tempBonus;
        
    }

    private Bonus GetOptionsFromActive() {
        Bonus tempBonus = null;

        do
        {
            do
            {
                tempBonus = GetRandomBonus(activeBonuses);
            } while (isAlreadyInOptions(tempBonus));

        } while (isMaxLevel(tempBonus));
        return tempBonus;
      

    }

    private void FillButtons() {

        for (int i=0;i<3;i++) {

            buttons[i].GetComponent<BonusButtonFill>().FillButton(bonusOptions[i]);
        
        }

    }

    public void SelectBonus(int option)
    {
        plane.SetActive(false);
        Bonus sameBonus = isAlreadyActive(bonusOptions[option]);
        if (sameBonus != null) {

            sameBonus.Upgrade();
            GetComponent<GameManager>().StopTime(false);
            return;
        }
        

        GameObject newBonus=null;

        foreach (GameObject bonus in bonusPool) {
            if (bonus.GetComponent<Bonus>().bonusName == bonusOptions[option].bonusName) {
               newBonus=Instantiate(bonus, bonusController.transform);
                break;
            }
        
        }

        if (newBonus == null) {
           
            foreach (GameObject bonus in fillerBonuses)
            {
                if (bonus.GetComponent<Bonus>().bonusName == bonusOptions[option].bonusName)
                {
                    newBonus = Instantiate(bonus, bonusController.transform);
                    break;
                }

            }
            if (newBonus == null)
            {
                //  Debug.LogError("No such bonus exists");
                return;
            }
        }

          bonusController.AddBonus(newBonus.GetComponent<Bonus>(),-1);
        GetComponent<GameManager>().StopTime(false);



    }

    //выбирает случайный бонус из пула
    private Bonus GetRandomBonus(List<GameObject> pool) {
        
        int index = Random.Range(0, pool.Count);
        
        return pool[index].GetComponent<Bonus>();
    
    }

    private Bonus isAlreadyActive(Bonus bonus) {

        foreach (GameObject activeBonus in activeBonuses) {
            if (activeBonus.GetComponent<Bonus>().bonusName == bonus.bonusName) return activeBonus.GetComponent<Bonus>();
        
        }
        return null;
    }

    private bool isMaxLevel(Bonus bonus) {
        if (bonus.BonusLevel >= bonus.Maxlevel)
        {
            RemoveFromPool(bonus);
            return true;
        }
        return false;
    }

    private bool isAlreadyInOptions(Bonus bonus) {
        if (bonus == null) return false;

        foreach (Bonus option in bonusOptions) {
            if (option.bonusName == bonus.bonusName) return true;
               
        }
        return false;
    }

    private void RemoveFromPool(Bonus bonus) {
        //   bonusPool.Remove(bonus.gameObject);
        for (int i = 0; i < bonusPool.Count; i++) {
            if (bonusPool[i].GetComponent<Bonus>().bonusName == bonus.bonusName) {
                bonusPool.RemoveAt(i);
                break;
            }
        
        }
    }
}
