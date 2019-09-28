using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FontEnlargeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text text;

    private int fontSize;

    private void Start()
    {
        fontSize = text.fontSize;   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = fontSize * 2;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = fontSize;
    }
}
