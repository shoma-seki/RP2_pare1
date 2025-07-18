using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 position;

    [SerializeField] Sprite pa;
    [SerializeField] Sprite gu;
    SpriteRenderer spriteRenderer;

    bool isClamp = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeSprite();
        ClampToScreen();
    }

    void Move()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    void ClampToScreen()
    {
        //position.x = Mathf.Clamp(position.x, -11, 11);
        position.y = Mathf.Clamp(position.y, -5.85f, 80);

        if (position.y < 6.53f)
        {
            isClamp = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (position.y > 6.53f)
            {
                isClamp = false;
            }
        }

        if (isClamp)
        {
            if (Input.GetMouseButton(0))
            {
                position.y = Mathf.Clamp(position.y, -5.85f, 6.53f);
            }
        }

        transform.position = position;
    }
}
