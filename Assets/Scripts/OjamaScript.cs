using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class OjamaScript : MonoBehaviour
{
    Player player;
    ShakerScript shaker;

    public enum OjamaType
    {
        Stone, Poison, Cannon
    }
    OjamaType type = OjamaType.Cannon;

    Vector3 position;
    Vector2 velocity;
    Vector2 direction = Vector2.down;
    float speed = 5f;

    //Poison
    Vector2 startPosition;
    float degree;
    float rotation;

    //Cannon
    [SerializeField] float exDistance;
    [SerializeField] float exPower;
    Vector3 rotationC;

    //つかんでるとき
    bool isFree = true;
    bool isGrabbed;
    bool isMouse;
    Vector2 targetPosition;
    Vector2 prePosition;
    [SerializeField] Vector2 gravity;

    //生存時間
    float aliveTime;

    //スプライト
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite stone;
    [SerializeField] Sprite poison;
    [SerializeField] Sprite cannon;

    //エフェクト
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        shaker = FindAnyObjectByType<ShakerScript>();

        position = transform.position;
        startPosition = transform.position;

        //スプライト変更
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case OjamaType.Stone:
                spriteRenderer.sprite = stone;
                break;

            case OjamaType.Poison:
                spriteRenderer.sprite = poison;
                break;

            case OjamaType.Cannon:
                spriteRenderer.sprite = cannon;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        FreeMove();
        GrabMove();

        prePosition = transform.position;

        aliveTime += Time.deltaTime;
        if (aliveTime > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        ThrowMove();
    }

    void FreeMove()
    {
        if (isFree)
        {
            switch (type)
            {
                case OjamaType.Stone:

                    speed = 10f;
                    velocity = direction * speed;
                    position += (Vector3)velocity * Time.deltaTime;

                    rotationC.z += 100 * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(rotationC);

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

                    rotationC.z += 100 * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(rotationC);

                    break;

                case OjamaType.Cannon:

                    velocity += gravity * Time.deltaTime;

                    if (velocity.y < gravity.y)
                    {
                        velocity.y = gravity.y;
                    }

                    position += (Vector3)velocity * 45f * Time.deltaTime;
                    transform.position = position;

                    rotationC.z += 100 * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(rotationC);

                    if (Vector2.Distance(shaker.transform.position, transform.position) < exDistance)
                    {
                        //爆発させる
                        shaker.isGrabbed = false;
                        shaker.velocity = (shaker.transform.position - transform.position).normalized * exPower;

                        Destroy(gameObject);

                        Instantiate(explosion, transform.position, Quaternion.identity);
                    }

                    break;
            }

            position.z = -5f;
            transform.position = position;
        }
    }

    void GrabMove()
    {
        if (isMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrabbed = true;
                isFree = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
            }
        }

        if (isGrabbed)
        {
            targetPosition = player.transform.position - new Vector3(0, 0.5f);
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);
            position.z = -5f;
            transform.position = position;

            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;

                velocity = (Vector2)transform.position - prePosition;
            }
        }
    }

    void ThrowMove()
    {
        if (!isFree)
        {
            if (!isGrabbed)
            {
                velocity += gravity * Time.deltaTime;

                if (velocity.y < gravity.y)
                {
                    velocity.y = gravity.y;
                }

                position += (Vector3)velocity * 45f * Time.deltaTime;
                transform.position = position;
            }
        }
    }

    public void Setting(OjamaType type, Vector2 direction, float speed)
    {
        this.type = type;
        this.direction = direction;
        this.speed = speed;

        if (type == OjamaType.Cannon)
        {
            velocity = this.direction * this.speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shaker")
        {
            Destroy(gameObject);
            if (type == OjamaType.Cannon)
            {
                //爆発させる
                shaker.isGrabbed = false;
                shaker.velocity = (shaker.transform.position - transform.position).normalized * exPower;
            }
        }

        if (collision.tag == "Mouse")
        {
            isMouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = false;
        }
    }
}
