using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{


    [SerializeField] BossBase boss;

     string bossName;
     string bossDescription;

     Sprite bossNPCSprite;
     Sprite bossBattleImage;

    public int current_health;
    public float health_procent;
    
    List<AttackBase> possibleAttacks;
    SpriteRenderer sprite_renderer;

    public string Name => bossName;
    public string Description => bossDescription;

    public Sprite BattleImage => bossBattleImage;
    public float HealthProcent => health_procent;

    

    public void Awake()
    {
        sprite_renderer = GetComponentInParent<SpriteRenderer>();
        bossNPCSprite = boss.NPCSprite;
        bossBattleImage = boss.BattleImage;

        bossName = boss.bossName;
        bossDescription = boss.bossDescription;

    }
    public void Start()
    {
        current_health = boss.maxHealth;
        sprite_renderer.sprite = bossNPCSprite;
        transform.localScale = new Vector3(1,1,1);
        //sprite_renderer.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().size = sprite_renderer.sprite.bounds.size;
        possibleAttacks = boss.PossibleAttacks;

    }

    public AttackBase Attack()
    {
        if(possibleAttacks.Count > 0)
        {
            AttackBase chosenAttack = possibleAttacks.PickRandom();
            return chosenAttack;
        }
        else return default(AttackBase);
    }

    public bool GetDamage(int amount)
    {
        current_health -= amount;
        health_procent = (float)current_health / (float)boss.maxHealth;
        if (current_health <= 0)
        {
            return true;
        }
        else return false;
    }

}

