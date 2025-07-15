using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatchBonusx2Script : MonoBehaviour
{
    TextMeshProUGUI text;
    Color color;

    CameraScript mainCamera;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindAnyObjectByType<CameraScript>();
        gameManager = FindAnyObjectByType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        color = mainCamera.pColor;
        color.a = 0.3f;

        if (!gameManager.isFever)
        {
            color.a = 0;
        }

        text.color = color;
    }
}
