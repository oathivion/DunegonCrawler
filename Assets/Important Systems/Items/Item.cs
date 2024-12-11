using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private string name;
    [SerializeField] private int value;
    [SerializeField] private string info;

    public override string ToString()
    {
        return name;
    }
}
