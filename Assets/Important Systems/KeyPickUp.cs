using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 childPosition;
    [SerializeField] Vector3 childSize;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            transform.SetParent(player.transform);
            transform.localPosition = childPosition;
            transform.localScale = childSize;
        }
    }
}
