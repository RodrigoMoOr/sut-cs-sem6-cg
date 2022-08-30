using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject health;

    public void SetHP(float value){

        health.transform.localScale = new Vector3(value, 1f);
    
    }

    public void SetHPText(int valueCurrent, int valueMax)
    {
        Text hptext = health.transform.Find("HPValue").GetComponent<Text>();
        hptext.text = $"{valueCurrent}/{valueMax}";
    }

    public Transform Health => health.transform;
}
