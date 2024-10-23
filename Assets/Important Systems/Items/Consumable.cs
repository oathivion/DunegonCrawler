using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Consumable : Item
{
    //MDetermines what the Consumable is doing whether it's healing or restoring magic
    [Serializable]
    private enum Modificatition
    {
        Health,
        Magic
    } 
    [SerializeField]Modificatition modification;
    [SerializeField]int valueToRestore;
}
