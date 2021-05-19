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
        turretSprite = Instantiate(turret);
        turretSprite.transform.position = mousePos2D;
        turretSprite.transform.parent = gameObject.transform;
        turretSprite.GetComponent<turretSelection>().enabled = false;
        sprite = turretSprite.GetComponent<SpriteRenderer>();
        turretSprite.SetActive(false);

        // Instanciation of the circle showing the range
        circleRange = Instantiate(circleRange_);
        circleRange.transform.localScale = new Vector3(1, 1, 1) * turret.gameObject.GetComponent<turretSelection>().range * 2;
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

        UpdateSelection();
    
        if (Input.GetMouseButtonDown(0))
        {
            if (trySpawnTurret()){
                MoneyCounter.MoneyValue -= prixTourelle;
                this.enabled = false;
            }
        }
        
        // Clic droit d�sactive le mode achat
        else if (Input.GetMouseButtonDown(1) && turretSprite.activeSelf)
        {
            this.enabled = false;
        }

        // TEMP : On suppose qu'un clic gauche active le mode achat de tourelle
        else
        {
            refreshBuildingIndicator(turret);
        }
    }

    /**
     * Mise � jour du cercle de selection
     * retourne si il est possible de placer la tourelle s�l�ctionn�e � l'emplacement de la souris
     */
    bool UpdateSelection()
    {
        // On r�cup�re la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On v�rifie que rien n'emp�che de faire appara�tre une tourelle � cette endroit
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        if (hit.Length <= 1)
        {
            // On affiche le cercle en vert si il est possible de poser une tourelle � cette emplacement
            sprite.color = new Color(0, 1, 0, 0.5f);
            sprite2.color = new Color(0, 1, 0, 0.5f);
            return true;
        }
        else
        {
            // On affiche le cercle en rouge si impossible de poser une tourelle � cette emplacement
            sprite.color = new Color(1, 0, 0, 0.5f);
            sprite2.color = new Color(1, 0, 0, 0.5f);
            return false;
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

        if (hit.Length <= 1)
        {
            // On fait appara�tre la tourelle s�l�ctionn�e
            GameObject spawned = Instantiate(turret);
            Debug.Log("Tourelle apparu " + spawned.gameObject.name);
            spawned.transform.position = mousePos2D;
            circleRange.SetActive(false);
            turretSprite.SetActive(false);
            return true;
        }

        else
        {
            Debug.Log("Un objet empeche le positionnement de la tourelle");
            return false;
            // TEMP : On supprime la tourelle en lui cliquant dessus                                 
        }

    }

    void refreshBuildingIndicator(GameObject turret)
    {
        this.turret = turret;

        radius = Mathf.Max(turret.gameObject.transform.localScale.x, turret.gameObject.transform.localScale.y)/2;

        // On initialise le cercle de taille de l'unit�
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        turretSprite.transform.position = mousePos2D;
        turretSprite.transform.parent = gameObject.transform;
        turretSprite.GetComponent<turretSelection>().enabled = false;
        sprite = turretSprite.GetComponent<SpriteRenderer>();

        turretSprite.SetActive(true);
        circleRange.SetActive(true);

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

