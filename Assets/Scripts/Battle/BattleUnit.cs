using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] BattleHud hud;


    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] float healthProcent;


    public BattleHud Hud
    {
        get { return hud; }
    }

    Image image;
    Vector3 orginalPos;
    Color originalColor;

    [SerializeField] public Boss Boss { get; set;}

    private void Awake()
    {
        image = GetComponent<Image>();
        orginalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void Setup(Boss boss)
    {
        Boss = boss;
        image.sprite = boss.Base.BattleImage;

        maxHealth = currentHealth = boss.Base.MaxHealth;
        healthProcent = 1f;

        hud.gameObject.SetActive(true);
        hud.SetData(boss);
        image.color = originalColor;
        transform.localScale = new Vector3(1, 1, 1);
        PlayEnterAnimation();
    }
    public void Setup()
    {

        image.sprite = PlayerStats.Instance.PlayerImage;
        maxHealth = currentHealth = PlayerStats.Instance.MaxHealth;
        healthProcent = 1f;
        hud.gameObject.SetActive(true);
        hud.SetData();
        image.color = originalColor;
        transform.localScale = new Vector3(1, 1, 1);
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
            image.transform.localPosition = new Vector3(500f, orginalPos.y);

            image.transform.DOLocalMoveX(orginalPos.x, 1f);
    }

    public void Clear()
    {
        hud.gameObject.SetActive(false);
    }

    public void PlayDieAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(orginalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }



    public bool GetDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;
        updateHealthProcent();
        StartCoroutine(hud.LowerHealthBar(healthProcent));
        if (currentHealth <= 0)
        {
            return true;
        }
        else return false;
    }

    private void updateHealthProcent()
    {
        healthProcent = (float)currentHealth / (float)maxHealth;
    }



}
