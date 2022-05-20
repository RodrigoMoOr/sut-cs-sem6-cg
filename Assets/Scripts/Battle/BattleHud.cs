using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] HPBar hpBar;
    [SerializeField] MPBar mpBar;
    [SerializeField] bool isPlayer;

    public void SetData()
    {
        if (isPlayer)
        {
            text.text = PlayerStats.Instance.getPlayerName();
        }
        else
        {
            text.text = "Monster";
        }
        hpBar.SetHP(0.5f/1f);
        mpBar.SetMP(0.5f / 1f);
    }

}
