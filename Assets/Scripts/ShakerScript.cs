﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerScript : MonoBehaviour
{
    Player player;
    GameManager gameManager;

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
    bool preIsCompleted;

    bool isPlusRotate;    //triggerRotation繧定ｶｳ縺吶°

    //繧ｹ繧ｿ繝ｼ繝医↓謌ｻ縺・
    Vector2 startPosition;

    //蜿榊ｰ・ｿよ焚
    [SerializeField] float reflection;

    //謚輔￡繧句鴨
    [SerializeField] float throwPower;

    //中身
    float shakeSpeed;       //振ったスピード
    float shakeDistance;    //振った距離
    float shakeCount;       //振った回数

    Vector2 preShakePoint;  //前回シェイクした場所
    public float cocktailProgress; //カクテルの完成度   

    public float cocktailProgressMax;
    float shakerHeight;     //繧ｷ繧ｧ繧､繧ｫ繝ｼ縺ｮ鬮倥＆

    //謚輔￡縺ｦ繧九→縺阪↓蜉邂・
    [SerializeField] float rotationSpeed;
    public float triggerRotation;

    //謖ｯ繧九→縺阪・繧・▽
    Vector2 direction;      //騾ｲ繧薙〒縺・ｋ譁ｹ蜷・
    Vector2 previousDirection;
    float directionChangeThreshold = 140f; // 隗貞ｺｦ蟾ｮ縺・5蠎ｦ莉･荳翫〒縲梧･螟峨阪→蛻､譁ｭ

    //豕ｨ縺・
    [SerializeField] Vector2 pourOffset;
    Vector2 pourCenter;
    public bool isPour;

    //Reset逕ｨ
    public float pourTime;
    [SerializeField] float kPourTime;

    //Effect
    [SerializeField] GameObject catchEffect;    //繧ｭ繝｣繝・メ縺励◆譎・
    [SerializeField] float catchOffset;

    [SerializeField] GameObject shakaEffect;    //謖ｯ縺｣縺溘→縺・
    [SerializeField] float shakaOffset;

    [SerializeField] GameObject gotuEffect;     //縺ｶ縺､縺九▲縺溘→縺・
    [SerializeField] float gotuOffset;

    [SerializeField] GameObject spillParticle;  //縺ｶ縺､縺九▲縺溘→縺阪・繝代・繝・ぅ繧ｯ繝ｫ


    [SerializeField] GameObject pourParticle;  //豕ｨ縺舌→縺阪・繝代・繝・ぅ繧ｯ繝ｫ

    [SerializeField] GameObject trickEffect;
    [SerializeField] GameObject trickParticle;
    float trickEffectRotate;

    //Sound
    [SerializeField] GameObject shakaSound;     //謖ｯ縺｣縺溘→縺阪・髻ｳ
    [SerializeField] GameObject catchSound;     //繧ｭ繝｣繝・メ縺励◆縺ｨ縺阪・髻ｳ
    [SerializeField] GameObject trickCatchSound;     //繝医Μ繝・け繧呈ｱｺ繧√※繧ｭ繝｣繝・メ縺励◆縺ｨ縺阪・髻ｳ
    [SerializeField] GameObject gotuSound;      //縺ｶ縺､縺九▲縺溘→縺阪・髻ｳ

    //テキスト
    RectTransform mainCanvas;
    [SerializeField] GameObject Oke;            //グラスを置け

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        gameManager = FindAnyObjectByType<GameManager>();
        position = transform.position;

        //繧ｹ繧ｿ繝ｼ繝医↓謌ｻ縺吶・繧ｸ繧ｷ繝ｧ繝ｳ
        startPosition = transform.position;
        targetPosition = startPosition;

        pourTime = kPourTime;

        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
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
        GlassScript nowGlass = FindAnyObjectByType<GlassScript>();
        if (nowGlass != null && !nowGlass.isFull) { Grab(); }
        Pour();
        ResetToStart();
        Clear();

        if (Input.GetMouseButtonUp(0))
        {
            velocity = preVelocity * throwPower;
        }

        //繧ｹ繧ｿ繝ｼ繝医↓謌ｻ縺・
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            position = transform.position;
            transform.rotation = Quaternion.identity;
            isGrounded = true;
        }

        //螳梧・
        if (cocktailProgress >= 100)
        {
            isCompleted = true;
        }

        if (isCompleted && !preIsCompleted)
        {
            Instantiate(Oke, mainCanvas);
        }
        preIsCompleted = isCompleted;

        //繧ｳ繝槭Φ繝・
#if UNITY_EDITOR
        Commands();
