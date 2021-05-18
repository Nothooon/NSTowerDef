using UnityEngine;

public class turretAdding : MonoBehaviour
{
    public GameObject spawnee;
    public float radius;
    public GameObject circleRange_;


    GameObject circleSpawn;
    SpriteRenderer sprite;

    float range;
    GameObject circleRange;
    SpriteRenderer sprite2;





    private void Start()
    {
        // On initialise le cercle de taille de l'unité
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        circleSpawn = Instantiate(spawnee);
        circleSpawn.transform.position = mousePos2D;
        circleSpawn.transform.parent = gameObject.transform;
        circleSpawn.GetComponent<turretSelection>().enabled = false;
        sprite = circleSpawn.GetComponent<SpriteRenderer>();
        circleSpawn.SetActive(false);

        // Instanciation of the circle showing the range
        circleRange = Instantiate(circleRange_);
        circleRange.transform.localScale = new Vector3(1, 1, 1) * spawnee.gameObject.GetComponent<turretSelection>().range * 2;
        circleRange.transform.position = mousePos2D;
        circleRange.transform.parent = gameObject.transform;
        sprite2 = circleRange.GetComponent<SpriteRenderer>();
        circleRange.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // On met à jour la position du cercle de séléction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circleSpawn.transform.position = new Vector2(mousePos.x, mousePos.y);
        circleRange.transform.position = new Vector2(mousePos.x, mousePos.y);

        // On met à jour la couleur du cercle de séléction
        if (circleSpawn.activeSelf)
        {
            UpdateSelection();
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Si le mode d'achat est actif, on essaye de spawn une tourelle
            if (circleSpawn.activeSelf)
            {
                trySpawnTurret();
            }

            // TEMP : On suppose qu'un clic gauche active le mode achat de tourelle
            else
            {
                turretChosen(spawnee);

                /**
                 * On devra utiliser la fonction turretChosen pour choisir une tourelle et passer en mode achat
                 * turretChosen(Turret);
                 */

            }
        }

        // Clic droit désactive le mode achat
        else if (Input.GetMouseButtonDown(1) && circleSpawn.activeSelf)
        {
            circleSpawn.SetActive(false);
            circleRange.SetActive(false);
        }
    }

    /**
     * Mise à jour du cercle de selection
     * retourne si il est possible de placer la tourelle séléctionnée à l'emplacement de la souris
     */
    bool UpdateSelection()
    {
        // On récupère la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On vérifie que rien n'empêche de faire apparaître une tourelle à cette endroit
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        if (hit.Length <= 1)
        {
            // On affiche le cercle en vert si il est possible de poser une tourelle à cette emplacement
            sprite.color = new Color(0, 1, 0, 0.5f);
            sprite2.color = new Color(0, 1, 0, 0.5f);
            return true;
        }
        else
        {
            // On affiche le cercle en rouge si impossible de poser une tourelle à cette emplacement
            sprite.color = new Color(1, 0, 0, 0.5f);
            sprite2.color = new Color(1, 0, 0, 0.5f);
            return false;
        }
    }


    /**
     * Essai d'apparition de la tourelle séléctionner
     */
    void trySpawnTurret()
    {
        // On récupère la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On vérifie que rien n'empêche de faire apparaître une tourelle à cette endroit
        RaycastHit2D[] hit = Physics2D.CircleCastAll(mousePos2D, radius, Vector2.zero);

        if (hit.Length <= 1)
        {
            // On fait apparaître la tourelle séléctionnée
            GameObject spawned = Instantiate(spawnee);
            Debug.Log("Tourelle apparu " + spawned.gameObject.name);
            spawned.transform.position = mousePos2D;

            // On sort du mode achat
            circleSpawn.SetActive(false);
            circleRange.SetActive(false);
        }

        else
        {
            Debug.Log("Un objet empêche le positionnement de la tourelle");
            // TEMP : On supprime la tourelle en lui cliquant dessus                                 
        }

    }

    void turretChosen(GameObject turret)
    {
        spawnee = turret;

        radius = Mathf.Max(turret.gameObject.transform.localScale.x, turret.gameObject.transform.localScale.y)/2;

        circleSpawn.SetActive(true);
        circleRange.SetActive(true);

    }

}
