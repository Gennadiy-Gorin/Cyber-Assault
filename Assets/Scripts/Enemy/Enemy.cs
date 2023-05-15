using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour,Damaging
{
    // Start is called before the first frame update

    protected Transform target;
    protected bool isDead;
    [SerializeField]
    protected float currentSpeed=0;
    [SerializeField]
    private bool isSlowed;
    protected EnemyData data;
    protected Health health;
    protected SpriteRenderer sprite;
    private GameObject crystalDrop;

    [SerializeField]
    private RuntimeAnimatorController[] animator;
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
        isDead = false;
        if (animator.Length !=0 ) {
           // Debug.Log("Animator length on"+data.EnemyName +" is "+ animator.Length);
            if (data.EnemyLevel == 5) GetComponent<Animator>().runtimeAnimatorController = animator[2];
            else if(data.EnemyLevel>2) GetComponent<Animator>().runtimeAnimatorController = animator[1];
            else GetComponent<Animator>().runtimeAnimatorController = animator[0];

        }
        GetComponent<Animator>().SetBool("isWalking", true);
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
        isDead = true;
        GetComponent<Animator>().SetBool("isWalking", false);
        int chance = 2+data.EnemyLevel;
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
        gameObject.GetComponent<Animator>().SetBool("isDying", true);
       StartCoroutine("Death");
    }

    protected void Suicide() {
        isDead = true;
       // Debug.Log("suicide is callsed");
        if(this!=null)
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
       
    }

    IEnumerator Death() {

       
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return data.EnemyDamage;
    }
}
