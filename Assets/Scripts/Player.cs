using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 position;

    [SerializeField] Sprite pa;
    [SerializeField] Sprite gu;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeSprite();
    }

    void Move()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = ClampToScreen();
        position.z = -15f;
        transform.position = position;
    }

    void ChangeSprite()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.sprite = gu;
        }

        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.sprite = pa;
        }
    }

    Vector2 ClampToScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        return Camera.main.ViewportToWorldPoint(viewPos);
    }
}
