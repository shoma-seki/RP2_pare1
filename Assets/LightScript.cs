using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightScript : MonoBehaviour
{
    Image image;

    public void SetColor(Color color)
    {
        image = GetComponent<Image>();
        image.color = color;
    }
}
