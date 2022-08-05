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
    [SerializeField] Image playerImage;
    [SerializeField] Image enemyImage;

    public void Awake()
    {
        playerImage = GameObject.Find("PlayerImage").GetComponent<Image>();
        enemyImage = GameObject.Find("EnemyImage").GetComponent<Image>();
    }

    public void SetData(Boss boss = null)
    {
        if (!boss)
        {
            text.text = PlayerStats.Instance.Name;
            playerImage.sprite = PlayerStats.Instance.PlayerImage;
        }
        else
        {
            text.text = boss.Name;
            enemyImage.sprite = boss.BattleImage;
        }
        hpBar.SetHP(0.5f/1f);
        mpBar.SetMP(0.5f / 1f);
    }

}
