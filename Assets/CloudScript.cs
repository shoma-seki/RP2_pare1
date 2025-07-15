using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite cloud1;
    [SerializeField] Sprite cloud2;

    Vector3 position;
    Vector2 direction;
    Vector2 velocity;
    [SerializeField] float speed;

    float aliveTime = 30;

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

        if (transform.position.x < 0)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        float scale = Random.Range(1.5f, 3f);
        Vector2 scale2 = new Vector2(scale, scale);
        transform.localScale = scale2;

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;
        position += (Vector3)velocity;
        position.z = -30f;
        transform.position = position;

        aliveTime -= Time.deltaTime;
        if (aliveTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
