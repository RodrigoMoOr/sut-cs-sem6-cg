using System;
using DG.Tweening;
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
        hpBar.SetHP(1f);
        mpBar.SetMP(1f);
    }

    public IEnumerator LowerHealthBar(float remainingHealth)
    {
        float last_health = hpBar.Health.localScale.x;
        for (float i = last_health; i > remainingHealth; i -= 0.01f)
        {
            hpBar.SetHP(i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator Kill()
    {
        if (!isPlayer)
        {
            yield return enemyImage.transform.DOMoveX(enemyImage.transform.position.x + 20, 100f);
            yield return enemyImage.DOFade(0f, 1);
        }
        else
        {
            yield return enemyImage.transform.DOMoveX(playerImage.transform.position.x - 20, 100f);
            yield return enemyImage.DOFade(0f, 1);
        }
    }

    public static IEnumerator ShowAttackSprite(Sprite toshow)
    {
        Image displayAttack = GameObject.Find("DisplayAttack").GetComponent<Image>();
        displayAttack.sprite = toshow;
        yield return displayAttack.DOFade(1f, 1);
        yield return new WaitForSeconds(0.7f);
        yield return displayAttack.DOFade(0f, 1);


    }

}
