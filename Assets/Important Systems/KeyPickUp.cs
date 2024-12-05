using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 childPosition;
    [SerializeField] Vector3 childSize;
    GameObject particels;
    bool doOnUpdate;
    [SerializeField] bool activateDoorOnPickup;
    [SerializeField] GameObject door;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particels = transform.GetComponentInChildren<ParticleSystem>().gameObject;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            particels.SetActive(false);
            doOnUpdate = true;
            transform.SetParent(player.transform);
            transform.localPosition = childPosition;
            transform.localScale = childSize;
            if (activateDoorOnPickup)
            {
                door.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void Update()
    {
        if (doOnUpdate)
        {
            transform.SetParent(player.transform);
            transform.localPosition = childPosition;
            transform.localScale = childSize;
        }
    }
}
