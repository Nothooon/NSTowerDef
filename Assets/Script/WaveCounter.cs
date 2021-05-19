using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{

    public static int WaveActual = 0;
    public static int WaveTotal = 3;
    Text wave;

    // Start is called before the first frame update
    void Start()
    {
        wave = GetComponent<Text>();
        WaveActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        wave.text = WaveActual + "/" + WaveTotal;
    }
}
