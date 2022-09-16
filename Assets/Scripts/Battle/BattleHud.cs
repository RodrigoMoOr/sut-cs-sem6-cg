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


    public void SetData(Boss boss = null)
    {
        if (!boss)
        {
            text.text = PlayerStats.Instance.Name;
        }
        else
        {
            text.text = boss.Base.Name;
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


    public static IEnumerator ShowAttackSprite(Sprite toshow)
    {
        Image displayAttack = GameObject.Find("DisplayAttack").GetComponent<Image>();
        displayAttack.sprite = toshow;
        yield return displayAttack.DOFade(1f, 1f);
        yield return new WaitForSeconds(2f);
        yield return displayAttack.DOFade(0f, 1f);


    }

}
