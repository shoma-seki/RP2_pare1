using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    Player player;
    ShakerScript shaker;

    Vector3 position;
    Vector2 targetPosition;
    Vector2 prePosition;

    Vector2 scale;

    Vector3 rotation;

    bool isMouse;
    bool isGrabbed;
    public bool isGrounded;
    bool isTouched;

    public bool isFull;
    bool preIsFull;
    bool isFullGrabbed;
    bool isClear;

    float releaseHeight;

    //注ぎ判定フィールド
    [SerializeField] GameObject pourField;

    //グラスの中身のゲームオブジェクト
    [SerializeField] GameObject drinkSprite;

    //評価コメント
    RectTransform canvas;
    [SerializeField] GameObject comment_great;
    [SerializeField] GameObject comment_soso;
    [SerializeField] GameObject comment_bad;

    //やじるし
    [SerializeField] GameObject arrow;
    GameObject newArrow;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        shaker = FindAnyObjectByType<ShakerScript>();

        position = transform.position;
        position.z = 30f;
        transform.position = position;
        scale = transform.localScale;

        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        prePosition = transform.position;

        Grab();
        Move();
        DrinkMove();
        Clear();

        if (isFull && !preIsFull)
        {
            newArrow = Instantiate(arrow, transform.position + new Vector3(2f, 0), Quaternion.identity);
        }

        preIsFull = isFull;

        //Debug.Log("isGrabbed" + isGrabbed);
        //Debug.Log("releaseHeight" + releaseHeight);
    }

    void Grab()
    {
        if (isMouse && shaker.isCompleted && !isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrabbed = true;
                isTouched = true;
                releaseHeight = -10;

                scale = Vector2.one * 0.32f;
                transform.localScale = scale;
            }
        }
    }

    void Move()
    {
        if (isGrabbed)
        {
            targetPosition = player.transform.position - new Vector3(0, 0.5f);
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);
            position.z = -5f;
            transform.position = position;

            Vector2 velocity = (Vector2)position - prePosition;

            //回転        
            if (velocity.magnitude < 0.1f)
            {
                rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
                transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                if (velocity.x < 0)
                {
                    rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 40f), 5f * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(rotation);
                }

                if (velocity.x > 0)
                {
                    rotation = Vector3.Lerp(rotation, new Vector3(0, 0, -40f), 5f * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(rotation);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isGrabbed = false;
                releaseHeight = transform.position.y;
            }
        }

        if (!isGrabbed && !isGrounded && isTouched)
        {
            position.y -= 6f * Time.deltaTime;
            transform.position = position;

            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void DrinkMove()
    {
        if (shaker.isPour)
        {
            drinkSprite.transform.position = Vector2.Lerp(drinkSprite.transform.position, transform.position, 1f * Time.deltaTime);
        }
    }

    void Clear()
    {
        if (isGrounded && isFull)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isFullGrabbed = true;
            }

            if (isFullGrabbed)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isClear = true;
                }
            }

            if (isClear)
            {
                position = Vector2.Lerp(position, new Vector2(20, -6f), 3f * Time.deltaTime);
                transform.position = position;

                if (position.x > 17)
                {
                    shaker.isClear = true;
                    Destroy(gameObject);
                    Destroy(newArrow);

                    //クリアコメントを表示
                    if (shaker.cocktailAmount > 79)
                    {
                        Instantiate(comment_great, canvas);
                    }
                    else if (shaker.cocktailAmount > 39)
                    {
                        Instantiate(comment_soso, canvas);
                    }
                    else
                    {
                        Instantiate(comment_bad, canvas);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
        }

        if (collision.tag == "Counter")
        {
            isGrounded = true;
            isGrabbed = false;
            Instantiate(pourField, transform.position, Quaternion.identity);
            transform.rotation = Quaternion.Euler(Vector3.zero);
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
