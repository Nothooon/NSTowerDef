using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTurret : MonoBehaviour
{
    GameObject turret;

    // Sell turret if the player clicked the button
    private void OnMouseUp()
    {
        turretSelection t = turret.gameObject.GetComponent<turretSelection>();
        MoneyCounter.MoneyValue = MoneyCounter.MoneyValue +(int)( 0.8f * t.GetPrice());
        t.Delete();
    }

    public void SetTurret(GameObject turret)
    {
        this.turret = turret;
    }
}
