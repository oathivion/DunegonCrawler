using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiWindowNavigtion : MonoBehaviour
{

    //Has the page list(made up of game objects), has a page forward function and a page back function.
    //On Start: Gets the list of pages from the parent object named Multi Text Window, page # = list index of this object's parent's parent
    //If this is the back button and the first page it sets the back button to grey, If this is the forward button and the last page it sets the forward button to an x
    //Page Back: if page # = 0, do nothing, otherwise set page #-1 to active then deactivate this page
    //Page Forward: if page # = list len-1, then close the text window, otherwise set page #+1 to active and then deactivate this page
    List<GameObject> pageList;
    [SerializeField] GameObject parentPage;
    GameObject grandparent;
    int pageNumber;
    public void Start()
    {
        pageList = transform.GetComponentInParent<MultiWindowPages>().pages;
        grandparent = transform.parent.parent.gameObject;
        pageNumber = pageList.IndexOf(parentPage);

        if (pageNumber == 0 && gameObject.name == "Back Button")
        {
            transform.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }
        if (pageNumber == pageList.Count-1 && gameObject.name == "Forward Button")
        {
            transform.GetComponentInChildren<TextMeshProUGUI>().text = "x";
        }

    }

    public void PageBack()
    {
        if (pageNumber!=0)
        {
            pageList[pageNumber - 1].SetActive(true);
            parentPage.SetActive(false);
        }
    }

    public void PageForward()
    {
        if (pageNumber == pageList.Count - 1)
        {
            Destroy(grandparent);
        }
        else
        {
            pageList[pageNumber + 1].SetActive(true);
            parentPage.SetActive(false);
        }
    }





}
