using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilder : MonoBehaviour
{

    public GameObject tourelleStandard;
    public GameObject tourelleCanon;
    public GameObject tourelleBallista;

    private GameObject tourelleAConstruire;

    private turretAdding outilPoseTourelle;


    public void Start(){
        outilPoseTourelle = gameObject.GetComponent(typeof(turretAdding)) as turretAdding;
    }

    public GameObject GetTourelleAConstruire(){
        return tourelleAConstruire;
    }

    public void SetTourelleAConstruire(GameObject tourelle){
        tourelleAConstruire = tourelle;
    }

    public turretAdding GetOutilPoseTourelle(){
        GameObject turretBuilder = GameObject.Find("TurretBuilder");
        outilPoseTourelle = turretBuilder.GetComponent(typeof(turretAdding)) as turretAdding;
        return outilPoseTourelle;
    }
}
