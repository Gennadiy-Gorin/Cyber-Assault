using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBonus : Bonus,Buffable
{
    private float currentDamage;// ������� ����,

    private float basicDamage;// ������� ����  (����� ���������� � ������� ������ � ������������� ���������)

    private int amountOfRockets;// ����������� �����

    private float fireRate;// ����� ����� ���������� ������� ������ ��������

    private float buff;

    private float blastRadius;

    public GameObject rocketPrefab;// ������ ������

    private Transform playerTransform;//������ �� ��������� � ���������� ������

    private float timeSinceLastThrow = 0f;// ������, ������������� ����� � ������� ���������� ��������




    public override void Activate()
    {
        bonusLevel = 1;
        basicDamage = 20f;
        maxlevel = 4;// ������������ ������� ������
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
        {// ���� ������ ������ ������� ��� throwingRate 
            LaunchRocket();// ������� ������ ����
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
            rocket = Instantiate(rocketPrefab, playerTransform.position, rotationToTarget);// ������� ��� �� ����� ������ � ������������ ��� �� ������ ����
            rocket.GetComponent<Rocket>().SetParams(currentDamage,blastRadius);//������������� ���� � �������� ����
        }
    }
}
