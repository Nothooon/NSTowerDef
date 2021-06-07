using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingEffect : MonoBehaviour
{
    private Vector3 scaleChange, limitUp, limitDown;
    private bool isShrinking;
    private int updateRate;

    void Start(){
        scaleChange = new Vector3(0.1f,0.1f,0.1f);
        limitUp = new Vector3(5f,5f,5f);
        limitDown = new Vector3(3f,3f,3f);
        isShrinking = true;
        updateRate = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(updateRate == 30){
            changeScale();
            updateRate = 0;
        }else{
            updateRate++;
        }
    }

    void changeScale(){
        if(isShrinking){
            if(this.transform.localScale == limitDown){
                isShrinking = false;
            }
            this.transform.localScale -= scaleChange;
        }
        else{
            if(this.transform.localScale == limitUp){
                isShrinking = true;
            }
            this.transform.localScale += scaleChange;
        }
    }
}
