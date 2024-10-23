using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    Armor[] armors;
    Weapon[] weapons;
    Consumable[] consumables;
    Item[] miscItems;

    string path = "Assets\\Systems\\Items\\ItemDataFiles\\";

    void Start()
    {
        armors = JsonHelper.FromJson<Armor>("ArmorData.Json", path);
        weapons = JsonHelper.FromJson<Weapon>("WeaponData.Json", path);
        consumables = JsonHelper.FromJson<Consumable>("ConsumableData.Json", path);
        miscItems = JsonHelper.FromJson<Item>("MiscItemData.Json", path);
    }
}
