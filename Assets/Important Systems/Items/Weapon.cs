using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon : Item
{
    [SerializeField] private int[] damageRange; //damage range
    [SerializeField] private int attack; //Likelyhood to hit
}
