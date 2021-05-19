using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{

    public static int MoneyValue = 200;
    Text money;

    // Start is called before the first frame update
    void Start()
    {
        money = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        money.text = "" + MoneyValue;
    }

}
