using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TImeTextScritpt : MonoBehaviour
{
    GameManager gameManager;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "ÇÃÇ±ÇËÅF" + gameManager.gameTime.ToString("f0") + "ïb";
    }
}
