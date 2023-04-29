using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour,Damaging
{
    // Start is called before the first frame update

    protected Transform target;
    [SerializeField]
    protected float currentSpeed=0;
    [SerializeField]
    private bool isSlowed;
    protected EnemyData data;
    protected Health health;
    protected SpriteRenderer sprite;
    private GameObject crystalDrop;
   // private GameObject gameManager;
    public static event Action<float> onPlayerKillEvent;
    public static event Action onDeathEvent;
    public bool IsSlowed { get=> isSlowed;  }
    public EnemyData Data { get => data; set => data = value; }
    public GameObject CrystalDrop { get => crystalDrop; set => crystalDrop = value; }

    /* void Start()
     {
         isSlowed = false;
         target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
         currrentSpeed = data.EnemySpeed;
         health = gameObject.GetComponent<Health>();
         sprite = gameObject.GetComponent<SpriteRenderer>();
         sprite.sprite = data.EnemySprite;
         health.maxHealth = data.EnemyHealth;
     }
    */



    // Update is called once per frame
    //void LateUpdate()
    // {
    //    HandleBehaviour();
    //
    // }

   
    protected abstract void HandleBehaviour();

    public virtual void ChangeData(EnemyData newData) {
        data = newData;
        currentSpeed = data.EnemySpeed;
        sprite.sprite = data.EnemySprite;
        health.maxHealth = data.EnemyHealth;
    }

    public void ChangeSpeed(bool isSlowing, float scale)
    {

        if (isSlowing&&!isSlowed)
        {

            Debug.Log("Enemy ("+gameObject.name +") is slowed: " +scale);
            //currentSpeed *= scale;
            currentSpeed = currentSpeed * scale;
            isSlowed = true;
        }
        else
        {
            if (isSlowed)
            {
                Debug.Log("Enemy (" + gameObject.name + ") is spop slowed: " + scale);
                currentSpeed /= scale;
                isSlowed = false;
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<ForceField>() != null&&!isSlowed) {
            
            ChangeSpeed(true, collision.GetComponent<ForceField>().PercentageOfSpeed);
        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ForceField>() != null && isSlowed)
        {
           
            ChangeSpeed(false, collision.GetComponent<ForceField>().PercentageOfSpeed);

        }
    }*/

    public void Activate() {
        health = gameObject.GetComponent<Health>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) target = gameObject.transform;
        else target = player.transform; //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isSlowed = false;
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager.onWaveComplete += Suicide;
    }

    public virtual void DoBeforeDestroy() {

        int chance = 4+data.EnemyLevel;
        if (UnityEngine.Random.Range(1, 10) <= chance) {
            Instantiate(crystalDrop, transform.position,new Quaternion());
        }
        //gameManager.GetComponent<GameManager>().GainXp(data.XpCost);
        //onDeathEvent?.Invoke(data.XpCost);
        if (onDeathEvent != null)
        {
            onDeathEvent();
        }
        if (onPlayerKillEvent != null) {
            onPlayerKillEvent(data.XpCost);
        }
    }

    private void Suicide() {
        //Debug.Log("suicide is callsed");
        if(this!=null)
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
       
    }

    public float GetDamage()
    {
        return data.EnemyDamage;
    }
}
