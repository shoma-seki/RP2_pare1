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
    public Vector2 velocity;
    [SerializeField] Vector2 gravity;

    Vector3 rotation;

    bool isMouse;
    public bool isGrabbed;
    public bool isGrounded = true;
    bool isCollision;

    public bool isClear;

    public bool isCompleted;

    bool isPlusRotate;    //triggerRotationを足すか

    //スタートに戻す
    Vector2 startPosition;

    //反射係数
    [SerializeField] float reflection;

    //投げる力
    [SerializeField] float throwPower;

    //中身
    float shakeSpeed;       //振ったスピード
    float shakeDistance;    //振った距離
    float shakeCount;       //振った回数
    float cocktailAmount;
    [SerializeField] float kCocktailAmount;   //カクテルの量
    [SerializeField] float cocktailAmountMinus; //カクテルの量を減らす係数
    Vector2 preShakePoint;  //前回シェイクした場所
    public float cocktailProgress; //カクテルの完成度   
    public float cocktailProgressMax;
    float shakerHeight;     //シェイカーの高さ

    //投げてるときに加算
    [SerializeField] float rotationSpeed;
    public float triggerRotation;

    //振るときのやつ
    Vector2 direction;      //進んでいる方向
    Vector2 previousDirection;
    float directionChangeThreshold = 140f; // 角度差が45度以上で「急変」と判断

    //注ぐ
    [SerializeField] Vector2 pourOffset;
    Vector2 pourCenter;
    public bool isPour;

    //Reset用
    public float pourTime;
    [SerializeField] float kPourTime;

    //エフェクト
    [SerializeField] GameObject catchEffect;    //キャッチした時
    [SerializeField] float catchOffset;

    [SerializeField] GameObject shakaEffect;    //振ったとき
    [SerializeField] float shakaOffset;

    [SerializeField] GameObject gotuEffect;     //ぶつかったとき
    [SerializeField] float gotuOffset;

    [SerializeField] GameObject spillParticle;  //ぶつかったときのパーティクル


    [SerializeField] GameObject pourParticle;  //注ぐときのパーティクル

    [SerializeField] GameObject trickEffect;
    [SerializeField] GameObject trickParticle;
    float trickEffectRotate;

    //音
    [SerializeField] GameObject shakaSound;     //振ったときの音
    [SerializeField] GameObject catchSound;     //キャッチしたときの音
    [SerializeField] GameObject trickCatchSound;     //トリックを決めてキャッチしたときの音
    [SerializeField] GameObject gotuSound;      //ぶつかったときの音

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        position = transform.position;

        //スタートに戻すポジション
        startPosition = transform.position;
        targetPosition = startPosition;

        pourTime = kPourTime;

        cocktailAmount = kCocktailAmount;
    }

    private void FixedUpdate()
    {
        prePosition = transform.position;

        if (!isGrounded && !isPour)
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
        ResetToStart();
        Clear();

        if (Input.GetMouseButtonUp(0))
        {
            velocity = preVelocity * throwPower;
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

        Debug.Log("カクテルの量" + cocktailAmount);

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
                    //Debug.Log("shakeCount" + shakeCount);

                    //シャカエフェクト
                    Vector2 shakaPoint = (Vector2)transform.position + previousDirection * shakaOffset;
                    Instantiate(shakaEffect, shakaPoint, Quaternion.identity);
                    Instantiate(shakaSound);

                    if (shakeCount >= 2)
                    {
                        shakeDistance = Vector2.Distance(preShakePoint, transform.position);

                        //カクテルの完成度を変更
                        cocktailProgress += shakeDistance * shakeSpeed / 10f;
                        //Debug.Log("カクテルの完成度" + cocktailProgress);
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

            //80以上で反転
            if (position.y >= 80)
            {
                velocity.y = 0;
                position.y = 80;
            }

            //回転させる
            rotation.z += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation);
            rotation = transform.rotation.eulerAngles;

            //float angle = Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
            //rotation = Vector3.Lerp(rotation, new Vector3(0, 0, angle + 90), 5f * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotation);
            //rotation = transform.rotation.eulerAngles;

            //高さで完成度を加算

            if (velocity.y > 0)
            {
                shakerHeight = position.y + 6f;
            }

            if (isPlusRotate)
            {
                triggerRotation += rotationSpeed * Time.deltaTime;

                trickEffectRotate += rotationSpeed * Time.deltaTime;
                //エフェクト
                if (trickEffectRotate > 360)
                {
                    trickEffectRotate = 0;
                    //Instantiate(trickEffect, transform.position, Quaternion.identity);
                    Instantiate(trickParticle, transform.position, Quaternion.identity);
                }
            }

            //if (triggerRotation > 360)
            //{
            //    cocktailProgress += shakerHeight / 5f;
            //    //Debug.Log("カクテルの完成度" + cocktailProgress);
            //    triggerRotation = 0;
            //}
        }

        transform.position = position;
    }

    void Grab()
    {
        if (isMouse && !isPour)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrounded = false;
                isGrabbed = true;

                //キャッチエフェクト
                Vector2 catchPoint = (Vector2)transform.position + new Vector2(Random.Range(-100f, 100f) / 100f, Random.Range(-100f, 100f) / 100f) * catchOffset;
                Instantiate(catchEffect, catchPoint, Quaternion.identity);
                Instantiate(catchSound);

                //Debug.Log("つかんだよ");

                //投げてキャッチのとき
                cocktailProgress += (triggerRotation / 1000f) * (shakerHeight / 5f);
                isPlusRotate = true;
                if (triggerRotation > 2160)
                {
                    Instantiate(trickCatchSound);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;

            shakerHeight = 0;
            triggerRotation = 0;
            //Debug.Log("離したよ");
        }
    }

    void Pour()
    {
        if (isPour)
        {
            targetPosition = pourCenter + pourOffset;
            position = Vector2.Lerp(position, targetPosition, 1f * Time.deltaTime);
            transform.position = position;

            //回転
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 100f), 1f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void ResetToStart()
    {
        if (isPour)
        {
            pourTime -= Time.deltaTime;
            if (pourTime < 0) {
                isPour = false;
                targetPosition = startPosition;
                pourTime = kPourTime;
                isGrounded = true;

                GlassScript glass = FindAnyObjectByType<GlassScript>();
                if (glass.isGrounded) {
                    glass.isFull = true;
                }
            }
        }

        if (isGrounded)
        {
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);
            transform.position = position;

            //回転
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void Clear()
    {
        if (isClear)
        {
            isClear = false;
            isCompleted = false;

            cocktailAmount = kCocktailAmount;
            cocktailProgress = 0;
        }
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
            triggerRotation = 0;
            isPlusRotate = false;

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
            if (!isGrounded)
            {
                Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
                Instantiate(gotuSound);

                Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
            }

            //量を減らす
            cocktailAmount -= velocity.magnitude * cocktailAmountMinus;
            if (cocktailAmount < 0)
            {
                cocktailAmount = 0;
            }
        }

        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / 5f;

            isCollision = true;

            triggerRotation = 0;

            //ゴツエフェクト
            if (!isGrounded)
            {
                Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
                Instantiate(gotuSound);

                Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
            }

            //量を減らす
            cocktailAmount -= velocity.magnitude * cocktailAmountMinus;
            if (cocktailAmount < 0)
            {
                cocktailAmount = 0;
            }
        }

        if (collision.tag == "PourField")
        {
            isPour = true;
            pourCenter = collision.transform.position;
            Instantiate(pourParticle, new Vector3(transform.position.x, transform.position.y, 10f), pourParticle.transform.rotation);
        }

        if (collision.tag == "Ojama")
        {
            triggerRotation = 0;
            isPlusRotate = false;

            //量を減らす
            cocktailAmount -= 10f;
            if (cocktailAmount < 0)
            {
                cocktailAmount = 0;
            }

            Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / 5f;

            isCollision = true;

            triggerRotation = 0;

            ////ゴツエフェクト
            //if (!isGrounded)
            //{
            //    Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
            //    Instantiate(gotuSound);

            //    Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
            //}

            ////量を減らす
            //cocktailAmount -= velocity.magnitude * cocktailAmountMinus;
            //if (cocktailAmount < 0)
            //{
            //    cocktailAmount = 0;
            //}
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
