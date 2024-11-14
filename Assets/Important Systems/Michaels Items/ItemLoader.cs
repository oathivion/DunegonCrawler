using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    private StatsSaveSystem statsSaveSystem;

    void Awake() 
    {
        statsSaveSystem = FindObjectOfType<StatsSaveSystem>();
        LoadItems();
    }

    private void LoadItems()
    {
        LoadItem((string)statsSaveSystem.GetStat("armor"));
        LoadItem((string)statsSaveSystem.GetStat("statItemOne"));
        LoadItem((string)statsSaveSystem.GetStat("statItemTwo"));
        LoadItem((string)statsSaveSystem.GetStat("statItemThree"));
        LoadItem((string)statsSaveSystem.GetStat("weaponOne"));
        LoadItem((string)statsSaveSystem.GetStat("weaponTwo"));
    }

    private void LoadItem(string itemName)
    {
        // Load the prefab from the specified path
        GameObject prefab = Resources.Load<GameObject>("Michaels Items/Prefabs/" + itemName);
        
        if (prefab != null)
        {
            // Instantiate the prefab as a child of this GameObject
            GameObject instantiatedPrefab = Instantiate(prefab, transform);

            // Optionally, set the local position if needed
            instantiatedPrefab.transform.localPosition = Vector3.zero; // Adjust as necessary
            Debug.Log("Prefab Loaded: " + itemName);
        }
        else
        {
            Debug.LogError("Prefab not found: " + itemName);
        }
    }
}
