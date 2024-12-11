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
        if (wizardTracker.GetWizards() == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (wizardTracker.GetWizards()==2)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }

    }

}
