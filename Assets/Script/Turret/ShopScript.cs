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

        if(MoneyCounter.MoneyValue < 100)
        {
            Debug.Log("Pas assez d'argent - TODO : Créer un message à l'écran");
            outilPoseTourelle.enabled = false;
        }else{

            turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleStandard);

            outilPoseTourelle.SetTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.SetRadius(0.5f);
            outilPoseTourelle.SetPrixTourelle(100);

            outilPoseTourelle.enabled = true;
        }
    }

    public void BuySecondaryTurret(){

        if(MoneyCounter.MoneyValue < 200)
        {
            Debug.Log("Pas assez d'argent - TODO : Créer un message à l'écran");
            outilPoseTourelle.enabled = false;
        }else{

            turretBuilder.SetTourelleAConstruire(turretBuilder.tourelleStandard);

            outilPoseTourelle.SetTurret(turretBuilder.GetTourelleAConstruire());
            outilPoseTourelle.SetRadius(0.5f);
            outilPoseTourelle.SetPrixTourelle(200);

            outilPoseTourelle.enabled = true;
        }
    }

}
