using UnityEngine;

public class turretAdding : MonoBehaviour
{
    private GameObject turret;
    private float radius;
    public GameObject circleRange_;
    private int prixTourelle;

    GameObject turretSprite;
    SpriteRenderer sprite;

    float range;
    GameObject circleRange;
    SpriteRenderer sprite2;

    private void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Instanciation of the circle showing the range
        circleRange = Instantiate(circleRange_);
        circleRange.transform.position = mousePos2D;
        circleRange.transform.parent = gameObject.transform;
        sprite2 = circleRange.GetComponent<SpriteRenderer>();
        circleRange.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        // On met � jour la position du cercle de s�l�ction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        circleRange.transform.position = new Vector2(mousePos.x, mousePos.y);

        // On met � jour la couleur du cercle de s�l�ction

        // On met à jour la couleur du cercle de séléction et sa position si en mode achat
        if (turretSprite != null && turretSprite.activeSelf)
        {
            UpdateSelection();
            turretSprite.transform.position = new Vector2(mousePos.x, mousePos.y);
        }

        // Si mode achat et clic gauche on essaye de poser
        if (Input.GetMouseButtonDown(0) && turretSprite != null && turretSprite.activeSelf)
        {
            if (trySpawnTurret()){
                MoneyCounter.MoneyValue -= turret.GetComponentInChildren<turretSelection>().GetPrice();
            }
        }
        
        // Si mode achat et Clic droit desactive le mode achat
        else if (Input.GetMouseButtonDown(1) && turretSprite != null && turretSprite.activeSelf)
        {
            Destroy(turretSprite.gameObject);
            circleRange.SetActive(false);
        }
    }

    /**
     * Mise à jour du cercle de selection
     */
    void UpdateSelection()
    {
        // On récupère la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On vérifie que rien n'empêche de faire apparaître une tourelle à cette endroit
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        if (FreeSpace())
        {
            // On affiche le cercle en vert si il est possible de poser une tourelle à cette emplacement
            ChangeColor(new Color(0, 1, 0, 0.5f));
        }
        else
        {
            // On affiche le cercle en rouge si impossible de poser une tourelle à cette emplacement
            ChangeColor(new Color(1, 0, 0, 0.5f));
        }
    }


    /**
     * Essai d'apparition de la tourelle s�l�ctionner
     */
    bool trySpawnTurret()
    {
        // On r�cup�re la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On v�rifie que rien n'emp�che de faire appara�tre une tourelle � cette endroit
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        if (FreeSpace())
        {
            // On fait apparaitre la tourelle selectionnee
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
     *  Active buy mode with the choosen turret
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
            sprite = turretSprite.GetComponentInChildren<SpriteRenderer>();
        }
    }

    void ChangeColor(Color c)
    {
        circleRange.GetComponent<SpriteRenderer>().color = c;
        foreach( SpriteRenderer s in turretSprite.GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = c;
        }
    }

    bool FreeSpace ()
    {
        // On r�cup�re la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On v�rifie que rien n'emp�che de faire appara�tre une tourelle � cette endroit
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        return (hit.Length <= 1 - turretSprite.transform.childCount); // Jsp pas pk
    }


    public void SetRadius (float radius){
        this.radius = radius;
    }

    public float GetRadius(){
        return this.radius;
    }

    public void SetTurret (GameObject turret){
        this.turret = turret;
    }

    public GameObject GetTurret(){
        return this.turret;
    }

    public void SetPrixTourelle (int prixTourelle){
        this.prixTourelle = prixTourelle;
    }

    public float GetPrixTourelle(){
        return this.prixTourelle;
    }

}

