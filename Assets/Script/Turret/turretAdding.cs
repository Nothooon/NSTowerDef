using UnityEngine;
using UnityEngine.UI;

public class turretAdding : MonoBehaviour
{
    public GameObject[] buttons;
    private GameObject turret;
    float radius;

    public GameObject circleRange_;
    float range;

    GameObject turretSprite;
    GameObject circleRange;

    private void Start()
    {
        // Recover mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Instanciation of the circle showing the range
        circleRange = Instantiate(circleRange_);
        circleRange.transform.position = mousePos2D;
        circleRange.transform.parent = gameObject.transform;
        circleRange.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Recover mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // if buy mode, update color and position of the sprites
        if (turretSprite != null && turretSprite.activeSelf)
        {
            UpdateSelectionColor();
            circleRange.transform.position = mousePos2D;
            turretSprite.transform.position = mousePos2D;
        }

        // If buy mode and left click, try to spawn
        if (Input.GetMouseButtonDown(0) && turretSprite != null && turretSprite.activeSelf)
        {
            if (trySpawnTurret()){
                MoneyCounter.MoneyValue -= turret.GetComponentInChildren<turretSelection>().GetPrice();
                ReactivateButtons();
            }
        }

        // If buy mode and right click, deactivate buy mode
        else if (Input.GetMouseButtonDown(1) && turretSprite != null && turretSprite.activeSelf)
        {
            Destroy(turretSprite.gameObject);
            circleRange.SetActive(false);
            ReactivateButtons();
        }
    }

    /**
     * Update the color depends on the availability of the mouse position
     */
    void UpdateSelectionColor()
    {
        if (FreeSpace())
        {
            // Change color to green if space is free
            ChangeColor(new Color(0, 1, 0, 0.5f));
        }
        else
        {
            // Change color to red if space is not free
            ChangeColor(new Color(1, 0, 0, 0.5f));
        }
    }


    /**
     * Try to spawn a turret at the mouse position
     */
    bool trySpawnTurret()
    {
        // Recover mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (FreeSpace())
        {
            // Bring up the selected turret
            GameObject spawned = Instantiate(turret);
            spawned.transform.position = mousePos2D;
            circleRange.SetActive(false);
            turretSprite.SetActive(false);
            return true;
        }

        else
        {
            Debug.Log("Un objet empeche le positionnement de la tourelle");
            return false;                               
        }

    }


    /**
     *  Activate buy mode with the choosen turret
     */
    public void ChooseTurret(GameObject turret)
    {
        if (turret != null)
        {
            // Update turret and its radius
            this.turret = turret;
            radius = Mathf.Max(turret.gameObject.transform.localScale.x, turret.gameObject.transform.localScale.y) / 2;

            // Update range of the turret
            circleRange.SetActive(true);
            circleRange.transform.localScale = new Vector3(1, 1, 1) * turret.gameObject.GetComponentInChildren<turretSelection>().range * 2;
            

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Initialize the turretSprite
            turretSprite = Instantiate(turret);
            turretSprite.transform.position = mousePos2D;
            turretSprite.transform.parent = gameObject.transform;
            turretSprite.GetComponentInChildren<turretSelection>().enabled = false;
        }
    }

    /**
     * Change the color of turretSprite and of circleRange
     */
    void ChangeColor(Color c)
    {
        // Change the color of the circleRange
        circleRange.GetComponent<SpriteRenderer>().color = c;

        // Change the color of the turretSprite and all its children
        foreach( SpriteRenderer s in turretSprite.GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = c;
        }
    }

    /**
     *  Return the availability of space
     */
    bool FreeSpace ()
    {
        // Recover mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Verify wich are in the circle zone
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        // Return if there are more collider than just the turretSprite
        return (hit.Length <= 1 - turretSprite.transform.childCount); // Idk
    }

    public float GetRadius(){
        return this.radius;
    }

    public GameObject GetTurret(){
        return this.turret;
    }


    private void ReactivateButtons(){
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
}

