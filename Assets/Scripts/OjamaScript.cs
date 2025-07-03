using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaScript : MonoBehaviour
{
    public enum OjamaType
    {
        Stone, Poison
    }
    OjamaType type = OjamaType.Stone;

    Vector3 position;
    Vector2 velocity;
    Vector2 direction = Vector2.down;
    float speed = 5f;

    //Poison
    Vector2 startPosition;
    float degree;
    float rotation;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case OjamaType.Stone:

                velocity = direction * speed;
                position += (Vector3)velocity * Time.deltaTime;

                break;
            case OjamaType.Poison:

                if (direction.x == 0)
                {
                    position.y += direction.y * speed * Time.deltaTime;

                    degree += 360f * Time.deltaTime;
                    rotation = degree * Mathf.Deg2Rad;

                    position.x = startPosition.x + Mathf.Cos(rotation) * 2f;
                }
                if (direction.y == 0)
                {
                    position.x += direction.x * speed * Time.deltaTime;

                    degree += 360f * Time.deltaTime;
                    rotation = degree * Mathf.Deg2Rad;

                    position.y = startPosition.y + Mathf.Sin(rotation) * 2f;
                }

                break;
        }

        position.z = -5f;
        transform.position = position;
    }

    public void Setting(OjamaType type, Vector2 direction, float speed)
    {
        this.type = type;
        this.direction = direction;
        this.speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shaker")
        {
            Destroy(gameObject);
        }
    }
}
