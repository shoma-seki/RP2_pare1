using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerScript : MonoBehaviour
{
    Player player;

    Vector2 position;
    Vector2 targetPosition;

    Vector2 prePosition;
    Vector2 preVelocity;
    Vector2 velocity;
    [SerializeField] Vector2 gravity;

    Vector3 rotation;

    bool isMouse;
    bool isGrabbed;
    bool isGrounded = true;
    bool isCollision;

    public bool isCompleted;

    //スタートに戻す
    Vector2 startPosition;

    //反射係数
    [SerializeField] float reflection;

    //中身
    float shakeSpeed;       //振ったスピード
    float shakeDistance;    //振った距離
    float shakeCount;       //振った回数
    float cocktailAmount;   //カクテルの量
    Vector2 preShakePoint;  //前回シェイクした場所
    public float cocktailProgress; //カクテルの完成度   
    public float cocktailProgressMax;
    float shakerHeight;     //シェイカーの高さ

    //投げてるときに加算
    [SerializeField] float rotationSpeed;
    float triggerRotation;

    //振るときのやつ
    Vector2 direction;      //進んでいる方向
    Vector2 previousDirection;
    float directionChangeThreshold = 140f; // 角度差が45度以上で「急変」と判断

    //注ぐ
    [SerializeField] Vector3 pourRotation;
    [SerializeField] Vector2 pourOffset;
    bool isPour;

    //エフェクト
    [SerializeField] GameObject catchEffect;    //キャッチした時
    [SerializeField] float catchOffset;

    [SerializeField] GameObject shakaEffect;    //振ったとき
    [SerializeField] float shakaOffset;

    [SerializeField] GameObject gotuEffect;
    [SerializeField] float gotuOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        position = transform.position;

        //スタートに戻すポジション
        startPosition = position;
    }

    private void FixedUpdate()
    {
        prePosition = transform.position;

        if (!isGrounded)
        {
            Move();
            Shake();
        }

        preVelocity = (Vector2)transform.position - prePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Grab();
        Pour();

        if (Input.GetMouseButtonUp(0))
        {
            velocity = preVelocity;
        }

        //スタートに戻す
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            position = transform.position;
            transform.rotation = Quaternion.identity;
            isGrounded = true;
        }

        //完成
        if (cocktailProgress >= 100)
        {
            isCompleted = true;
        }

        //コマンド
#if UNITY_EDITOR
        Commands();
#endif
    }

    void Shake()
    {
        //シェイクのスピード
        shakeSpeed = ((Vector2)transform.position - prePosition).magnitude;

        //シェイクのタイミング
        direction = ((Vector2)transform.position - prePosition).normalized;
        Vector2 currentDirection = direction.normalized; // 現在の進行方向（velocityなどを想定）

        if (!isCollision)
        {
            if (previousDirection != Vector2.zero)
            {
                float angleDiff = Vector2.Angle(previousDirection, currentDirection);

                if (angleDiff > directionChangeThreshold)
                {
                    shakeCount++;
                    Debug.Log("shakeCount" + shakeCount);

                    //シャカエフェクト
                    Vector2 shakaPoint = (Vector2)transform.position + previousDirection * shakaOffset;
                    Instantiate(shakaEffect, shakaPoint, Quaternion.identity);

                    if (shakeCount >= 2)
                    {
                        shakeDistance = Vector2.Distance(preShakePoint, transform.position);

                        //カクテルの完成度を変更
                        cocktailProgress += shakeDistance * shakeSpeed / 10f;
                        Debug.Log("カクテルの完成度" + cocktailProgress);
                    }

                    preShakePoint = transform.position;
                }
            }
        }

        isCollision = false;

        previousDirection = currentDirection;
    }

    void Move()
    {
        if (isGrabbed)
        {
            targetPosition = player.transform.position;
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);

            //回転
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 90f), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (!isGrabbed && !isGrounded)
        {
            if (velocity.y < gravity.y)
            {
                velocity.y = gravity.y;
            }

            velocity += gravity * Time.deltaTime;
            position += velocity * 45f * Time.deltaTime;

            //回転させる
            rotation.z += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation);
            rotation = transform.rotation.eulerAngles;

            //float angle = Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
            //rotation = Vector3.Lerp(rotation, new Vector3(0, 0, angle + 90), 5f * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotation);
            //rotation = transform.rotation.eulerAngles;

            //高さで完成度を加算
            shakerHeight = position.y + 6f;
            triggerRotation += rotationSpeed * Time.deltaTime;

            if (triggerRotation > 360)
            {
                cocktailProgress += shakerHeight / 5f;
                Debug.Log("カクテルの完成度" + cocktailProgress);
                triggerRotation = 0;
            }
        }

        transform.position = position;
    }

    void Grab()
    {
        if (isMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrounded = false;
                isGrabbed = true;

                //キャッチエフェクト
                Vector2 catchPoint = (Vector2)transform.position + new Vector2(Random.Range(-100f, 100f) / 100f, Random.Range(-100f, 100f) / 100f) * catchOffset;
                Instantiate(catchEffect, catchPoint, Quaternion.identity);

                //Debug.Log("つかんだよ");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
            //Debug.Log("離したよ");
        }
    }

    void Pour()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
            ///Debug.Log("マウスオーバーだお");
        }

        if (collision.tag == "Wall")
        {
            if (transform.position.x < 0)
            {
                velocity = new Vector2(1, 1) / reflection;
            }

            if (transform.position.x > 0)
            {
                velocity = new Vector2(-1, 1) / reflection;
            }

            isCollision = true;

            //ゴツエフェクト   
            Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
        }

        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / 5f;

            isCollision = true;

            //ゴツエフェクト
            Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = false;
        }
    }

    void Commands()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            cocktailProgress = 110f;
        }
    }
}
