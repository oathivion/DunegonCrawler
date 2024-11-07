using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSystem : MonoBehaviour
{
    [SerializeField] GameObject linkedObject;
    bool pressedDown = false;
    public void Update()
    {
        if (pressedDown && linkedObject.activeSelf)
        {
            linkedObject.SetActive(false);
        }
        else if (!pressedDown && !linkedObject.activeSelf)
        {
            linkedObject.SetActive(true);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!pressedDown)
        {
            pressedDown = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        pressedDown = false;
    }


}
