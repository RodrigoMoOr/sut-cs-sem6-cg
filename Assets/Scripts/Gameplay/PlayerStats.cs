using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] string playerName;
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    [SerializeField] int mana;
    [SerializeField] int maxMana;
    [SerializeField] int ectsPoints = 0;
    [SerializeField] Sprite playerBattleImage;

    [SerializeField] List<MoveBase> moves;

    [SerializeField] int reached;
    [SerializeField] List<QuestBase> completed;

    public static PlayerStats Instance { get; private set; }


    float healthProcent;

    private void Awake()
    {
        Instance = this;
    }

    public string Name => playerName;
    public int Health => health;

    public float HealthProcent => healthProcent;
    public int MaxHealth => maxHealth;
    public int Mana => mana;
    public int MaxMana => maxMana;
    public int ECTS => ectsPoints;
    public List<MoveBase> Move => moves;

    public Sprite PlayerImage => playerBattleImage;

    public List<QuestBase> Completed => completed;
    public int Reached { get => reached; set => reached = value; }

    
    public void AddHP(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddECTS(int amount)
    {
        ectsPoints+= amount;
    }

    public bool GetDamage(int amount)
    {
        health -= amount;
        healthProcent = (float) health/ (float) maxHealth;
        if(health <= 0)
        {
            return true;
        }
        else return false;
    }

    public void AddMP(int amount)
    {
        mana += amount;
        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }

    public void AddMaxHP(int amount)
    {
        maxHealth += amount;
    }
}
