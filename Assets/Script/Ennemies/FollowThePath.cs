using UnityEngine;

public class FollowThePath : MonoBehaviour {

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    public Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	private void Start () {

        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        this.moveSpeed = GetComponent<Ennemy>().GetCurrentSpeed();
	}
	
	// Update is called once per frame
	private void Update () {

        this.moveSpeed = GetComponent<Ennemy>().GetCurrentSpeed();
        // Move Enemy
        Move();
	}


    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);
            if(transform.position.x > waypoints[waypointIndex].transform.position.x){
                spriteRenderer.flipX = true;
            }else{
                spriteRenderer.flipX = false;
            }
            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
        else{
            if (LifeCounter.LifeValue > 0)
            {
                LifeCounter.LifeValue -= GetComponent<Ennemy>().damage;
            }
            Destroy(gameObject);
        }
    }
    
    public void SetWaypoints(Transform[] waypoints){
        this.waypoints = waypoints;
    }

    public void SetMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
    }

    public void SetSpriteRenderer(SpriteRenderer renderer){
        spriteRenderer = renderer;
    }
}
