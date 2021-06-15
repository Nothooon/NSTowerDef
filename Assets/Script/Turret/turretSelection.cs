using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class turretSelection : MonoBehaviour
{
    string enemyTag = "Enemy"; // tag to regroup enemy    

    public GameObject target;

    public int price;

    public float range = 4f;
    
    public float fireRate = 0.6f; // number of seconds between two shots

    public GameObject projectilePrefab; // Object of the projectile
    public float projectileSpeed;

    float rangeCanon = 2f; // range of the explosion of the projectileCanon

    


    // Start is called before the first frame update
    void Start()
    {
        

        // Call of the shot at the chosen frequency
        StartCoroutine(TryShootingCo());
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTarget(); // target recalculation
        FollowTarget(); // target tracking        
    }

    /**
     * Calculation of the rotation to the target
     */
    void FollowTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;

            // We calculate the turret rotation
            Vector3 difference = transform.position - targetPos;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), 500 * Time.deltaTime);

        }
    }

    /**
     * Find the enemy to target
     */
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        target = null;

        // We run through the enemies and stop at the first in our range
        // foreach goes through them in their order of creation
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                if(target == null || target.GetComponent<FollowThePath>().GetDistanceTraveled() < enemy.GetComponent<FollowThePath>().GetDistanceTraveled())
                {
                    target = enemy;
                }             
            }
        }
    }

    /**
     * if there is a target, shoot to the target
     */ 
    void TryShooting()
    {
        if (target != null)
        {
            Vector3 difference = target.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float distance = difference.magnitude;

            Vector2 direction = difference / distance;
            direction.Normalize();
            Shoot(direction, rotationZ);
        }
    }

    IEnumerator TryShootingCo()
    {

        TryShooting();
        yield return new WaitForSeconds(1/fireRate);
        StartCoroutine(TryShootingCo());
    }

    /**
     * Create a projectile at the turret position with the trajectory to the target
     */
    void Shoot(Vector2 direction, float rotationZ)
    {
        GameObject projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position;
        projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed * Time.deltaTime;

        if (projectile.GetComponent<projectile>() != null)
        {
            projectile.GetComponent<projectile>().SetTarget(target);
        }
        else if (projectile.GetComponent<projectileCanon>() != null)
        {
            projectile.GetComponent<projectileCanon>().SetRange(rangeCanon);
            projectile.GetComponent<projectileCanon>().SetTarget(target);            
        } 
        else if (projectile.GetComponent<projectileGlue>() != null)
        {
            projectile.GetComponent<projectileGlue>().SetTarget(target);
        }
        else if (projectile.GetComponent<projectileBallista>() != null)
        {
            projectile.GetComponent<projectileBallista>().SetTarget(target);
        }
    }

    /**
     * Return the turret price
     */
    public int GetPrice()
    {
        return price;
    }

    public float GetRange()
    {
        return range;
    }

    public Vector3 GetTargetPos()
    {
        if(target!= null)
        {
            return target.transform.position;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }   

    public void Upgrade()
    {
        fireRate *= 1.5f;
        if (projectilePrefab.GetComponent<projectileCanon>() != null)
        {
            rangeCanon *= 2f;
        }
        
    }

}
