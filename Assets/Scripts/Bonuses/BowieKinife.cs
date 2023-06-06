using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowieKinife : Bonus,Buffable
{

    private float currentDamage;// Текущий урон, наносимый ножом

    private float basicDamage;// Базовый урон ножа (будет изменяться с помощью абилок и характеристик персонажа)

    private int amountOfKnifes;// Колличество выбрасываемых ножей за раз

    private float throwingRate;// Через какой промежуток времени ножи бросаються

    private float buff;

    public GameObject knifePrefab;// Префаб ножа

    private Transform playerTransform;//Ссылка на положение и ориентацию игрока

    private float timeSinceLastThrow=0f;// Таймер, отсчитывающий время с момента последнего броска

    public LayerMask enemyMask;// Маска для обнаружения коллайдеров в функции  ThrowKnife()

    public override void Activate()
    {
        bonusLevel = 1;
        basicDamage = 25f;
        maxlevel = 4;// Максимальный уровень бонуса
        currentDamage = basicDamage;
        amountOfKnifes = 1;
        throwingRate = 1f;
        playerTransform = gameObject.GetComponentInParent<Transform>();
        buff = 0;

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
            amountOfKnifes++;
            currentDamage = (basicDamage*(1f+buff)) * (1f+0.6f*bonusLevel);// Каждый уровень увеличивает урон на 60% от базового урона
        }
        if (bonusLevel == maxlevel) currentDamage = 120f;
    }

    

    // Update is called once per frame
    void Update()
    {
        timeSinceLastThrow += Time.deltaTime;
        if (timeSinceLastThrow > throwingRate) {// Если прошло больше времени чем throwingRate 
            ThrowKnife();// Функция броска ножа
            timeSinceLastThrow = 0;
        
        }
    }

    /*Данная функция обеспечивает создание префаба ножа и поворот его в сторону либо ближайшего врага, 
     * либо на случайный угол относительно персонажа игрока */
    private void ThrowKnife()
    {
        float radius = 3;// Радиус, в пределах которого, определяются коллайдеры

        Vector2 point = new Vector2(playerTransform.position.x, playerTransform.position.y);// Точка в 2д пространстве, определяющая центр окружности определения врагов

        Collider2D[] ObjectColliders; //Массив всех коллайдеров в пределах радиуса

        List<Collider2D> ememyColliders=new List<Collider2D>();// Список коллайдеров противников

        int closestIndex = 0;// Индекс ближайшего коллайдера врага в списке коллайдеров врагов

        float closestDistance = float.MaxValue;// Текущая ближайшая дистанция до коллайдера врага

        Quaternion rotationToTarget;// Угол, на который необходимо повернуть нож, при создании

        GameObject knife;// Содержит созданный нож

       // do
        //{
            ObjectColliders = Physics2D.OverlapCircleAll(point, radius, enemyMask);// Получаем все коллайдеры в пределе радиуса и находящиеся на слое enemyMask
          //  radius++;

        // } while (ObjectColliders.Length == 0||radius < 4);// если не нашли ни одного коллайдера, наращиваем радиус и проверяем еще раз
      
        // Для каждого найденного колайдера проверяем если он не является триггером, добавляем его в массив вражеских коллайдеров
        foreach (Collider2D collider in ObjectColliders) {

            if (!collider.isTrigger) ememyColliders.Add(collider);

        }

        Debug.LogWarning("coliders length: " + ememyColliders.Count);

        
        for (int i = 0; i < amountOfKnifes; i++)
        {
            int t = 0;// Переменная определяющая текущий индекс в списке коллайдеров

            closestDistance = float.MaxValue;// Сбрасываем ближайшую дистанцию каждый раз передопределением ближайшего коллайдера

            foreach (Collider2D enemyColider in ememyColliders)
            {
                //Проверяем не пустой ли элемент в списке
                if (enemyColider != null)
                {
                    float tempDistance = Vector3.Distance(playerTransform.position, enemyColider.transform.position);// определяем дистанцию от игрока до коллайдера

                    if (tempDistance < closestDistance)// если дистанция меньше чем текущая минимальная дистанция, устанавливаем ее текуще и указываем индекс колладера в списке
                    {
                        closestDistance = tempDistance;
                        closestIndex = t;
                    }
                }
                    t++;
                
            }
            Debug.LogWarning("Index: " + closestIndex);
            //Если список коллайдеров не был пуст или элемент списка коллайдеров с индексом не пуст
            if (ememyColliders.Count!=0 && ememyColliders[closestIndex] != null)
            {
                Transform enemy = ememyColliders[closestIndex].transform;// Получаем положение врага
                ememyColliders[closestIndex] = null;// Устанавливаем текущий элемент списка в null, чтобы все ножи не летели в одного ближайшего врага
                float angle = Vector3.SignedAngle(Vector3.up, (enemy.position - playerTransform.position).normalized, Vector3.forward);
                rotationToTarget = Quaternion.Euler(0, 0, angle);// Определяем угол поворота для ножа
            }
            else {
                // Если вокруг нет врагов, либо их меньше, чем количество врагов, то поворачиваем на рандомный угол.
                rotationToTarget = Quaternion.Euler(0, 0, UnityEngine.Random.Range(90,270));
               
            }

            knife = Instantiate(knifePrefab, playerTransform.position, rotationToTarget);// Создаем нож на месте игрока и поворачиваем его на нужный угол
            knife.GetComponent<Bullet>().SetDamage(currentDamage);//Устанавливаем урон и скорость ножа
            knife.GetComponent<Bullet>().Speed = 3f;


        }
    }

    public void Buff(float buffAmount)
    {
        buff = buffAmount;
        currentDamage = (basicDamage * (1f + buff)) * (1f + 0.05f * bonusLevel);
    }
}
