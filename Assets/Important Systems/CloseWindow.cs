using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [SerializeField] GameObject windowToClose;

    //Destroys the object selected
    public void DestroyWindow()
    {
        Destroy(windowToClose);
    }

}
