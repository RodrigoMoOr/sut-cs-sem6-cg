using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject health;

    public void SetHP(float value){

        health.transform.localScale = new Vector3(value, 1f); 
    
    }
}
