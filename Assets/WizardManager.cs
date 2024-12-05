using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    GameObject player;
    List<GameObject> wizards = new List<GameObject>();
    WizardTracker wizardTracker;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wizardTracker = player.GetComponent<WizardTracker>();
        for (int i = 0; i < transform.childCount; i++)
        {
            wizards.Add(transform.GetChild(0).gameObject);

        }
        for (int i = 0; i < wizardTracker.GetWizards(); i++)
        {
            wizards[i].SetActive(true);
        }

    }

}
