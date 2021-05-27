using UnityEngine;
using System.Collections; 

public class ShopScript : MonoBehaviour
{
    TurretBuilder turretBuilder;
    turretAdding outilPoseTourelle;

    
    private void Start(){
        turretBuilder = GameObject.Find("TurretBuilder").GetComponent(typeof(TurretBuilder)) as TurretBuilder;
        outilPoseTourelle = turretBuilder.GetOutilPoseTourelle();
    }

    public void BuyStandardTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleStandard);

        if(MoneyCounter.MoneyValue < turretBuilder.GetTourelleAConstruire().GetComponent<turretSelection>().GetPrice())
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

    public void BuySecondaryTurret(){

        turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleCanon);
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

}
