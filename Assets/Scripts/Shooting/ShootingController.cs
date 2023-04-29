using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireTypes {Single,Burst,Shotgun }
public class ShootingController : MonoBehaviour
{
    // Start is called before the first frame update

    //public Gun currentGun;
    [Tooltip("Whether this shooting controller is controled by the player")]
    public bool isPlayerControlled = false;
    // The last time this component was fired
    private float lastFired = Mathf.NegativeInfinity;
    [SerializeField]
    private GunData gunData;
    private float currentDamage;
    private bool reloading = false;
    private int bulletsLeft;
    public Transform projectileHolder;
     private float damageBuff;

    void Start()
    {
        /* currentGun = gameObject.GetComponent<Gun>();//gameObject.GetComponentInChildren<Gun>();

         if (currentGun.GunData == null) Debug.Log("Gun null");
         if (currentGun.GunData != null)
         {
             gunData = currentGun.GunData;
             reloading = false;
             bulletsLeft = gunData.BulletCapasity;

         }
         // if (!isPlayerControlled) { damageBuff = 0f; }
         // else damageBuff = gameObject.GetComponent<PlayerController>().GunDamageBonus;*/
        if (gunData != null) gameObject.GetComponent<SpriteRenderer>().sprite = gunData.Icon;
        damageBuff = 0;
        projectileHolder = GameObject.FindGameObjectWithTag("Holder").transform;

    }

    public void SetDamageBuff(float buffAmount) {
        damageBuff = buffAmount; 
     }

    // Update is called once per frame
    void Update()
    {
       // if (gunData.name != currentGun.GunData.name) {
       //     NewGun(currentGun.GunData);
       // }
        if (!reloading&&gunData!=null)
        {

            Fire();
           
        }
       
    }

    public void NewGun(GunData newGun)
    {
        Debug.Log("New gun called");
        gunData = newGun;
        reloading = false;
        bulletsLeft = gunData.BulletCapasity;
        gameObject.GetComponent<SpriteRenderer>().sprite = gunData.Icon;
        //currentGun.GunData = newGun;
    }



    private void Fire()
    {
        // If the cooldown is over fire a projectile
       if ((Time.timeSinceLevelLoad - lastFired) > gunData.FireRate)
        {
            /*if ((Damaging)this.GetComponent(typeof(Damaging)) != null)
            {
                currentDamage = currentGun.GunData.AttackDamage + ((Damaging)this.GetComponent(typeof(Damaging))).GetDamage();
            }
            else currentDamage = currentGun.GunData.AttackDamage;*/
            if (isPlayerControlled) {
                currentDamage = gunData.AttackDamage * (1f + damageBuff);
            }
            else currentDamage = gunData.AttackDamage;

            // Launches a projectile
            switch (gunData.FireType)
            {
                case FireTypes.Single:
                    SpawnProjectileSingle();
                    break;
                case FireTypes.Burst:
                    SpawnProjectileBurst(gunData.BulletFire);
                    break;
                case FireTypes.Shotgun:
                    SpawnProjectileShotgun(gunData.BulletFire);
                    break;
                default:
                    Debug.LogWarning("Unset Fire Type");
                    break;
            }
          

           /* if (fireEffect != null)
            {
                Instantiate(fireEffect, transform.position, transform.rotation, null);
            }*/

            // Restart the cooldown
            lastFired = Time.timeSinceLevelLoad;

            bulletsLeft--;
            if (bulletsLeft <= 0) { reloading = true;

                StartCoroutine("Reload", gunData.ReloadSpeed);
            }
        }
    }

    private void FireBurst(int quantity) { 
    
    }


    private void FireShotgun(int amount) { 
    
    }


   private void SpawnProjectileSingle()
    {
        // Check that the prefab is valid
        GameObject bulletPrefab = gunData.Bullet;

        

        if (bulletPrefab != null)
        {
            // Create the projectile
            
                GameObject projectileGameObject = Instantiate(bulletPrefab, transform.position, transform.rotation, null);
            SetBulletStats(projectileGameObject);
               // projectileGameObject.GetComponent<Bullet>().SetDamage(currentDamage);
                // Account for spread
                /* Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
                 rotationEulerAngles.z += Random.Range(-projectileSpread, projectileSpread);
                 projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);*/

                // Keep the heirarchy organized
               // if (projectileHolder != null)
               // {
               //     projectileGameObject.transform.SetParent(projectileHolder);
              //  }
        }
    }

    private void SpawnProjectileBurst(int burstSize) {
        GameObject bulletPrefab = gunData.Bullet;
        StartCoroutine(BurtFire(bulletPrefab, burstSize));

    }

    private void SpawnProjectileShotgun(int amount) {
        GameObject bulletPrefab = gunData.Bullet;
        float angle = -15f;
        float angleDelta = 30f / amount;
        for (int i = 0; i < amount; i++) {
            GameObject projectileGameObject = Instantiate(bulletPrefab, transform.position, transform.rotation, null); // It would be wise to use the gun barrel's position and rotation to align the bullet to.
            Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
            rotationEulerAngles.z += angle+i*angleDelta;
            projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);
            SetBulletStats(projectileGameObject);
           /* projectileGameObject.GetComponent<Bullet>().SetDamage(currentDamage);

            if (projectileHolder != null)
            {
                projectileGameObject.transform.SetParent(projectileHolder);
            }*/

        }



    }


  
    IEnumerator BurtFire(GameObject bulletPrefab, int burstSize) {

        for (int i = 0; i < burstSize; i++)
        {
           
            GameObject projectileGameObject = Instantiate(bulletPrefab,transform.position, transform.rotation, null); // It would be wise to use the gun barrel's position and rotation to align the bullet to.
           // projectileGameObject.GetComponent<Bullet>().SetDamage(currentDamage);
            Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
            rotationEulerAngles.z += Random.Range(-5, 5);
            projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);
            SetBulletStats(projectileGameObject);
           /* if (projectileHolder != null)
            {
                projectileGameObject.transform.SetParent(projectileHolder);
            }*/
            yield return new WaitForSeconds(0.05f); // wait till the next round
        }


    }


    IEnumerator Reload(float reloadSpeed) {

        yield return new WaitForSeconds(reloadSpeed);

        bulletsLeft = gunData.BulletCapasity;
        reloading = false;
    
    
    }

    private void SetBulletStats(GameObject bullet) {
        bullet.GetComponent<Bullet>().SetDamage(currentDamage);
        bullet.GetComponent<Bullet>().Speed = gunData.BulletSpeed;

        if (projectileHolder != null)
        {
            bullet.transform.SetParent(projectileHolder);
        }
    }

}
