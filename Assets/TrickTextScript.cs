using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrickTextScript : MonoBehaviour
{
    ShakerScript shaker;
    TextMeshProUGUI text;
    string triggerRotation;
    float visibleSecond;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shaker.isGrabbed && !shaker.isGrounded)
        {
            triggerRotation = shaker.triggerRotation.ToString("f0");
            text.text = triggerRotation;
        }
        else if (visibleSecond < 2f)
        {
            text.text = triggerRotation;
            visibleSecond += Time.deltaTime;
        }
        else
        {

        }
    }
}
