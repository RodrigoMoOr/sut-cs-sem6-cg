using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] string playerName;
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    public static PlayerStats Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }


    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getHealth()
    {
        return health;
    }
}
