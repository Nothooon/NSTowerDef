using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretSelection : MonoBehaviour
{
    string enemyTag = "Enemy"; // tag to regroup enemy

    public GameObject circle;  // Object circle to display the range and the target 
    GameObject circleEnemy;
    GameObject circleRange;

    public GameObject target;

    public int price;

    public float range = 4f;
    public bool displayRange; // if we want to see the range


    public GameObject projectilePrefab; // Object of the projectile
    public float projectileSpeed = 60.0f;

    public GameObject sellButton;
    GameObject buttonSell;

    public float fireRate = 0.6f; // number of seconds between two shots


    // Start is called before the first frame update
    void Start()
    {
        // Instanciation of the circle showing the target
        circleEnemy = Instantiate(circle);
        circleEnemy.transform.localScale = new Vector3(1, 1, 1);
        circleEnemy.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0, 0.6f, 0.5f);
        circleEnemy.SetActive(false);
        circleEnemy.transform.parent = gameObject.transform;

        // Instanciation of the circle showing the range
        circleRange = Instantiate(circle);
        circleRange.transform.localScale = new Vector3(1, 1, 1) * range * 2;
        circleRange.transform.position = gameObject.transform.position;
        circleRange.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.25f);
        //circleRange.transform.parent = gameObject.transform;


        buttonSell = Instantiate(sellButton);
        buttonSell.GetComponent<SellTurret>().SetTurret(gameObject);
        buttonSell.transform.position = new Vector2(3.5f,-3.15f);
        buttonSell.SetActive(false);

        // Call of the shot at the chosen frequency
        StartCoroutine(TryShootingCo());
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTarget(); // target recalculation
        FollowTarget(); // target tracking

        circleRange.SetActive(displayRange); // Display the range if wanted
    }

    /**
     * Calculation of the rotation to the target
     */
    void FollowTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;

            // We show the target tracked
            circleEnemy.SetActive(displayRange);
            circleEnemy.transform.position = targetPos;

            // We calculate the turret rotation
            Vector3 difference = transform.position - targetPos;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), 500 * Time.deltaTime);

        }
        else
        {
            circleEnemy.SetActive(false);
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

    /**
     * The click activate the display mode
     */
    private void OnMouseUp()
    {
        if(!displayRange)
        {
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            foreach (GameObject turret in turrets)
            {
                turret.GetComponentInChildren<turretSelection>().displayRange = false;
            }
        }
        displayRange = !displayRange;
        buttonSell.SetActive(displayRange);
    }

    /**
     * Display the turret and its components
     */
    public void Delete()
    {
        Destroy(circleEnemy);
        Destroy(circleRange);
        Destroy(buttonSell);
        Destroy(gameObject);
    }

}
