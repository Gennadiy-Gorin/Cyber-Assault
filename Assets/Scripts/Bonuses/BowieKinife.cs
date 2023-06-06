using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowieKinife : Bonus,Buffable
{

    private float currentDamage;// ������� ����, ��������� �����

    private float basicDamage;// ������� ���� ���� (����� ���������� � ������� ������ � ������������� ���������)

    private int amountOfKnifes;// ����������� ������������� ����� �� ���

    private float throwingRate;// ����� ����� ���������� ������� ���� ����������

    private float buff;

    public GameObject knifePrefab;// ������ ����

    private Transform playerTransform;//������ �� ��������� � ���������� ������

    private float timeSinceLastThrow=0f;// ������, ������������� ����� � ������� ���������� ������

    public LayerMask enemyMask;// ����� ��� ����������� ����������� � �������  ThrowKnife()

    public override void Activate()
    {
        bonusLevel = 1;
        basicDamage = 25f;
        maxlevel = 4;// ������������ ������� ������
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
            currentDamage = (basicDamage*(1f+buff)) * (1f+0.6f*bonusLevel);// ������ ������� ����������� ���� �� 60% �� �������� �����
        }
        if (bonusLevel == maxlevel) currentDamage = 120f;
    }

    

    // Update is called once per frame
    void Update()
    {
        timeSinceLastThrow += Time.deltaTime;
        if (timeSinceLastThrow > throwingRate) {// ���� ������ ������ ������� ��� throwingRate 
            ThrowKnife();// ������� ������ ����
            timeSinceLastThrow = 0;
        
        }
    }

    /*������ ������� ������������ �������� ������� ���� � ������� ��� � ������� ���� ���������� �����, 
     * ���� �� ��������� ���� ������������ ��������� ������ */
    private void ThrowKnife()
    {
        float radius = 3;// ������, � �������� ��������, ������������ ����������

        Vector2 point = new Vector2(playerTransform.position.x, playerTransform.position.y);// ����� � 2� ������������, ������������ ����� ���������� ����������� ������

        Collider2D[] ObjectColliders; //������ ���� ����������� � �������� �������

        List<Collider2D> ememyColliders=new List<Collider2D>();// ������ ����������� �����������

        int closestIndex = 0;// ������ ���������� ���������� ����� � ������ ����������� ������

        float closestDistance = float.MaxValue;// ������� ��������� ��������� �� ���������� �����

        Quaternion rotationToTarget;// ����, �� ������� ���������� ��������� ���, ��� ��������

        GameObject knife;// �������� ��������� ���

       // do
        //{
            ObjectColliders = Physics2D.OverlapCircleAll(point, radius, enemyMask);// �������� ��� ���������� � ������� ������� � ����������� �� ���� enemyMask
          //  radius++;

        // } while (ObjectColliders.Length == 0||radius < 4);// ���� �� ����� �� ������ ����������, ���������� ������ � ��������� ��� ���
      
        // ��� ������� ���������� ��������� ��������� ���� �� �� �������� ���������, ��������� ��� � ������ ��������� �����������
        foreach (Collider2D collider in ObjectColliders) {

            if (!collider.isTrigger) ememyColliders.Add(collider);

        }

        Debug.LogWarning("coliders length: " + ememyColliders.Count);

        
        for (int i = 0; i < amountOfKnifes; i++)
        {
            int t = 0;// ���������� ������������ ������� ������ � ������ �����������

            closestDistance = float.MaxValue;// ���������� ��������� ��������� ������ ��� ����������������� ���������� ����������

            foreach (Collider2D enemyColider in ememyColliders)
            {
                //��������� �� ������ �� ������� � ������
                if (enemyColider != null)
                {
                    float tempDistance = Vector3.Distance(playerTransform.position, enemyColider.transform.position);// ���������� ��������� �� ������ �� ����������

                    if (tempDistance < closestDistance)// ���� ��������� ������ ��� ������� ����������� ���������, ������������� �� ������ � ��������� ������ ��������� � ������
                    {
                        closestDistance = tempDistance;
                        closestIndex = t;
                    }
                }
                    t++;
                
            }
            Debug.LogWarning("Index: " + closestIndex);
            //���� ������ ����������� �� ��� ���� ��� ������� ������ ����������� � �������� �� ����
            if (ememyColliders.Count!=0 && ememyColliders[closestIndex] != null)
            {
                Transform enemy = ememyColliders[closestIndex].transform;// �������� ��������� �����
                ememyColliders[closestIndex] = null;// ������������� ������� ������� ������ � null, ����� ��� ���� �� ������ � ������ ���������� �����
                float angle = Vector3.SignedAngle(Vector3.up, (enemy.position - playerTransform.position).normalized, Vector3.forward);
                rotationToTarget = Quaternion.Euler(0, 0, angle);// ���������� ���� �������� ��� ����
            }
            else {
                // ���� ������ ��� ������, ���� �� ������, ��� ���������� ������, �� ������������ �� ��������� ����.
                rotationToTarget = Quaternion.Euler(0, 0, UnityEngine.Random.Range(90,270));
               
            }

            knife = Instantiate(knifePrefab, playerTransform.position, rotationToTarget);// ������� ��� �� ����� ������ � ������������ ��� �� ������ ����
            knife.GetComponent<Bullet>().SetDamage(currentDamage);//������������� ���� � �������� ����
            knife.GetComponent<Bullet>().Speed = 3f;


        }
    }

    public void Buff(float buffAmount)
    {
        buff = buffAmount;
        currentDamage = (basicDamage * (1f + buff)) * (1f + 0.05f * bonusLevel);
    }
}
