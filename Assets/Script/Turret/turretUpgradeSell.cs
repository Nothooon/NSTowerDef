using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turretUpgradeSell : MonoBehaviour
{
    public GameObject circle;  // Object circle to display the range and the target 
    GameObject circleEnemy;
    GameObject circleRange;

    bool displayRange; // if we want to see the range

    public GameObject buttonAsset;

    GameObject refundButton;
    int refundPrice;

    GameObject upgradeButton;
    bool upgraded;
    int upgradePrice;

    int price;

    ShopScript shop;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<turretSelection>() != null)
        {
            // Recover the turret's price
            price = GetComponent<turretSelection>().GetPrice();
        }
        else if ( GetComponent<ghostSelection>() != null )
        {
            // Recover the turret's price
            price = GetComponent<ghostSelection>().GetPrice();
        }

        // Instanciation of the circle showing the target
        circleEnemy = Instantiate(circle);
        circleEnemy.transform.localScale = new Vector3(1, 1, 1);
        circleEnemy.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0, 0.6f, 0.5f);
        circleEnemy.SetActive(false);
        circleEnemy.transform.parent = gameObject.transform;

        // Instanciation of the circle showing the range
        circleRange = Instantiate(circle);        
        circleRange.transform.position = gameObject.transform.position;
        circleRange.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.25f);
        if (GetComponent<turretSelection>() != null)
        {
            circleRange.transform.localScale = new Vector3(1, 1, 1) * GetComponent<turretSelection>().GetRange() * 2;
        }
        else if (GetComponent<ghostSelection>() != null)
        {
            circleRange.transform.localScale = new Vector3(1, 1, 1) * GetComponent<ghostSelection>().GetRange() * 2;
        }


        // Instanciation of the refund Button
        refundPrice = (int)(0.8f * price);
        refundButton = Instantiate(buttonAsset);
        refundButton.transform.SetParent(GameObject.Find("UI").transform);
        refundButton.transform.localScale = Vector3.one;
        refundButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, -520, 0);
        refundButton.GetComponentInChildren<Text>().text = "Sell : " + refundPrice;
        refundButton.GetComponent<Button>().onClick.AddListener(delegate { Sell(); });
        refundButton.SetActive(false);

        // Instanciation of the upgrade Button
        upgraded = false;
        upgradePrice = (int)(0.5f * price);
        upgradeButton = Instantiate(buttonAsset);
        upgradeButton.transform.SetParent(GameObject.Find("UI").transform);
        upgradeButton.transform.localScale = Vector3.one;
        upgradeButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, -550, 0);
        upgradeButton.GetComponentInChildren<Text>().text = "Upgrade : " + upgradePrice;
        upgradeButton.GetComponent<Button>().onClick.AddListener(delegate { TryUpgrade(); });
        upgradeButton.SetActive(false);

        // Recover of the shop script
        shop = GameObject.Find("Shop").GetComponent<ShopScript>();
    }

    void Update()
    {            
        if(displayRange)
        {
            // Update the target position
            if (GetComponent<turretSelection>() != null)
            {
                circleEnemy.transform.position = GetComponent<turretSelection>().GetTargetPos();
            }
            else if (GetComponent<ghostSelection>() != null)
            {
                circleEnemy.transform.position = GetComponent<ghostSelection>().GetTargetPos();
            }
        }
        // Display the range and the enemy target   
        circleRange.SetActive(displayRange);
        circleEnemy.SetActive(displayRange && circleEnemy.transform.position != Vector3.zero);
    }

    /**
     * The click activate the display mode
     */
    private void OnMouseUp()
    {
        if (enabled)
        {
            if (!displayRange)
            {
                // Reset the displayRange to false of all the turret
                GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
                foreach (GameObject turret in turrets)
                {
                    if (turret.GetComponentInChildren<turretUpgradeSell>() != null)
                    {
                        turretUpgradeSell tmp = turret.GetComponentInChildren<turretUpgradeSell>();
                        tmp.displayRange = false;
                        tmp.refundButton.SetActive(false);
                        tmp.upgradeButton.SetActive(false);
                    }
                }
            }
            // Change displayRange and display the buttons if needed
            displayRange = !displayRange;
            refundButton.SetActive(displayRange);
            upgradeButton.SetActive(displayRange && !upgraded);
        }
    }

    public void Sell()
    {
        MoneyCounter.MoneyValue = MoneyCounter.MoneyValue + refundPrice;
        Delete();
    }

    /**
     * Delete the turret and its components after it has been sold
     */
    void Delete()
    {
        Destroy(circleEnemy);
        Destroy(circleRange);
        Destroy(refundButton);
        Destroy(upgradeButton);
        if (transform.parent != null && transform.parent.tag == "Turret")
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

    public void TryUpgrade()
    {
        if (MoneyCounter.MoneyValue >= upgradePrice && !upgraded) // If we have enough money and not yet upgraded
        {
            MoneyCounter.MoneyValue -= upgradePrice; 
            upgraded = true; 
            upgradeButton.SetActive(false); // Disable the upgrade button
            if (GetComponent<turretSelection>() != null)
            {
                GetComponent<turretSelection>().Upgrade(); // Upgrade the turret
            }
            else if (GetComponent<ghostSelection>() != null)
            {
                GetComponent<ghostSelection>().Upgrade(); // Upgrade the turret
            }
            refundPrice += (int)(0.8f * upgradePrice); // Update the refund price taking into account the upgrade
            refundButton.GetComponentInChildren<Text>().text = "Sell : " + refundPrice;
        }
        else if (MoneyCounter.MoneyValue < upgradePrice)
        {
            shop.GenerateErrorMessage("Not enough money to upgrade this turret"); // Notify that we don't have enough money to upgrade
        }
    }
    
}
