using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite cloud1;
    [SerializeField] Sprite cloud2;

    Vector2 position;
    Vector2 direction;
    Vector2 velocity;
    [SerializeField] float speed;

    float aliveTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Random.Range(0, 100) < 50)
        {
            spriteRenderer.sprite = cloud1;
        }
        else
        {
            spriteRenderer.sprite = cloud2;
        }

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;
        position += velocity;
        transform.position = position;

        aliveTime -= Time.deltaTime;
        if (aliveTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
