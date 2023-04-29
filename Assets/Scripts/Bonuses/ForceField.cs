using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : Bonus
{

    private float radius;
    public float PercentageOfSpeed=0.8f;
    private float rechargeTime;
    private float activeTime;
    public Sprite fieldImage;
    bool isActivated=false;
   // private float lastActivated = Mathf.NegativeInfinity;
    private float timer = 0f;

    

    // Update is called once per frame
    void Update()
    {
        if (isActivated) {
            timer += Time.deltaTime;
          //  Debug.Log("time: " +( timer));
            if ( timer >= activeTime) {
                timer = 0;
                isActivated = false;
                StartCoroutine("RechargeField");
            }
        
        }

    }

    public override void Upgrade() {

        bonusLevel++;
        PercentageOfSpeed -=0.1f;
        rechargeTime -= 0.1f;
        activeTime += 1f;
        radius *= 1.5f;
        this.gameObject.transform.localScale = new Vector3(radius,radius,1);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.gameObject.tag == "Enemy"&&isActivated) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.ChangeSpeed(true,PercentageOfSpeed);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isActivated)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(!enemy.IsSlowed)
            enemy.ChangeSpeed(true, PercentageOfSpeed);
        }
    }*/
   /* private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject colidedGameObject = collision.gameObject;
        if (colidedGameObject.tag == "Enemy")
        {
            Enemy enemy = colidedGameObject.GetComponent<Enemy>();
                if (enemy.IsSlowed)
               
            enemy.ChangeSpeed(false, PercentageOfSpeed);
        }
    }*/
      private void OnTriggerExit2D(Collider2D collision)
      {
        if (collision.isTrigger) return;
        GameObject colidedGameObject = collision.gameObject;
          if (colidedGameObject.tag == "Enemy" )
          {

               colidedGameObject.GetComponent<Enemy>().ChangeSpeed(false,PercentageOfSpeed);
          }
      }


    public override void Activate()
    {
        PercentageOfSpeed = 0.5f;
        bonusLevel = 1;
        radius = 2.5f;
        activeTime = 5;
        rechargeTime = 2.5f;
       // Debug.Log("Bonus Active. Radius= "+radius+"Active time="+activeTime );
        isActivated = true;
        this.gameObject.transform.localScale = new Vector3(radius, radius, 1);
        FieldChangeState(true);
      
    }

    public override void Deactivate()
    {
        Destroy(this.gameObject);

    }

    private void FieldChangeState(bool isActive) {

        this.gameObject.GetComponent<SpriteRenderer>().enabled = isActive;
        this.gameObject.GetComponent<CircleCollider2D>().enabled = isActive;

    }

    IEnumerator RechargeField() {

        FieldChangeState(false);

        yield return new WaitForSeconds(rechargeTime);

        FieldChangeState(true);
        isActivated = true;
        //lastActivated = Time.time;

    }
}
