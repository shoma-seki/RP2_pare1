using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBallTexScript : MonoBehaviour
{
    GameManager gameManager;
    Vector2 position;
    Vector2 targetPosition;
    [SerializeField] Vector2 bottom;
    [SerializeField] Vector2 top;

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rectTransform = GetComponent<RectTransform>();
        position = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isFever)
        {
            targetPosition = top;
        }
        else
        {
            targetPosition = bottom;
        }

        position = Vector2.Lerp(position, targetPosition, 5f * Time.deltaTime);
        rectTransform.anchoredPosition = position;
    }
}
