using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoss
{

    string Name { get; set; }

    string Description { get; set; }

    int MaxHealth { get; set; }

    int Damage { get; set; }

    int Defense { get; set; }



    public void Attack();


}