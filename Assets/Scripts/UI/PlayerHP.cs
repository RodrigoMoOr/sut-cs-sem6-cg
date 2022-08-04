using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    [SerializeField] Text HP;


    private void Update()
    {
        HP.text = $"HP: {PlayerStats.Instance.Health}";
    }
}
