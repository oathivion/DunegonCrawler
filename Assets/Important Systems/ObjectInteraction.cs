using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    //interact key
    [SerializeField] KeyCode interactKey = KeyCode.E;
    //The canvas where the window is displayed
    [SerializeField] GameObject canvas;
    //The text window prefab
    [SerializeField] GameObject textWindowPrefab;
    //The text to display
    [SerializeField] string textToDisplay;
    //Check for input in the update function
    bool checkForInput = false;
    //The Gameobject where the window is stored
    GameObject window;


    //Multi Text Window Only Variables
    //Number of pages for text
    [SerializeField] int pages;
    //Each windows text
    [SerializeField] List<string> pageText;
    //The prefab for multi text window page
    [SerializeField] GameObject multiTextWindowPagePrefab;



    public void Update()
    {
        if (checkForInput)
        {
            //When checking for input if the input is reccived then it instantiates the text window prefab assigned. It then changes that text window's text to the text assigned.
            if (Input.GetKeyDown(interactKey))
            {
                if (window == null)
                {
                    window = Instantiate(textWindowPrefab, canvas.transform);
                    if (window.name.Contains("Multi Text Window"))
                    {
                        //If the prefab selected is the multi text window it dose this:
                        //For the number of pages, First it instantiates a page prefab(that contains text and buttons), Second it then sets the text on the page to the the that corresponds in the page text
                        //Third it adds the page prefab to the page list of the multi text window
                        //It then sets every page other than the first to inactive
                        for (int i = 0; i < pages; i++)
                        {
                            GameObject pagePrefab = Instantiate(multiTextWindowPagePrefab, window.transform);
                            pagePrefab.GetComponentInChildren<TextMeshProUGUI>().text = pageText[i];
                            window.GetComponent<MultiWindowPages>().pages.Add(pagePrefab);
                            if (i>0)
                            {
                                pagePrefab.SetActive(false);
                            }
                        }

                    }
                    else
                    {
                        window.GetComponentInChildren<TextMeshProUGUI>().text = textToDisplay;
                    }
                    
                }
                

            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Turns on checking for the key when the player enters the trigger
        if (collision.tag == "Player")
        {
            checkForInput = true;
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //Turns off checking for the key when the player leaves the trigger
        if (collision.tag == "Player")
        {
            checkForInput = false;
        }
    }
}
