using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizardTracker : MonoBehaviour
{
    int wizardsCollected;
    [SerializeField] string homeScene;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wizard")
        {
            wizardsCollected++;
            Destroy(collision.gameObject);
            SceneManager.LoadScene(homeScene);
        }
    }

    public int GetWizards()
    {
        return wizardsCollected;
    }
}
