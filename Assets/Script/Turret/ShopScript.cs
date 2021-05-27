using UnityEngine;
using System.Collections; 
using TMPro;

public class ShopScript : MonoBehaviour
{
    TurretBuilder turretBuilder;
    turretAdding outilPoseTourelle;
    public GameObject errorPosition;
    public GameObject errorMessage;
    
    private void Start(){
        turretBuilder = GameObject.Find("TurretBuilder").GetComponent(typeof(TurretBuilder)) as TurretBuilder;
        outilPoseTourelle = turretBuilder.GetOutilPoseTourelle();
        errorPosition = GameObject.Find("ErrorMessages");
    }


    public void BuyStandardTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleStandard);

        if(MoneyCounter.MoneyValue < turretBuilder.GetTourelleAConstruire().GetComponent<turretSelection>().GetPrice())
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.enabled = false;
        }
        else
        {
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
        
    }

    public void BuySecondaryTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleCanon);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.enabled = false;
        }
        else
        {
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
       
    }

    public void BuyFourthTurret()
    {

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleColle);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            Debug.Log("Pas assez d'argent - TODO : Créer un message à l'écran");
            outilPoseTourelle.enabled = false;
        }
        else
        {
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
    }

    public void BuyThirdTurret()
    {

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleBallista);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            Debug.Log("Pas assez d'argent - TODO : Créer un message à l'écran");
            outilPoseTourelle.enabled = false;
        }
        else
        {
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
    }

    private void GenerateErrorMessage(string message){
        errorMessage.GetComponent<TextMeshProUGUI>().text = "caca";
        Instantiate(errorMessage, errorPosition.transform.position, errorPosition.transform.rotation);
    }


}
