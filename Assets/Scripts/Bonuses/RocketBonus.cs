using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBonus : Bonus,Buffable
{
    private float currentDamage;// Текущий урон,

    private float basicDamage;// Базовый урон  (будет изменяться с помощью абилок и характеристик персонажа)

    private int amountOfRockets;// Колличество ракет

    private float fireRate;// Через какой промежуток времени ракеты вылетают

    private float buff;

    private float blastRadius;

    public GameObject rocketPrefab;// Префаб ракеты

    private Transform playerTransform;//Ссылка на положение и ориентацию игрока

    private float timeSinceLastThrow = 0f;// Таймер, отсчитывающий время с момента последнего выстрела




    public override void Activate()
    {
        bonusLevel = 1;
        basicDamage = 20f;
        maxlevel = 4;// Максимальный уровень бонуса
        currentDamage = basicDamage;
        amountOfRockets = 1;
        fireRate = 4f;
        playerTransform = gameObject.GetComponentInParent<Transform>();
        buff = 0;
        blastRadius = 1.5f;
    }

    public void Buff(float buffAmount)
    {
        buff = buffAmount;
        currentDamage = (basicDamage * (1f + buff)) * (1f + 0.05f * bonusLevel); 
    }

    public override void Deactivate()
    {
        Destroy(gameObject);
    }

    public override void Upgrade()
    {
        if (bonusLevel <= maxlevel)
        {
            bonusLevel++;
            switch (bonusLevel) {

                case 2:
                    fireRate = 3f;
                    blastRadius = 2f;
                    break;

                case 3:
                    currentDamage = (basicDamage * (1f + buff)) * 2f;
                    break;
                case 4:
                    amountOfRockets = 2;
                    currentDamage = (basicDamage * (1f + buff)) * 3f;
                    blastRadius = 2.5f;
                    fireRate = 2.5f;
                    break;
                default: Debug.Log("UNknown condition");
                    break;
            
            }
           
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        timeSinceLastThrow += Time.deltaTime;
        if (timeSinceLastThrow > fireRate)
        {// Если прошло больше времени чем throwingRate 
            LaunchRocket();// Функция броска ножа
            timeSinceLastThrow = 0;

        }
    }

    private void LaunchRocket()
    {
        Quaternion rotationToTarget;
        GameObject rocket;
        for (int i = 0; i < amountOfRockets; i++)
        {
            rotationToTarget = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
            rocket = Instantiate(rocketPrefab, playerTransform.position, rotationToTarget);// Создаем нож на месте игрока и поворачиваем его на нужный угол
            rocket.GetComponent<Rocket>().SetParams(currentDamage,blastRadius);//Устанавливаем урон и скорость ножа
        }
    }
}
