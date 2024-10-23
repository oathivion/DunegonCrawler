using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectInteraction : MonoBehaviour
{
    //interact key
    [SerializeField] KeyCode interactKey = KeyCode.E;
    //The canvas where the window is displayed
    GameObject canvas;
    //The text window prefab
    [SerializeField] GameObject textWindowPrefab;
    //The text to display
    [SerializeField] string textToDisplay;
    //Check for input in the update function
    bool checkForInput = false;
    public void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public void Update()
    {
        if (checkForInput)
        {
            //When checking for input if the input is reccived then it instantiates the text window prefab assigned. It then changes that text window's text to the text assigned.
            if (Input.GetKeyDown(interactKey))
            {
                GameObject clone = Instantiate(textWindowPrefab, canvas.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = textToDisplay;

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