#endif
    }

    void Shake()
    {
        //繧ｷ繧ｧ繧､繧ｯ縺ｮ繧ｹ繝斐・繝・
        shakeSpeed = ((Vector2)transform.position - prePosition).magnitude;

        //繧ｷ繧ｧ繧､繧ｯ縺ｮ繧ｿ繧､繝溘Φ繧ｰ
        direction = ((Vector2)transform.position - prePosition).normalized;
        Vector2 currentDirection = direction.normalized; // 迴ｾ蝨ｨ縺ｮ騾ｲ陦梧婿蜷托ｼ・elocity縺ｪ縺ｩ繧呈Φ螳夲ｼ・

        if (!isCollision)
        {
            if (previousDirection != Vector2.zero)
            {
                float angleDiff = Vector2.Angle(previousDirection, currentDirection);

                if (angleDiff > directionChangeThreshold)
                {
                    shakeCount++;
                    //Debug.Log("shakeCount" + shakeCount);

                    //繧ｷ繝｣繧ｫ繧ｨ繝輔ぉ繧ｯ繝・
                    Vector2 shakaPoint = (Vector2)transform.position + previousDirection * shakaOffset;
                    Instantiate(shakaEffect, shakaPoint, Quaternion.identity);
                    Instantiate(shakaSound);

                    if (shakeCount >= 2)
                    {
                        shakeDistance = Vector2.Distance(preShakePoint, transform.position);

                        //繧ｫ繧ｯ繝・Ν縺ｮ螳梧・蠎ｦ繧貞､画峩
                        cocktailProgress += shakeDistance * shakeSpeed / 10f;
                        //Debug.Log("繧ｫ繧ｯ繝・Ν縺ｮ螳梧・蠎ｦ" + cocktailProgress);
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

            //蝗櫁ｻ｢
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

            //80莉･荳翫〒蜿崎ｻ｢
            if (position.y >= 80)
            {
                velocity.y = 0;
                position.y = 80;
            }

            //蝗櫁ｻ｢縺輔○繧・
            rotation.z += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation);
            rotation = transform.rotation.eulerAngles;

            //float angle = Mathf.Atan2(velocity.normalized.y, velocity.normalized.x) * Mathf.Rad2Deg;
            //rotation = Vector3.Lerp(rotation, new Vector3(0, 0, angle + 90), 5f * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(rotation);
            //rotation = transform.rotation.eulerAngles;

            //鬮倥＆縺ｧ螳梧・蠎ｦ繧貞刈邂・

            if (velocity.y > 0)
            {
                shakerHeight = position.y + 6f;
            }

            if (isPlusRotate)
            {
                triggerRotation += rotationSpeed * Time.deltaTime;

                trickEffectRotate += rotationSpeed * Time.deltaTime;
                //繧ｨ繝輔ぉ繧ｯ繝・
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
            //    //Debug.Log("繧ｫ繧ｯ繝・Ν縺ｮ螳梧・蠎ｦ" + cocktailProgress);
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

                //繧ｭ繝｣繝・メ繧ｨ繝輔ぉ繧ｯ繝・
                Vector2 catchPoint = (Vector2)transform.position + new Vector2(Random.Range(-100f, 100f) / 100f, Random.Range(-100f, 100f) / 100f) * catchOffset;
                Instantiate(catchEffect, catchPoint, Quaternion.identity);
                Instantiate(catchSound);

                //Debug.Log("縺､縺九ｓ縺繧・);

                //謚輔￡縺ｦ繧ｭ繝｣繝・メ縺ｮ縺ｨ縺・
                cocktailProgress += (triggerRotation / 150f);
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
            //Debug.Log("髮｢縺励◆繧・);
        }
    }

    void Pour()
    {
        if (isPour)
        {
            targetPosition = pourCenter + pourOffset;
            position = Vector2.Lerp(position, targetPosition, 1f * Time.deltaTime);
            transform.position = position;

            //蝗櫁ｻ｢
            rotation = Vector3.Lerp(rotation, new Vector3(0, 0, 100f), 1f * Time.deltaTime);
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    void ResetToStart()
    {
        if (isPour)
        {
            pourTime -= Time.deltaTime;
            if (pourTime < 0)
            {
                isPour = false;
                targetPosition = startPosition;
                pourTime = kPourTime;
                isGrounded = true;

                GlassScript glass = FindAnyObjectByType<GlassScript>();
                glass.isFull = true;
            }
        }

        if (gameManager.isRestart)
        {
            isGrounded = true;
            targetPosition = startPosition;
        }

        if (isGrounded)
        {
            position = Vector2.Lerp(position, targetPosition, 30f * Time.deltaTime);
            transform.position = position;

            //蝗櫁ｻ｢
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

            cocktailProgress = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            isMouse = true;
            ///Debug.Log("繝槭え繧ｹ繧ｪ繝ｼ繝舌・縺縺・);
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

            //繧ｴ繝・お繝輔ぉ繧ｯ繝・  
            if (!isGrounded)
            {
                Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
                Instantiate(gotuSound);

                Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
            }
        }

        if (collision.tag == "Counter")
        {
            velocity = Vector2.Reflect(velocity.normalized, Vector2.up) / 5f;

            isCollision = true;

            triggerRotation = 0;

            //繧ｴ繝・お繝輔ぉ繧ｯ繝・
            if (!isGrounded)
            {
                Instantiate(gotuEffect, transform.position + new Vector3(0, gotuOffset, 0), Quaternion.identity);
                Instantiate(gotuSound);

                Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
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

            Instantiate(spillParticle, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 1080f))));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Counter")
        {
            velocity.y = 0.15f;

            isCollision = true;

            triggerRotation = 0;
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
