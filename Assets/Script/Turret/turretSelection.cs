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

    public float range = 4f;
    public bool displayRange; // if we want to see the range


    public GameObject projectilePrefab; // Object of the projectile
    public float projectileSpeed = 60.0f;

    public float fireRate = 1.5f; // number of seconds between two shots



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
        circleRange.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);
        circleRange.transform.parent = gameObject.transform;

        // Call of the shot at the chosen frequency
        InvokeRepeating("TryShooting", 0.5f, fireRate);
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTarget(); // target recalculation
        FollowTarget(); // target tracking

        circleRange.SetActive(displayRange); // Display the range if wanted
    }

    void FollowTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;

            // We show the target tracked
            circleEnemy.SetActive(true);
            circleEnemy.transform.position = targetPos;

            // We calculate the turret rotation
            Vector3 difference = transform.position - targetPos;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), 120 * Time.deltaTime);

        }
        else
        {
            circleEnemy.SetActive(false);
        }
    }

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
                target = enemy;
                break;
            }
        }
    }

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

    void Shoot(Vector2 direction, float rotationZ)
    {
        // We create a projectile with the trajectory to the target
        GameObject projectile = Instantiate(projectilePrefab) as GameObject;        
        projectile.transform.parent = gameObject.transform;
        projectile.GetComponent<projectile>().target = target;
        projectile.transform.position = transform.position;
        projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed * Time.deltaTime;
        Debug.Log("Shoot");
    }

}
