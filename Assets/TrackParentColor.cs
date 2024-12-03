using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackParentColor : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer selfSprite;
    SpriteRenderer parentSprite;
    void Start()
    {
        selfSprite = gameObject.GetComponent<SpriteRenderer>();
        parentSprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfSprite.color != parentSprite.color)
        {
            selfSprite.color = parentSprite.color;
        }
    }
}
