using System.Collections;
using TMPro;
using UnityEngine;

public class AutoScrollText : MonoBehaviour
{
    public float scrollSpeed = 30f; // Speed at which the text scrolls

    private TextMeshProUGUI textComponent;
    private RectTransform textRectTransform;
    private float initialPosition;
    private float textHeight;

   

    void Update()
    {
        transform.Translate(new Vector3(0, scrollSpeed * Time.deltaTime, 0));
    }
}
