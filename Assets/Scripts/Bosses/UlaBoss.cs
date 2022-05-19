using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UlaBoss : MonoBehaviour, IBoss, Interactable
{
    [SerializeField] public new string name;
    [TextArea]
    [SerializeField] public string description;
    [SerializeField] public int maxHealth;
    [SerializeField] public int damage;
    [SerializeField] public int defense;

    public int current_health;



    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Health { get => current_health; set => current_health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Defense { get => defense; set => defense = value; }
   

    public void Interact() {
        
        if (current_health > 0) { current_health -= 10; }
    }

    public void Start()
    {
        current_health = maxHealth;
    }

}

