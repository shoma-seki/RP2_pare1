using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 position;

    // Update is called once per frame
    void Update()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = ClampToScreen();
        transform.position = position;
    }

    Vector2 ClampToScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        return Camera.main.ViewportToWorldPoint(viewPos);
    }
}
