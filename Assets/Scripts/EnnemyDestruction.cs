using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDestruction : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.name == "destroyer"){
            Destruction();
        }
    }

    void Destruction(){
        Destroy(this.gameObject);
    }
}
