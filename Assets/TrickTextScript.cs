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
    Color targetColor;

    float scale;

    // Start is called before the first frame update
    void Start()
    {
        shaker = FindAnyObjectByType<ShakerScript>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //値によってイロイロ変更
        if (shaker.triggerRotation > 3600)
        {
            scale = 2.5f;
        }
        else if (shaker.triggerRotation > 2160)
        {
            scale = 2f;
        }
        else if (shaker.triggerRotation > 720)
        {
            scale = 1.5f;
        }
        else
        {
            targetColor = Color.white;
            scale = 1f;
        }

        transform.localScale = new Vector2(scale, scale);

        //テキストを変更
        if (!shaker.isGrabbed && !shaker.isGrounded)
        {
            triggerRotation = (Mathf.Floor(shaker.triggerRotation / 360f) * 360f).ToString();
            text.text = triggerRotation;
            text.color = targetColor;
            visibleSecond = 0;
        }
        else if (visibleSecond < 2f)
        {
            text.text = triggerRotation;
            visibleSecond += Time.deltaTime;
        }
        else
        {
            visibleSecond = 10;
            text.color = Color.Lerp(text.color, Color.clear, 2f * Time.deltaTime);
        }
    }
}
