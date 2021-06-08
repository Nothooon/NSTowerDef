using UnityEngine;
using System.Collections; 
using TMPro;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    TurretBuilder turretBuilder;
    turretAdding outilPoseTourelle;
    public GameObject errorMessage;
    public GameObject[] buttons;

    private void Start(){
        turretBuilder = GameObject.Find("TurretBuilder").GetComponent(typeof(TurretBuilder)) as TurretBuilder;
        outilPoseTourelle = turretBuilder.GetOutilPoseTourelle();
    }


    public void BuyStandardTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleStandard);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.ReactivateButtons();
            outilPoseTourelle.enabled = false;
        }
        else
        {
            DeactivateButtons();
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
        
    }

    public void BuyCannonTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleCanon);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.ReactivateButtons();
            outilPoseTourelle.enabled = false;
        }
        else
        {
            DeactivateButtons();
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
       
    }

    public void BuyStickyTurret()
    {

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleColle);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.ReactivateButtons();
            outilPoseTourelle.enabled = false;
        }
        else
        {
            DeactivateButtons();
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
    }

    public void BuyBallistaTurret()
    {

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleBallista);
        GameObject t = turretBuilder.GetTourelleAConstruire();
        int prix = t.GetComponentInChildren<turretSelection>().GetPrice();

        if (MoneyCounter.MoneyValue < prix)
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.ReactivateButtons();
            outilPoseTourelle.enabled = false;
        }
        else
        {
            DeactivateButtons();
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
    }

    public void BuyAntiGhostTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleAntiGhost);

        if(MoneyCounter.MoneyValue < turretBuilder.GetTourelleAConstruire().GetComponent<GhostSelection>().GetPrice())
        {
            GenerateErrorMessage("Not enough money to buy this turret");
            outilPoseTourelle.ReactivateButtons();
            outilPoseTourelle.enabled = false;
        }
        else
        {
            DeactivateButtons();
            outilPoseTourelle.ChooseTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.enabled = true;
        }
        
    }

    public void GenerateErrorMessage(string message){
        errorMessage.GetComponent<TextMeshProUGUI>().text = message;
        errorMessage.GetComponent<Animator>().Play("FadeAwayText");
    }

    public void DeactivateButtons(){
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
}
