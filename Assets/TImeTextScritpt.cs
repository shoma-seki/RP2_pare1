using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TImeTextScritpt : MonoBehaviour
{
    GameManager gameManager;
    TextMeshProUGUI text;

    float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = gameManager.gameTime;
        if (gameTime <= 0)
        {
            gameTime = 0;
        }
        text.text = "‚Ì‚±‚èF" + gameTime.ToString("f0") + "•b";
    }
}
