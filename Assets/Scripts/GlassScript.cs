using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    Player player;
    ShakerScript shaker;
    GameManager gameManager;

    Vector3 position;
    Vector2 targetPosition;
    Vector2 prePosition;

    Vector3 rotation;

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

    float preGameTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        shaker = FindAnyObjectByType<ShakerScript>();
        gameManager = FindAnyObjectByType<GameManager>();

        position = transform.position;
        position.z = 30f;
        transform.position = position;

        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        prePosition = transform.position;

        //Move();
        DrinkMove();
        Clear();

        if (isFull && !preIsFull)
        {
            newArrow = Instantiate(arrow, transform.position + new Vector3(2f, 0), Quaternion.identity);
        }

        if (gameManager.gameTime < 0 && preGameTime >= 0)
        {
            Instantiate(pourField, transform.position, Quaternion.identity);
        }

        preIsFull = isFull;
        preGameTime = gameManager.gameTime;

        //Debug.Log("isGrabbed" + isGrabbed);
        //Debug.Log("releaseHeight" + releaseHeight);
    }

    //void Move()
    //{
    //    position.y -= 6f * Time.deltaTime;
    //    transform.position = position;

    //    rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
    //    transform.rotation = Quaternion.Euler(rotation);
    //}

    void DrinkMove()
    {
        if (shaker.isPour)
        {
            drinkSprite.transform.position = Vector2.Lerp(drinkSprite.transform.position, transform.position, 1f * Time.deltaTime);
        }
    }

    void Clear()
    {
        if (isFull)
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
                    if (shaker.cocktailProgress > 79)
                    {
                        Instantiate(comment_great, canvas);
                    }
                    else if (shaker.cocktailProgress > 39)
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
        if (collision.tag == "Counter")
        {
            Instantiate(pourField, transform.position, Quaternion.identity);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
